using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public class HistoricalHcps
    {
        public List<string> hP = new List<string>();
        Dictionary<string, Compet> hC = new Dictionary<string, Compet>();
        public List<HH_Rnd> hRnds = new List<HH_Rnd>();
        public Dictionary<string, HH_Hcp> Hcps = new Dictionary<string, HH_Hcp>();
        public HistoricalHcps(Dictionary<string, Compet> Compets)
        {
            hC = Compets;
        }
        public void Calc(int nbRndToIgnore = 0)
        {
            getPlayersOutHistory(); //get a List of historical Players 
            getHH();//calc for every compet(year) and extract needed Info for every round
            calcHcp(nbRndToIgnore); //loop in reverse order get the last 20 valid rounds and average the 8 best to get the new hcp
        }
        void getPlayersOutHistory()
        {
            foreach (Compet c in hC.Values)
            {
                //aggregate Players
                foreach (Player ctP in c.Players)
                {
                    switch (ctP.name.ToLower())
                    {
                        case "choff":
                        case "bob":
                        case "tim":
                        case "chams":
                            break;
                        default:
                            if (!hP.Contains(ctP.name))
                                hP.Add(ctP.name);
                            break;
                    }
                }
            }
        }
        void getHH()
        {
            foreach (Compet c in hC.Values)
            {
                c.calculate(c.days.Count);
                foreach (day d in c.days)
                {
                    if (!d.stblPoints.isValidForStblDay())
                        continue;
                    HH_Rnd hhRnd = new HH_Rnd { year = c.year, day = d.nr };
                    hhRnd.par = d.courseDefinition.getSumPar();
                    hhRnd.cr = d.courseDefinition.cr;
                    hhRnd.sr = d.courseDefinition.sr;
                    hhRnd.desc = d.getRndShortDescription();

                    var r = c.getResultsbyDayNr(d.nr);
                    foreach (string ctPName in hP)
                    {
                        HH_Stbl hhStbl = new HH_Stbl { pName = ctPName, year = hhRnd.year, day = hhRnd.day };

                        Player P = c.Players.Find(i => i.name == ctPName);
                        if (r.ContainsKey(ctPName) && P != null && d.stblPoints.isValidForStblDay() && !d.PlayersSurLaTouche.Contains(ctPName))
                        {
                            hhStbl.hcpFix = P.initialHcp;
                            hhStbl.hcpPlay = d.getMyBall(ctPName).GetPlayingHcp();
                            hhStbl.stbl = r[ctPName].StblDay;
                            hhStbl.hcpOldCalcMethod = d.NewHcps.hpcs[ctPName];

                            //(par+playing-(stbl-36)-CR)*113/SR
                            double diff = hhRnd.par; // d.courseDefinition.getSumPar();
                            diff += hhStbl.hcpPlay; // d.getMyBall(ctPName).GetPlayingHcp();
                            diff -= (hhStbl.stbl - 36); //(r[ctPName].StblDay - 36);
                            diff -= hhRnd.cr; // d.courseDefinition.cr;
                            diff *= 113 / hhRnd.sr; // d.courseDefinition.sr;
                            hhStbl.hcpDiff = diff;
                        }
                        else
                        {
                            hhStbl.valid = false;
                        }
                        hhRnd.Stbls.Add(ctPName, hhStbl);
                    }
                    hRnds.Add(hhRnd);
                }
            }
        }
        void calcHcp(int nbRndToIgnore = 0)
        {
            //sort list of rounds (year desc, day desc)
            hRnds = hRnds.OrderByDescending(o => o.orderHelper).ToList<HH_Rnd>();
            int cntRnd = 0;
            #region Remove Rnds if specified
            List<HH_Rnd> hRndsToRemove = new List<HH_Rnd>();
            foreach (HH_Rnd hhRnd in hRnds)
            {
                if (cntRnd < nbRndToIgnore)
                    hRndsToRemove.Add(hhRnd);
                else
                    break;
                cntRnd++;
            }
            foreach (HH_Rnd hhRnd in hRndsToRemove)
            {
                hRnds.Remove(hhRnd);
            }
            #endregion
            foreach (string ctPName in hP)
            {
                List<HH_Stbl> tmpStbls = new List<HH_Stbl>();
                cntRnd = 0;
                #region 1st loop and set "in20range", normally 20, but could be less for new player
                foreach (HH_Rnd hhRnd in hRnds)
                {
                    if(hhRnd.Stbls[ctPName].valid)
                    {
                        if (cntRnd < 20)
                        {
                            hhRnd.Stbls[ctPName].isIn20Range = true;
                            tmpStbls.Add(hhRnd.Stbls[ctPName]);
                            cntRnd++;
                        }
                    }
                }
                #endregion
                HH_Hcp hhHcp = new HH_Hcp { pName = ctPName };
                hhHcp.cntRndIn20Range = cntRnd;
                int bestScoreLimit = 0;
                switch (hhHcp.cntRndIn20Range)
                {
                    case 20:
                        bestScoreLimit = 8;
                        break;
                    case 19:
                        bestScoreLimit = 7;
                        break;
                    case 18: 
                    case 17:
                        bestScoreLimit = 6;
                        break;
                    case 16: 
                    case 15:
                        bestScoreLimit = 5;
                        break;
                    case 14: 
                    case 13: 
                    case 12:
                        bestScoreLimit = 4;
                        break;
                    case 11: 
                    case 10: 
                    case 9:
                        bestScoreLimit = 3;
                        break;
                    case 8: 
                    case 7:
                        bestScoreLimit = 2;
                        break;
                    default:
                        bestScoreLimit = 1;
                        break;
                }

                int cntRndBest = 0;
                foreach (HH_Stbl tmpStbl in tmpStbls.OrderBy(o => o.hcpDiff))
                {
                    if(cntRndBest < bestScoreLimit)
                    {
                        tmpStbl.isBestInRange = true;
                        cntRndBest++;
                    }
                }
                hhHcp.cntBestRnd = cntRndBest;
                #region 2nd loop and set "isBestInRange", normally 8, but could be less for new player, avg for new hcp
                double newHcp = 0;
                foreach (HH_Rnd hhRnd in hRnds)
                {
                    if(tmpStbls.Exists(o => o.orderHelper == hhRnd.orderHelper && o.isBestInRange))
                    {
                        hhRnd.Stbls[ctPName].isBestInRange = true;
                        newHcp += hhRnd.Stbls[ctPName].hcpDiff;
                    }
                }
                newHcp /= hhHcp.cntBestRnd;
                hhHcp.hcp = newHcp;
                Hcps.Add(ctPName, hhHcp);
                #endregion

            }
        }
    }
    public class HH_Rnd
    {
        public string orderHelper
        {
            get { return $"{year}_{day}"; }
        }
        public string year;
        public int day;
        public string desc;
        public int par;
        public double cr;
        public double sr;
        public Dictionary<string, HH_Stbl> Stbls = new Dictionary<string, HH_Stbl>();
    }
    public class HH_Stbl
    {
        public string orderHelper
        {
            get { return $"{year}_{day}"; }
        }
        public string year;
        public int day;
        public string pName;
        public double hcpFix;
        public double hcpPlay;
        public int stbl;
        public double hcpDiff;
        public double hcpOldCalcMethod = -1;
        public bool valid = true;
        public bool isIn20Range = false;
        public bool isBestInRange = false;
    }
    public class HH_Hcp
    {
        public string pName;
        public double hcp;
        public double cntRndIn20Range;
        public double cntBestRnd;
    }
}
