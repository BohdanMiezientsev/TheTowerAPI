using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TheTowerAPI.Handlers;
using TheTowerAPI.Models;
using TheTowerAPI.Services;
using TheTowerAPI.Services.DAL;

namespace TheTowerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseMySql("server=localhost;database=TheTowerDb;user=root;password=Ujycfktp16062000");
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "The Tower API", Version = "v1" });
            });

            services.AddAuthentication("AuthenticationBasic")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("AuthenticationBasic", null);
            
            services.AddTransient<DbService>();
            services.AddSingleton<SHA256Hasher>();
            services.AddTransient<JwtTokenManager>();
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Tower API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}