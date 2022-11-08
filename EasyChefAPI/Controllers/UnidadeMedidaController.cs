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
    public class UnidadeMedidaController : ControllerBase
    {
        private readonly IUnidadeMedidaService _unidadeMedidaService;

        public UnidadeMedidaController(IUnidadeMedidaService unidadeMedidaService)
        {
            _unidadeMedidaService = unidadeMedidaService;
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<UnidadeMedidaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GeUnidadeMedida(Guid id)
        {
            try
            {
                var response = _unidadeMedidaService.GetUnidadeMedida(id);
                if (response != null)
                    return Ok(new ResponseBase<UnidadeMedidaModel>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumUnidadeMedidaEncontrada));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarUnidadeMedida, e.Message));
            }
        }

        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<UnidadeMedidaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUnidadeMedida([FromBody] UnidadeMedidaModel model)
        {
            try
            {
                var usuarioCriou = User.Identity.Name;
                _unidadeMedidaService.CreateUnidadeMedida(model, usuarioCriou);
                return Ok(new ResponseBase<UnidadeMedidaModel>(ResponseStatus.Sucesso, Mensagens.SucessoCriarUnidadeMedida));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarUnidadeMedida, e.Message));
            }
        }

        [HttpGet("Lista")]
        //[Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<Paginacao<List<UnidadeMedidaModel>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetIngredientes(string? busca = "", int pagina = 1, int quantidade = 15)
        {
            try
            {
                var response = _unidadeMedidaService.GetUnidadesMedidas(busca, pagina, quantidade);
                if (response != null)
                    return Ok(new ResponseBase<Paginacao<List<UnidadeMedidaModel>>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
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
