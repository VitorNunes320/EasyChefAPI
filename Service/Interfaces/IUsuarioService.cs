using Domain.Models.Autenticacao;

namespace Service.Interfaces
{
    public interface IUsuarioService
    {

        public void CriarUsuario(NovoUsuarioModel NovoUsuarioModel);

        public TokenUsuarioResponse Login(LoginModel loginModel);

        public bool EsqueciSenha(string email);

        public bool RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel);
        
        public Guid? GetUsuarioEmpresaId(Guid usuarioId);
    }
}
