using AspNetCore.Identity.Mongo;
using Challenge.API.Extensions;
using Challenge.API.Hubs;
using Challenge.Core.Models;
using Challenge.Core.Redis;
using Challenge.Core.Repositories;
using Challenge.Core.Services;
using Challenge.Core.Settings;
using Challenge.Data.Repository;
using Challenge.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Challenge.API
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
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IRedisRepository, RedisRepository>();
            services.AddSingleton<IRedisService, RedisService>();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<,>), typeof(Service<,>));


            services.AddIdentityMongoDbProvider<ChallengeUser, ChallengeUserRole, string>(identity =>
            {
                identity.Password.RequireNonAlphanumeric = true;
                identity.User.RequireUniqueEmail = true;
            },
            mongo =>
            {
                mongo.ConnectionString = Configuration.GetConnectionString("MongoDbDatabase");
            });
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = Configuration["Redis:ConnectionString"];
            });
            services.AddControllers();
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOptions>();
            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOption"));
            services.Configure<RedisSettings>(Configuration.GetSection("RedisSettings"));
            services.Configure<MongoSettings>(Configuration.GetSection("MongoSettings"));
            services.AddCustomTokenAuth(tokenOptions);        
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRedisService redisService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            redisService.Connect();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>("/messanger");
                endpoints.MapControllers();
            });
        }
    }
}
