using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // устанавливаем контекст данных
            services.AddDbContext<SongsContext>(options =>
            options.UseSqlServer(SqlConnectionIntegratedSecurity));
            services.AddControllers(); // используем контроллеры без представлений
        }
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "tcp:testserver666.database.windows.net,1433",
                    // ѕодключение будет с проверкой подлинности пользовател€ Windows
                    IntegratedSecurity = false,
                    // Ќазвание целевой базы данных.
                    InitialCatalog = "Songs"
                };
                return sb.ConnectionString;
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
