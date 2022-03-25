using System;
using System.Collections.Generic;

namespace ClassesAndObjects.Classes
{
    public class Renta
    {
        private readonly List<RentaInfo> _rentas = new List<RentaInfo>();

        public void ShowMenu()
        {
            bool run = true;
            int selectedIndex = 0;
            const int printSize = 5;
            while (run)
            {
                var end = Math.Min(_rentas.Count, selectedIndex + printSize);
                var start = Math.Max(0, end - printSize);
                Console.WriteLine("__________________ Rentas ________________________");
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine($"{_rentas[i].Consola.Serial}: {_rentas[i].Consola.GetName()} {_rentas[i].Start} - {_rentas[i].End}");
                }
                Console.WriteLine("Flechas [Up] [Down] navega, ENTER salir al menu anterior\n\n\n");
                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (selectedIndex - 1 < 0)
                            {
                                Console.Beep();
                                break;
                            }

                            selectedIndex--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (selectedIndex + 1 >= _rentas.Count)
                            {
                                Console.Beep();
                                break;
                            }

                            selectedIndex++;
                            break;
                        }
                    case ConsoleKey.Enter:
                        {
                            run = false;
                            break;
                        }
                }
            }

        }

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

                // la fecha de inicio no debe estar entre el inicio o el fin de otra renta
                if (start >= rentaInfo.Start && start < rentaInfo.End)
                {
                    return true;
                }


                // la fecha de fin no  debe estar entre el inicio o el fin de otra renta
                if (end < rentaInfo.End && end >= rentaInfo.Start)
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