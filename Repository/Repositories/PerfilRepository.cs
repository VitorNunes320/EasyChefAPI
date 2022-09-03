using Domain.Entities;
using Data.Contexts;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Repository.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        private readonly DbSet<Perfil> _perfis;

        public PerfilRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _perfis = easyChefContext.Perfis;
        }

        public List<PerfilResponse> GetPerfis()
        {
            return _perfis.Select(perfil => new PerfilResponse(perfil.Id, perfil.Descricao)).ToList();
        }
    }
}
