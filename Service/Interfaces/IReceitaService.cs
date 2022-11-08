using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;

namespace Service.Interfaces
{
    public interface IReceitaService
    {
        public bool CreateReceita(ReceitaModel model, string usuarioCriou);

        public bool DeleteReceita(Guid id, string usuarioAtualizou, Guid empresaId);

        public ReceitaModel? GetReceita(Guid id, Guid empresaId);

        public Paginacao<List<ReceitaModel>> GetReceitas(Guid empresaId, string busca, int pagina, int quantidade);

        public bool UpdateReceita(ReceitaModel model, string usuarioAtualizou, Guid empresaId);
    }
}
