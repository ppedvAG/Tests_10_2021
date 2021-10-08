// See https://aka.ms/new-console-template for more information

using ppedv.GMV.Logic;
using ppedv.GMV.Model;

Console.WriteLine("Hello, World!");

var core = new Core(new ppedv.GMV.Data.EfCore.EfRepository());

foreach (var mess in core.Repository.GetAll<Messung>())
{
    Console.WriteLine($"{mess.Datum} {mess.Gerät.Hersteller} {mess.Gerät.Modell}");
    foreach (var mw in mess.Messwerte)
    {
        Console.WriteLine($"\t{mw.MesswertBeschreibung}: {mw.Wert} {mw.Einheit}");
    }
}

Console.WriteLine("Ende");
Console.ReadLine();

