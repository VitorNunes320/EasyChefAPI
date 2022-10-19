using Domain.Models;
using Repository.Interfaces;
using Service.Interfaces;

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
