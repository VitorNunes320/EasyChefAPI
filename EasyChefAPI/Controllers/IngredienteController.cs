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
    public class IngredienteController : ControllerBase
    {
        private readonly IIngredienteService _ingredienteService;
        private readonly IUsuarioService _usuarioService;
        public IngredienteController(
            IIngredienteService ingredienteService,
            IUsuarioService usuarioService
            )
        {
            _ingredienteService = ingredienteService;
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<IngredienteModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetIngrediente(Guid id)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }

                var response = _ingredienteService.GetIngrediente(id, (Guid)empresaId);
                if (response != null)
                    return Ok(new ResponseBase<IngredienteModel>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumIngredienteEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarIngredientes, e.Message));
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateIngrediente([FromBody] IngredienteModel model)
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

                _ingredienteService.CreateIngrediente(model, usuarioCriou);
                return Ok(new ResponseBase<IngredienteModel>(ResponseStatus.Sucesso, Mensagens.SucessoCriarIngrediente));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroCriarIngrediente, e.Message));
            }
        }


        [HttpPut]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateIngrediente([FromBody] IngredienteModel model)
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
                _ingredienteService.UpdateIngrediente(model, usuarioAtualizou, (Guid)empresaId);
                return Ok(new ResponseBase<IngredienteModel>(ResponseStatus.Sucesso, Mensagens.SucessoEditarIngrediente));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroEditarIngrediente, e.Message));
            }
        }

        [HttpGet("Lista")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<Paginacao<List<IngredienteModel>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetIngredientes(string? busca = "", int pagina = 1, int quantidade = 15)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }
                var response = _ingredienteService.GetIngredientes((Guid)empresaId, busca, pagina, quantidade);
                if (response != null)
                    return Ok(new ResponseBase<Paginacao<List<IngredienteModel>>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumIngredienteEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarIngredientes, e.Message));
            }
        }
    }
}
