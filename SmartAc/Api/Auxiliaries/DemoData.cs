using System;
using Api.Models.Domain;

namespace Api.Auxiliaries
{
    public static class DemoData
    {
        internal static void SeedDb(Context context)
        {

            context.Devices.AddRange(Devices);
            context.SaveChanges();
        }

        private static Device[] Devices => new[]
        {
            new Device
            {
                Id = "abcdefghijabcdefghij000001", Secret = "secretto_1", 
                Major = 2, Minor = 1, Patch = 7,
                InitedOn = DateTime.Now.AddDays(-5), UpdatedOn = DateTime.Now.AddDays(-3)
            },
            new Device
            {
                Id = "abcdefghijabcdefghij000002", Secret = "secretto_2",
                Major = 2, Minor = 1, Patch = 7,
                InitedOn = DateTime.Now.AddDays(-4.2), UpdatedOn = DateTime.Now.AddDays(-2.5)
            },
            new Device
            {
                Id = "abcdefghijabcdefghij000003", Secret = "secretto_3",
                Major = 2, Minor = 2, Patch = 2,
                InitedOn = DateTime.Now.AddDays(-4.1), UpdatedOn = DateTime.Now.AddDays(-2)
            },
            new Device
            {
                Id = "abcdefghijabcdefghij000004", Secret = "secretto_4",
                Major = 2, Minor = 3, Patch = 0,
                InitedOn = DateTime.Now.AddDays(-1.9), UpdatedOn = DateTime.Now.AddDays(-1.9)
            },
            new Device { Id = "abcdefghijabcdefghij000005", Secret = "secretto_5" },
            new Device { Id = "abcdefghijabcdefghij000006", Secret = "secretto_6" },
            new Device { Id = "abcdefghijabcdefghij000007", Secret = "secretto_7" },
            new Device { Id = "abcdefghijabcdefghij000008", Secret = "secretto_8" },
            new Device { Id = "abcdefghijabcdefghij000009", Secret = "secretto_9" },
            new Device { Id = "abcdefghijabcdefghij00000a", Secret = "secretto_A" },
            new Device { Id = "abcdefghijabcdefghij00000b", Secret = "secretto_B" },
            new Device { Id = "abcdefghijabcdefghij00000c", Secret = "secretto_C" },
            new Device { Id = "abcdefghijabcdefghij00000d", Secret = "secretto_D" },
            new Device { Id = "abcdefghijabcdefghij00000e", Secret = "secretto_E" },
            new Device { Id = "abcdefghijabcdefghij00000f", Secret = "secretto_F" }
        };
    }
}