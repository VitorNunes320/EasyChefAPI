using Domain.Models.Autenticacao;

namespace Service.Interfaces
{
    public interface IPerfilService
    {
        public List<PerfilResponse> GetPerfis();
    }
}
