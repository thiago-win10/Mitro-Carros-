using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MitroVehicle.Application.Common.AES;
using MitroVehicle.Application.Common.Behaviours;
using MitroVehicle.Application.Common.Interfaces;
using MitroVehicle.Application.Common.Jwt;
using MitroVehicle.Application.Common.Redis;
using MitroVehicle.Common;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

namespace MitroVehicle.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerfomanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PaginationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            var encryptionKey = Configuration.EncryptionKey;

            services.AddScoped<AuthService>();
            services.AddTransient<AesEncryptionService>();
            services.AddScoped<IRedisCaching, RedisCaching>();



            //Redis

            var redis = Configuration.RedisConnection;
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redis));
            services.AddHttpContextAccessor();

            //services.AddStackExchangeRedisCache(x =>
            //{
            //    x.InstanceName = "instance";
            //    x.Configuration = "localhost:5001";
            //});

            services.AddAuthentication(configureOptions: options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
                {
                    var key = Encoding.ASCII.GetBytes(encryptionKey);

                    jwt.SaveToken = true;
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = false,
                        ValidateLifetime = true
                    };
                });


            return services;

        }
        public static IServiceCollection AddSimpleApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerfomanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PaginationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            return services;
        }

    }
}
