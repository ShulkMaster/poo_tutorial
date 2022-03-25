using System;
using System.Collections.Generic;

namespace ClassesAndObjects.Classes
{
    public class Renta
    {
        private readonly List<RentaInfo> _rentas = new List<RentaInfo>();

        public void AddRent(Client client, Consola console, DateTime start, DateTime end)
        {
            var isInRent = 
            _rentas.Exists(rentaInfo =>
            {
                // debe ser misma consola
                if (console.Serial != rentaInfo.Consola.Serial)
                {
                    return false;
                }
                
                if (start >= rentaInfo.Start && start < rentaInfo.End)
                {
                    return true;
                }
                
                if(end < rentaInfo.End && end >= rentaInfo.Start)
                {
                    return true;
                }

                return false;
            });

            if (isInRent)
            {
                Console.WriteLine($"La fecha ya esta reservada con la consola {console.Serial}");
                return;
            }
           
            _rentas.Add(new RentaInfo(start, end, client, console));
            Console.WriteLine($"La consola {console.Serial} fue reservada");
        }
    }

    public class RentaInfo
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Client Client { get; set; }
        public Consola Consola { get; set; }

        public RentaInfo(DateTime start, DateTime end, Client client, Consola consola)
        {
            Start = start;
            End = end;
            Client = client;
            Consola = consola;
        }
    }
    
}