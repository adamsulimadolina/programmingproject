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
    public class Team
    {
        private String Name;
        private String Sport;
        private int WinCounter, LoseCounter;
        private List<Player> PlayerList = new List<Player>();
        public static String Volleyball = "Volleyball";
        public static String Dodgeball = "Dodgeball";
        public static String TugOfWar = "TugOfWar";
        public Team()
        {
            WinCounter = 0;
            LoseCounter = 0;
        }
        public Team(String x)
        {
            Name = x;
            WinCounter = 0;
            LoseCounter = 0;
        }
        public void setTeamName(string teamname) //ustawienie nazwy druzyny
        {
            Name = teamname;
        }
        public List<Player> getPlayers() //zwraca liste zawodnikow w druzynie
        {
            return PlayerList;
        }
        public String getTeamName() //pobranie nazwy druzyny
        {
            return Name;
        }
        public void addWin() //dodanie zwyciestwa/porazki
        {
            WinCounter++;
        }
        public void addLose()
        {
            LoseCounter++;
        }
        public int getWins()  //pobranie zwyciestw/porazek
        {
            return WinCounter;

        }
        public int getLoses()
        {
            return LoseCounter;
        }
        public void AddPlayer(Player x) //dodanie zawodnika
        {
            PlayerList.Add(x);
        }
        public void RemovePlayer(Player x) //usuniecie zawodnika
        {
            int i = 0;
            foreach (Player a in PlayerList)
            {
                if (x.getName() == a.getName() && x.getSurname() == a.getSurname())
                {
                    PlayerList.RemoveAt(i);
                }
                i++;
            }
        }
        public void setVolleyball() //3 metody ustawiaja sport
        {
            Sport = Volleyball;
        }
        public void setDodgeball()
        {
            Sport = Dodgeball;
        }
        public void setTugOfWar()
        {
            Sport = TugOfWar;
        }
        public String getSport() //zwraca sport
        {
            return Sport;
        }
        public void setWins(int z) //ustawienie zwyciestw / porazek
        {
            WinCounter = z;
        }
        public void setLoses(int z)
        {
            LoseCounter = z;
        }
        public bool CheckName(string x) //sprawdzanie poprawnosci nazwy
        {
            string pomoc = x;

            for(int i=0;i<x.Length;i++)
            {
                if (x.Length > 1)
                {
                    if (!char.IsLetterOrDigit(x, i)) return false;
                }
            }
            return true;
        }
    }
}
