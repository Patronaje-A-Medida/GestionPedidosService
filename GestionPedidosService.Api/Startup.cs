using System;
using AutoMapper;
using GestionPedidosService.Api.ApiConventions;
using GestionPedidosService.Business.Mapper;
using GestionPedidosService.Business.ServicesCommand.Implements;
using GestionPedidosService.Business.ServicesCommand.Interfaces;
using GestionPedidosService.Business.ServicesQuery.Implements;
using GestionPedidosService.Business.ServicesQuery.Interfaces;
using GestionPedidosService.Business.Utils;
using GestionPedidosService.Persistence.Context;
using GestionPedidosService.Persistence.Interfaces;
using GestionPedidosService.Persistence.Repositories.Implements;
using GestionPedidosService.Persistence.Repositories.Interfaces;
using GestionPedidosService.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GestionPedidosService.Api
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
            services.AddDbContext<AppDbContext>(
                opts => opts.UseSqlServer(Configuration.GetConnectionString("LocalConnection"))//.LogTo(Console.WriteLine)
            );

            services.AddSingleton<IConfigurationManager, ConfigurationManager>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFeatureGarmentRepository, FeatureGarmentRepository>();
            services.AddScoped<IGarmentRepository, GarmentRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IPatternDimensionRepository, PatternDimensionRepository>();
            services.AddScoped<IPatternGarmentRepository, PatternGarmentRepository>();
            services.AddScoped<IDictionaryTypeRepository, DictionaryTypeRepository>();
            services.AddScoped<IAtelierRepository, AtelierRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IOrderServiceQuery, OrderServiceQuery>();
            services.AddScoped<IPatternGarmentServiceQuery, PatternGarmentServiceQuery>();
            services.AddScoped<IGarmentServiceQuery, GarmentServiceQuery>();
            services.AddScoped<IDictionaryTypeServiceQuery, DictionaryTypeServiceQuery>();

            services.AddScoped<IGarmentServiceCommand, GarmentServiceCommand>();
            services.AddScoped<IOrderServiceCommand, OrderServiceCommand>();

            var mapperConfig = new MapperConfiguration(
                mc => mc.AddProfile(new MapperProfile())
            );
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Version = "v1",
                    Title = "API Gestión de Pedidos v1",
                    Description = "Servicio de Gestión de Pedidos del sistema Patronaje A Medida",
                    Contact = new OpenApiContact
                    {
                        Name = "Patronaje A Medida",
                        Email = "",
                        Url = new Uri("https://github.com/Patronaje-A-Medida")
                    }
                });
            });

            services.AddCors(
                opts => opts.AddPolicy(
                    "All",
                    builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*")
                )
            );

            services.AddControllers(
                    config => config.Conventions.Add(new ApiVersionConvention())
                )
                .AddNewtonsoftJson(
                    opts => opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                } 
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gestión de Pedidos v1"));

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error-development");
                //app.UseExceptionHandler("/error");
            }

            app.UseCors("All");

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}