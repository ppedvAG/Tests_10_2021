using FluentAssertions;
using ppedv.GMV.Model;
using System;
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
        public void GetGer�tWithHighestMesswert__()
        {
            var core = new Core(new TestRepository());
            var result = core.GetGer�tWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");
        }
    }
}