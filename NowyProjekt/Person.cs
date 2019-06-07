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
    /// Podstawowa klasa osob
    /// </summary>
    [Serializable]
    public abstract class Person
    {
        private String Name, Surname;
        public Person() { }
        public Person(string Name, string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
        }
        public void setName(String a)
        {
            Name = a;
        }
        public void setSurname(String a)
        {
            Surname = a;
        }
        public String getName()
        {
            return Name;
        }
        public String getSurname()
        {
            return Surname;
        }
        /// <summary>
        /// sprawdza poprawnosc imienia
        /// </summary>
        /// <param name="x">nawisko do sprawdzenia</param>
        /// <returns></returns>
        public bool CheckName(string x) 
        {
            for(int i=0;i<x.Length;i++)
            {
                if (x.Length>3)
                {
                    if (!char.IsLetter(x, i)) return false;
                }
            }
            return true;
        }
    }
    /// <summary>
    /// klasa sedziow
    /// </summary>
    [Serializable]
    public class Referee : Person
    {
        public Referee() { }
        public Referee(string Name, string Surname) : base(Name, Surname) { }
    }
    /// <summary>
    /// klasa zawodnikow
    /// </summary>
    [Serializable]
    public class Player : Person
    {
        public Player() { }
        public Player(string Name, string Surname) : base(Name, Surname) { }
    }
}
