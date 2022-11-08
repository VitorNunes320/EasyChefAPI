using CrossCutting.Utils;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Autenticacao;
using Domain.Models.Pedido;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Extensions;
using Service.Interfaces;
using System.Net.Mime;

namespace EasyChefAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly IMesaService _mesaService;
        private readonly IUsuarioService _usuarioService;

        public MesaController(IMesaService mesaService, IUsuarioService usuarioService)
        {
            _mesaService = mesaService;
            _usuarioService = usuarioService;
        }

        [HttpGet("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<MesaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetMesa(Guid id)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }

                var response = _mesaService.GetMesa(id, (Guid)empresaId);
                if (response != null)
                    return Ok(new ResponseBase<MesaModel>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumMesaEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarMesas, e.Message));
            }
        }

        [HttpGet("Lista")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<Paginacao<List<MesaModel>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetMesas(string? busca = "", int pagina = 1, int quantidade = 15)
        {
            try
            {
                var usuarioId = User.GetUserId().GetValueOrDefault();
                var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
                if (empresaId == null)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
                }
                var response = _mesaService.GetMesas((Guid)empresaId, busca, pagina, quantidade);
                if (response != null)
                    return Ok(new ResponseBase<Paginacao<List<MesaModel>>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumMesaEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarMesas, e.Message));
            }
        }
    }
}
