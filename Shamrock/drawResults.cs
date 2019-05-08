using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public class drawResults
    {
        public List<day> days = new List<day>();
        public day getDaybyNr(int nr)
        {
            day ret = days[nr - 1];
            if (ret.nr != nr)
                throw new Exception(String.Format("could not find hole with nr {0}, holes probably wrong sorted", nr));
            return ret;
        }
        public drawResults quickCloneForDraw()
        {
            drawResults ret = new drawResults();
            foreach (day d in days)
                ret.days.Add(d.quickCloneForDraw());
            return ret;
        }
        
        public Dictionary<int, int> statXTFlight;
        public Dictionary<int, int> statXTEnemy;
        public Dictionary<int, int> statXTIn2B;
        public Dictionary<int, int> statXTInWinch;
        public Dictionary<int, int> statXTIn4B;
        public void calculateDrawStatXT(List<Player> lPlayers2)
        {
            Dictionary<int, int> xTFlight = new Dictionary<int, int>();
            Dictionary<int, int> xTEnemy = new Dictionary<int, int>();
            Dictionary<int, int> xTIn2B = new Dictionary<int, int>();
            Dictionary<int, int> xTinWinch = new Dictionary<int, int>();
            Dictionary<int, int> xTIn4B = new Dictionary<int, int>();

            for (int iP1 = 0; iP1 < lPlayers2.Count; iP1++)
            {
                int ctHTin2B = GetHowManyTimeIn2B(lPlayers2[iP1]);
                if (xTIn2B.ContainsKey(ctHTin2B))
                    ++xTIn2B[ctHTin2B];
                else
                    xTIn2B.Add(ctHTin2B, 1);

                int ctHTinWinch = GetHowManyTimeInWinch(lPlayers2[iP1]);
                if (xTinWinch.ContainsKey(ctHTinWinch))
                    ++xTinWinch[ctHTinWinch];
                else
                    xTinWinch.Add(ctHTinWinch, 1);

                int ctHTIn4B = GetHowManyTimeIn4B(lPlayers2[iP1]);
                if (xTIn4B.ContainsKey(ctHTIn4B))
                    ++xTIn4B[ctHTIn4B];
                else
                    xTIn4B.Add(ctHTIn4B, 1);
                
                for (int iP2 = 0; iP2 < lPlayers2.Count; iP2++)
                {
                    if (iP1 > iP2)
                    {
                        int ctHTFlight = GetHowManyTimeInSameFlight(lPlayers2[iP1], lPlayers2[iP2]);
                        if (xTFlight.ContainsKey(ctHTFlight))
                            ++xTFlight[ctHTFlight];
                        else
                            xTFlight.Add(ctHTFlight, 1);

                        int ctHTEnemy = GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                        if (xTEnemy.ContainsKey(ctHTEnemy))
                            ++xTEnemy[ctHTEnemy];
                        else
                            xTEnemy.Add(ctHTEnemy, 1);
                    }
                }
            }
            statXTFlight = new Dictionary<int, int>();
            statXTEnemy = new Dictionary<int, int>();
            statXTIn2B = new Dictionary<int, int>();
            statXTInWinch = new Dictionary<int, int>();
            statXTIn4B = new Dictionary<int, int>();
            for (int i = 0; i < 10; i++) //make sure it is sorted and from 0 to 9
            {
                if (xTFlight.ContainsKey(i))
                    statXTFlight.Add(i, xTFlight[i]);
                else
                    statXTFlight.Add(i, 0);

                if (xTEnemy.ContainsKey(i))
                    statXTEnemy.Add(i, xTEnemy[i]);
                else
                    statXTEnemy.Add(i, 0);

                if (xTIn2B.ContainsKey(i))
                    statXTIn2B.Add(i, xTIn2B[i]);
                else
                    statXTIn2B.Add(i, 0);
                
                if (xTinWinch.ContainsKey(i))
                    statXTInWinch.Add(i, xTinWinch[i]);
                else
                    statXTInWinch.Add(i, 0);

                if (xTIn4B.ContainsKey(i))
                    statXTIn4B.Add(i, xTIn4B[i]);
                else
                    statXTIn4B.Add(i, 0);
            }
        }
        public day newDay(day.PlayMode mode)
        {
            day newDay = new day(mode, "");
            days.Add(newDay);
            days.Last().nr = days.Count;
            return days.Last();
        }
        public int GetHowManyTimeInSameTeam(Player P1, Player P2)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeInSameTeam(P1, P2);
            return ret;
        }
        public int GetHowManyTimeInSameFlight(Player P1, Player P2)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeInSameFlight(P1, P2);
            return ret;
        }
        public int GetHowManyTimeEnemy(Player P1, Player P2)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeEnemy(P1, P2);
            return ret;
        }
        public int GetHowManyTimeIn2B(Player P)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeIn2B(P);
            return ret;

        }
        public int GetHowManyTimeInWinch(Player P)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeInWinch(P);
            return ret;

        }
        public int GetHowManyTimeIn4B(Player P)
        {
            int ret = 0;
            foreach (day ctDay in days)
                ret += ctDay.GetHowManyTimeIn4B(P);
            return ret;

        }

    }

}
