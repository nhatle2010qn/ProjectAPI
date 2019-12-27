using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAPI.Helpers;

namespace WebApplicationAPI.Authentication
{
    public static class AuthorizationModule
    {
        
        public static void RegisterAuthorizationModule(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(section);

            var settings = section.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(settings.Secret);
          
        }
    }
}
