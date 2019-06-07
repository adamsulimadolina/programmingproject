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
    class Program
    {
        static void Main(string[] args)
        {
            Controler.Deserialization();
            Menu.Start();
            Controler.Serialization();

        }
    }
}
