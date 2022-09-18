using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using CrossCutting.Utils;
using Domain.Exceptions;
using System.Net.Mime;

namespace EasyChefAPI.Controllers
{
    /// <summary>
    /// Autenticação
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="usuarioService"></param>
        /// <param name="perfilService"></param>
        public AutenticacaoController(IUsuarioService usuarioService, IPerfilService perfilService)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
        }

        /// <summary>
        /// Utilizado para criar novos usuários
        /// </summary>
        /// <param name="criarUsuarioModel">Dados do usuário</param>
        /// <response code="200">Registro realizado com sucesso</response>
        /// <response code="400">E-mail já utilizado</response>
        /// <response code="404">Perfil de usuário acesso inválido</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("Registrar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDadosBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult RegistrarUsuario([FromBody] CriarUsuarioModel criarUsuarioModel)
        {
            try
            {
                _usuarioService.CriarUsuario(criarUsuarioModel);
                return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoRegistrarUsuario));
            }
            catch (PerfilNaoExisteException e)
            {
                return NotFound(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (EmailUtilizadoException e)
            {
                return BadRequest(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroRegistrarUsuario, e.Message));
            }
        }

        /// <summary>
        /// Utilizado para realizar login no sistema
        /// </summary>
        /// <param name="loginModel">Dados utilizados no login</param>
        /// <response code="200">Login realizado com sucesso</response>
        /// <response code="400">Email ou senha inválidos</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("Login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDadosBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var response = _usuarioService.Login(loginModel);
                return Ok(new ResponseDadosBase<TokenUsuarioResponse>(ResponseStatus.Sucesso, Mensagens.SucessoLogin, response));
            }
            catch (EmailSenhaInvalidoException e)
            {
                return BadRequest(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroLogin, e.Message));
            }
        }


        /// <summary>
        /// Busca todos os perfis disponíveis no sistema
        /// </summary>
        /// <returns>Uma lista de perfis</returns>
        /// <response code="200">Retorna uma lista perfis</response>
        /// <response code="400">Email ou senha inválidos</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet("Perfis")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseDadosBase<List<PerfilResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDadosBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetPerfis()
        {
            try
            {
                var response = _perfilService.GetPerfis();
                if (response.Count > 0)
                    return Ok(new ResponseDadosBase<List<PerfilResponse>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase(ResponseStatus.Falha, Mensagens.ErroNenhumPerfilEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarPerfis, e.Message));
            }
        }

        /// <summary>
        /// Envia um email com o link de recuperação de senha
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <response code="200">Senha recuperada com sucesso</response>
        /// <response code="400">E-mail inválido</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpGet("EsqueciSenha/{email}")]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDadosBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult EsqueciSenha(string email)
        {
            try
            {
                var sucesso = _usuarioService.EsqueciSenha(email);
                if (sucesso)
                    return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoEsqueciSenha));
                else
                    return BadRequest(new ResponseBase(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (EmailNaoCadastradoException e)
            {
                return BadRequest(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
            }
        }

        /// <summary>
        /// Troca a senha do usuário
        /// </summary>
        /// <param name="redefinirSenhaModel">Token e a nova senha do usuário</param>
        /// <response code="200">Senha redefinida com sucesso</response>
        /// <response code="400">Token inválido</response>
        /// <response code="404">Token não existe</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("RedefinirSenha")]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseDadosBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                var sucesso = _usuarioService.RedefinirSenha(redefinirSenhaModel);
                if (sucesso)
                    return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoRedefinirSenha));
                else
                    return BadRequest(new ResponseBase(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (TokenNaoExisteException e)
            {
                return NotFound(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (TokenUtilizadoException e)
            {
                return BadRequest(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (TokenExpirouException e)
            {
                return BadRequest(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
            }
        }
    }
}
