using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Repository.Interfaces
{
    public interface IPerfilRepository : IRepositoryBase<Perfil>
    {
        public List<PerfilResponse> GetPerfis();
    }
}
