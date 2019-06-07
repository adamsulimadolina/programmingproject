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
    public class Teams
    {
        private List<Team> VBlist = new List<Team>();
        private List<Team> DBlist = new List<Team>();
        private List<Team> TOWlist = new List<Team>();
        public String ShowTeam(int i)
        {
            return VBlist[i].getTeamName();
        }
        public void AddTeam(Team x) //dodawanie druzyny
        {
            if (x.getSport() == Projekt.Team.Volleyball)
            {
                VBlist.Add(x);
                Controler.VolleyballTournament.isTournamentPlayed = false;
                Controler.VolleyballTournament.isFinalsPlayed = false;
            }
            if (x.getSport() == Projekt.Team.Dodgeball)
            {
                DBlist.Add(x);
                Controler.DodgeballTournament.isTournamentPlayed = false;
                Controler.DodgeballTournament.isFinalsPlayed = false;
            }
            if (x.getSport() == Projekt.Team.TugOfWar)
            {
                TOWlist.Add(x);
                Controler.TugOfWarTournament.isTournamentPlayed = false;
                Controler.TugOfWarTournament.isFinalsPlayed = false;
            }
        }
        public void RemoveTeam(Team x)
        {
            bool checkmark = false;
            int i = 0;
            foreach (Team a in VBlist)
            {
                if (x.getTeamName() == a.getTeamName())
                {
                    VBlist.RemoveAt(i);
                    Controler.VolleyballTournament.isTournamentPlayed = false;
                    Controler.VolleyballTournament.isFinalsPlayed = false;
                    Console.Clear();
                    Console.WriteLine("Usuniecie druzyny powiodlo sie.");
                    Console.ReadKey();
                    checkmark = true;
                    break;
                }
                i++;
            }
            i = 0;
            foreach (Team a in DBlist)
            {
                if (x.getTeamName() == a.getTeamName())
                {
                    DBlist.RemoveAt(i);
                    Controler.DodgeballTournament.isTournamentPlayed = false;
                    Controler.DodgeballTournament.isFinalsPlayed = false;
                    Console.Clear();
                    Console.WriteLine("Usuniecie druzyny powiodlo sie.");
                    Console.ReadKey();
                    checkmark = true;
                    break;
                }
                i++;
            }
            i = 0;
            foreach (Team a in TOWlist)
            {
                if (x.getTeamName() == a.getTeamName())
                {
                    TOWlist.RemoveAt(i);
                    Controler.TugOfWarTournament.isTournamentPlayed = false;
                    Controler.TugOfWarTournament.isFinalsPlayed = false;
                    Console.Clear();
                    Console.WriteLine("Usuniecie druzyny powiodlo sie.");
                    Console.ReadKey();
                    checkmark = true;
                    break;
                }
                i++;
            }
            if(checkmark==false)
            {
                Console.Clear();
                Console.WriteLine("Usuniecie druzyny nie powiodlo sie.");
                Console.ReadKey();
            }
        }
        public List<Team> getVolleyballTeams() //pobieranie teamow
        {
            return VBlist;
        }
        public List<Team> getDodgeballTeams()
        {
            return DBlist;
        }
        public List<Team> getTugOfWarTeams()
        {
            return TOWlist;
        }
        public void Show()          //pokazanie wszystkich druzyn
        {
            Console.Clear();
            if (VBlist.Count != 0) AllTeams(VBlist);
            if (DBlist.Count != 0) AllTeams(DBlist);
            if (TOWlist.Count != 0) AllTeams(TOWlist);
        }
        private void AllTeams(List<Team> teamlist)
        {
            try
            {
                foreach (Team x in teamlist)
                {
                    Console.WriteLine();
                    Console.WriteLine("[{0} - {1}]:", x.getTeamName(), x.getSport());
                    foreach(Player a in x.getPlayers())
                    {
                        Console.WriteLine("[{0} {1}]", a.getName(), a.getSurname());
                    }
                    Console.WriteLine();
                }
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }

        //volleyball teams
        public void SerializeVolleyballTeams()
        {
            FileStream file;
            file = new FileStream("VolleyballTeams.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, VBlist);
            file.Close();
        }
        public void DeserializeVolleyballTeams()
        {
            try
            {
                FileStream file;
                file = new FileStream("VolleyballTeams.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                VBlist = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //dodgeball teams
        public void SerializeDodgeballTeams()
        {
            FileStream file;
            file = new FileStream("DodgeballTeams.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, DBlist);
            file.Close();
        }
        public void DeserializeDodgeballTeams()
        {
            try
            {
                FileStream file;
                file = new FileStream("DodgeballTeams.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                DBlist = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }

        //tow teams
        public void SerializeTugOfWarTeams()
        {
            FileStream file;
            file = new FileStream("TugOfWarTeams.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, TOWlist);
            file.Close();
        }
        public void DeserializeTugOfWarTeams()
        {
            try
            {
                FileStream file;
                file = new FileStream("TugOfWarTeams.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                TOWlist = (List<Team>)formatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }
        }
    }
}
