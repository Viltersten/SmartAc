using System;
using System.Reflection;
using System.Threading.Tasks;
using Api.Auxiliaries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Api.Interfaces;
using Api.Models.Configs;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddDbContext<Context>(ContextOptions());
            //services.AddEntityFrameworkInMemoryDatabase()
            //    .AddDbContext<Context>(options => options.UseInMemoryDatabase("Squicker"));

            services.AddOptions<AlertConfig>().Bind(Configuration.GetSection("Alerts"));

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IDeviceService, DeviceService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
                IdentityModelEventSource.ShowPII = true;
            }
            await MigrateDb(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private Action<DbContextOptionsBuilder> ContextOptions()
        {
            string connection = Configuration.GetConnectionString("TargetDb");
            string assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            Action<DbContextOptionsBuilder> output = builder
                => builder.UseSqlServer(
                    connection,
                    options => options.MigrationsAssembly(assembly)
                );

            return output;
        }

        private static async Task MigrateDb(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices
                .GetService<IServiceScopeFactory>()?.CreateScope();
            if (scope == null)
                throw new OperationCanceledException();

            Context context = scope.ServiceProvider.GetRequiredService<Context>();
            // todo Ask on Stacky.
            //await context.Database.MigrateAsync();
            //scope.Dispose();
            context.Database.Migrate();
        }
    }
}
