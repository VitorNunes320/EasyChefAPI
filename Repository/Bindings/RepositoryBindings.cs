﻿using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;

namespace Repository.Bindings
{
    public class RepositoryBindings
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddTransient<ITokenUsuarioRepository, TokenUsuarioRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
        }
    }
}
