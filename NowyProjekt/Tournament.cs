using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projekt
{
    [Serializable]
    public class Tournament
    {
        private List<Match> MatchList = new List<Match>();
        private List<Team> Scoreboard = new List<Team>();
        private List<Team> FinalsList = new List<Team>();
        private List<Match> FinalsMatchList = new List<Match>();
        public bool isTournamentPlayed = false;
        public bool isFinalsPlayed = false;
        /// <summary>
        /// //ustawienie sportu dla obiektu turnieju
        /// </summary>
        /// <param name="TeamList">lista druzyn ktora przekazujemy</param>
        /// <returns></returns>
        private Match ChooseTournamentSport(List<Team> TeamList) 
        {
            if (TeamList[0].getSport() == Projekt.Team.Volleyball)
            {
                Volleyball m = new Volleyball();
                return m;
            }
            if (TeamList[0].getSport() == Projekt.Team.Dodgeball)
            {
                Dodgeball m = new Dodgeball();
                return m;
            }
            else
            {
                TugOfWar m = new TugOfWar();
                return m;
            }
        }
        /// <summary>
        /// rozegranie turnieju
        /// </summary>
        /// <param name="TeamList">lista druzyn do rozegrania turnieju</param>
        /// <param name="refs">sedziowie ktorzy beda sedziowali</param>
        public void PlayTournament(List<Team> TeamList, Referees refs) 
        {
            if (isTournamentPlayed == false)
            {
                int i = 0;
                List<Team> list = new List<Team>();
                List<Match> h = new List<Match>();
                foreach (Team w in TeamList)
                {
                    w.setLoses(0);
                    w.setWins(0);
                    list.Add(w);
                }
                while (list.Count > 0)
                {
                    i = 0;
                    Team team = list[0];
                    list.RemoveAt(0);
                    foreach (Team x in list)
                    {
                        Match m = ChooseTournamentSport(list);
                        m.setTeam1(team);
                        m.ChooseReferee(refs);
                        m.setTeam2(list[i]);
                        m.Play();
                        MatchList.Add(m);
                        i++;
                    }

                }
            }
            isTournamentPlayed = true;

        }
        /// <summary>
        /// sortowanie tablicy, stworzenie rankingu
        /// </summary>
        /// <param name="TeamList">lista druzyn do posortowania</param>
        /// <returns></returns>
        private List<Team> MakeScoreboard(List<Team> TeamList) 
        {
            TeamList.Sort((p, q) => -1 * p.getWins().CompareTo(q.getWins()));
            return TeamList;
        }
        /// <summary>
        /// wyswietlenie rankingu
        /// </summary>
        /// <param name="LoT">posortowana lista druzyn</param>
        public void ShowScoreboard(List<Team> LoT) 
        {
            Scoreboard = MakeScoreboard(LoT);
            int i = 0;
            foreach (Team x in Scoreboard)
            {
                Console.WriteLine($"{i+1}. {Scoreboard[i].getTeamName()} W:{Scoreboard[i].getWins()} L:{Scoreboard[i].getLoses()}");
                i++;
            }
            
        }
        /// <summary>
        /// wyswietlenie rozegranych meczów
        /// </summary>
        public void ShowMatches() 
        {
            int j = 1;
            foreach(Match a in MatchList)
            {
                Console.WriteLine("[[[[Match {0}]]]]", j++);
                a.ShowScore();
                Console.WriteLine();
            }
        }
        /// <summary>
        /// wyswietlenie tabeli finalowej
        /// </summary>
        /// <param name="LoT"></param>
        public void ShowFinalsScoreboard(List<Team> LoT)
        {
            FinalsList = MakeScoreboard(LoT);
            int i = 0;
            foreach (Team x in FinalsList)
            {
                Console.WriteLine($"{i + 1}. {FinalsList[i].getTeamName()}");
                i++;
            }
        }
        /// <summary>
        /// stowrzenie rankingu dla finalow
        /// </summary>
        public void MakeFinalsList() 
        {
            if (FinalsList.Count < 4)
            {
                for (int i = 0; i < 4 && i < Scoreboard.Count; i++)
                {
                    Team w = new Team();
                    w.setTeamName(Scoreboard[i].getTeamName());
                    FinalsList.Add(w);
                    FinalsList[i].setWins(0);
                    FinalsList[i].setLoses(0);
                }
            }
        }
        /// <summary>
        /// /rozegranie finalow
        /// </summary>
        public void PlayFinals() 
        {
            Match x = ChooseTournamentSport(Scoreboard);
            Match y = ChooseTournamentSport(Scoreboard);
            Match z = ChooseTournamentSport(Scoreboard);
            x.setTeam1(FinalsList[0]);
            x.setTeam2(FinalsList[3]);
            y.setTeam1(FinalsList[1]);
            y.setTeam2(FinalsList[2]);
            x.Play();
            FinalsMatchList.Add(x);
            y.Play();
            FinalsMatchList.Add(y);
            FinalsList = MakeScoreboard(FinalsList);
            z.setTeam1(FinalsList[0]);
            z.setTeam2(FinalsList[1]);
            z.Play();
            FinalsMatchList.Add(z);
            FinalsList = MakeScoreboard(FinalsList);

        }
        /// <summary>
        /// wyswietlenie wynikow finalow
        /// </summary>
        public void Finals() 
        {
            MakeScoreboard(Scoreboard);
            MakeFinalsList();
            if (isFinalsPlayed == false)
            {
                PlayFinals();
                isFinalsPlayed = true;
            }
            Console.WriteLine("Semifinal I:");
            FinalsMatchList[0].ShowScore();
            Console.WriteLine("Semifinal II:");
            FinalsMatchList[1].ShowScore();
            Console.WriteLine("Final:");
            FinalsMatchList[2].ShowScore();
            Console.WriteLine("Finals result:");
            ShowFinalsScoreboard(FinalsList);
        }

        //Volleyball FINALS MATCHLIST
        public void SerializeVolleyballFinalsMatchList()
        {
            FileStream file;
            file = new FileStream("VolleyballFinalsMatchList.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsMatchList);
            file.Close();
        }
        public void DeserializeVolleyballFinalsMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyballFinalsMatchList.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsMatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //TOW FINALS MATCHLIST
        public void SerializeTOWFinalsMatchList()
        {
            FileStream file;
            file = new FileStream("TOWFinalsMatchList.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsMatchList);
            file.Close();
        }
        public void DeserializeTOWFinalsMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("TOWFinalsMatchList.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsMatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        //DODGEBALL FINALS MATCHLIST
        public void SerializeDodgeballFinalsMatchList()
        {
            FileStream file;
            file = new FileStream("DodgeballFinalsMatchList.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsMatchList);
            file.Close();
        }
        public void DeserializeDodgeballFinalsMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballFinalsMatchList.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsMatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //VOLLEYBALL IS TOURNAMENT PLAYED
        public void SerializeVolleyIsTournamentPlayed()
        {
            FileStream file;
            file = new FileStream("VolleyisTournamentPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isTournamentPlayed);
            file.Close();
        }
        public void DeserializeVolleyisTournamentPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyisTournamentPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isTournamentPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //VOLLEYBALL IS FINALS PLAYED
        public void SerializeVolleyIsFinalsPlayed()
        {
            FileStream file;
            file = new FileStream("VolleyisFinalsPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isFinalsPlayed);
            file.Close();
        }
        public void DeserializeVolleyisFinalsPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyisFinalsPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isFinalsPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        // DODGEBALL IS TOURNAMENT PLAYED
        public void SerializeDodgeballIsTournamentPlayed()
        {
            FileStream file;
            file = new FileStream("DodgeballisTournamentPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isTournamentPlayed);
            file.Close();
        }
        public void DeserializeDodgeballisTournamentPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballisTournamentPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isTournamentPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        // DODGEBALL IS FINALS PLAYED
        public void SerializeDodgeballIsFinalsPlayed()
        {
            FileStream file;
            file = new FileStream("DodgeballisFinalsPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isFinalsPlayed);
            file.Close();
        }
        public void DeserializeDodgeballisFinalsPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballisFinalsPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isFinalsPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        // TOW is TOURNAMENT PLAYED
        public void SerializeTOWIsTournamentPlayed()
        {
            FileStream file;
            file = new FileStream("TOWisTournamentPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isTournamentPlayed);
            file.Close();
        }
        public void DeserializeTOWisTournamentPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("TOWisTournamentPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isTournamentPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        // TOW IS FINALS PLAYED
        public void SerializeTOWIsFinalsPlayed()
        {
            FileStream file;
            file = new FileStream("TOWisFinalsPlayed.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, isFinalsPlayed);
            file.Close();
        }
        public void DeserializeTOWisFinalsPlayed()
        {
            FileStream file;
            try
            {
                file = new FileStream("TOWisFinalsPlayed.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                isFinalsPlayed = (bool)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //VOLLEYBALL SCOREBOARD
        public void SerializeVolleyballScoreboard()
        {
            FileStream file;
            file = new FileStream("VolleyballScoreboard.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, Scoreboard);
            file.Close();
        }
        public void DeserializeVolleyballScoreboard()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyballScoreboard.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Scoreboard = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //DODGEBALL SCOREBOARD
        public void SerializeDodgeballScoreboard()
        {
            FileStream file;
            file = new FileStream("DodgeballScoreboard.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, Scoreboard);
            file.Close();
        }
        public void DeserializeDodgeballScoreboard()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballScoreboard.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Scoreboard = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //TOW SCOREBOARD
        public void SerializeTugOfWarScoreboard()
        {
            FileStream file;
            file = new FileStream("TugOfWarScoreboard.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, Scoreboard);
            file.Close();
        }
        public void DeserializeTugOfWarScoreboard()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballScoreboard.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                Scoreboard = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //VOLLEYBALL FINALS
        public void SerializeVolleyballFinals()
        {
            FileStream file;
            file = new FileStream("VolleyballFinals.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsList);
            file.Close();
        }
        public void DeserializeVolleyballFinals()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyballFinals.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsList = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //DODGEBALL FINALS
        public void SerializeDodgeballFinals()
        {
            FileStream file;
            file = new FileStream("DodgeballFinals.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsList);
            file.Close();
        }
        public void DeserializeDodgeballFinals()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballFinals.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsList = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //TOW FINALS
        public void SerializeTugOfWarFinals()
        {
            FileStream file;
            file = new FileStream("TugOfWarFinals.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, FinalsList);
            file.Close();
        }
        public void DeserializeTugOfWarFinals()
        {
            FileStream file;
            try
            {
                file = new FileStream("TugOfWarFinals.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                FinalsList = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //VOLLEYBALL MATCHLIST
        public void SerializeVolleyballMatchlist()
        {
            FileStream file;
            file = new FileStream("VolleyballMatchlist.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, MatchList);
            file.Close();
        }
        public void DeserializeVolleyballMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("VolleyballMatchlist.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                MatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //DODGEBALL MATCHLIST
        public void SerializeDodgeballMatchlist()
        {
            FileStream file;
            file = new FileStream("DodgeballMatchlist.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, MatchList);
            file.Close();
        }
        public void DeserializeTugOfWarMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("TugOfWarMatchlist.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                MatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //TOW MATCHLIST
        public void SerializeTugOfWarMatchlist()
        {
            FileStream file;
            file = new FileStream("TugOfWarMatchlist.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, MatchList);
            file.Close();
        }
        public void DeserializeDodgeballMatchList()
        {
            FileStream file;
            try
            {
                file = new FileStream("DodgeballMatchlist.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                MatchList = (List<Match>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
        
    }
}


