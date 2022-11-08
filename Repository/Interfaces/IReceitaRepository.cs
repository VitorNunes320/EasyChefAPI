using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;

namespace Repository.Interfaces
{
    public interface IReceitaRepository : IRepositoryBase<Receita>
    {
        public ReceitaModel? GetReceita(Guid id, Guid empresaId);

        public List<ReceitaModel> GetReceitas(Guid empresaId, string busca = "", int pagina = 1, int quantidade = 15);

        public int GetQuantidadeReceitas(Guid empresaId, string busca);
    }
}