using Event.Models.Configuration;
using Event.Services;
using Event.Services.External;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Event.Web.Api
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
            
            services.AddCors();
            
            services.AddControllers();
            
            services.Configure<HttpEndpointSettings>(Configuration.GetSection("HttpEndpointSettings"));
            services.AddTransient<IHttpEndpointConfigurationSettings, HttpEndpointConfigurationSettings>();
            
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IEventDataService, EventDataService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ICacheService, CacheService>();
            
            services.AddTransient<IExternalEventService, EonetEventService>();
            
            
            services.AddMvcCore()
                .AddNewtonsoftJson();
            
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            
        }
    }
}