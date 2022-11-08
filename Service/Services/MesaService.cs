using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Pedido;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class MesaService : IMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public MesaModel? GetMesa(Guid id, Guid empresaId)
        {
            return _mesaRepository.GetMesa(id, empresaId);
        }

        public Paginacao<List<MesaModel>> GetMesas(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return new Paginacao<List<MesaModel>>
            {
                Quantidade = _mesaRepository.GetQuantidadeMesas(empresaId, busca),
                Dados = _mesaRepository.GetMesas(empresaId, busca, pagina, quantidade),
            };
        }
    }
}