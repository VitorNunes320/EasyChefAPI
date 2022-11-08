using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using Domain.Models.Receita;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Services
{
    public class IngredienteService : IIngredienteService
    {
        private readonly IIngredienteRepository _ingredienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public IngredienteService(IIngredienteRepository ingredienteRepository, IUsuarioRepository usuarioRepository)
        {
            _ingredienteRepository = ingredienteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public bool CreateIngrediente(IngredienteModel model, string usuarioCriou)
        {
            var usuario = _usuarioRepository.GetUsuarioByEmail(usuarioCriou);
            Ingrediente ingrediente = new Ingrediente
            {
                Nome = model.Nome,
                Imagem = model.Imagem,
                UnidadeMedidaId = model.UnidadeMedidaId,
                Quantidade = 0,
                Descricao = model.Descricao,
                Valor = model.Valor,
                UsuarioCriou = usuarioCriou,
                EmpresaId = usuario.EmpresaId,
            };

            _ingredienteRepository.Add(ingrediente);
            return true;
        }

        public IngredienteModel? GetIngrediente(Guid id, Guid empresaId)
        {
            return _ingredienteRepository.GetIngrediente(id, empresaId);
        }

        public bool UpdateIngrediente(IngredienteModel model, string usuarioAtualizou, Guid empresaId)
        {
            var ingrediente = _ingredienteRepository.GetById((Guid)model.Id);
            if (ingrediente.EmpresaId == empresaId)
            {
                ingrediente.Nome = model.Nome;
                ingrediente.Imagem = model.Imagem;
                ingrediente.UnidadeMedidaId = model.UnidadeMedidaId;
                ingrediente.Quantidade = 0;
                ingrediente.Descricao = model.Descricao;
                ingrediente.Valor = model.Valor;
                ingrediente.UsuarioCriou = usuarioAtualizou;

                _ingredienteRepository.Edit(ingrediente);
                return true;
            }

            return false;
        }

        public bool DeleteIngrediente(Guid id, string usuarioAtualizou, Guid empresaId)
        {
            var ingrediente = _ingredienteRepository.GetById(id);
            if (ingrediente.EmpresaId == empresaId)
            {
                ingrediente.Habilitado = false;
                ingrediente.UsuarioAtualizou = usuarioAtualizou;
                _ingredienteRepository.Edit(ingrediente);
                return true;
            }

            return false;
        }

        public Paginacao<List<IngredienteModel>> GetIngredientes(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return new Paginacao<List<IngredienteModel>>
            {
                Quantidade = _ingredienteRepository.GetQuantidadeIngredientes(empresaId, busca),
                Dados = _ingredienteRepository.GetIngredientes(empresaId, busca, pagina, quantidade),
            };
        }
    }
}