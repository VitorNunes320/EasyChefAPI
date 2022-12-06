using CrossCutting.Utils;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Autenticacao;
using Domain.Models.Pedido;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Extensions;
using Service.Interfaces;
using Service.Services;

namespace EasyChefAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IUsuarioService _usuarioService;

        public PedidoController(IPedidoService pedidoService, IUsuarioService usuarioService)
        {
            _pedidoService = pedidoService;
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista")]
        [Authorize]
        public ActionResult GetPedidos(string? busca, int pagina = 1, int quantidade = 15)
        {
            var usuarioId = User.GetUserId().GetValueOrDefault();
            var empresaId = _usuarioService.GetUsuarioEmpresaId(usuarioId);
            if (empresaId == null)
            {
                return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaUsuarioAcessoEmpresa));
            }

            var response = _pedidoService.GetPedidos((Guid)empresaId, busca, pagina, quantidade);
            if (response.Quantidade <= 0)
            {
                return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaNenhumPedidoEncontrado));
            }

            return Ok(new ResponseBase<Paginacao<List<PedidoModel>>>(ResponseStatus.Sucesso, Mensagens.Ok, response));

        }

        [HttpPost]
        public IActionResult CreatePedido([FromBody] PedidoModel model)
        {
            try
            {
                var response = _pedidoService.CreatePedido(model);
                if (!response)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.ErroCriarPedido));
                }

                return Ok(new ResponseBase<object>(ResponseStatus.Sucesso, Mensagens.SucessoCriarPedido));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroCriarPedido, e.Message));
            }
        }

    }
}
