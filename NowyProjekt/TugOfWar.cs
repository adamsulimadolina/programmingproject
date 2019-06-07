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
    /// klasa dla sportu TugOfWar
    /// </summary>
    [Serializable]
    public class TugOfWar : Match
    {
        private Referee r;
        private int T1Score = 0, T2Score = 0;
        public override void ChooseReferee(Referees o)
        {
            List<Referee> p = o.getRefs();
            Random x = new Random();
            int l = x.Next(p.Count);
            r = p[l];
        }
        public override void ShowRef()
        {
            Console.WriteLine("Referee: {0} {1}", r.getName(), r.getSurname());
        }
        public override void Play()
        {
            Random w = new Random();
            int l = w.Next(2);
            if (l == 0)
            {
                T1Score++;
                T1.addWin();
                T2.addLose();
            }
            else
            {
                T2Score++;
                T2.addWin();
                T1.addLose();
            }
        }
        public override void ShowScore()
        {
            Console.WriteLine("{0} [{1}] - [{2}] {3}", T1.getTeamName(), T1Score, T2Score, T2.getTeamName());
        }
    }
}
