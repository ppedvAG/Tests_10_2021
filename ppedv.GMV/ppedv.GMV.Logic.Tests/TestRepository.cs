using ppedv.GMV.Model;
using ppedv.GMV.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.GMV.Logic.Tests
{
    class TestRepository : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Messwert))
            {
                var g1 = new Gerät() { Hersteller="g1"};
                var m1 = new Messung() { Gerät = g1 };
                m1.Messwerte.Add(new Messwert() { Wert = 15, Messung = m1 });
                m1.Messwerte.Add(new Messwert() { Wert = 5, Messung = m1 });
                g1.Messungen.Add(m1);

                var g2 = new Gerät() { Hersteller = "g2" };
                var m2 = new Messung() { Gerät = g2 };
                m2.Messwerte.Add(new Messwert() { Wert = 12, Messung = m2 });
                m2.Messwerte.Add(new Messwert() { Wert = 19, Messung = m2 });
                g2.Messungen.Add(m2);


                return m1.Messwerte.Union(m2.Messwerte).Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T GetbyId<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}