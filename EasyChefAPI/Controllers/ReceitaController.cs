using System.Net.Mime;
using CrossCutting.Utils;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Autenticacao;
using Domain.Models.Receita;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Extensions;
using Service.Interfaces;

namespace EasyChefAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _receitaService;
        private readonly IUsuarioService _usuarioService;

        public ReceitaController(
            IReceitaService receitaService,
            IUsuarioService usuarioService
            )
        {
            _receitaService = receitaService;
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<ReceitaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetReceita(Guid id)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }

                var response = _receitaService.GetReceita(id, (Guid)empresaId);
                if (response != null)
                    return Ok(new ResponseBase<ReceitaModel>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumaReceitaEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarReceitas, e.Message));
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateReceita([FromBody] ReceitaModel model)
        {
            try
            {
                var usuarioCriou = User.Identity.Name;
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }

                _receitaService.CreateReceita(model, usuarioCriou);
                return Ok(new ResponseBase<ReceitaModel>(ResponseStatus.Sucesso, Mensagens.SucessoCriarReceita));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroCriarReceita, e.Message));
            }
        }


        [HttpPut]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateReceita([FromBody] ReceitaModel model)
        {
            try
            {
                var usuarioAtualizou = User.Identity.Name;
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }
                _receitaService.UpdateReceita(model, usuarioAtualizou, (Guid)empresaId);
                return Ok(new ResponseBase<ReceitaModel>(ResponseStatus.Sucesso, Mensagens.SucessoEditarReceita));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroEditarReceita, e.Message));
            }
        }

        [HttpGet("Lista")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<Paginacao<List<ReceitaModel>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetReceitas(string? busca = "", int pagina = 1, int quantidade = 15)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }
                var response = _receitaService.GetReceitas((Guid)empresaId, busca, pagina, quantidade);
                if (response != null)
                    return Ok(new ResponseBase<Paginacao<List<ReceitaModel>>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumaReceitaEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarReceitas, e.Message));
            }
        }
    }
}
