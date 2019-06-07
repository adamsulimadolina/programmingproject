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
    public class Referees
    {
        private List<Referee> RefList = new List<Referee>();
        public void AddRef(Referee x) //dodaje sedziego
        {
            RefList.Add(x);
        }
        public void RemoveRef(Referee x) //usuwa sedziego
        {
            int i = 0; bool checkmark = false;
            foreach (Referee a in RefList)
            {
                if (x.getName() == a.getName() && x.getSurname() == a.getSurname())
                {
                    RefList.RemoveAt(i);
                    Console.Clear();
                    Console.WriteLine("Usuniecie sedziego powiodlo sie.");
                    Console.ReadKey();
                    checkmark = true;
                    break;
                }
                i++;
            }
            if (checkmark == false)
            {
                Console.Clear();
                Console.WriteLine("Usuniecie sedziego nie powiodlo sie.");
                Console.ReadKey();
            }
        }
        public List<Referee> getRefs() //zwraca sedziow
        {
            return RefList;
        }
        public void ShowReferees() //pokazuje sedziow
        {
            foreach (Referee x in RefList)
            {
                Console.WriteLine($"Referee: {x.getName()} {x.getSurname()}");
            }
        }

        //referees
        public void SerializeReferees()
        {
            FileStream file;
            file = new FileStream("Referees.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, RefList);
            file.Close();
        }
        public void DeserializeReferees()
        {
            FileStream file;
            file = new FileStream("Referees.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            RefList = (List<Referee>)formatter.Deserialize(file);
            file.Close();
        }
    }

}
