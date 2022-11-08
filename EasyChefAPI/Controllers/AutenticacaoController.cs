using System.Net.Mime;
using CrossCutting.Utils;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models.Autenticacao;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

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
        /// <param name="NovoUsuarioModel">Dados do usuário</param>
        /// <response code="200">Registro realizado com sucesso</response>
        /// <response code="400">E-mail já utilizado</response>
        /// <response code="404">Perfil de usuário acesso inválido</response>
        /// <response code="500">Erro interno no servidor</response>
        [HttpPost("Registrar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult RegistrarUsuario([FromBody] NovoUsuarioModel NovoUsuarioModel)
        {

                _usuarioService.CriarUsuario(NovoUsuarioModel);
                return Ok(new ResponseBase<object>(ResponseStatus.Sucesso, Mensagens.SucessoRegistrarUsuario));

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
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var response = _usuarioService.Login(loginModel);
                return Ok(new ResponseBase<TokenUsuarioResponse>(ResponseStatus.Sucesso, Mensagens.SucessoLogin, response));
            }
            catch (EmailSenhaInvalidoException e)
            {
                return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroLogin, e.Message));
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
        [ProducesResponseType(typeof(ResponseBase<List<PerfilResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult GetPerfis()
        {
            try
            {
                var response = _perfilService.GetPerfis();
                if (response.Count > 0)
                    return Ok(new ResponseBase<List<PerfilResponse>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return NotFound(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.ErroNenhumPerfilEncontrado));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarPerfis, e.Message));
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
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult EsqueciSenha(string email)
        {
            try
            {
                var sucesso = _usuarioService.EsqueciSenha(email);
                if (sucesso)
                    return Ok(new ResponseBase<object>(ResponseStatus.Sucesso, Mensagens.SucessoEsqueciSenha));
                else
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (EmailNaoCadastradoException e)
            {
                return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
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
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseBase<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseBase<string>), StatusCodes.Status500InternalServerError)]
        public IActionResult RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                var sucesso = _usuarioService.RedefinirSenha(redefinirSenhaModel);
                if (sucesso)
                    return Ok(new ResponseBase<object>(ResponseStatus.Sucesso, Mensagens.SucessoRedefinirSenha));
                else
                    return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (TokenNaoExisteException e)
            {
                return NotFound(new ResponseBase<object>(ResponseStatus.Falha, e.Message));
            }
            catch (TokenUtilizadoException e)
            {
                return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, e.Message));
            }
            catch (TokenExpirouException e)
            {
                return BadRequest(new ResponseBase<object>(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
            }
        }
    }
}
