using CrossCutting.Security;
using CrossCutting.Utils;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly ITokenUsuarioService _tokenUsuarioService;
        private readonly IEmailService _emailService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository, ITokenUsuarioService tokenUsuarioService,
            IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _tokenUsuarioService = tokenUsuarioService;
            _emailService = emailService;
        }

        public void CriarUsuario(CriarUsuarioModel usuarioModel)
        {
            var usuarioExiste = _usuarioRepository.GetUsuarioByEmail(usuarioModel.Email);
            if (usuarioExiste != null)
            {
                throw new EmailUtilizadoException();
            }

            var usuario = new Usuario
            {
                Nome = usuarioModel.Nome,
                Email = usuarioModel.Email,
                Senha = SHA2.GenerateHash(usuarioModel.Senha, usuarioModel.Email),
                UsuarioCriou = usuarioModel.Email
            };

            usuario.PerfisUsuarios = new List<PerfilUsuario>();
            foreach(Guid perfilId in usuarioModel.Perfis)
            {
                var perfilExiste = _perfilRepository.GetById(perfilId);
                if (perfilExiste == null)
                {
                    throw new PerfilNaoExisteException();
                }

                var perfil = new PerfilUsuario(usuario.Id, perfilId, usuarioModel.Email);
                usuario.PerfisUsuarios.Add(perfil);
            };

            _usuarioRepository.Add(usuario);
        }

        public TokenUsuarioResponse Login(LoginModel loginModel)
        {

            var usuario = _usuarioRepository.GetUsuarioByEmailSenha(loginModel.Email, SHA2.GenerateHash(loginModel.Senha, loginModel.Email));
            if (usuario == null)
            {
                throw new EmailSenhaInvalidoException();
            }

            var response = new TokenUsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Foto = usuario.Foto,
                Tokens = _tokenUsuarioService.GerarToken(usuario)
            };

            return response;
        }

        public bool EsqueciSenha(string email)
        {
            var usuarioExiste = _usuarioRepository.GetUsuarioByEmail(email);
            if (usuarioExiste == null)
            {
                throw new EmailNaoCadastradoException();
            }

            var token = _tokenUsuarioService.GerarTokenUsuario(usuarioExiste.Id, 1, 6);
            var mensagem = $"Seu código de recuperação de senha é { token.Token }";
            return _emailService.SendEmail(email, "Esqueceu a senha", mensagem);
        }

        public  bool RedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            var tokenValido = _tokenUsuarioService.ValidarTokenUsuario(redefinirSenhaModel.Token);
            if (tokenValido != null)
            {
                var usuario = _usuarioRepository.GetById(tokenValido.UsuarioId);
                usuario.Senha = SHA2.GenerateHash(redefinirSenhaModel.NovaSenha, usuario.Email);
                usuario.AtualizadoEm = DataUtils.GetDateTimeBrasil();
                _usuarioRepository.Edit(usuario);
                return true;
            }

            return false;
        }
    }
}
