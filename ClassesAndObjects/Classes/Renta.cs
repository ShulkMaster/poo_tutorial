namespace ClassesAndObjects.Classes
{
    public class Renta
    {
        public Usuario UserRenta { get; set; }
        public Consola ConsolaRenta { get; set; }
        
        public Renta(Usuario userRenta, Consola consolaRenta)
        {
            UserRenta = userRenta;
            ConsolaRenta = consolaRenta;
        }
    }
}