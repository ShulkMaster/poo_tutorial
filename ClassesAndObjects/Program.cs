using System;
using System.Collections.Generic;
using ClassesAndObjects.Classes;

namespace ClassesAndObjects
{
    public class Program
    {
        private bool _isRunning = true;
        private readonly List<Consola> _consolas = new List<Consola>();
        private readonly List<Client> _clients = new List<Client>();
        private readonly Renta rents;

        public Program()
        {
            rents = new Renta();
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        private void Run()
        {
            AddStuff();
            
            while (_isRunning)
            {
                PrintMenu();
                string? input = Console.ReadLine()?.Trim();
                _isRunning = Process(input);
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("__________________ MENU ________________________");
            Console.WriteLine("1) Lista de consolas");
            Console.WriteLine("2) Lista de rentas");
            Console.WriteLine("3) Lista de clientes");
            Console.WriteLine("4) Salir");
            Console.WriteLine("________________________________________________");
        }

        private bool Process(string? input)
        {
            switch (input)
            {
                case "1":
                {
                    ConsoleSubmenu();
                    return true;
                }
                case "2":
                {
                        rents.ShowMenu();
                    return true;
                }
                case "3":
                {
                    UserSubmenu();
                    return true;
                } 
                case "4":
                {
                    Console.WriteLine("Gracias, hasta pronto");
                    return false;
                }
                default:
                {
                    Console.WriteLine("Por favor escriba una opcion valida");
                    return true;
                }
            }
        }

        private void ConsoleSubmenu()
        {
            bool run = true;
            int selectedIndex = 0;
            const int printSize = 5;
            while (run)
            {
                Console.WriteLine("__________________ Consolas ________________________");

                var end = Math.Min(_consolas.Count, selectedIndex + printSize);
                var start = Math.Max(0, end - printSize);
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine($"{_consolas[i].Serial}: {_consolas[i].GetName()} ${_consolas[i].GerHourRate()}");
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
                        if (selectedIndex + 1 >= _consolas.Count)
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
        
        private void UserSubmenu()
        {
            bool run = true;
            int selectedIndex = 0;
            const int printSize = 5;
            while (run)
            {
                Console.WriteLine("__________________ Usuarios ________________________");

                var end = Math.Min(_clients.Count, selectedIndex + printSize);
                var start = Math.Max(0, end - printSize);
                for (int i = start; i < end; i++)
                {
                    Console.WriteLine($"{_clients[i].Dui}: {_clients[i].Name}");
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
                        if (selectedIndex + 1 >= _consolas.Count)
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
        
        private void AddStuff()
        {
            _consolas.Add(new Consola("s001", "Game Cube", 0.75f));
            _consolas.Add(new Consola("s002", "Play Station", 2.55f));
            _consolas.Add(new Consola("s003", "X Box", 1.75f));
            _consolas.Add(new Consola("s004", "Game Cube Morado", 0.95f));
            _consolas.Add(new Consola("s005", "Game Cube", 0.75f));
            _consolas.Add(new Consola("s006", "Play Station", 2.55f));
            _consolas.Add(new Consola("s007", "X Box", 1.75f));
            _consolas.Add(new Consola("s008", "Game Cube Morado", 0.95f));
            _consolas.Add(new Consola("s009", "Game Cube", 0.75f));
            _consolas.Add(new Consola("s010", "Play Station", 2.55f));
            _consolas.Add(new Consola("s011", "X Box", 1.75f));
            _consolas.Add(new Consola("s012", "Game Cube Morado", 0.95f));
            _consolas.Add(new Consola("s013", "Game Cube", 0.75f));
            _consolas.Add(new Consola("s014", "Play Station", 2.55f));
            _consolas.Add(new Consola("s015", "X Box", 1.75f));
            _consolas.Add(new Consola("s016", "Game Cube Morado", 0.95f));
            _clients.Add(new Client{ Dui = "001", Name = "Pedro"});
            _clients.Add(new Client{ Dui = "002", Name = "Juan"});
            _clients.Add(new Client{ Dui = "003", Name = "Miguel"});
            _clients.Add(new Client{ Dui = "004", Name = "Rodolfo"});
            _clients.Add(new Client{ Dui = "005", Name = "Augusto"});
            _clients.Add(new Client{ Dui = "006", Name = "Jacinto"});
            rents.AddRent(
                _clients[0],
                _consolas[0],
                new DateTime(2022, 1, 1),
                new DateTime(2022, 1, 10)
                );
            rents.AddRent(
                _clients[0],
                _consolas[0],
                new DateTime(2022, 1, 10),
                new DateTime(2022, 1, 15)
            );
        }
    }
}
