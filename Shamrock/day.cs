using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public class day
    {
        public enum PlayMode { P7_4b_W, P7_Fs_Fs3, P8_2x4b, P8_Fs, P8_2x4bbb, P10_2x4b_2b, P10_2xFs_2b, P10_2xFs5W, P9_4b_4b5fs, P9_3xW, P9_Fs_Fs5W, P9_432, P9_3xH, NoGame };
        public int nr = 0;
        public Boolean isFoursomeOrBestBall = false;
        public PlayMode playMode = PlayMode.P8_2x4b;
        public bool is9Holes = false;
        public string playModeDisplay = "";
        public int nbOfPlayerForDay = 0;
        public string year = "";
        public string displayName
        {
            get
            {
                return string.Format("{2}_R{0}_{1}",nr.ToString(),playMode.ToString(),year);
            }
        }
        public List<Player> PlayersLeftToDraw = new List<Player>();
        public List<Player> PlayersForTheDay = new List<Player>();
        public List<string> PlayersSurLaTouche = new List<string>();
        public day(PlayMode mode, string Year)
        {
            year = Year;
            playMode = mode;
            string MatchName = "";
            switch (mode)
            {
                case PlayMode.P7_4b_W:
                    nbOfPlayerForDay = 7;
                    playModeDisplay = "4b + Winch";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
                    break;
                case PlayMode.P7_Fs_Fs3:
                    nbOfPlayerForDay = 7;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "Foursome";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome3, MatchName));
                    break;
                case PlayMode.P8_2x4b:
                    nbOfPlayerForDay = 8;
                    playModeDisplay = "4Balles";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    break;
                case PlayMode.P8_Fs:
                    nbOfPlayerForDay = 8;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "Foursome";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    break;
                case PlayMode.P8_2x4bbb:
                    nbOfPlayerForDay = 8;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "4b best only";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4bbb, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4bbb, MatchName));
                    break;
                case PlayMode.P9_3xW:
                    nbOfPlayerForDay = 9;
                    playModeDisplay = "3xWinch";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
                    MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
                    break;
                case PlayMode.P9_4b_4b5fs:
                    nbOfPlayerForDay = 9;
                    playModeDisplay = "4Balles";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b5, MatchName));
                    break;
                case PlayMode.P9_Fs_Fs5W:
                    nbOfPlayerForDay = 9;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "Foursome";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.FoursomeWinch5, MatchName));
                    break;
                case PlayMode.P9_432:
                    nbOfPlayerForDay = 9;
                    playModeDisplay = "432";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
                    MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Match2b, MatchName));
                    break;
                case PlayMode.P9_3xH:
                    nbOfPlayerForDay = 9;
                    playModeDisplay = "3xHunters";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Hunters, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Hunters, MatchName));
                    MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Hunters, MatchName));
                    break;
                case PlayMode.P10_2x4b_2b:
                    nbOfPlayerForDay = 10;
                    playModeDisplay = "4Balles";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
                    MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Match2b, MatchName));
                    break;
                case PlayMode.P10_2xFs5W:
                    nbOfPlayerForDay = 10;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "Foursome";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.FoursomeWinch5, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.FoursomeWinch5, MatchName));
                    break;
                case PlayMode.P10_2xFs_2b:
                    nbOfPlayerForDay = 10;
                    isFoursomeOrBestBall = true;
                    playModeDisplay = "Foursome";
                    MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
                    MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Match2b, MatchName));
                    break;
            }
        }
        day() { }
        public day quickCloneForDraw()
        {
            day ret = new day();
            ret.nr = nr;
            ret.playMode = playMode;
            ret.nbOfPlayerForDay = nbOfPlayerForDay;
            ret.isFoursomeOrBestBall = isFoursomeOrBestBall;
            ret.PlayersForTheDay = PlayersForTheDay.ConvertAll<Player>(x => new Player {name = x.name, startNumber = x.startNumber });
            ret.PlayersLeftToDraw = PlayersLeftToDraw.ConvertAll<Player>(x => new Player { name = x.name, startNumber = x.startNumber });
            foreach (flight f in flights.Values)
                ret.flights.Add(f.name, f.quickCloneForDraw());
            return ret;
        }
        //public void initialiseEmptyFlightsAccordingToNbOfPlayer(int NbOfPlayer, PlayMode playMode)
        //{
        //    string MatchName = "";
        //    switch (NbOfPlayer)
        //    {
        //        case 11:
        //            if (isFoursome)
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Foursome3, MatchName));
        //            }
        //            else
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
        //            }
        //            break;
        //        case 10:
        //            if (isFoursome)
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Match2b, MatchName));
        //            }
        //            else
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Match2b, MatchName));
        //            }
        //            break;
        //        case 9:
        //            if(isFoursome)
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.FoursomeWinch5, MatchName));
        //            }
        //            else if(is9_3Winch)
        //            {
        //                MatchName = "A";  flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
        //                MatchName = "C"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
        //            }
        //            else
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b5, MatchName));
        //            }
        //            break;
        //        case 8:
        //            if (isFoursome)
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //            }
        //            else
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //            }
        //            break;
        //        case 7:
        //            if (isFoursome)
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Foursome, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Foursome3, MatchName));
        //            }
        //            else
        //            {
        //                MatchName = "A"; flights.Add(MatchName, new flight(flight.MatchType.Match4b, MatchName));
        //                MatchName = "B"; flights.Add(MatchName, new flight(flight.MatchType.Winch, MatchName));
        //            }
        //            break;
        //        default:
        //            throw new Exception("no flight configuration for NbOfPlayer " + NbOfPlayer);
        //    }
        //}

        public course courseDefinition;
        public dailyScores scores = new dailyScores();
        public dailyExtras extras = new dailyExtras();
        public MatchScores matchScores = new MatchScores();
        public dailySblPoints stblPoints = new dailySblPoints();
        public dailySblPoints stblPointsForNewHcp = new dailySblPoints();
        public List<HoleForStat> statHoles = new List<HoleForStat>();
        public Hcps NewHcps = new Hcps();
        public Dictionary<String, flight> flights = new Dictionary<string, flight>();
        public Boolean ContainsPlayer(String name)
        {
            foreach (flight ctT in flights.Values)
            {
                if (ctT.ContainsPlayer(name))
                    return true;
            }
            return false;
        }
        public Player GetPlayer(String name)
        {
            foreach (flight ctT in flights.Values)
            {
                if (ctT.ContainsPlayer(name))
                    return ctT.GetPlayer(name);
            }
            return null;
        }
        public flight getMyFlight(String name)
        {
            foreach (flight f in flights.Values)
            {
                if (f.ContainsPlayer(name))
                    return f;
            }
            return null;
        }
        public playingBall getMyBall(String name)
        {
            return getMyFlight(name).getMyTeam(name).getMyBall(name);
        }
        public team getMyTeam(String name)
        {
            return getMyFlight(name).getMyTeam(name);
        }
        public string getRndDescription()
        {
            return $"Rnd {nr} ({playModeDisplay}): {courseDefinition.name} ({courseDefinition.TeeColor} par:{courseDefinition.getSumPar()} cr:{courseDefinition.cr} sr:{courseDefinition.sr} Dist: {courseDefinition.Yards})";
        }
        public string getRndShortDescription()
        {
            return $"({playModeDisplay}) {courseDefinition.name}";
        }

        public int CountPlayer()
        {
            int ret = 0;
            foreach (flight ctT in flights.Values)
            {
                ret += ctT.GetRealNbOfPlayer();
            }
            return ret;
        }
        public int GetHowManyTimeInSameTeam(Player P1, Player P2)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
                ret += ctFlight.GetHowManyTimeInSameTeam(P1, P2);
            return ret;
        }
        public int GetHowManyTimeInSameFlight(Player P1, Player P2)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
                ret += ctFlight.GetHowManyTimeInSameFlight(P1, P2);
            return ret;
        }
        public int GetHowManyTimeEnemy(Player P1, Player P2)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
                ret += ctFlight.GetHowManyTimeEnemy(P1, P2);
            return ret;
        }
        public int GetHowManyTimeIn2B(Player P)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
            {
                if (ctFlight.matchType == flight.MatchType.Match2b && ctFlight.ContainsPlayer(P.name))
                    ++ret;
            }
            return ret;

        }
        public int GetHowManyTimeInWinch(Player P)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
            {
                if ((ctFlight.matchType == flight.MatchType.Winch || ctFlight.matchType == flight.MatchType.Hunters) && ctFlight.ContainsPlayer(P.name))
                    ++ret;
            }
            return ret;

        }
        public int GetHowManyTimeIn4B(Player P)
        {
            int ret = 0;
            foreach (flight ctFlight in flights.Values)
            {
                if ((ctFlight.matchType == flight.MatchType.Match4b ||
                    ctFlight.matchType == flight.MatchType.Match4bbb ||
                    ctFlight.matchType == flight.MatchType.Foursome ||
                    ctFlight.matchType == flight.MatchType.Foursome3 ||
                    ctFlight.matchType == flight.MatchType.FoursomeWinch5 ||
                    ctFlight.matchType == flight.MatchType.Match4b5) && ctFlight.ContainsPlayer(P.name))
                    ++ret;
            }
            return ret;

        }
        public void SetPlayer(dayInputTeamForPlayer diP, Player P)
        {
            if (flights.ContainsKey(diP.FlightName))
            {
                flights[diP.FlightName].SetPlayer(diP, P);
            }
            else
                throw new Exception(String.Format("Flight {0} not found in day {1}", diP.FlightName, nr));
        }
        public void CalcCoupsRecu()
        {
            foreach (flight f in flights.Values)
                f.CalcCoupsRecu();
        }
        public void InitialiseMatchsWithTeams()
        {
            foreach (flight f in flights.Values)
                f.InitialiseMatchsWithTeams();
        }
        public void calculateStblNewHcps(Hcps oldHcps)
        {
            //ToCheck9
            stblPointsForNewHcp.points.Clear();
            foreach (flight ctF in flights.Values)
            {
                foreach (team ctT in ctF.teams.Values)
                {
                    foreach (playingBall b in ctT.playingBalls.Values)
                    {
                        string playerString = b.GetPlayersString();
                        foreach (Player ctP in b.players.Values)
                        {
                            Double oldHcp = ctP.initialHcp;
                            if (oldHcps != null && oldHcps.hpcs.ContainsKey(ctP.name))
                                oldHcp = oldHcps.hpcs[ctP.name];
                            int ctPlayingHcp = (int)Math.Round(courseDefinition.getPlayingHcp(oldHcp), 0);
                            stblPointsForNewHcp.points.Add(ctP.name, new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                            if (scores.holeResults.ContainsKey(playerString))
                            {
                                for (int holeNr = 1; holeNr <= 18; holeNr++)
                                {
                                    hole ctHoleDefinition = courseDefinition.getHolebyNr(holeNr);
                                    int Stbl = calcStblPointforHole(scores.getScore(playerString, holeNr), ctHoleDefinition.par, ctHoleDefinition.hcp, ctPlayingHcp);
                                    if(b.nbOfPlayerForBall == 1)
                                        stblPointsForNewHcp.points[ctP.name][holeNr - 1] = Stbl;
                                }
                                double nhcp = oldHcp;
                                if (stblPointsForNewHcp.isValidForStblDay() && b.nbOfPlayerForBall == 1)
                                    nhcp = NewHcps.calcNewHcpASG(oldHcp, stblPointsForNewHcp.getStblPointsForLastHoles(ctP.name));

                                if (NewHcps.hpcs.ContainsKey(ctP.name))
                                    NewHcps.hpcs[ctP.name] = nhcp;
                                else
                                    NewHcps.hpcs.Add(ctP.name, nhcp);
                            }
                            else
                                throw new Exception(String.Format("Could not find holeResults for Player {0}", playerString));
                        }
                    }
                }
            }
        }
        public void calculateStbl()
        {
            statHoles.Clear();
            stblPoints.points.Clear();
            foreach (flight ctF in flights.Values)
            {
                foreach(team ctT in ctF.teams.Values)
                {
                    foreach (playingBall b in ctT.playingBalls.Values)
                    {
                        string playerString = b.GetPlayersString();
                        int ctPlayingHcp = (int)Math.Round(b.GetPlayingHcp(), 0);
                        //stblPoints.points.Add(playerString, new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                        foreach (Player pbp in b.players.Values) //duplicate for the players of a foursome
                        {
                            if (!stblPoints.points.ContainsKey(pbp.name))
                                stblPoints.points.Add(pbp.name, new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                            if(pbp.name != playerString) //duplicate score for player
                            {
                                if (scores.holeResults.ContainsKey(playerString) && !scores.holeResults.ContainsKey(pbp.name))
                                    scores.holeResults.Add(pbp.name, scores.holeResults[playerString].CloneJson());
                            }
                        }
                        if (scores.holeResults.ContainsKey(playerString))
                        {
                            for (int holeNr = 1; holeNr <= 18; holeNr++)
                            {
                                hole ctHoleDefinition = courseDefinition.getHolebyNr(holeNr);
                                int Stbl = calcStblPointforHole(scores.getScore(playerString, holeNr), ctHoleDefinition.par, ctHoleDefinition.hcp, ctPlayingHcp);
                                int StblBrut = calcStblPointforHole(scores.getScore(playerString, holeNr), ctHoleDefinition.par, 1, 0);
                                int StblBrut36 = calcStblPointforHole(scores.getScore(playerString, holeNr), ctHoleDefinition.par, 1, 36);
                                //stblPoints.points[playerString][holeNr - 1] = Stbl;
                                foreach (Player pbp in b.players.Values) //duplicate for the players of a foursome
                                {
                                    stblPoints.points[pbp.name][holeNr - 1] = Stbl;

                                    HoleForStat statHole = new HoleForStat
                                    {
                                        playerName = pbp.name,
                                        cpsRecu = GetAdjustement(ctHoleDefinition.hcp, ctPlayingHcp),
                                        dayNr = nr,
                                        defHole = ctHoleDefinition,
                                        Parcours = courseDefinition.name,
                                        ptsNet = Stbl,
                                        ptsBrut = StblBrut,
                                        ptsBrut36 = StblBrut36
                                    };
                                    statHoles.Add(statHole);
                                }


                            }
                        }
                        else
                            throw new Exception(String.Format("Could not find holeResults for Player {0}", playerString));
                    }
                }
            }            
        }
        public int calcStblPointforHole(int NbCoup, int Par, int hcphole, int hcpPlayer, int PointForPar = 2)
        {
            int tempA;
            if (NbCoup > 0)
            {
                tempA = Par - GetScoreNet(NbCoup, hcphole, hcpPlayer) + PointForPar;
                if (tempA < 0)
                    return 0;
                else
                    return tempA;
            }
            else
                return 0;
        }
        public int GetScoreNet(int NbCoup, int hcphole, int hcpPlayer)
        {
            int ret = 0;
            ret = NbCoup - GetAdjustement(hcphole, hcpPlayer);
            return ret;
        }
        public int GetAdjustement(int hcphole, int hcpPlayer)
        {
            int ret = 0;
            if (hcpPlayer <= 0)
            {
                if ((hcpPlayer + 18) >= hcphole)
                    ret = 0;
                else
                    ret = -1;
            }
            else if (hcpPlayer <= 18)
            {
                if (hcpPlayer >= hcphole)
                    ret = 1;
                else
                    ret = 0;
            }
            else if (hcpPlayer <= 36)
            {
                if ((hcpPlayer - 18) >= hcphole)
                    ret = 2;
                else
                    ret = 1;

            }
            else //>36
            {
                if ((hcpPlayer - 36) >= hcphole)
                    ret = 3;
                else
                    ret = 2;
            }
            return ret;
        }

        public double getShPointsForExtra(string PlayerName)
        {
            double ret = 0;
            if (extras.extras.ContainsKey(PlayerName))
                ret = extras.extras[PlayerName];
            return ret;
        }
        public double getShPointsForMatch(String PlayerName, ConfigsForYear shPointsDef, bool is9Holes, bool isSurLaTouche = false)
        {
            double ret = 0;
            if (isSurLaTouche)
            {
                foreach (flight f in flights.Values)
                {
                    double shForMatch = getShPointsForMatchType(f.matchType, shPointsDef, is9Holes);
                    return shForMatch / 2; //Half the points if sur la touche
                }
            }

            flight ctF = getMyFlight(PlayerName);
            if (ctF != null)
            {
                double shForMatch = getShPointsForMatchType(ctF.matchType, shPointsDef, is9Holes);
                foreach (match m in ctF.matchs.Values)
                {
                    team myTeam = m.GetMyTeam(PlayerName);
                    if (myTeam == null)
                        continue; //suis pas dans le match
                    else
                    {
                        if (matchScores.matchResults.ContainsKey(m.name))
                        {
                            switch(matchScores.matchResults[m.name].WinnerteamName)
                            {
                                case 0:
                                    ret += shForMatch / 2; //half the points for a draw
                                    break; 
                                case 1:
                                    if(m.Team1.name1based == myTeam.name1based)
                                        ret += shForMatch;
                                    break; //half the points for a draw
                                case 2:
                                    if (m.Team2.name1based == myTeam.name1based)
                                        ret += shForMatch;
                                    break; //half the points for a draw
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public double getShPointsForStblDaily(String PlayerName, ConfigsForYear shPointsDef)
        {
            double ret = 0;
            if(stblPoints.isValidForStblDay())
            {
                int Position = stblPoints.getSortedPlayerList().FindIndex(x => x == PlayerName);
                String[] splits = shPointsDef.stlDay.Replace(" ", "").Split(',');
                if (splits.Length > Position)
                    Double.TryParse(splits[Position], out ret);
            }
            return ret;
        }
        public double getShPointsForMatchType(flight.MatchType matchType, ConfigsForYear shPointsDef, bool is9Holes)
        {
            double ret = 2;
            String[] splits = shPointsDef.ptsMatch.Replace(" ", "").Split(',');
            if (matchType == flight.MatchType.Foursome && splits.Length > 1) //Foursome used to be only 1 points
            {
                double.TryParse(splits[1], out ret); // old
            }
            else
            {
                double.TryParse(splits[0], out ret);
                switch (matchType)
                {
                    case flight.MatchType.FoursomeWinch5:
                    case flight.MatchType.Winch:
                        ret = ret / 2; // only half points for Winch 
                        break;
                }
            }
            if (is9Holes) //only half points for 9 holes
                ret = ret / 2;
            return ret;
        }
        public void initialiseEmptyExtra()
        {
            extras.initialise(this);
        }
        public void initialiseEmptyScore()
        {
            scores.initialise(this);
        }
        public void simulateScores()
        {
            scores.simulate(this);
        }
    }
    public class dailyExtras //extra shamrock points (saut a pied joint, chips...)
    {
        public Dictionary<string, double> extras = new Dictionary<string, double>();
        public void initialise(day d)
        {
            foreach (flight f in d.flights.Values)
            {
                foreach (team t in f.teams.Values)
                {
                    foreach(Player p in t.players.Values)
                    {
                        extras.Add(p.name, 0);
                    }
                }
            }
        }
    }
    public class dailyScores //score by holes (serialized)
    {
        public Dictionary<String, List<int>> holeResults = new Dictionary<String, List<int>>();
        public int getScore(String playerName, int nr)
        {
            return holeResults[playerName][nr - 1];
        }
        public void initialise(day d)
        {
            foreach (flight f in d.flights.Values)
            {
                foreach (team t in f.teams.Values)
                {
                    foreach (playingBall b in t.playingBalls.Values)
                    {
                        holeResults.Add(b.GetPlayersString(), new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                    }
                }
            }
        }
        public void simulate(day d)
        {
            Random rnd = new Random();
            holeResults.Clear();
            foreach (flight f in d.flights.Values)
            {
                foreach(team t in f.teams.Values)
                {
                    foreach(playingBall b in t.playingBalls.Values)
                    {
                        List<int> sims = new List<int>();
                        for(int i = 1; i <= 18; i++)
                        {
                            int r = Convert.ToInt32(1 + 10 * rnd.NextDouble());
                            if (r > 9)
                                r = 0;
                            sims.Add(r);
                        }
                        if(!holeResults.ContainsKey(b.GetPlayersString()))
                            holeResults.Add(b.GetPlayersString(), sims);
                    }
                }
            }
        }
        public int getScoreOut(String playerName)
        {
            int ret = 0;
            for (int i = 1; i <= 9; i++)
            {
                int cntResult = holeResults[playerName][i-1];
                if (cntResult <= 0)
                    return -1;
                ret += cntResult;
            }
            return ret;
        }
        public int getScoreIn(String playerName)
        {
            int ret = 0;
            for (int i = 10; i <= 18; i++)
            {
                int cntResult = holeResults[playerName][i- 1];
                if (cntResult <= 0)
                    return -1;
                ret += cntResult;
            }
            return ret;
        }
        public int getScoreTotal(String playerName)
        {
            int ret = 0;
            for (int i = 1; i <= 18; i++)
            {
                int cntResult = holeResults[playerName][i - 1];
                if (cntResult <= 0)
                    return -1;
                ret += cntResult;
            }
            return ret;
        }
    }
    public class MatchScores //score for the Matchs (serialized)
    {
        public Dictionary<String, MatchScore> matchResults = new Dictionary<String, MatchScore>();
        public void initialise(Dictionary<String, flight> flights)
        {
            foreach (flight ctFlight in flights.Values)
            {
                matchResults.Add(ctFlight.name, new MatchScore());
            }
        }
    }
    public class MatchScore
    {
        public MatchScore()
        {
            WinnerteamName = -1;

        }
        public int WinnerteamName { get; set; }
        public int nrHolesLeft { get; set; }
        public int nrPoints { get; set; }
    }
    public class dailySblPoints //stbl Points by holes (calculated)
    {
        public Dictionary<String, List<int>> points = new Dictionary<String, List<int>>();
        public int getStblPointsForHole(String playerName, int holeNr)
        {
            return points[playerName][holeNr - 1];
        }
        public int getStblPointsForLastHoles(String playerName, int nrLastHoles = 18)
        {
            int ret = 0;
            for (int i = 1; i <= nrLastHoles; i++)
            {
                ret += points[playerName][18 -i];
            }
            return ret;
        }
        public Boolean isValidForStblDay()
        {
            foreach (String name in points.Keys) //for each players
            {
                if (getStblPointsForLastHoles(name) > 0)
                    return true;
            }
            return false;
        }
        public List<String> getSortedPlayerList()
        {
            Dictionary<String, Double> d = new Dictionary<string, Double>();
            foreach (String name in points.Keys) //for each players
                d.Add(name, getSortableScore(name));
            List<String> ret = d.OrderByDescending(kp => kp.Value)
                                      .Select(kp => kp.Key)
                                      .ToList();
            return ret;
        }
        Double getSortableScore(String name)
        {
            Double ret = 0;
            int nrLastHoles = 18;
            ret += ((double)getStblPointsForLastHoles(name, nrLastHoles) * 100000000);
            nrLastHoles = 9;
            ret += ((double)getStblPointsForLastHoles(name, nrLastHoles) * 1000000);
            nrLastHoles = 6;
            ret += ((double)getStblPointsForLastHoles(name, nrLastHoles) * 10000);
            nrLastHoles = 3;
            ret += ((double)getStblPointsForLastHoles(name, nrLastHoles) * 100);
            nrLastHoles = 1;
            ret += ((double)getStblPointsForLastHoles(name, nrLastHoles) * 1);
            return ret;
        }
    }
    public class Hcps
    {
        public Dictionary<String, Double> hpcs = new Dictionary<string, double>();
        public double calcNewHcpASG(Double oldHcp, int stblPoints)
        {
            int tempA;
            tempA = stblPoints- 36;

            Double ret;
            if (oldHcp < 4.5)
            {
                if (tempA > 0)
                    ret = oldHcp - (tempA * 0.1);
                else
                {
                    if (tempA < -1)
                        ret = oldHcp + 0.1;
                    else
                        ret = oldHcp;
                }
            }
            else if (oldHcp < 11.4)
            {
                if (tempA > 0)
                    ret = oldHcp - (tempA * 0.2);
                else
                {
                    if (tempA < -2)
                        ret = oldHcp + 0.1;
                    else
                        ret = oldHcp;
                }
            }
            else if (oldHcp < 18.4)
            {
                if (tempA > 0)
                    ret = oldHcp - (tempA * 0.3);
                else
                {
                    if (tempA < -3)
                        ret = oldHcp + 0.1;
                    else
                        ret = oldHcp;
                }
            }
            else if (oldHcp < 26.4)
            {
                if (tempA > 0)
                    ret = oldHcp - (tempA * 0.4);
                else
                {
                    if (tempA < -4)
                        ret = oldHcp + 0.1;
                    else
                        ret = oldHcp;
                }
            }
            else
            {
                if (tempA > 0)
                    ret = oldHcp - (tempA * 0.5);
                else
                {
                    if (tempA < -5)
                        ret = oldHcp + 0.2;
                    else
                        ret = oldHcp;
                }
            }
            return ret;
        }
    }
    public class HoleForStat
    {
        public enum resType { Albatros, Eagle, Birdie, Par, Boguey, Bog2, Bog3, X};
        public string playerName = "";
        public string Parcours = "";
        public string year = "";
        public int dayNr = 0;
        public hole defHole = new hole();
        public int ptsNet = 0;
        public int ptsBrut = 0;
        public int ptsBrut36 = 0; // les pts bruts ne suffisent pas pour savoir si il s'agit d'un Bog2 ou bog3
        public int cpsRecu = 0;
        public resType getResType()
        {
            switch(ptsBrut)
            {
                case 5: return resType.Albatros;
                case 4: return resType.Eagle;
                case 3: return resType.Birdie;
                case 2: return resType.Par;
                case 1: return resType.Boguey;
                default:
                    if (ptsBrut36 == 2)
                        return resType.Bog2;
                    else if (ptsBrut36 == 1)
                        return resType.Bog3;
                    else
                        return resType.X;
            }
        }
    }
}
