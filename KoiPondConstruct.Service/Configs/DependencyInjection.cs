using KoiPondConstruct.Data;
using KoiPondConstruct.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace KoiPondConstruct.Service.Configs
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureBALServices(this IServiceCollection services)
        {
            // In case no tracking ASYNC programming
            services.AddDbContext<FA24_SE1702_PRN221_G2_KoiPondConstructContext>(options =>
            {
                options.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking);
            });


            services.AddScoped<UnitOfWork>();

            // Add AutoMapper
            // Your custom service configuration
            services.AddAutoMapper(typeof(DependencyInjection).Assembly, typeof(MappingProfile).Assembly);

            services.AddScoped<AuthService>();
            services.AddScoped<CustomerRequestService>();
            services.AddScoped<DataInitService>();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            return services;
        }
    }
}
