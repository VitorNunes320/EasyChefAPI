using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITokenUsuarioService
    {
        public TokenResponse GerarToken(Usuario usuario);

        public TokenUsuario GerarTokenUsuario(Guid usuarioId, int dias, int? tamanho);

        public TokenUsuario? ValidarTokenUsuario(string token);
    }
}
