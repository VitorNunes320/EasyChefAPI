﻿using CrossCutting.Utils;
using Domain.Enums;
using Domain.Models.Autenticacao;
using Domain.Models.Pedido;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EasyChefAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public ActionResult GetPedidos(string busca, int pagina, int quantidade)
        {
            try
            {
                var response = _pedidoService.GetPedidos(busca, pagina, quantidade);
                if (response.Count <= 0)
                {
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.SucessoLogin));
                }

                return Ok(new ResponseBase<List<PedidoModel>>(ResponseStatus.Sucesso, Mensagens.SucessoLogin, response));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroLogin, e.Message));
            }
        }

        [HttpPost]
        public IActionResult CreatePedido([FromBody] PedidoModel model)
        {
            try
            {
                var response = _pedidoService.CreatePedido(model);
                if (response)
                {
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.SucessoLogin));
                }

                return Ok(new ResponseBase<object>(ResponseStatus.Sucesso, Mensagens.SucessoLogin));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroLogin, e.Message));
            }
        }
    }
}
