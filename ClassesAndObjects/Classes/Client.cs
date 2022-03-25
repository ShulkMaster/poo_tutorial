using System;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace ClassesAndObjects.Classes
{
    public class Client
    {
        public string Dui { get; set; }

        public string Name { get; set; }

        public Client() { }
        
        public Client(string dui)
        {
            Dui = dui;
        }
        
        public Client(string dui, string name)
        {
            Dui = dui;
            Name = name;
        }
        
        public Client(int dui, string name)
        {
            Dui = dui.ToString();
            Name = name;
        }

        public int metodo1()
        {
            return 0;
        }
        
        public int metodo1(int numero)
        {
            return numero;
        }
    }
}