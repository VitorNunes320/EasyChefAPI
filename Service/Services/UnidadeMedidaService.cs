using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class UnidadeMedidaService : IUnidadeMedidaService
    {
        private readonly IUnidadeMedidaRepository _unidadeMedidaRespository;

        public UnidadeMedidaService(IUnidadeMedidaRepository unidadeMedidaRespository)
        {
            _unidadeMedidaRespository = unidadeMedidaRespository;
        }

        public void CreateUnidadeMedida(UnidadeMedidaModel model, string usuarioCriou)
        {
            UnidadeMedida unidadeMedida = new UnidadeMedida
            {
                Nome = model.Nome,
                UsuarioCriou = usuarioCriou,
            };
            _unidadeMedidaRespository.Add(unidadeMedida);
        }

        public UnidadeMedidaModel? GetUnidadeMedida(Guid id)
        {
            return _unidadeMedidaRespository.GetUnidadeMedida(id);
        }

        public void UpdateUnidadeMedida(UnidadeMedida model, string usuarioAtualizou)
        {
            var unidadeMedida = _unidadeMedidaRespository.GetById(model.Id);
            unidadeMedida.Nome = model.Nome;
            unidadeMedida.UsuarioAtualizou = usuarioAtualizou;
            unidadeMedida.AtualizadoEm = DateTime.UtcNow;

            _unidadeMedidaRespository.Edit(unidadeMedida);
        }

        public void RemoveUnidadeMedida(Guid id, string usuarioAtualizou)
        {
            var unidadeMedida = _unidadeMedidaRespository.GetById(id);
            unidadeMedida.Habilitado = false;
            unidadeMedida.UsuarioAtualizou = usuarioAtualizou;
            unidadeMedida.AtualizadoEm = DateTime.UtcNow;

            _unidadeMedidaRespository.Edit(unidadeMedida);
        }

        public Paginacao<List<UnidadeMedidaModel>> GetUnidadesMedidas(string busca, int pagina, int quantidade)
        {
            return new Paginacao<List<UnidadeMedidaModel>>
            {
                Quantidade = _unidadeMedidaRespository.GetQuantidadeUnidadeMedidas(busca),
                Dados = _unidadeMedidaRespository.GetUnidadesMedidas(busca, pagina, quantidade),
            };
        }
    }
}
