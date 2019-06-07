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
    /// <summary>
    /// klasa dla sportu Volleyball
    /// </summary>
    [Serializable]
    public class Volleyball : Match
    {
        private int T1Score = 0, T2Score = 0, setScore1 = 0, setScore2 = 0;
        private int[,] Table = new int[6,2];
        private Referee[] RefTab = new Referee[3];
        public Volleyball()
        {

        }
        public override void ChooseReferee(Referees r) //wybor sedziow do meczu
        {
            List<Referee> p = new List<Referee>();
            foreach(Referee reff in r.getRefs())
            {
                p.Add(reff);
            }
            Random x = new Random();
            int l = x.Next(p.Count);
            RefTab[0] = p[l];
            p.RemoveAt(l);
            l = x.Next(p.Count);
            RefTab[1] = p[l];
            p.RemoveAt(l);
            l = x.Next(p.Count);
            RefTab[2] = p[l];
        }
        public override void Play() //rozegranie meczu siatkowki
        {
            Random w = new Random();
            int l, s, i = 0;
            while (T1Score < 3 && T2Score < 3)
            {
                l = w.Next(2);
                if (l==0)
                {
                    setScore1 = 25;
                    s = w.Next(15,26);
                    setScore2 = s;
                    while(setScore1-setScore2<2)
                    {
                        setScore1++;
                    }
                    T1Score++;
                }
                else
                {
                    setScore2 = 25;
                    s = w.Next(15,26);
                    setScore1 = s;
                    while(setScore2-setScore1<2)
                    {
                        setScore2++;
                    }
                    T2Score++;
                }
                Table[i , 0] = setScore1;
                Table[i , 1] = setScore2;
                i++;
            }
            if (T1Score == 3)
            {
                T1.addWin();
                T2.addLose();
            }
            else
            {
                T1.addLose();
                T2.addWin();
            }
        }
        public override void ShowScore()
        {
            Console.WriteLine("{0} [{2}] - [{3}] {1}",T1.getTeamName(),T2.getTeamName(),T1Score,T2Score);
            for(int i=0; i<5; i++)
            {
                if(Table[i,0]>10)
                {
                    Console.WriteLine("Set {2}: [{0}] - [{1}]", Table[i, 0], Table[i, 1],i+1);
                }
            }
            
        }
        public override void ShowRef()
        {
            int i = 0;
            foreach(Referee x in RefTab)
            {
                Console.WriteLine("Referee {2}: {0} {1}", x.getName(), x.getSurname(),++i);
            }
        }
        public Referee[] getReferees()
        {
            return RefTab;
        }
    }
}
