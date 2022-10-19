using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        public Usuario? GetUsuarioByEmail(string email);

        public Usuario? GetUsuarioByEmailSenha(string email, string senha);
    }
}
