using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUsuarioService
    {

        public void CriarUsuario(CriarUsuarioModel criarUsuarioModel);

        public TokenUsuarioResponse Login(LoginModel loginModel);

        public bool EsqueciSenha(string email);

        public bool RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel);
    }
}
