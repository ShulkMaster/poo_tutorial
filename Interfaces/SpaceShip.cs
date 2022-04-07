using Interfaces.Engine;
using Interfaces.Weapons;
using System;

namespace Interfaces
{
    public class SpaceShip
    {
        private Engine.Engine _engine = new AntimatterEngine();
        private IWeaponeable? _weapon;

        private const string Model = @"
              /\
             /  \
            /    \
           | (  ) |
           | (  ) |              
   /\      |      |      /\
  /  \    /|   .  |\    /  \
 |----| /  |   .  |  \ |----|
 |    /    |   .  |    \    |
 |  /      |   .  |      \  |
 |/        |   .  |        \|
 /   NASA  |   .  |  NASA   \
/  \-----/  \    /  \-----/  \";

        public void Run()
        {
            while (true)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                    {
                        return;
                    }
                    case ConsoleKey.D1:
                    {
                        if (!(_weapon is IceWeapon))
                        {
                            _weapon = new IceWeapon();
                        }

                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        if (!(_weapon is ProtonWeapon))
                        {
                            _weapon = new ProtonWeapon();
                        }

                        break;
                    }
                    case ConsoleKey.D3:
                    {
                        if(!(_weapon is PlasmaWeapon))
                        { 
                            _weapon = new PlasmaWeapon();
                        }
                        break;
                    }
                    case ConsoleKey.Spacebar:
                    {
                        if(!(_engine is ElectonEngine))
                        {
                            _engine = new ElectonEngine();
                        }
                        break;
                    } 
                    
                }

                Console.WriteLine(_weapon?.Fire());
                Console.WriteLine(Model);
                Console.WriteLine(_engine.Thrust());
            }
        }
    }
}