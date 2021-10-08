using FluentAssertions;
using Moq;
using ppedv.GMV.Model;
using ppedv.GMV.Model.Contracts;
using System;
using System.Linq;
using Xunit;

namespace ppedv.GMV.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void GetMesswerteOfGerät_gerät_with_2_messungen_with_3_Werten_results_6()
        {
            var g = new Gerät();
            var m1 = new Messung();
            m1.Messwerte.Add(new Messwert());
            m1.Messwerte.Add(new Messwert());
            m1.Messwerte.Add(new Messwert());
            var m2 = new Messung();
            m2.Messwerte.Add(new Messwert());
            m2.Messwerte.Add(new Messwert());
            m2.Messwerte.Add(new Messwert());
            g.Messungen.Add(m1);
            g.Messungen.Add(m2);

            var core = new Core(null);
            var result = core.GetMesswerteOfGerät(g);

            result.Should().HaveCount(6);
        }

        [Fact]
        public void GetMesswerteOfGerät_gerät_NULL_should_thow_ArgumentNullException()
        {
            var core = new Core(null);
            var act = () => core.GetMesswerteOfGerät(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetGerätWithHighestMesswert()
        {
            var core = new Core(new TestRepository());
            var result = core.GetGerätWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");
        }

        [Fact]
        public void GetGerätWithHighestMesswert_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messwert>()).Returns(() =>
            {
                var g1 = new Gerät() { Hersteller = "g1" };
                var m1 = new Messung() { Gerät = g1 };
                m1.Messwerte.Add(new Messwert() { Wert = 15, Messung = m1 });
                m1.Messwerte.Add(new Messwert() { Wert = 5, Messung = m1 });
                g1.Messungen.Add(m1);

                var g2 = new Gerät() { Hersteller = "g2" };
                var m2 = new Messung() { Gerät = g2 };
                m2.Messwerte.Add(new Messwert() { Wert = 12, Messung = m2 });
                m2.Messwerte.Add(new Messwert() { Wert = 19, Messung = m2 });
                g2.Messungen.Add(m2);

                return m1.Messwerte.Union(m2.Messwerte);
            });
            var core = new Core(mock.Object);

            var result = core.GetGerätWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");

            mock.Verify(x => x.GetAll<Messwert>(), Times.Once);
            mock.Verify(x => x.Save(), Times.Never);

        }

        [Fact]
        public void GetGerätWithHighestMesswert_2_geräte_with_same_value_return_orderd_by_date()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messwert>()).Returns(() =>
            {
                var m1 = new Messwert() { Wert = 12, Messung = new Messung() { Datum = DateTime.Now.AddMinutes(15), Gerät = new Gerät() { Hersteller = "g1" } } };
                var m2 = new Messwert() { Wert = 12, Messung = new Messung() { Datum = DateTime.Now.AddMinutes(5), Gerät = new Gerät() { Hersteller = "g2" } } };

                return new[] { m1, m2 };
            });
            var core = new Core(mock.Object);

            var result = core.GetGerätWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");

            mock.Verify(x => x.GetAll<Messwert>(), Times.Once);
            mock.Verify(x => x.Save(), Times.Never);

        }
    }
}