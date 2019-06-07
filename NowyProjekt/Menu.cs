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
    class Menu
    {
        public static void Start()
        {
            int a, b = 100;
            bool x = true;
            while (x == true)
            {
                Console.WriteLine("Co chcesz zrobic?");
                Console.WriteLine("1. Menu druzyn.");
                Console.WriteLine("2. Menu sedziow.");
                Console.WriteLine("3. Menu turnieju.");
                Console.WriteLine("0. Wyjscie.");
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
                            Operations.TeamsMenu();
                            break;
                        } 
                    case 2:
                        {
                            Operations.RefereeMenu();
                            break;
                        }
                    case 3:
                        {
                            if(Controler.referees.getRefs().Count == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Brak sedziow - nie mozna rozegrac tunieju.");
                                Console.ReadKey();
                            }
                            else Operations.TournamentMenu();
                            break;
                        }
                    case 0:
                        {
                            x = false;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                Console.Clear();
            }
        }
    }
}
