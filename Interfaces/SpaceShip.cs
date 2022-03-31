using Interfaces.Engine;
using Interfaces.Weapons;
using System;

namespace Interfaces
{
    public class SpaceShip
    {
        private readonly Engine.Engine _engine = new SuperProtonEngine();
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
                }

                Console.WriteLine(_weapon?.Fire());
                Console.WriteLine(Model);
                Console.WriteLine(_engine.Thrust());
            }
        }
    }
}