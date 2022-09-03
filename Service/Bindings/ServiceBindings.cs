using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Bindings
{
    public class ServiceBindings
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<ITokenUsuarioService, TokenUsuarioService>();
            services.AddTransient<IPerfilService, PerfilService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
