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
    public abstract class Match
    {
        protected String Winner;
        protected Team T1, T2;
        public Match()
        {

        }
        public void setTeam1(Team x)
        {
            T1 = x;
        }
        public void setTeam2(Team x)
        {
            T2 = x;
        }
        public void ShowTeams()
        {
            Console.WriteLine($"{T1.getTeamName()} : {T2.getTeamName()}");
        }
        public virtual void ShowScore() { }
        public virtual void ChooseReferee(Referees r) { }
        public virtual void Play() { }
        public virtual void ShowRef() { }
    }
}
