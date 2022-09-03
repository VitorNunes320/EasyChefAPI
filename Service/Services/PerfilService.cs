using CrossCutting.Security;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PerfilService : IPerfilService
    {

        private readonly IPerfilRepository _perfilRepository;

        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public List<PerfilResponse> GetPerfis()
        {
            return _perfilRepository.GetPerfis();
        }
    }
}
