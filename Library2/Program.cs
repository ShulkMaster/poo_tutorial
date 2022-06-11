using Library2.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library2;

public static class Program
{
    // haciendo main async Task para poder usar await
    public static async Task Main()
    {
        /*
         * Configurando la factory de Loggers usando el metodo de create en lugar del
         * constructor directamente
         */
        var myLogger = LoggerFactory.Create(opt =>
        {
            /*
             * Agregando System.Cosole como un proveedor de loggin en la app, se pueden tener varios
             * Este es el unico proveedor que no persiste los logs de ninguna manera ya que solo quedan en la consola
             * mientras el programa se ejecuta
             * Este metodo solo esta disponible si se instalo en paquete de extensiones Microsoft.Extensions.Logging.Console
             */ 
            opt.AddConsole();
            
            /*
             * Agregando proveedor de loggin en Event Logs esto solo funciona en Windows,
             * se puede abrir el log d eventos con Windows + R y escribiendo eventvwr eso lo abrira
             * Dentro del registro se crea un log llamado Library Log que tiene este programa como origen
             * asi que la columna source tiene el nombre de Library App
             */
            opt.AddEventLog(evt =>
            {
                evt.LogName = "Library Log";
                evt.SourceName = "Library App";
            });
        });
        
        /*
         * Usando mi clase Builder custom para cargar un archivo JSON de configuracion desde el disco de la computadora
         */
        var builder = new LibraryContextBuilder("appsettings.json");
        await builder.LoadFromFile();
        
        /*
         * Configurando opciones de EF
         */
        builder.UseSqServer()
            .UseLoggerFactory(myLogger) // Para que logguee los Queries y errores usando los proveedores configurados antes
            .EnableDetailedErrors() // Activar los errores detallados
            .EnableSensitiveDataLogging(); // Activar el loggin completo de informacion
        
        
        // Crar una instancia del contexto de DB con las opciones de arriba
        var options = builder.Options;
        var db = new LibraryContext(options);
        
        /*
         * Consulta equivale a:
         * Select * limit 3
         * from User as U
         * inner join Role as R
         * on U.RoleId = R.Id
         * order by U.Name
         * OFFSET 1 ROWS FETCH NEXT 3 ROWS ONLY
        */
        var list = await db.Users.Include(u => u.Role)
            .OrderByDescending(u => u.Name)
            .Skip(1)
            .Take(3)
            .ToListAsync();
        
        foreach (var user in list)
        {
            Console.WriteLine($"{user.Id} {user.Name} {user.Role.Name}");
        }

        Console.WriteLine("------------------------------------------");
    }
}