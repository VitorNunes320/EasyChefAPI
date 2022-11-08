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
    public class ReceitaService : IReceitaService
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly IReceitaIngredienteRepository _receitaIngredienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ReceitaService(
            IReceitaRepository receitaRepository, 
            IUsuarioRepository usuarioRepository, 
            IReceitaIngredienteRepository receitaIngredienteRepository
            )
        {
            _receitaRepository = receitaRepository;
            _usuarioRepository = usuarioRepository;
            _receitaIngredienteRepository = receitaIngredienteRepository;
        }

        public bool CreateReceita(ReceitaModel model, string usuarioCriou)
        {
            var usuario = _usuarioRepository.GetUsuarioByEmail(usuarioCriou);
            Receita receita = new Receita
            {
                Nome = model.Nome,
                Imagem = model.Imagem,
                Descricao = model.Descricao,
                Valor = model.Valor,
                UsuarioCriou = usuarioCriou,
                EmpresaId = usuario.EmpresaId,
                ReceitasIngredientes = new()
            };

            foreach (var ingrediente in model.Ingredientes)
            {
                var receitaIngrediente = new ReceitaIngrediente
                {
                    ReceitaId = receita.Id,
                    IngredienteId = ingrediente.IngredienteId,
                    Quantidade = ingrediente.Quantidade,
                    UnidadeMedidaId = ingrediente.UnidadeMedidaId,
                };

                receita.ReceitasIngredientes.Add(receitaIngrediente);
            }

            _receitaRepository.Add(receita);
            return true;
        }

        public ReceitaModel? GetReceita(Guid id, Guid empresaId)
        {
            return _receitaRepository.GetReceita(id, empresaId);
        }

        public bool UpdateReceita(ReceitaModel model, string usuarioAtualizou, Guid empresaId)
        {
            var receita = _receitaRepository.GetById((Guid)model.Id);
            if (receita.EmpresaId == empresaId)
            {
                receita.Nome = model.Nome;
                receita.Imagem = model.Imagem;
                receita.Descricao = model.Descricao;
                receita.Valor = model.Valor;
                receita.UsuarioAtualizou = usuarioAtualizou;
                receita.AtualizadoEm = DateTime.UtcNow;
                receita.ReceitasIngredientes = new();

                foreach (var ingrediente in receita.ReceitasIngredientes)
                {
                    _receitaIngredienteRepository.Remove(ingrediente);
                }

                if (model.Ingredientes != null)
                {
                    foreach (var ingrediente in model.Ingredientes)
                    {
                        var receitaIngrediente = new ReceitaIngrediente
                        {
                            ReceitaId = receita.Id,
                            IngredienteId = ingrediente.IngredienteId,
                            Quantidade = ingrediente.Quantidade,
                            UnidadeMedidaId = ingrediente.UnidadeMedidaId,
                        };

                        receita.ReceitasIngredientes.Add(receitaIngrediente);
                    }
                }

                _receitaRepository.Edit(receita);
                return true;
            }

            return false;
        }

        public bool DeleteReceita(Guid id, string usuarioAtualizou, Guid empresaId)
        {
            var receita = _receitaRepository.GetById(id);
            if (receita.EmpresaId == empresaId)
            {
                receita.Habilitado = false;
                receita.UsuarioAtualizou = usuarioAtualizou;
                receita.AtualizadoEm = DateTime.UtcNow;
                _receitaRepository.Edit(receita);
                return true;
            }

            return false;
        }

        public Paginacao<List<ReceitaModel>> GetReceitas(Guid empresaId, string busca, int pagina, int quantidade)
        {
            return new Paginacao<List<ReceitaModel>>
            {
                Quantidade = _receitaRepository.GetQuantidadeReceitas(empresaId, busca),
                Dados = _receitaRepository.GetReceitas(empresaId, busca, pagina, quantidade),
            };
        }
    }
}