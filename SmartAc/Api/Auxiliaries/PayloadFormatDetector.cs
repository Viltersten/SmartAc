using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Models.Domain;
using Api.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Api.Auxiliaries
{
    internal class PayloadFormatDetector
    {
        public RequestDelegate Next { get; }

        public PayloadFormatDetector(RequestDelegate next) => Next = next;

        public async Task Invoke(HttpContext http, Context context)
        {
            string payload = await GetPayloadAsync(http.Request);
            bool valid = ValidEntries(payload);
            if (!valid)
                await SaveJunk(context, payload);

            await Next(http);
        }

        private async Task<string> GetPayloadAsync(HttpRequest request)
        {
            request.EnableBuffering();

            int size = Convert.ToInt32(request.ContentLength);
            byte[] buffer = new byte[size];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            string result = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            return result;
        }

        private bool ValidEntries(string payload)
        {
            string temperature = payload.Field("temperature");
            string humidity = payload.Field("humidity");
            string carbon = payload.Field("carbon");
            string health = payload.Field("health");

            bool ok = true;
            double _;
            int temp;
            ok &= double.TryParse(temperature, out _);
            ok &= double.TryParse(humidity, out _);
            ok &= double.TryParse(carbon, out _);
            ok &= int.TryParse(health, out temp);
            ok &= Enum.IsDefined(typeof(HealthStatus), temp);

            return ok;
        }

        private async Task SaveJunk(Context context, string payload = "")
        {
            Junk junk = new Junk
            {
                DeviceId = payload.Field("deviceId"),
                CreatedOn = DateTime.UtcNow,
                Payload = payload[..Math.Min(payload.Length, 499)]
            };

            await context.AddAsync(junk);

            int count = await context.Junks
                .CountAsync(a => a.DeviceId == junk.DeviceId);
            if (count > 500)
            {
                Alert alert = new Alert
                {
                    DeviceId = junk.DeviceId,
                    RecordedOn = junk.CreatedOn,
                    Message = "Device sending unintelligible data",
                    RecognizedOn = junk.CreatedOn,
                    Resolution = ResolutionStatus.New,
                    View = ViewStatus.New,
                    Type = AlertType.None
                };
                await context.AddAsync(alert);
            }

            await context.SaveChangesAsync();
        }
    }
}