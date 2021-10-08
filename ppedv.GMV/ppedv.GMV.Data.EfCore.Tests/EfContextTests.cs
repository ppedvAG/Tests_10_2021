using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.GMV.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ppedv.GMV.Data.EfCore.Tests
{
    public class EfContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=GMV;Trusted_Connection=true";

        [Fact]
        public void Can_create_db()
        {
            var con = new EfContext(conString);
            con.Database.EnsureDeleted();

            Assert.True(con.Database.EnsureCreated());
        }

        [Fact]
        public void Can_CRUD_Messwert()
        {
            var mw = new Messwert() { MesswertBeschreibung = $"Dinge_{Guid.NewGuid()}" };
            var newMesswertBeschreibung = $"Zeug_{Guid.NewGuid()}";

            using (var con = new EfContext(conString))
            {
                //CREATE
                con.Messwerte.Add(mw);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                //READ
                var loaded = con.Messwerte.Find(mw.Id);

                //Assert.NotNull(loaded);
                loaded.Should().NotBeNull();
                //Assert.Equal(mw.MesswertBeschreibung, loaded.MesswertBeschreibung);
                loaded.MesswertBeschreibung.Should().Be(mw.MesswertBeschreibung);

                //UPDATE
                loaded.MesswertBeschreibung = newMesswertBeschreibung;
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                //Update test
                var loaded = con.Messwerte.Find(mw.Id);
                Assert.Equal(newMesswertBeschreibung, loaded.MesswertBeschreibung);

                //DELETE
                con.Messwerte.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                //Delete test
                var loaded = con.Messwerte.Find(mw.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_create_and_read_Messung_with_all_references()
        {
            var fix = new Fixture();
            //https://pastebin.com/nnZdkB5S    
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id)));
            fix.Behaviors.Add(new OmitOnRecursionBehavior());

            var mess = fix.Create<Messung>();

            using (var con = new EfContext(conString))
            {
                con.Messungen.Add(mess);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Messungen.Find(mess.Id);
                loaded.Should().BeEquivalentTo(mess, x => x.IgnoringCyclicReferences());
            }
        }


        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;
            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }
            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();
                return new NoSpecimen();
            }
        }

    }
}