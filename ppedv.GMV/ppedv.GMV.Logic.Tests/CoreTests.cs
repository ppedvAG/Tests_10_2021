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
        public void GetMesswerteOfGer�t_ger�t_with_2_messungen_with_3_Werten_results_6()
        {
            var g = new Ger�t();
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
            var result = core.GetMesswerteOfGer�t(g);

            result.Should().HaveCount(6);
        }

        [Fact]
        public void GetMesswerteOfGer�t_ger�t_NULL_should_thow_ArgumentNullException()
        {
            var core = new Core(null);
            var act = () => core.GetMesswerteOfGer�t(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetGer�tWithHighestMesswert()
        {
            var core = new Core(new TestRepository());
            var result = core.GetGer�tWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");
        }

        [Fact]
        public void GetGer�tWithHighestMesswert_moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messwert>()).Returns(() =>
            {
                var g1 = new Ger�t() { Hersteller = "g1" };
                var m1 = new Messung() { Ger�t = g1 };
                m1.Messwerte.Add(new Messwert() { Wert = 15, Messung = m1 });
                m1.Messwerte.Add(new Messwert() { Wert = 5, Messung = m1 });
                g1.Messungen.Add(m1);

                var g2 = new Ger�t() { Hersteller = "g2" };
                var m2 = new Messung() { Ger�t = g2 };
                m2.Messwerte.Add(new Messwert() { Wert = 12, Messung = m2 });
                m2.Messwerte.Add(new Messwert() { Wert = 19, Messung = m2 });
                g2.Messungen.Add(m2);

                return m1.Messwerte.Union(m2.Messwerte);
            });
            var core = new Core(mock.Object);

            var result = core.GetGer�tWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");

            mock.Verify(x => x.GetAll<Messwert>(), Times.Once);
            mock.Verify(x => x.Save(), Times.Never);

        }

        [Fact]
        public void GetGer�tWithHighestMesswert_2_ger�te_with_same_value_return_orderd_by_date()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetAll<Messwert>()).Returns(() =>
            {
                var m1 = new Messwert() { Wert = 12, Messung = new Messung() { Datum = DateTime.Now.AddMinutes(15), Ger�t = new Ger�t() { Hersteller = "g1" } } };
                var m2 = new Messwert() { Wert = 12, Messung = new Messung() { Datum = DateTime.Now.AddMinutes(5), Ger�t = new Ger�t() { Hersteller = "g2" } } };

                return new[] { m1, m2 };
            });
            var core = new Core(mock.Object);

            var result = core.GetGer�tWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");

            mock.Verify(x => x.GetAll<Messwert>(), Times.Once);
            mock.Verify(x => x.Save(), Times.Never);

        }
    }
}