using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWith.NET5.Model.Context;
using RestWith.NET5.Business;
using RestWith.NET5.Business.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWith.NET5.Repository;
using Serilog;
using RestWith.NET5.Repository.Generic;
using RestWith.NET5.Hypermedia.Filters;
using RestWith.NET5.Hypermedia.Enricher;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWith.NET5
{
    public class Startup
    {
        // O Environment sempre ser� inicializado no m�todo do StartUP
        public IWebHostEnvironment Environment { get;  }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            // Logger para as migrations
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Adiciona o CORS
            services.AddCors(options => options.AddDefaultPolicy(builder => 
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            // Faz a conex�o com o BD
            var connection = Configuration["ConnectionStrings:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
            .AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);

            // Faz o versionamento, mas � preciso instalar o pacote Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning();

            // Adiciona o Swagger (documenta��o da API), mas � preciso instalar o pacote Swashbuckle.AspNetCore
            // No "Configure" tamb�m adicionamos algumas coisas
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API with ASP.NET Core 5",
                        Version = "v1",
                        Description = "API RESTful developed in course",
                        Contact = new OpenApiContact
                        {
                            Name = "Josivaldo Jo�o",
                            Url = new Uri("https://github.com/josivaldojoao14")
                        }
                    });
            });

            // Inje��o de depend�ncia *person*
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            // Inje��o de depend�ncia *books*
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();

            // Inje��o de depend�ncia *reposit�rio gen�rico*
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Habilita o CORS depois das configura��es feitas no ConfigureServies
            // o UseCores deve ficar depois do "HttpsRedirection" e antes do "UseEndPoints" 
            app.UseCors();

            // Permite a utiliza��o do SWAGGER, esse primeiro � respons�vel por gerar a documenta��o em JSON
            app.UseSwagger();

            // Gera uma p�gina HTML, onde podemos acessar a documenta��o
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                    "REST API with ASP.NET Core 5 - v1");
            });

            // Configura��o da Swagger page
            var option = new RewriteOptions();
            option.AddRedirect("^$","swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }

        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                { 
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            };
        }
    }
}
