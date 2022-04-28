namespace Exceptional;

public static class Program
{

    public static void Main()
    {
        Console.WriteLine("Escriba dividendo entero");
        var numA = Console.ReadLine() ?? "0";
        Console.WriteLine("Escriba divisor entero");
        var numB = Console.ReadLine() ?? "1";
        bool success;
        if(string.Equals(numA, "perro", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidWordException("perro");
        }
        success = int.TryParse(numA, out int numAValue);
        int numBValue = 1;
        success = success && int.TryParse(numB, out numBValue);
        if (!success) {
            Console.WriteLine("Error de formato");
            return;
        }
        
        if (numBValue == 0 || Check(numBValue)) {
            Console.WriteLine("No se puede dividor entre 0");
            return;
        }
        Console.WriteLine((float)numAValue / numBValue);
    }

    static bool Check(int n)
    {
        if (n % 2 != 0) {
            throw new NonPairNumberException();
        }
        return true;
    }

}
