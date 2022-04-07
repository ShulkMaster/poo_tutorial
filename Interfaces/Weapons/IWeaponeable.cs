namespace Interfaces.Weapons
{
    public interface IWeaponeable
    {
        // Interface methods are public and abstract by default
        string Fire();

        string Fire2()
        {
            return Fire();
        }
    }
}