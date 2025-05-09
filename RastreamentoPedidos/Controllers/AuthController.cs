using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Usuario;
using RastreamentoPedido.Core.Repositories.Interface.IUsuarioRepository;
using RastreamentoPedido.WebApi.Core.Controllers;
using RastreamentoPedido.WebApi.Core.Identidade;
using RastreamentoPedidos.Model;



namespace RastreamentoPedidos.Controllers
{

    [Produces("application/json")]
    [Route("api/identidade")]
    // usar "auth-v1" para versionar a api
    [ApiExplorerSettings(GroupName = "auth-v1")]
    public class AuthController : MainController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(ILogger<AuthController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUsuarioRepository usuarioRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        /// <summary>
        /// Rota para criação de nova conta
        /// </summary>
        /// <param name="usuario">Objeto com os dados de cadastro do usuario</param>
        /// <returns>Retorna status code 200 caso dê certo o cadastro</returns>
        [HttpPost("nova-conta")]
        [AllowAnonymous]
        public async Task<IActionResult> NovaConta(UsuarioRegistro usuario)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            if (!usuario.ValidationResult.IsValid)
            {
                return CustomResponse(usuario.ValidationResult);
            }
            var user = new ApplicationUser
            {
                UserName = usuario.email,
                Email = usuario.email,
                statusUsser = ApplicationUser.StatusUsuario.OffLine
            };
            var result = await _userManager.CreateAsync(user, usuario.senha);
            if (result.Succeeded)
            {
                return CustomResponse(await GerarJWT(usuario.email));
            }
            foreach (var erro in result.Errors)
            {
                AdicionarErroProcessamento(erro.Description);
            }
            return CustomResponse();
        }

        /// <summary>
        /// Rota de autenticação
        /// </summary>
        /// <param name="usuario">Objeto com os parametros de nome de usuario e senha</param>
        /// <returns>Token da autenticacao</returns>
        /// <response code="200">Retorna os dados da autenticação</response>
        [HttpPost("autenticar")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioRespostaLogin), 200)]
        public async Task<IActionResult> Autenticar(UsuarioLogin usuario)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }
            if (!usuario.ValidationResult.IsValid)
            {
                return CustomResponse(usuario.ValidationResult);
            }
            var user = await _userManager.FindByNameAsync(usuario.Email);
            if (user == null)
            {
                return CustomResponse("Usuário não localizado");
            }

            if (user.EmailConfirmed)
            {
                return CustomResponse("E-mail não verificado.");
            }

            var usuarioLogado = await _usuarioRepository.CarregarPorId(user.idUsuario);

            if (!usuarioLogado.ativo)
            {
                return CustomResponse("O usuário se encontra inativo");
            }

            var result = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJWT(usuario.Email));
            }

            if (result.IsLockedOut)
            {
                return CustomResponse("Usuário temporariamente bloqueado.");
            }
            return CustomResponse("Usuário ou Senha incorretos.");
        }

        [HttpPost("mudar-senha")]
        [Authorize]
        [ProducesResponseType(typeof(UsuarioRespostaLogin), 200)]
        public async Task<IActionResult> MudarSenha(UsuarioSenha usuario)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            if (!usuario.ValidationResult.IsValid)
            {
                return CustomResponse(usuario.ValidationResult);
            }

            var user = await _userManager.FindByEmailAsync(usuario.Email);
            if (user == null)
            {
                return CustomResponse("Usuário não localizado");
            }

            var result = await _userManager.ChangePasswordAsync(user, usuario.SenhaAntiga, usuario.SenhaNova);

            if (result.Succeeded)
            {
                return CustomResponse(await GerarJWT(usuario.Email));
            }

            foreach (var erro in result.Errors)
            {
                AdicionarErroProcessamento(erro.Description);
            }

            return CustomResponse();
        }

        [HttpGet("todos")]
        [Authorize]
        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            IList<Usuario> lista = new List<Usuario>();

            var usuarios = await _userManager.Users
                            .OrderByDescending(u => u.statusUsser)
                            .ThenBy(u => u.nomeUsuario)
                            .AsNoTracking()
                            .ToListAsync();
            foreach (var usuario in usuarios)
            {
               lista.Add(new Usuario
               {
                   idUsuario = usuario.idUsuario,
                   nomeUsuario = usuario.nomeUsuario,
                   email = usuario.Email ?? string.Empty,              
               });
            }
            return lista;
        }


        private async Task<UsuarioRespostaLogin> GerarJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new DomainException("Usuário não localizado");
            var claims = await _userManager.GetClaimsAsync(user);
            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);
            var response = ObterRespostaToken(encodedToken, user, claims);

            return response;
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("idUsuario", user.idUsuario.ToString()));

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;

        }

        private string CodificarToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = AppSettings.Emissor,
                Audience = AppSettings.ValidoEm,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(AppSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            return tokenHandler.WriteToken(token);
        }
        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims) => new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(AppSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email ?? "",
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })

            }
        };
        private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}