using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using CrossCutting.Utils;
using Domain.Exceptions;

namespace EasyChefAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPerfilService _perfilService;

        public AutenticacaoController(IUsuarioService usuarioService, IPerfilService perfilService)
        {
            _usuarioService = usuarioService;
            _perfilService = perfilService;
        }

        /// <summary>
        /// Utilizado para criar novos usuários
        /// </summary>
        /// <param name="criarUsuarioModel">Dados do usuário</param>
        /// <returns></returns>
        [HttpPost("Registrar")]
        public IActionResult RegistrarUsuario(CriarUsuarioModel criarUsuarioModel)
        {
            try
            {
                _usuarioService.CriarUsuario(criarUsuarioModel);
                return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoRegistrarUsuario));
            }
            catch (PerfilNaoExisteException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (EmailUtilizadoException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroRegistrarUsuario, e.Message));
            }
        }

        /// <summary>
        /// Utilizado para realizar login no sistema
        /// </summary>
        /// <param name="loginModel">Dados utilizados no login</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var response = _usuarioService.Login(loginModel);
                return Ok(new ResponseDadosBase<TokenUsuarioResponse>(ResponseStatus.Sucesso, Mensagens.SucessoLogin, response));
            }
            catch (EmailSenhaInvalidoException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroLogin, e.Message));
            }
        }


        /// <summary>
        /// Busca todos os perfis disponíveis no sistema
        /// </summary>
        /// <returns></returns>
        [HttpGet("Perfis")]
        public IActionResult GetPerfis()
        {
            try
            {
                var response = _perfilService.GetPerfis();
                if (response.Count > 0)
                    return Ok(new ResponseDadosBase<List<PerfilResponse>>(ResponseStatus.Sucesso, Mensagens.Ok, response));
                else
                    return Ok(new ResponseDadosBase<List<PerfilResponse>>(ResponseStatus.Falha, Mensagens.ErroNenhumPerfilEncontrado, response));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.ErroBuscarPerfis, e.Message));
            }
        }

        /// <summary>
        /// Envia um email com o link de recuperação de senha
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <returns></returns>
        [HttpGet("EsqueciSenha/{email}")]
        public IActionResult EsqueciSenha(string email)
        {
            try
            {
                var sucesso = _usuarioService.EsqueciSenha(email);
                if (sucesso)
                    return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoEsqueciSenha));
                else
                    return Ok(new ResponseBase(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (EmailNaoCadastradoException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message
                    ));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
            }
        }

        /// <summary>
        /// Troca a senha do usuário
        /// </summary>
        /// <param name="redefinirSenhaModel">Token e a nova senha do usuário</param>
        /// <returns></returns>
        [HttpPost("RedefinirSenha")]
        public IActionResult RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                var sucesso = _usuarioService.RedefinirSenha(redefinirSenhaModel);
                if (sucesso)
                    return Ok(new ResponseBase(ResponseStatus.Sucesso, Mensagens.SucessoRedefinirSenha));
                else
                    return Ok(new ResponseBase(ResponseStatus.Falha, Mensagens.FalhaEsqueciSenha));
            }
            catch (TokenNaoExisteException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (TokenUtilizadoException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (TokenExpirouException e)
            {
                return Ok(new ResponseBase(ResponseStatus.Falha, e.Message));
            }
            catch (Exception e)
            {
                return Ok(new ResponseDadosBase<string>(ResponseStatus.Erro, Mensagens.FalhaEsqueciSenha, e.Message));
            }
        }
    }
}
