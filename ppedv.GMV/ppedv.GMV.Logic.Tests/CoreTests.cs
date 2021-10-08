using FluentAssertions;
using ppedv.GMV.Model;
using System;
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
        public void GetGerätWithHighestMesswert__()
        {
            var core = new Core(new TestRepository());
            var result = core.GetGerätWithHighestMesswert();

            result.Should().NotBeNull();
            result.Hersteller.Should().Be("g2");
        }
    }
}