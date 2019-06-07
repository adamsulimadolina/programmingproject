using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;


namespace Projekt
{
    [Serializable]
    class Operations
    {

        /// <summary>
        /// Ustawianie nazwiska na prawidlowy format
        /// </summary>
        /// <param name="x">nazwisko</param>
        /// <returns></returns>
        public static String Normalize(string x)
        {
            StringBuilder help = new StringBuilder(x);
            for(int i=0;i<x.Length;i++)
            {
                if (i == 0) help[i] = Char.ToUpper(x[0]);
                else
                {
                    help[i] = Char.ToLower(x[i]);
                }
            }
            return help.ToString();
        }

        /// <summary>
        /// dodawanie sedziow w programie
        /// </summary>
        public static void AddReferee()
        {
            Referee referee = new Referee();
            bool z = true;
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Podaj nazwisko sedziego: ");
                string imie = Console.ReadLine();
                imie = Normalize(imie);
                Regex reg = new Regex(@"^[a-zA-Z]+$");
                if (reg.IsMatch(imie))
                {
                    if (referee.CheckName(imie) == false)
                    {
                        Console.WriteLine("Nieprawidłowy format imienia.");
                        Console.ReadKey();
                    }
                    else
                    {
                        referee.setSurname(imie);
                        z = false;
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy format nazwiska.");
                    Console.ReadKey();
                }
            }
            z = true;
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Podaj imie sedziego: ");
                string imie = Console.ReadLine();
                imie = Normalize(imie);
                Regex reg = new Regex(@"^[a-zA-Z]+$");
                if (reg.IsMatch(imie))
                {
                    if (referee.CheckName(imie) == false)
                    {
                        Console.WriteLine("Nieprawidłowy format imienia.");
                        Console.ReadKey();
                    }
                    else
                    {
                        referee.setName(imie);
                        z = false;
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy format nazwiska.");
                    Console.ReadKey();
                }
            }
            try
            {
                Controler.referees.AddRef(referee);
                Console.Clear();
                Console.WriteLine("Dodawanie sedziego powiodlo sie.");
                Console.ReadKey();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Dodawanie sedziego nie powiodlo sie.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// usuwanie Sędziów w programie
        /// </summary>
        public static void RemoveReferee()
        {
            bool z = true;
            Referee reftoremove = new Referee();
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Podaj dane sedziego do usuniecia.");
                Console.WriteLine("Nazwisko:");
                string surname = Console.ReadLine();
                surname = Normalize(surname);
                Regex reg = new Regex(@"^[a-zA-Z]+$");
                if (reg.IsMatch(surname))
                {
                    if (reftoremove.CheckName(surname) == false)
                    {
                        Console.WriteLine("Nieprawidłowy format nazwiska.");
                        Console.ReadKey();
                    }
                    else
                    {
                        reftoremove.setSurname(surname);
                        z = false;
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy format nazwiska.");
                    Console.ReadKey();
                }
            }
            z = true;
            while (z == true)
            {
                Console.WriteLine("Imie:");
                string name = Console.ReadLine();
                name = Normalize(name);
                Regex reg = new Regex(@"^[a-zA-Z]+$");
                if (reg.IsMatch(name))
                {
                    if (reftoremove.CheckName(name) == false)
                    {
                        Console.WriteLine("Nieprawidłowy format imienia.");
                        Console.ReadKey();
                    }
                    else
                    {
                        reftoremove.setName(name);
                        z = false;
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy format nazwiska.");
                    Console.ReadKey();
                }
            }

            Controler.referees.RemoveRef(reftoremove);

        }
        
        /// <summary>
        /// wyswietlanie sedziow
        /// </summary>
        public static void ShowReferees()
        {
            Console.Clear();
            Console.WriteLine("Lista sedziow: ");
            Controler.referees.ShowReferees();
            Console.ReadKey();
        }

        /// <summary>
        /// dodanie druzyny
        /// </summary>
        public static void AddTeam()
        {
            int a, b = 100;
            bool z = true;
            Team team = new Team();
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Podaj nazwe druzyny (max. 3 czlonowa, tylko litery i cyfry):");
                string teamname = Console.ReadLine();
                teamname = Normalize(teamname);
                Regex reg1 = new Regex(@"^[a-zA-Z0-9]+\s[a-zA-z0-9]+\s[a-zA-z0-9]+$");
                Regex reg2 = new Regex(@"^[a-zA-Z0-9]+\s[a-zA-z0-9]+$");
                Regex reg3 = new Regex(@"^[a-zA-Z0-9]+$");
                if (reg1.IsMatch(teamname) || reg2.IsMatch(teamname) || reg3.IsMatch(teamname))
                {
                    team.setTeamName(teamname);
                    z = false;
                }
                else
                {
                    Console.WriteLine("Nieprawidlowy format nazwy druzyny.");
                    Console.ReadKey();
                }
            }
            for (int i = 0; i < 5; i++)
            {
                Player newPlayer = new Player();
                Console.Clear();
                Console.WriteLine("Podaj nazwisko zawodnika {0}: ", i + 1);
                string nazwisko = Console.ReadLine();
                nazwisko = Normalize(nazwisko);
                Regex reg = new Regex(@"^[a-zA-Z]+$");
                if (reg.IsMatch(nazwisko))
                {
                    if (newPlayer.CheckName(nazwisko) == false) throw new NameException();
                    else
                    {
                        newPlayer.setSurname(nazwisko);
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidlowy format nazwiska.");
                    Console.ReadKey();
                    i--;
                    continue;
                }
                
                z = true;
                while (z == true)
                {
                    Console.Clear();
                    Console.WriteLine("Podaj imie zawodnika {0}: ", i + 1);
                    string imie = Console.ReadLine();
                    imie = Normalize(imie);
                    if (reg.IsMatch(imie))
                    {
                        if (newPlayer.CheckName(imie) == false)
                        {
                            Console.WriteLine("Nieprawidłowy format imienia.");
                            Console.ReadKey();
                        }
                        else
                        {
                            newPlayer.setName(imie);
                            z = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy format nazwiska.");
                        Console.ReadKey();
                    }
                }
                team.AddPlayer(newPlayer);
            }
            bool y = true;
            while (y == true)       //ustawienie sportu dla druzyny
            {
                Console.Clear();
                Console.WriteLine("Wybierz sport:");
                Console.WriteLine("1. Siatkowka.");
                Console.WriteLine("2. Zbijak.");
                Console.WriteLine("3. Przeciaganie liny.");
                Console.WriteLine("0. Powrot.");
                try
                {
                    b = Convert.ToInt32(Console.ReadLine());
                }
                catch { }
                a = b;
                switch (a)
                {
                    case 1:
                        {
                            team.setVolleyball();
                            y = false;
                            break;
                        }
                    case 2:
                        {
                            team.setDodgeball();
                            y = false;
                            break;
                        }
                    case 3:
                        {
                            team.setTugOfWar();
                            y = false;
                            break;
                        }
                    case 0:
                        {
                            y = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            try
            {
                Controler.teams.AddTeam(team);
                Console.Clear();
                Console.WriteLine("Dodanie druzyny powiodlo sie.");
                Console.ReadKey();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Dodanie druzyny nie powiodlo sie.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// usuniecie druzyny
        /// </summary>
        public static void RemoveTeam()
        {
            bool z = true;
            Team team = new Team();
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Podaj nazwe druzyny (tylko litery i cyfry):");
                string teamname = Console.ReadLine();
                teamname = Normalize(teamname);
                Regex reg1 = new Regex(@"^[a-zA-Z0-9]+\s[a-zA-z0-9]+\s[a-zA-z0-9]+$");
                Regex reg2 = new Regex(@"^[a-zA-Z0-9]+\s[a-zA-z0-9]+$");
                Regex reg3 = new Regex(@"^[a-zA-Z0-9]+$");
                if (reg1.IsMatch(teamname) || reg2.IsMatch(teamname) || reg3.IsMatch(teamname))
                {
                    if (team.CheckName(teamname) == false)
                    {
                        Console.WriteLine("Nieprawidlowy format nazwy druzyny.");
                        Console.ReadKey();
                    }
                    else
                    {
                        team.setTeamName(teamname);
                        z = false;
                    }
                }
            }
            Controler.teams.RemoveTeam(team);
            try
            {
                
            }
            catch
            {
                
            }
        }

        /// <summary>
        /// wyswietlenie listy druzyn z zawodnikami
        /// </summary>
        public static void ShowTeams()
        {
            Controler.teams.Show();
            Console.ReadKey();
        }
        /// <summary>
        /// menu sedziow
        /// </summary>
        public static void RefereeMenu() 
        {
            bool z = true;
            while (z == true)
            {
                int a, b = 100;
                Console.Clear();
                Console.WriteLine("Menu sedziow.");
                Console.WriteLine("1. Dodaj sedziego.");
                Console.WriteLine("2. Usun sedziego.");
                Console.WriteLine("3. Przeglad sedziow.");
                Console.WriteLine("0. Powrot");
                try
                {
                    b = Convert.ToInt32(Console.ReadLine());
                }
                catch { b = 100; }
                a = b;
                switch (a)
                {
                    case 1:
                        {
                            Operations.AddReferee();
                            break;
                        }
                    case 2:
                        {
                            Operations.RemoveReferee();
                            break;
                        }
                    case 3:
                        {
                            Operations.ShowReferees();
                            break;
                        }
                    case 0:
                        {
                            z = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// menu druzyn
        /// </summary>
        public static void TeamsMenu() 
        {
            bool z = true;
            int a, b = 100;
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Menu druzyn.");
                Console.WriteLine("1. Dodaj druzyne.");
                Console.WriteLine("2. Usun druzyne.");
                Console.WriteLine("3. Przeglad druzyn.");
                Console.WriteLine("0. Powrot.");
                try
                {
                    b = Convert.ToInt32(Console.ReadLine());
                }
                catch { b = 100; }
                a = b;
                switch (a)
                {
                    case 1:
                        {
                            Operations.AddTeam();
                            break;
                        }
                    case 2:
                        {
                            Operations.RemoveTeam();
                            break;
                        }
                    case 3:
                        {
                            Operations.ShowTeams();
                            break;
                        }
                    case 0:
                        {
                            z = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// po wybraniu sportu uruchamia turniej
        /// </summary>
        /// <param name="x"></param>
        public static void SportMenu(Team x) 
        {
            int sport = 0; // 1 - siatkowka, 2 - zbijak, 3 - przeciganie liny
            int a, b = 100;
            bool y = true;
            string s = x.getSport();
            if (s == Projekt.Team.Dodgeball)
            {
                Controler.DodgeballTournament.PlayTournament(Controler.teams.getDodgeballTeams(), Controler.referees);
                Controler.DodgeballTournament.ShowScoreboard(Controler.teams.getDodgeballTeams());
                s = "Zbijak";
                sport = 2;
            }
            if (s == Projekt.Team.Volleyball)
            {

                Controler.VolleyballTournament.PlayTournament(Controler.teams.getVolleyballTeams(), Controler.referees);
                Controler.VolleyballTournament.ShowScoreboard(Controler.teams.getVolleyballTeams());
                s = "Siatkowka";
                sport = 1;
            }
            if (s == Projekt.Team.TugOfWar)
            {
                Controler.TugOfWarTournament.PlayTournament(Controler.teams.getTugOfWarTeams(), Controler.referees);
                Controler.TugOfWarTournament.ShowScoreboard(Controler.teams.getTugOfWarTeams());
                s = "Przeciaganie liny";
                sport = 3;
            }
            while (y == true) //obsluga pojedynczego turnieju
            {
                Console.Clear();
                
                Console.WriteLine("Menu {0}.", s);
                Console.WriteLine("1. Tablica wynikow.");
                Console.WriteLine("2. Liste meczow.");
                Console.WriteLine("3. Finaly.");
                Console.WriteLine("0. Powrot.");
                try
                {
                    b = Convert.ToInt32(Console.ReadLine());
                }
                catch { b = 100; }
                a = b;
                switch (a)
                {
                    case 1:
                        {
                            Console.Clear();
                            if (sport == 1)
                            {
                                Controler.VolleyballTournament.ShowScoreboard(Controler.teams.getVolleyballTeams());
                            }
                            if (sport == 2)
                            {
                                Controler.DodgeballTournament.ShowScoreboard(Controler.teams.getDodgeballTeams());
                            }
                            if (sport == 3) Controler.TugOfWarTournament.ShowScoreboard(Controler.teams.getTugOfWarTeams());
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            if (sport == 1) Controler.VolleyballTournament.ShowMatches();
                            if (sport == 2) Controler.DodgeballTournament.ShowMatches();
                            if (sport == 3) Controler.TugOfWarTournament.ShowMatches();
                            Console.ReadKey();
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            if (sport == 1)
                            {
                                Controler.VolleyballTournament.Finals();
                            }
                            if (sport == 2)
                            {
                                Controler.DodgeballTournament.Finals();
                            }
                            if (sport == 3)
                            {
                                Controler.TugOfWarTournament.Finals();
                            }
                            Console.ReadKey();
                            break;
                        }
                    case 0:
                        {
                            y = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        /// <summary>
        ///  wybor sportu dla turnieju
        /// </summary>
        public static void TournamentMenu()
        {
            int a, b = 100;
            bool z = true;
            while (z == true)
            {
                Console.Clear();
                Console.WriteLine("Menu turnieju.");
                Console.WriteLine("Wybierz sport:");
                Console.WriteLine("1. Siatkowka.");
                Console.WriteLine("2. Zbijak.");
                Console.WriteLine("3. Przeciaganie liny.");
                Console.WriteLine("0. Powrot.");
                try
                {
                    b = Convert.ToInt32(Console.ReadLine());
                }
                catch { b = 100; }
                a = b;
                switch (a)
                {
                    case 1:
                        {
                            if (Controler.teams.getVolleyballTeams().Count < 4) 
                            {
                                Console.Clear();
                                Console.WriteLine("Za malo druzyn w siatkowce. (musza byc conajmniej 4)");
                                Console.ReadKey();
                            }
                            else
                            {
                                Team help = new Team();
                                help.setVolleyball();
                                Operations.SportMenu(help);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (Controler.teams.getDodgeballTeams().Count < 4)
                            {
                                Console.Clear();
                                Console.WriteLine("Za malo druzyn w zbijaku. (musza byc conajmniej 4)");
                                Console.ReadKey();
                            }
                            else
                            {
                                Team help = new Team();
                                help.setDodgeball();
                                Operations.SportMenu(help);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (Controler.teams.getTugOfWarTeams().Count < 4)
                            {
                                Console.Clear();
                                Console.WriteLine("Za malo druzyn w przeciaganiu liny. (musza byc conajmniej 4)");
                                Console.ReadKey();
                            }
                            else
                            {
                                Team help = new Team();
                                help.setTugOfWar();
                                Operations.SportMenu(help);
                            }
                            break;
                        }
                    case 0:
                        {
                            z = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        
    }
}
