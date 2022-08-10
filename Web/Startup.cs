using CMC.ReadStack;
using CMC.Repositories;
using CMC.Repositories.Interfaces;
using CMC.Services;
using CMC.Services.Interface;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Web.Pipelines;

namespace Web
{
    public class Startup
    {
        public const string AllowedSpecificOrigins = "AllowedSpecificOrigins";
        private string[] _allowedCorsHosts;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _allowedCorsHosts = Configuration.GetSection("Cors:Allow").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedSpecificOrigins,
                    builder =>
                    {
                        builder
                            .WithOrigins(_allowedCorsHosts)
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(_ => true)
                            .AllowAnyMethod();
                    });
            });

            // register MediatR
            services.RegisterRequestHandlers();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

            // https://github.com/serilog/serilog-extensions-logging
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddControllers();

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IShippingService, ShippingService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper(typeof(ReadStackAutoMapper).Assembly,
                typeof(RepositoryAutoMapper).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowedSpecificOrigins);
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
