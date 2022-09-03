using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        public Usuario? GetUsuarioByEmail(string email);

        public Usuario? GetUsuarioByEmailSenha(string email, string senha);
    }
}
