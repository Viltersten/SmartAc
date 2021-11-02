using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Models.Domain;
using Api.Models.Enums;
using Api.Models.Exceptions;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Test
{
    public class DeviceServiceTest
    {
        private Context Context { get; }
        private IDeviceService Sut { get; }

        public DeviceServiceTest()
        {
            AlertConfig alertConfig = new()
            {
                { "Temperature", new AlertConfig.AlertDetail { Max = 100, Min = -30, Message = "oops hot" } },
                { "Humidity", new AlertConfig.AlertDetail { Max = 100, Min = 0, Message = "oops moisty" } },
                { "Carbon", new AlertConfig.AlertDetail { Max = 9, Min = 0, Message = "oops stuffy" } },
                { "Health", new AlertConfig.AlertDetail { Message = "oops not okay" } }
            };
            SecurityConfig securityConfig = new() { Password = "password", Secret = "12345678901234567890abcd" };

            Context = new(new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("test-" + new Guid()).Options);
            Context.Database.EnsureDeleted();
            DemoData.SeedDb(Context);

            ISecurityService service = new SecurityService(Options.Create(securityConfig));

            // todo Mock token generation and nullify security service instance.
            Sut = new DeviceService(Options.Create(alertConfig), Context, service);
        }

        [Fact]
        public async Task BeDev01_VirginRegister()
        {
            Device device = new()
            {
                Id = "abcdefghijabcdefghij000007",
                Secret = "secretto_7",
                Major = 1,
                Minor = 2,
                Patch = 3
            };

            string token = await Sut.Register(device);
            Device actual = Context.Devices.Single(a => a.Id == device.Id);

            Assert.False(string.IsNullOrEmpty(token));
            Assert.Equal(2, token.Count(a => a == '.'));
            Assert.StartsWith("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", token);

            Assert.Equal(device.Major, actual.Major);
            Assert.Equal(device.Minor, actual.Minor);
            Assert.Equal(device.Patch, actual.Patch);
            Assert.NotNull(actual.InitedOn);
            Assert.NotNull(actual.UpdatedOn);
        }

        [Fact]
        public async Task BeDev01_ResetRegister()
        {
            Device device = new()
            {
                Id = "abcdefghijabcdefghij000001",
                Secret = "secretto_1",
                Major = 13,
                Minor = 37,
                Patch = 0
            };

            string token = await Sut.Register(device);
            Device actual = Context.Devices.Single(a => a.Id == device.Id);

            Assert.False(string.IsNullOrEmpty(token));
            Assert.Equal(2, token.Count(a => a == '.'));
            Assert.StartsWith("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", token);

            Assert.Equal(device.Major, actual.Major);
            Assert.Equal(device.Minor, actual.Minor);
            Assert.Equal(device.Patch, actual.Patch);
            Assert.NotNull(actual.UpdatedOn);
            Assert.NotEqual(actual.InitedOn, actual.UpdatedOn);
        }

        [Fact]
        public async Task BeDev01_CredentialsFailure()
        {
            Device device = new()
            {
                Id = "abcdefghijabcdefghij000001",
                Secret = "not_secretto_1",
                Major = 13,
                Minor = 37,
                Patch = 0
            };

            InvalidCredentialsException actual = await Assert.ThrowsAsync<InvalidCredentialsException>(
                async () => await Sut.Register(device));
            Assert.Equal(device.Id + "/" + device.Secret, actual.Message);
        }

        [Fact]
        public async Task BeDev02_ReportValues()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure1 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = 13.37,
                Humidity = 13.57,
                Carbon = 4.6,
                Health = HealthStatus.OK
            };
            Measure measure2 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(2),
                Temperature = 12.98,
                Humidity = 12.34,
                Carbon = 7.2,
                Health = HealthStatus.OK
            };
            Measure measure3 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(3),
                Temperature = 12.55,
                Humidity = 12.93,
                Carbon = 4.1,
                Health = HealthStatus.OK
            };

            Measure[] payload = { measure1, measure2, measure3 };

            bool result = await Sut.Report(payload);
            List<Measure> actual = Context.Measures.ToList();

            Assert.True(result);
            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public async Task BeDev03_DetectAlarms()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure1 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = -31,
                Humidity = 13.57,
                Carbon = 4.6,
                Health = HealthStatus.OK
            };
            Measure measure2 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(2),
                Temperature = 12.98,
                Humidity = 12.34,
                Carbon = 9.1,
                Health = HealthStatus.OK
            };
            Measure measure3 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(3),
                Temperature = 12.55,
                Humidity = -3,
                Carbon = 4.1,
                Health = HealthStatus.Service
            };

            Measure[] payload = { measure1, measure2, measure3 };

            bool result = await Sut.Report(payload);
            List<Alert> actual = Context.Alerts.ToList();

            Assert.True(result);
            Assert.Equal(4, actual.Count);
            Assert.Contains(actual, a => a.Type == AlertType.Temperature);
            Assert.Contains(actual, a => a.Type == AlertType.Humidity);
            Assert.Contains(actual, a => a.Type == AlertType.Carbon);
            Assert.Contains(actual, a => a.Type == AlertType.Health);
        }

        [Fact]
        public async Task BeDev03_DescribeTemperatureAlarm()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = -31,
                Humidity = 13.57,
                Carbon = 4.6,
                Health = HealthStatus.OK
            };

            Measure[] payload = { measure };

            bool result = await Sut.Report(payload);
            Alert actual = Context.Alerts.SingleOrDefault();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Equal(measure.DeviceId, actual.DeviceId);
            Assert.Equal(measure.RecordedOn, actual.RecordedOn);
            Assert.Equal("oops hot", actual.Message);
            Assert.Equal(AlertType.Temperature, actual.Type);
            Assert.NotEqual(actual.RecordedOn, actual.RecognizedOn);
            Assert.Null(actual.ResolvedOn);
            Assert.Equal(ResolutionStatus.New, actual.Resolution);
            Assert.Equal(ViewStatus.New, actual.View);
        }

        [Fact]
        public async Task BeDev03_DescribeHumidityAlarm()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = 16,
                Humidity = 101,
                Carbon = 4.6,
                Health = HealthStatus.OK
            };

            Measure[] payload = { measure };

            bool result = await Sut.Report(payload);
            Alert actual = Context.Alerts.SingleOrDefault();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Equal("oops moisty", actual.Message);
            Assert.Equal(AlertType.Humidity, actual.Type);
        }

        [Fact]
        public async Task BeDev03_DescribeCarbonAlarm()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = 16,
                Humidity = 80,
                Carbon = 14,
                Health = HealthStatus.OK
            };

            Measure[] payload = { measure };

            bool result = await Sut.Report(payload);
            Alert actual = Context.Alerts.SingleOrDefault();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Equal("oops stuffy", actual.Message);
            Assert.Equal(AlertType.Carbon, actual.Type);
        }

        [Fact]
        public async Task BeDev03_DescribeHealthAlarm()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Temperature = 16,
                Humidity = 80,
                Carbon = 2,
                Health = HealthStatus.Filter
            };

            Measure[] payload = { measure };

            bool result = await Sut.Report(payload);
            Alert actual = Context.Alerts.SingleOrDefault();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Equal("oops not okay", actual.Message);
            Assert.Equal(AlertType.Health, actual.Type);
        }

        [Fact]
        public async Task BeDev04_MergedAlarmsOnlyUpdateOccasion()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure1 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Health = HealthStatus.Filter
            };
            Measure measure2 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(2),
                Health = HealthStatus.Filter
            };

            Measure[] payload = { measure1, measure2 };

            bool result = await Sut.Report(payload);
            List<Alert> actual = Context.Alerts.ToList();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Single(actual);
            Assert.Equal(measure2.RecordedOn, actual.Single().RecordedOn);
            Assert.NotEqual(measure1.RecordedOn, actual.Single().RecordedOn);
        }

        [Fact]
        public async Task BeDev04_DifferentAlarmsDoNotMerge()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure1 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Health = HealthStatus.Filter
            };
            Measure measure2 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(2),
                Health = HealthStatus.Filter
            };
            Measure measure3 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(3),
                Carbon = 13,
                Health = HealthStatus.Filter
            };

            Measure[] payload = { measure1, measure2, measure3 };

            bool result = await Sut.Report(payload);
            List<Alert> actual = Context.Alerts.ToList();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public async Task BeDev05_AlarmSelfResolves()
        {
            DateTime now = DateTime.Today.AddMinutes(100);
            Measure measure1 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(1),
                Carbon = 17
            };
            Measure measure2 = new()
            {
                DeviceId = "abcdefghijabcdefghij000001",
                RecordedOn = now.AddMinutes(2),
                Carbon = 2
            };

            Measure[] payload = { measure1, measure2 };

            bool result = await Sut.Report(payload);
            List<Alert> actual = Context.Alerts.ToList();

            Assert.True(result);
            Assert.NotNull(actual);
            Assert.Single(actual);
            Assert.Equal(ResolutionStatus.Resolved, actual.Single().Resolution);
            Assert.Equal(measure2.RecordedOn, actual.Single().ResolvedOn);
            Assert.Equal(ViewStatus.New, actual.Single().View);
        }
    }
}
