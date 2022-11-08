using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Services;

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
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IUnidadeMedidaService, UnidadeMedidaService>();
            services.AddTransient<IIngredienteService, IngredienteService>();
            services.AddTransient<IReceitaService, ReceitaService>();
            services.AddTransient<IMesaService, MesaService>();
        }
    }
}
