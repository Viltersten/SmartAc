using System;
using System.Linq;
using System.Reflection;
using System.Text;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerOptions());

            //services.AddDistributedSqlServerCache( => );
            services.AddDbContext<Context>(ContextOptions());

            services.AddOptions<SecurityConfig>().Bind(Configuration.GetSection("Security"));
            services.AddOptions<AlertConfig>().Bind(Configuration.GetSection("Alerts"));
            services.AddOptions<UserConfig>().Bind(Configuration.GetSection("Users"));

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
                IdentityModelEventSource.ShowPII = true;
            }
            MigrateDb(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<PayloadFormatDetector>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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

        private Action<JwtBearerOptions> JwtBearerOptions()
        {
            string secret = Configuration.GetSection("Security:Secret").Value;
            byte[] encoding = Encoding.ASCII.GetBytes(secret);
            Action<JwtBearerOptions> output = options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(encoding)
                };
            };

            return output;
        }

        private static void MigrateDb(IApplicationBuilder app)
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

            if (!context.Devices.Any())
                DemoData.SeedDb(context);
        }
    }
}
