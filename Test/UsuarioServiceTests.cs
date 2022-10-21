using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossCutting.Security;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.Autenticacao;
using Moq;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Services;
using Xunit;

namespace Test
{
    public class UsuarioServiceTests
    {
        private Mock<IUsuarioRepository> _usuarioRepository;
        private Mock<IPerfilRepository> _perfilRepository;
        private Mock<ITokenUsuarioService> _tokenUsuarioService;
        private Mock<IEmailService> _emailService;
        private IUsuarioService _usuarioService;
        public UsuarioServiceTests()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _perfilRepository = new Mock<IPerfilRepository>();
            _tokenUsuarioService = new Mock<ITokenUsuarioService>();
            _emailService = new Mock<IEmailService>();

            _usuarioRepository.Setup(us => us.GetUsuarioByEmailSenha("email@email.com.br", SHA2.GenerateHash("senha123", "email@email.com.br")))
                .Returns(new Usuario()
                {
                    Id = Guid.Parse("9bbb5df2-ede5-40ed-aa5d-33632f43ebcc"),
                    Nome = "Fulano da Silva",
                    Email = "email@email.com.br",
                    Senha = SHA2.GenerateHash("senha123", "email@email.com.br"),
                }
            );

            _usuarioService = new UsuarioService(_usuarioRepository.Object, _perfilRepository.Object, _tokenUsuarioService.Object, _emailService.Object);
        }

        /// <summary>
        /// Tenta logal com um e-mail e senha válido
        /// </summary>
        [Fact]
        public void LoginComSucesso()
        {
            string email = "email@email.com.br";
            string senha = "senha123";
            LoginModel model = new LoginModel(email, senha);
            var usuario = _usuarioService.Login(model);

            Assert.NotNull(usuario);
        }
        /// <summary>
        /// Tenta logar com um e-mail e senha inválido
        /// </summary>
        [Fact]
        public void LoginEmailSenhaInvalido()
        {
            string email = "emailqualqer@email.com.br";
            string senha = "senha123";
            LoginModel model = new LoginModel(email, senha);

            Assert.Throws<EmailSenhaInvalidoException>(() => _usuarioService.Login(model));
        }
    }
}
