using Domain.Models;

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
