using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public class flight
    {
        public enum MatchType{Foursome, Match4b, Match2b, Winch, Match4b5, FoursomeWinch5, Foursome3 };
        public MatchType matchType = MatchType.Match4b;
        public readonly List<String> WinchCombin = new List<String> { "01", "02", "12" }; //do not change this, or the saved results (MatchScoresN.json) will be wrong interpreted
        public Dictionary<string, team> teams = new Dictionary<string, team>();
        public Dictionary<string, match> matchs = new Dictionary<string, match>();
        public String name = "";
        public flight(MatchType type, String FlightName)
        {
            name = FlightName;
            matchType = type;
            string teamName = "";
            switch(type)
            {
                case MatchType.Match2b:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    break;
                case MatchType.Match4b:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(2, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(2, 2, teamName));
                    break;
                case MatchType.Foursome:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 2, teamName));
                    break;
                case MatchType.Match4b5:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(2, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(2, 3, teamName));
                    break;
                case MatchType.Winch:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    break;
                case MatchType.FoursomeWinch5:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    break;
                case MatchType.Foursome3:
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 2, teamName));
                    teamName = teams.Count().ToString(); teams.Add(teamName, new team(1, 1, teamName));
                    break;
            }

        }
        flight()
        { }
        public flight quickCloneForDraw()
        {
            flight ret = new flight();
            ret.matchType = matchType;
            ret.name = name;
            foreach (team t in teams.Values)
                ret.teams.Add(t.name, t.quickCloneForDraw());
            return ret;
        }
        public Boolean ContainsPlayer(String name)
        {
            foreach (team ctT in teams.Values)
            {
                if(ctT.players.ContainsKey(name))
                    return true;
            }
            return false;
        }
        public Player GetPlayer(String name)
        {
            foreach (team ctT in teams.Values)
            {
                if (ctT.players.ContainsKey(name))
                    return ctT.players[name];
            }
            return null;
        }
        public team getMyTeam(String name)
        {
            foreach (team t in teams.Values)
            {
                if (t.ContainsPlayer(name))
                    return t;
            }
            return null;
        }
        public playingBall getMyBall(String name)
        {
            return getMyTeam(name).getMyBall(name);
        }

        public int GetTheoreticalNbOfPlayer()
        {
            int ret = 0;
            foreach (team ctT in teams.Values)
            {
                ret += ctT.GetTheoreticalNbOfPlayer();
            }
            return ret;
        }
        public int GetRealNbOfPlayer()
        {
            int ret = 0;
            foreach(team ctT in teams.Values)
            {
                ret += ctT.GetRealNbOfPlayer();
            }
            return ret;
        }
        public List<Player> GetFligtMates(Player Me)
        {
            List<Player> ret = new List<Player>();
            foreach (team ctT in teams.Values)
            {
                foreach(Player ctPlayer in ctT.players.Values)
                {
                    if(ctPlayer.name != Me.name)
                        ret.Add(ctPlayer);
                }
            }
            return ret;
        }
        public int GetHowManyTimeInSameTeam(Player P1, Player P2)
        {
            int ret = 0;
            foreach (team ctT in teams.Values)
                ret += ctT.GetHowManyTimeInSameTeam(P1, P2);
            return ret;
        }
        public int GetHowManyTimeEnemy(Player P1, Player P2)
        {
            int ret = 0;
            if (GetRealNbOfPlayer() > 1 && ContainsPlayer(P1.name) && ContainsPlayer(P2.name) & GetHowManyTimeInSameTeam(P1, P2) == 0 )
                ++ret;
            return ret;
        }
        public int GetHowManyTimeInSameFlight(Player P1, Player P2)
        {
            int ret = 0;
            if (GetRealNbOfPlayer() > 1 && ContainsPlayer(P1.name) && ContainsPlayer(P2.name))
                ++ret;
            return ret;
        }
        public double GetBestHcpInFlight()
        {
            double bestHcp = 150;
            foreach (team ctT in teams.Values)
            {
                foreach(playingBall b in ctT.playingBalls.Values)
                {
                    if (b.GetPlayingHcp() < bestHcp)
                    {
                        bestHcp = b.GetPlayingHcp();
                    }
                }
            }
            return bestHcp;
        }
        public double GetBestAvgHcpTeamInFlight()
        {
            double bestHcp = 150;
            foreach (team ctT in teams.Values)
            { 
                if (ctT.GetAvgPlayingHcp() < bestHcp)
                {
                    bestHcp = ctT.GetAvgPlayingHcp();
                }
            }
            return bestHcp;
        }
        public team GetMyTeam(string me)
        {
            team ret = null;
            foreach (team ctT in teams.Values)
            {
                if(ctT.players.ContainsKey(me))
                {
                    ret = ctT;
                    break;
                }
            }
            return ret;
        }
        public void SetPlayer(dayInputTeamForPlayer diP, Player P)
        {
            if (teams.ContainsKey(diP.TeamName0based))
                teams[diP.TeamName0based].SetPlayer(diP, P);
            else
                throw new Exception(String.Format("Team {0} (zeroBase:{2}) not found in flight {1}", diP.TeamName, name, diP.TeamName0based));
        }
        public void CalcCoupsRecu(Double perc4B = 0.85)
        {
            //ToCheck9
            double BestHcpInFlight = GetBestHcpInFlight();
            double bestTeamHcp = GetBestAvgHcpTeamInFlight();
            foreach (team ctT in teams.Values)
            {
                foreach (playingBall b in ctT.playingBalls.Values)
                {
                    coupsRecu4B.Add(ctT.name + b.name, (b.GetPlayingHcp() - BestHcpInFlight) * perc4B);
                }
                coupsRecuFS.Add(ctT.name, (ctT.GetAvgPlayingHcp() - bestTeamHcp) * perc4B);
            }
        }
        /// <summary>
        /// 4b/2b/Foursome: 
        /// - 1match with both teams (match.name = flight.name)
        /// Winch:
        /// - 3match with combination of the 3 teams (match.names ex: B1B3 where B is the flight.name)
        /// </summary>
        public void InitialiseMatchsWithTeams()
        {
            if(matchType == MatchType.Winch || matchType == MatchType.FoursomeWinch5)
            {
                foreach (string ctc in WinchCombin)
                {
                    string ctt1 = (Convert.ToInt32(ctc.Left(1))).ToString();
                    string ctt2 = (Convert.ToInt32(ctc.Substring(1))).ToString();

                    string matchName = name + teams[ctt1].name1based + name + teams[ctt2].name1based;
                    matchs.Add(matchName, new match { name = matchName, Team1 = teams[ctt1], Team2 = teams[ctt2] });
                }
            }
            else
            {
                matchs.Add(name, new match { name = name, Team1 = teams["0"], Team2 = teams["1"] });
            }
        }
        public Dictionary<String, Double> coupsRecu4B = new Dictionary<String, Double>();
        public Dictionary<String, Double> coupsRecuFS = new Dictionary<String, Double>();
    }
    public class team
    {
        public int nbOfBall = 2;
        public int nbOfMensh = 2;
        public int GetRealNbOfPlayer()
        {
            return players.Count();
        }
        public int GetTheoreticalNbOfPlayer()
        {
            return nbOfMensh;
        }
        public Dictionary<String, Player> players = new Dictionary<string, Player>();
        //public Dictionary<String, Player> players {
        //    get {
        //        Dictionary<string, Player> ret = new Dictionary<string, Player>();
        //        foreach (playingBall b in playingBalls.Values)
        //        {
        //            foreach(string k in b.ballPlayers.Keys)
        //                ret.Add(k, b.ballPlayers[k]);
        //        }
        //        return ret;
        //    }
        //}
        public Dictionary<String, playingBall> playingBalls = new Dictionary<String, playingBall>();
        
        public String name = "";
        public string name1based = "";
        string desc = "";
        public team(int NbOfBall, int NbOfMensch, String TeamName)
        {
            name = TeamName;
            name1based = (Convert.ToInt32(TeamName) + 1).ToString();
            nbOfBall = NbOfBall;
            nbOfMensh = NbOfMensch;

            string ballName = "";
            switch(NbOfBall)
            {
                case 1:
                    ballName = "a"; playingBalls.Add(ballName, new playingBall(NbOfMensch, ballName));
                    break;
                case 2:
                    if (nbOfMensh == 2)
                    {
                        ballName = "a"; playingBalls.Add(ballName, new playingBall(1, ballName));
                        ballName = "b"; playingBalls.Add(ballName, new playingBall(1, ballName));
                    }
                    else if (nbOfMensh == 3)
                    {
                        ballName = "a"; playingBalls.Add(ballName, new playingBall(1, ballName));
                        ballName = "b"; playingBalls.Add(ballName, new playingBall(2, ballName));
                    }
                    break;
            }
        }
        team() {
        }
        public team quickCloneForDraw()
        {
            team ret = new team();
            ret.nbOfBall = nbOfBall;
            ret.nbOfMensh = nbOfMensh;
            ret.name = name;
            ret.name1based = name1based;

            foreach (playingBall b in playingBalls.Values)
                ret.playingBalls.Add(b.name, b.quickCloneForDraw());
            foreach (Player p in players.Values)
                ret.players.Add(p.name, new Player { name = p.name, startNumber = p.startNumber });
            return ret;
        }

        public void SetPlayer(dayInputTeamForPlayer diP, Player P)
        {
            players.Add(P.name, P);
            if (string.IsNullOrEmpty(diP.BallName))
            {
                foreach (playingBall b in playingBalls.Values)
                {
                    if (b.players.Count < b.nbOfPlayerForBall) //add to playingBall (if there is some empty place)
                    {
                        b.SetPlayer(P);
                        break;
                    }
                }
            }
            else //not empty means ballName specified (a or b)
            {
                if (playingBalls.ContainsKey(diP.BallName))
                    playingBalls[diP.BallName].SetPlayer(P);
                else
                    throw new Exception(String.Format("PlayingBall {0} not found in Team {1}", diP.BallName, name));
            }
            desc = GetPlayersString();
        }
        public bool IsFull()
        {
            bool ret = true;
            foreach(playingBall b in playingBalls.Values)
            {
                if (!b.IsFull())
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }
        public Boolean ContainsPlayer(String name)
        {
            if (players.ContainsKey(name))
                return true;
            return false;
        }
        public playingBall getMyBall(String name)
        {
            foreach (playingBall b in playingBalls.Values)
            {
                if (b.ContainsPlayer(name))
                    return b;
            }
            return null;
        }

        public List<Player> GetTeamMates(Player Me)
        {
            List<Player> ret = new List<Player>();
            foreach (Player ctPlayer in players.Values)
            {
                if (ctPlayer.name != Me.name)
                    ret.Add(ctPlayer.CloneJson());
            }
            return ret;
        }

        public Boolean GetTeamMate(Player me, ref Player TeamMate)
        {
            //ToCheck9
            // find somebody that is not me!
            foreach (String ctName in players.Keys)
            {
                if (ctName != me.name)
                {
                    TeamMate = players[ctName].CloneJson();
                    return true;
                }
            }
            return false;
        }
        public string GetTeamMate(string me)
        {
            //ToCheck9
            // find somebody that is not me!
            foreach (string ctName in players.Keys)
            {
                if (ctName.ToLower().Trim() != me.ToLower().Trim())
                {
                    return players[ctName].name;
                }
            }
            return "";
        }
        public string GetPlayer1()
        {
            List<string> keyList = new List<string>(players.Keys);
            return keyList[0];
        }
        public int GetHowManyTimeInSameTeam(Player P1, Player P2)
        {
            int ret = 0;
            if (players.Count > 1 && players.ContainsKey(P1.name) && players.ContainsKey(P2.name))
                ++ret;
            return ret;
        }
        public Double GetAvgPlayingHcp()
        {
            Double ret = 0;
            foreach (playingBall b in playingBalls.Values)
            {
                ret += b.GetPlayingHcp();
            }
            if (playingBalls.Count > 0)
                ret = ret / playingBalls.Count;
            return ret;
        }
        public String GetPlayersString()
        {
            //return string.Join(" & ", playingBalls.Values);
            String ret = "START";
            foreach (playingBall b in playingBalls.Values)
            {
                ret += " & ";
                ret += b.GetPlayersString();
            }
            return ret.Replace("START & ", "");
        }
        public override string ToString()
        {
            return GetPlayersString();
        }
    }
    public class playingBall
    {
        public int nbOfPlayerForBall = 1; //2 for foursome
        public string name = "";
        public string desc = "";
        public Dictionary<String, Player> players = new Dictionary<String, Player>();
        public playingBall(int NbOfPlayerForBall, String BallName)
        {
            nbOfPlayerForBall = NbOfPlayerForBall;
            name = BallName;
        }
        public playingBall quickCloneForDraw()
        {
            playingBall ret = new playingBall(nbOfPlayerForBall, name);
            foreach (Player p in players.Values)
                ret.players.Add(p.name, new Player { name = p.name, startNumber = p.startNumber});
            return ret;
        }
        public void SetPlayer(Player P)
        {
            players.Add(P.name, P);
            desc = GetPlayersString();
        }
        public bool IsFull()
        {
            if (nbOfPlayerForBall > players.Count)
                return false;
            else
                return true;
        }
        public Boolean ContainsPlayer(String name)
        {
            if (players.ContainsKey(name))
                return true;
            return false;
        }
        public Boolean GetBallMate(Player me, ref Player TeamMate)
        {
            // find somebody that is not me!
            foreach (String ctName in players.Keys)
            {
                if (ctName != me.name)
                {
                    TeamMate = players[ctName].CloneJson();
                    return true;
                }
            }
            return false;
        }
        public string GetBallMate(string me)
        {
            // find somebody that is not me!
            foreach (string ctName in players.Keys)
            {
                if (ctName.ToLower().Trim() != me.ToLower().Trim())
                {
                    return players[ctName].name;
                }
            }
            return "";
        }
        public string GetPlayer1()
        {
            List<string> keyList = new List<string>(players.Keys);
            return keyList[0];
        }
        public int GetHowManyTimeInSameBall(Player P1, Player P2)
        {
            int ret = 0;
            if (players.Count > 1 && players.ContainsKey(P1.name) && players.ContainsKey(P2.name))
                ++ret;
            return ret;
        }
        public String GetPlayersString()
        {
            String ret = "START";
            foreach (Player ctP in players.Values)
            {
                ret += "+" + ctP.name;
            }
            return ret.Replace("START+", "");
        }
        public Double GetPlayingHcp()
        {
            Double ret = 0;
            foreach (Player ctP in players.Values)
            {
                ret += ctP.playingHcp;
            }
            if (players.Count > 0)
                ret = ret / players.Count;
            return ret;
        }
    }
    public class match
    {
        public int GetNbOfPlayers()
        {
            return Team1.GetRealNbOfPlayer() + Team2.GetRealNbOfPlayer();
        }
        public string name { get; set; }
        public team Team1 { get; set; }
        public team Team2 { get; set; }
        public List<team> teams // do not change it to a dictionary as it has to stay in this order
        {
            get { return new List<team> { Team1, Team2 }; }
        }
        public team GetMyTeam(string me)
        {
            if (Team1.players.ContainsKey(me))
                return Team1;
            else if (Team2.players.ContainsKey(me))
                return Team2;
            else
                return null;
        }
        public team GetOpponnentTeam(string me)
        {
            if (Team1.players.ContainsKey(me))
                return Team2; //other team (not me!)
            else if (Team2.players.ContainsKey(me))
                return Team1; //other team (not me!)
            else
                return null;
        }
        public team GetWinnerTeam(int WinnerTeamFromMatchScore)
        {
            if (WinnerTeamFromMatchScore == 1)
                return Team1;
            else if (WinnerTeamFromMatchScore == 2)
                return Team2;
            else
                return null;
        }
        public int myMatchResult(string me, int WinnerTeamFromMatchScore)
        {
            if (WinnerTeamFromMatchScore == 0)
                return 0;
            else if (GetWinnerTeam(WinnerTeamFromMatchScore).name == GetMyTeam(me).name)
                return 1;
            else
                return -1;

        }
    }
    public class statsMatch
    {
        List<string> playersPairsKey = new List<string>();
        public Dictionary<string, MatchHistory> histMatchsForPlayer = new Dictionary<string, MatchHistory>();
        public Dictionary<string, MatchHistory> histMatchsAsTeam = new Dictionary<string, MatchHistory>();
        public Dictionary<string, Dictionary<string, MatchHistory>> histMatchsBeteNoire = new Dictionary<string, Dictionary<string, MatchHistory>>();
        public Dictionary<string, wonPlayedStat> statMatchsForPlayer = new Dictionary<string, wonPlayedStat>();
        public Dictionary<string, statMatchsForKey> statMatchsAsTeam = new Dictionary<string, statMatchsForKey>();
        public Dictionary<string, Dictionary<string, wonPlayedStat>> statMatchsBeteNoire = new Dictionary<string, Dictionary<string, wonPlayedStat>>();
        string getPairsKey(string p1, string p2)
        {
            string key = "";
            if (string.Compare(p2, p1) > 0)
                key = p1 + "|" + p2;
            else
                key = p2 + "|" + p1;
            return key;
        }
        string getPairsKey(team Team)
        {
            List<string> keyList = new List<string>(Team.players.Keys);
            return getPairsKey(keyList[0], keyList[1]);
        }
        public void addC(Compet ctC)
        {
            foreach (day ctDay in ctC.days)
            {
                foreach (flight ctFlight in ctDay.flights.Values)
                {
                    foreach (match match in ctFlight.matchs.Values)
                    {
                        if (ctDay.matchScores.matchResults.ContainsKey(match.name))
                        {
                            MatchScore ctMS = ctDay.matchScores.matchResults[match.name];
                            foreach (team ctT in match.teams)
                            {
                                #region store MatchInfo at Player level
                                foreach (Player cttp in ctT.players.Values)
                                {
                                    MatchInfo ctMInfo = new MatchInfo { year = ctC.year, matchScore = ctMS.CloneJson(), flight = ctFlight.CloneJson(), dayNr = ctDay.nr, matchName = match.name };
                                    ctMInfo.myTeam = match.GetMyTeam(cttp.name).CloneJson();
                                    ctMInfo.opTeam = match.GetOpponnentTeam(cttp.name).CloneJson();

                                    if (!histMatchsForPlayer.ContainsKey(cttp.name))
                                        histMatchsForPlayer.Add(cttp.name, new MatchHistory { name = cttp.name });
                                    MatchHistory ctMHist = histMatchsForPlayer[cttp.name];
                                    ctMHist.matchs.Add(ctMInfo);

                                    if (!histMatchsBeteNoire.ContainsKey(cttp.name))
                                        histMatchsBeteNoire.Add(cttp.name, new Dictionary<string,MatchHistory>());
                                    Dictionary<string, MatchHistory>  ctMHistBN = histMatchsBeteNoire[cttp.name];
                                    foreach(string ctOpP in ctMInfo.opTeam.players.Keys)
                                    {
                                        if (!ctMHistBN.ContainsKey(ctOpP))
                                            ctMHistBN.Add(ctOpP, new MatchHistory { name = ctOpP });
                                        MatchHistory ctMHistBNp = ctMHistBN[ctOpP];
                                        ctMHistBNp.matchs.Add(ctMInfo.CloneJson());
                                    }
                                }
                                #endregion
                                #region store MatchInfo at Team level (2 players playing together)
                                if (match.GetNbOfPlayers() == 4)
                                {
                                    string key = getPairsKey(ctT);
                                    if (!histMatchsAsTeam.ContainsKey(key))
                                        histMatchsAsTeam.Add(key, new MatchHistory { name = key });
                                    MatchHistory ctMHist = histMatchsAsTeam[key];
                                    MatchInfo ctMInfo = new MatchInfo { year = ctC.year, matchScore = ctMS.CloneJson(), flight = ctFlight.CloneJson(), dayNr = ctDay.nr, matchName = match.name };
                                    ctMInfo.myTeam = match.GetMyTeam(ctT.GetPlayer1()).CloneJson();
                                    ctMInfo.opTeam = match.GetOpponnentTeam(ctT.GetPlayer1()).CloneJson();

                                    ctMHist.matchs.Add(ctMInfo);
                                }
                                #endregion
                            }
                        }
                        else
                            throw new Exception(String.Format("Could not find matchScore for this match on day {0}: {1}", ctDay.displayName, match.name));
                    }
                }
            }
        }
        public void Calc()
        {
            foreach(string ctPairKey in histMatchsAsTeam.Keys)
            {
                if (!statMatchsAsTeam.ContainsKey(ctPairKey))
                    statMatchsAsTeam.Add(ctPairKey, new statMatchsForKey(ctPairKey));
                statMatchsForKey ctSMs = statMatchsAsTeam[ctPairKey];
                foreach(MatchInfo ctPMInfo in histMatchsAsTeam[ctPairKey].matchs)
                {
                    match ctMatch = ctPMInfo.flight.matchs[ctPMInfo.matchName];
                    if (ctMatch.myMatchResult(ctSMs.playerName1, ctPMInfo.matchScore.WinnerteamName) == 0)
                    {
                        ctSMs.won += 0.5;
                    }
                    else if (ctMatch.myMatchResult(ctSMs.playerName1, ctPMInfo.matchScore.WinnerteamName) == 1)
                    {
                        ctSMs.won += 1;
                    }
                    ctSMs.played += 1;
                }
            }
            foreach(string ctPlayer in histMatchsForPlayer.Keys)
            {
                if (!statMatchsForPlayer.ContainsKey(ctPlayer))
                    statMatchsForPlayer.Add(ctPlayer, new wonPlayedStat());
                wonPlayedStat ctSMs = statMatchsForPlayer[ctPlayer];
                foreach (MatchInfo ctMInfo in histMatchsForPlayer[ctPlayer].matchs)
                {
                    match ctMatch = ctMInfo.flight.matchs[ctMInfo.matchName];
                    if (ctMatch.myMatchResult(ctPlayer, ctMInfo.matchScore.WinnerteamName) == 0)
                    {
                        ctSMs.won += 0.5;
                    }
                    else if (ctMatch.myMatchResult(ctPlayer, ctMInfo.matchScore.WinnerteamName) == 1)
                    {
                        ctSMs.won += 1;
                    }
                    ctSMs.played += 1;
                }
            }
            foreach (string ctPlayer in histMatchsBeteNoire.Keys)
            {
                if (!statMatchsBeteNoire.ContainsKey(ctPlayer))
                    statMatchsBeteNoire.Add(ctPlayer, new Dictionary<string, wonPlayedStat>());
                Dictionary<string, wonPlayedStat> ctDictWP = statMatchsBeteNoire[ctPlayer];
                Dictionary<string, MatchHistory> ctMHistBN = histMatchsBeteNoire[ctPlayer];
                foreach(string ctOpponent in ctMHistBN.Keys)
                {
                    if (!ctDictWP.ContainsKey(ctOpponent))
                        ctDictWP.Add(ctOpponent, new wonPlayedStat());
                    wonPlayedStat ctSMs = ctDictWP[ctOpponent];
                    foreach (MatchInfo ctMInfo in ctMHistBN[ctOpponent].matchs)
                    {
                        match ctMatch = ctMInfo.flight.matchs[ctMInfo.matchName];
                        if (ctMatch.myMatchResult(ctPlayer, ctMInfo.matchScore.WinnerteamName) == 0)
                        {
                            ctSMs.won += 0.5;
                        }
                        else if (ctMatch.myMatchResult(ctPlayer, ctMInfo.matchScore.WinnerteamName) == 1)
                        {
                            ctSMs.won += 1;
                        }
                        ctSMs.played += 1;
                    }
                }
            }
        }
    }
    public class statMatchsForKey
    {
        public statMatchsForKey(string pairKey)
        {
            PairKey = pairKey;
            playerName1 = pairKey.Split('|')[0];
            playerName2 = pairKey.Split('|')[1];
        }
        public string PairKey;
        public string playerName1;
        public string playerName2;
        public double won = 0;
        public double played = 0;
        public double percWon { get { return won / played; } }
    }
    public class wonPlayedStat
    {
        public double won = 0;
        public double played = 0;
        public double percWon { get { return won / played; } }
    }
    public class MatchHistory
    {
        public string name { get; set; }
        public List<MatchInfo> matchs = new List<MatchInfo>();
    }
    public class MatchInfo
    {
        public string year { get; set; }
        public int dayNr { get; set; }
        public string matchName { get; set; } //normally same as flight name but not for Winch
        public team myTeam { get; set; }
        public team opTeam { get; set; }
        public MatchScore matchScore { get; set; }
        public flight flight { get; set; }
    }
}
