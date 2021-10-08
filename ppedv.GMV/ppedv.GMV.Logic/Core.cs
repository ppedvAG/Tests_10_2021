using ppedv.GMV.Model;
using ppedv.GMV.Model.Contracts;

namespace ppedv.GMV.Logic
{
    public class Core
    {

        public IRepository Repository { get; }

        public Core(IRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<Messwert> GetMesswerteOfGerät(Gerät gerät)
        {
            if (gerät == null)
                throw new ArgumentNullException(nameof(gerät));

            return gerät.Messungen.SelectMany(X => X.Messwerte)
                                  .OrderBy(x => x.Wert);
        }

        public Gerät GetGerätWithHighestMesswert()
        {
            return Repository.GetAll<Messwert>()
                             .OrderByDescending(x => x.Wert)
                             .FirstOrDefault()
                             .Messung
                             .Gerät;
        }

    }
}