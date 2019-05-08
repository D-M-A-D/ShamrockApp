using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Shamrock
{
    public class Compet
    {
        public string year;
        public int ctDayOfCompetition = 0;
        public int nbOfPlayer = 0;
        public List<Player> Players = new List<Player>();
        public List<day> days = new List<day>();
        public Compet(string yearOrFolder)
        {
            year = yearOrFolder;
        }
        public void initialiseflightsForDossard(List<Player> Players, int dayNr)
        {
            List<Player> tmpPlayers = Players.CloneJson();
            tmpPlayers.Sort((x, y) => x.startNumber.CompareTo(y.startNumber));
            int[] dayOrder = new int[]{};
            switch(dayNr)
            {
                case 1: dayOrder = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; break;
                case 2: dayOrder = new int[] { 9, 2, 5, 3, 7, 10, 4, 8, 6, 1 }; break;
                case 3: dayOrder = new int[] { 10, 2, 8, 1, 9, 3, 7, 6, 5, 4 }; break;
                case 4: dayOrder = new int[] { 9, 1, 5, 8, 3, 6, 4, 10, 7, 2 }; break;
                case 5: dayOrder = new int[] { 9, 7, 1, 4, 2, 5, 10, 6, 3, 8 }; break;
            }
            course ctCourse = getDaybyNr(dayNr).courseDefinition;

            int cnt = -1;
            foreach (flight ctFlight in getDaybyNr(dayNr).flights.Values)
            {
                foreach (team ctTeam in ctFlight.teams.Values)
                {
                    for (int i = 0; i < ctTeam.nbOfBall; i++)
                    {
                        ++cnt;
                        Player candidate = tmpPlayers[dayOrder[cnt]-1];
                        candidate.playingHcp = ctCourse.getPlayingHcp(candidate.initialHcp);
                        ctTeam.SetPlayer(new dayInputTeamForPlayer { PlayerName = candidate.name }, candidate); //not sure it works
                    }
                }
                ctFlight.CalcCoupsRecu();
            }
        }
        public void initialiseflightsFromInputTeam(int dayNr, List<TeamInput> InputTeams)
        {
            #region loop through inputTeams, extract info for day
            List<dayInputTeamForPlayer> diPs = new List<dayInputTeamForPlayer>();
            foreach (TeamInput ti in InputTeams)
            {
                String ctInput = "";
                switch (dayNr)
                {
                    case 1: ctInput = ti.R1; break;
                    case 2: ctInput = ti.R2; break;
                    case 3: ctInput = ti.R3; break;
                    case 4: ctInput = ti.R4; break;
                    case 5: ctInput = ti.R5; break;
                }
                if (!String.IsNullOrWhiteSpace(ctInput) && ctInput != "0" && ctInput.Length >= 2) //0=Abandon
                {
                    ctInput = ctInput.Trim();
                    diPs.Add(new dayInputTeamForPlayer
                    {
                        PlayerName = ti.Player,
                        FlightName = ctInput.Substring(0, 1),
                        TeamName = ctInput.Substring(1, 1),
                        BallName = ctInput.Remove(0, 2)
                    });
                }
            }
            #endregion

            day ctDay = getDaybyNr(dayNr);
            course ctCourse = ctDay.courseDefinition;
            foreach (dayInputTeamForPlayer diP in diPs)
            {
                Player P = Players.Find(x => x.name == diP.PlayerName).CloneJson();
                P.playingHcp = ctCourse.getPlayingHcp(P.initialHcp);
                ctDay.SetPlayer(diP, P);
            }
            ctDay.CalcCoupsRecu();
            ctDay.InitialiseMatchsWithTeams();
        }

        public day newDay(day.PlayMode mode)
        {
            day newDay = new day(mode, year);
            newDay.nr = days.Count + 1;

            days.Add(newDay);
            return days.Last();
        }
        public day getDaybyNr(int nr)
        {
            day ret = days[nr - 1];
            if (ret.nr != nr)
                throw new Exception(String.Format("could not find hole with nr {0}, holes probably wrong sorted", nr));
            return ret;
        }
        #region results
        public void calculate(int toDayNr)
        {
            #region 1st Loop with calc daily stbl
            ctDayOfCompetition = toDayNr;
            Hcps prHcps = null;
            for (int i = 1; i <= toDayNr; ++i)
            {
                day ctDay = getDaybyNr(i);
                ctDay.calculateStbl(); //calculate stbl for the day and every Players
                ctDay.calculateStblNewHcps(prHcps);
                prHcps = ctDay.NewHcps;
                Dictionary<string, PlayerResult> ctDailyResults = new Dictionary<string, PlayerResult>();
                List<String> sortedList = ctDay.stblPoints.getSortedPlayerList();
                foreach (Player P in Players)
                {
                    PlayerResult ctPR = new PlayerResult();
                    ctPR.PlayerName = P.name;
                    ctPR.StblDay = ctDay.stblPoints.getStblPointsForLastHoles(P.name);
                    ctPR.shMatch = ctDay.getShPointsForMatch(P.name, configForYear);
                    ctPR.shStblDay = ctDay.getShPointsForStblDaily(P.name, configForYear);
                    int Position = sortedList.FindIndex(x => x == P.name);
                    ctPR.posStblDay = Position + 1;
                    ctDailyResults.Add(ctPR.PlayerName, ctPR);
                }
                results.Add(ctDailyResults); // stores daily scores for the day
            }
            #endregion
            #region 2nd Loop Sum up and sort for weekly stable
            for (int i = 1; i <= toDayNr; ++i)
            {
                Dictionary<String, Double> sortableScores = new Dictionary<string, Double>();
                foreach (Player P in Players)
                {
                    Double factor = 1;
                    Double sortableScore = (100 - (double)getResultsbyDayNr(1)[P.name].posStblDay) / 1000; //initialise with position from the 1st stbl
                    int sumStbl = 0;
                    Double sumEff = 0;
                    for (int j = 1; j <= i; ++j) //loop from begin to ctToDayNr
                    {
                        int ctStbl = getResultsbyDayNr(j)[P.name].StblDay; // get the daily stbl
                        sumStbl += ctStbl; // sum up
                        sortableScore += factor * (double)ctStbl; //store for sort
                        factor *= 1000;
                        sumEff += getResultsbyDayNr(j)[P.name].shMatch + getResultsbyDayNr(j)[P.name].shStblDay;
                    }
                    sortableScore += factor * (double)sumStbl; //store for sort
                    getResultsbyDayNr(i)[P.name].shEffective = sumEff; //sum of matchs + daily sh
                    getResultsbyDayNr(i)[P.name].StblWeek = sumStbl; //save sum for week
                    sortableScores.Add(P.name, sortableScore); //add sortableScore to dictionary
                }
                List<String> sortedList = sortableScores.OrderByDescending(kp => kp.Value).Select(kp => kp.Key).ToList();
                sortableScores.Clear();
                foreach (Player P in Players) //loop again to get Position and sh
                {
                    int Position = sortedList.FindIndex(x => x == P.name);
                    if (!getDaybyNr(i).stblPoints.isValidForStblDay())
                        Position = 8; // make sure 
                    getResultsbyDayNr(i)[P.name].posStblWeek = Position + 1;
                    String[] splits = configForYear.stlWeek.Replace(" ", "").Split(',');
                    double ctSh = 0;
                    if (splits.Length > Position)
                        Double.TryParse(splits[Position], out ctSh);
                    getResultsbyDayNr(i)[P.name].shStblWeek = ctSh;
                    getResultsbyDayNr(i)[P.name].shVirtual = getResultsbyDayNr(i)[P.name].shEffective + ctSh;

                    #region sortable score for overall Position
                    Double sortableScore = 0;
                    Double factor = 1;
                    sortableScore += 20 - getResultsbyDayNr(i)[P.name].posStblWeek * factor;
                    factor *= 1000;
                    sortableScore += getResultsbyDayNr(i)[P.name].shVirtual * factor;
                    sortableScores.Add(P.name, sortableScore);
                    #endregion
                }
                sortedList.Clear();
                sortedList = sortableScores.OrderByDescending(kp => kp.Value).Select(kp => kp.Key).ToList();
                foreach (Player P in Players) //loop again to get Position and sh
                {
                    int Position = sortedList.FindIndex(x => x == P.name);
                    getResultsbyDayNr(i)[P.name].posVirtual = Position + 1;
                }
            }
            #endregion
        }
        public List<Dictionary<String, PlayerResult>> results = new List<Dictionary<string, PlayerResult>>();
        public Dictionary<String, PlayerResult> getResultsbyDayNr(int dayNr)
        {
            return results[dayNr - 1];
        }
        public List<string> getSortedPlayer(int dayNr)
        {
            Dictionary<string, double> temp = new Dictionary<string, double>();
            foreach(PlayerResult pr in getResultsbyDayNr(dayNr).Values)
            {
                temp.Add(pr.PlayerName, pr.posVirtual);
            }
            List<String> ret = temp.OrderBy(kp => kp.Value)
                                      .Select(kp => kp.Key)
                                      .ToList();
            return ret;
        }
        public ConfigsForYear configForYear = new ConfigsForYear();
        public List<HoleForStat> GetHoleStats (int toDayNr)
        {
            List<HoleForStat> ret = new List<HoleForStat>();
            for (int i = 1; i <= toDayNr; ++i)
            {
                day ctDay = getDaybyNr(i);
                ret.AddRange(ctDay.statHoles);
            }
            return ret;
        }
        #endregion
        #region Stats Team
        public Dictionary<int, int> statXTFlight;
        public Dictionary<int, int> statXTTeam;
        public Dictionary<int, int> statXTEnemy;
        public Dictionary<int, int> statXTIn2B;
        public Dictionary<int, int> statXTInWinch;
        public Dictionary<int, int> statXTIn4B;
        public void calculateDrawStatXT(List<Player> lPlayers2)
        {
            Dictionary<int, int> xTFlight = new Dictionary<int, int>();
            Dictionary<int, int> xTTeam = new Dictionary<int, int>();
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

                        int ctHTTeam = GetHowManyTimeInSameTeam(lPlayers2[iP1], lPlayers2[iP2]);
                        if (xTTeam.ContainsKey(ctHTTeam))
                            ++xTTeam[ctHTTeam];
                        else
                            xTTeam.Add(ctHTTeam, 1);

                        int ctHTEnemy = GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                        if (xTEnemy.ContainsKey(ctHTEnemy))
                            ++xTEnemy[ctHTEnemy];
                        else
                            xTEnemy.Add(ctHTEnemy, 1);
                    }
                }
            }
            statXTFlight = new Dictionary<int, int>();
            statXTTeam = new Dictionary<int, int>();
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

                if (xTTeam.ContainsKey(i))
                    statXTTeam.Add(i, xTTeam[i]);
                else
                    statXTTeam.Add(i, 0);

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
        #endregion

    }
    public class ConfigsForYear
    {
        public string ptsMatch { get; set; }
        public string stlDay { get; set; }
        public string stlWeek { get; set; }
        public int PlayModeR1 { get; set; }
        public int PlayModeR2 { get; set; }
        public int PlayModeR3 { get; set; }
        public int PlayModeR4 { get; set; }
        public int PlayModeR5 { get; set; }
        public day.PlayMode getPlayModeForRound(int Rnr)
        {
            day.PlayMode ret = day.PlayMode.P8_2x4b;
            int playModeNr = 0;
            switch(Rnr)
            {
                case 1:
                    playModeNr = PlayModeR1;
                    break;
                case 2:
                    playModeNr = PlayModeR2;
                    break;
                case 3:
                    playModeNr = PlayModeR3;
                    break;
                case 4:
                    playModeNr = PlayModeR4;
                    break;
                case 5:
                    playModeNr = PlayModeR5;
                    break;
            }
            switch (playModeNr)
            {
                case 71: ret = day.PlayMode.P7_4b_W; break;
                case 72: ret = day.PlayMode.P7_Fs_Fs3; break;
                case 81: ret = day.PlayMode.P8_2x4b; break;
                case 82: ret = day.PlayMode.P8_Fs; break;
                case 91: ret = day.PlayMode.P9_4b_4b5fs; break;
                case 92: ret = day.PlayMode.P9_3xW; break;
                case 93: ret = day.PlayMode.P9_Fs_Fs5W; break;
                case 94: ret = day.PlayMode.P9_432; break;
                case 101: ret = day.PlayMode.P10_2x4b_2b; break;
                case 102: ret = day.PlayMode.P10_2xFs_2b; break;
                case 103: ret = day.PlayMode.P10_2xFs5W; break;
                default: ret = day.PlayMode.NoGame; break;
            }
            return ret;
        }
    }
    public class PlayerResult
    {
        public string PlayerName;
        public double shMatch;
        public int StblDay;
        public int posStblDay;
        public double shStblDay;
        public int StblWeek;
        public int posStblWeek;
        public double shStblWeek;
        public double shEffective;
        public double shVirtual;
        public int posVirtual;
    }
    public class dayInputTeamForPlayer
    {
        public string PlayerName = "";
        public string FlightName = "";
        public string TeamName = "";
        public string BallName = "";
        public string TeamName0based
        {
            get
            {
                int i = 0;
                if (int.TryParse(TeamName, out i))
                {
                    return (i - 1).ToString();
                }
                else
                    throw new Exception(string.Format("Invalid TeamName {0} in inputTeams", TeamName));
            }
        }
    }
}
