using System;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Models.Domain;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Test
{
    public class UserServiceTest
    {
        private Context Context { get; }
        private IUserService Sut { get; }

        public UserServiceTest()
        {
            UserConfig userConfig = new() { };

            Context = new(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("test-" + new Guid()).Options);
            Context.Database.EnsureDeleted();
            DemoData.SeedDb(Context);

            Sut = new UserService(Options.Create(userConfig), Context);
        }

        [Fact]
        public void BeAdm03_UnrectrictedTimeRange()
        {
            Device[] actual = Sut.GetDevices(null, null, 0, 2);

            Assert.Equal(2, actual.Length);
            Assert.Equal("abcdefghijabcdefghij000004", actual[0].Id);
            Assert.Equal("abcdefghijabcdefghij000003", actual[1].Id);
            Assert.True(actual[0].UpdatedOn > actual[1].UpdatedOn);
        }

        [Fact]
        public void BeAdm03_WithinTimeRange()
        {
            DateTime startOn = DateTime.Today.AddDays(-2.9);
            DateTime endOn = DateTime.Today.AddDays(-2.0);

            Device[] actual = Sut.GetDevices(startOn, endOn, 0, 3);

            Assert.Equal(2, actual.Length);
            Assert.Equal("abcdefghijabcdefghij000003", actual[0].Id);
            Assert.Equal("abcdefghijabcdefghij000002", actual[1].Id);
        }
    }
}
