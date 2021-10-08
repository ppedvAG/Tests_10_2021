using ppedv.GMV.Data.EfCore;
using ppedv.GMV.Model;

namespace ppedv.GMV.Logic
{
    public class Core
    {
        public IEnumerable<Messwert> GetMesswerteOfGerät(Gerät gerät)
        {
            if (gerät == null)
                throw new ArgumentNullException(nameof(gerät));

            return gerät.Messungen.SelectMany(X => X.Messwerte)
                                  .OrderBy(x => x.Wert);
        }

        public Gerät GetGerätWithHighestMesswert()
        {
            var con = new EfContext("Server=(localdb)\\mssqllocaldb;Database=GMV;Trusted_Connection=true");
            return con.Messwerte.OrderBy(x => x.Wert)
                                .FirstOrDefault()
                                .Messung.Gerät;
        }

    }
}