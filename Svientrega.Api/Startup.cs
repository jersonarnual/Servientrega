using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Servientrega.Business.Interface;
using Servientrega.Business.Repository;
using Servientrega.Data.Context;
using Servientrega.Data.Interface;
using Servientrega.Data.Repository;
using Servientrega.Infraestructure.Util;

namespace Svientrega.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<ServientregaContext>(item => item.UseSqlServer(Configuration.GetConnectionString("DevDB")), ServiceLifetime.Transient);
            services.AddDbContext<ServientregaContext>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ServientregaProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Servientrega.Api", Version = "v1" });
            });
            LoadScopes(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Svientrega.Api v1"));

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region private method
        private static void LoadScopes(IServiceCollection services)
        {
            services.AddScoped<IAvionRepository, AvionRepository>();
            services.AddScoped<IAvionBusiness, AvionBusiness>();
        }
        #endregion
    }
}
