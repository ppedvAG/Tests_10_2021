using FluentAssertions;
using Moq;
using ppedv.GMV.Logic;
using ppedv.GMV.Model;
using ppedv.GMV.Model.Contracts;
using ppedv.GMV.UI.Wpf.ViewModels;
using System.Collections.Generic;
using Xunit;

namespace ppedv.GMV.UI.Wpf.Tests
{
    public class MesswerteViewModelsTests
    {
        [Fact]
        public void Über1000_bool_is_updated()
        {
            var mw = new Messwert() { Wert = 12 };
            var vm = new MesswerteViewModel();

            vm.SelectedMesswert = mw;
            vm.Über1000.Should().BeFalse();

            mw.Wert = 5000;
            vm.SelectedMesswert = mw;
            vm.Über1000.Should().BeTrue();
        }

        [Fact]
        public void MesswerteListe_filled_by_moq()
        {
            var repoMock = new Mock<IRepository>();
            
            var mw1 = new Messwert() { Wert = 12 };
            var mw2 = new Messwert() { Wert = 12 };

            repoMock.Setup(x => x.GetAll<Messwert>()).Returns(() => new[] { mw1, mw2 }); ;
            var core = new Core(repoMock.Object);

            var vm = new MesswerteViewModel(core);

            vm.MesswerteListe.Should().HaveCount(2);
        }
    }
}