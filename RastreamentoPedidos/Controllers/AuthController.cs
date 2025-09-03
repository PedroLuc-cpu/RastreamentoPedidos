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
using RastreamentoPedido.Core.ViewModels;
using RastreamentoPedido.WebApi.Core.Controllers;
using RastreamentoPedido.WebApi.Core.Identidade;
using RastreamentoPedidos.Model;



namespace RastreamentoPedidos.Controllers
{

    [Produces("application/json")]
    [Route("api/identidade")]
    [ApiExplorerSettings(GroupName = "auth-v1")]
    public class AuthController : MainController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(ILogger<AuthController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                UserName = usuario.NomeUsuario,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, usuario.Senha);
            if (result.Succeeded)
            {
                await MudarNivel(new MudarNivelViewModel
                {
                    Id = Guid.Parse(user.Id),
                    Nivel = Roles.Usuario
                });
                return CustomResponse(await GerarJWT(usuario.Email));
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
            var user = await _userManager.FindByEmailAsync(usuario.Email);
            if (user == null)
            {
                return CustomResponse("Usuário não localizado");
            }

            //if (!user.EmailConfirmed)
            //{
            //    return CustomResponse("E-mail não verificado.");
            //}

            var result = await _signInManager.PasswordSignInAsync(user, usuario.Senha, false, true);

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

        [HttpPost("sair")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UsuarioToken), 200)]
        public async Task<IActionResult> Sair()
        {
            await _signInManager.SignOutAsync();
            return CustomResponse("Usuário deslogado com sucesso");
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
        public async Task<IEnumerable<UsuarioComRoles>> ObterTodos()
        {
            IList<UsuarioComRoles> lista = new List<UsuarioComRoles>();

            var usuarios = await _userManager.Users
                            .OrderByDescending(u => u.StatusUser)
                            .ThenBy(u => u.UserName)
                            .AsNoTracking()
                            .ToListAsync();
            foreach (var usuario in usuarios)
            {
                lista.Add(new UsuarioComRoles
                {
                    Id = Guid.Parse(usuario.Id),
                    UserName = usuario.UserName ?? string.Empty,
                    DescricaoStatus = usuario.DescricaoStatus,
                    StatusUser = usuario.StatusUser,
                    Email = usuario.Email ?? string.Empty,
                });

                var roles = await _userManager.GetRolesAsync(usuario);

                if (roles != null && roles.Count > 0)
                {
                    if (roles.Contains(Roles.Administrador))
                    {
                        lista.Last().Role = Roles.Administrador;
                    }
                    else if (roles.Contains(Roles.Gerente))
                    {
                        lista.Last().Role = Roles.Gerente;
                    }
                    else if (roles.Contains(Roles.Transportadora))
                    {
                        lista.Last().Role = Roles.Transportadora;
                    }
                    else if (roles.Contains(Roles.Entregador))
                    {
                        lista.Last().Role = Roles.Entregador;
                    }
                    else
                    {
                        lista.Last().Role = Roles.Usuario;

                    }

                }

            }
            return lista;
        }

            /// <summary>
            /// Rota para listar todas as roles permitidas.
            /// Roles: Administrador
            /// </summary>
            /// <returns>Retorna uma lista contendo todas as roles registradas</returns>
            [HttpGet("listar-todas-roles")]
            [Authorize(Roles = Roles.Administrador)]
            [ProducesResponseType(typeof(IList<string>), 200)]
            public IActionResult ListarTodasRoles()
            {
                var roles = new List<string>
            {
                Roles.Administrador,
                Roles.Usuario,
                Roles.Gerente,
                Roles.Transportadora,
                Roles.Entregador
            };
                roles.Sort();
                return Ok(roles);
            }

            /// <summary>
            /// Rota para listar todas as roles permitidas.
            /// </summary>
            /// <returns>Retorna uma lista contendo as roles permitidas no cadastro de novos usuarios</returns>
            [HttpGet("listar-roles-cadastro")]
            [AllowAnonymous]
            [ProducesResponseType(typeof(IList<string>), 200)]
            public IActionResult ListarRolesCadastro()
            {
                var roles = new List<string>
            {
                Roles.Usuario,
                Roles.Transportadora,
                Roles.Entregador
            };
                return Ok(roles);
            }

            /// <summary>
            /// Rota para mudar o nível do usuário dentro do sistema
            /// Roles: Administrador
            /// </summary>
            /// <param name="viewModel">Objeto com os parametros para alteração</param>
            [HttpPost("mudar-nivel")]
            [Authorize(Roles = Roles.Administrador)]
            public async Task<IActionResult> MudarNivel(MudarNivelViewModel viewModel)
            {
                if (!ModelState.IsValid)
                {
                    return CustomResponse(ModelState);
                }
                if (!viewModel.ValidationResult.IsValid)
                {
                    return CustomResponse(viewModel.ValidationResult);
                }
                var user = await _userManager.FindByIdAsync(viewModel.Id.ToString());

                if (user == null)
                {
                    return CustomResponse("Id do usuário inválido");
                }

                var rolesAntigas = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, rolesAntigas);
                var rolesNovas = new List<string>();
                if (viewModel.Nivel == Roles.Administrador)
                {
                    rolesNovas.Add(Roles.Administrador);
                }
                else if (viewModel.Nivel == Roles.Gerente)
                {
                    rolesNovas.Add(Roles.Gerente);
                }
                else if (viewModel.Nivel == Roles.Transportadora)
                {
                    rolesNovas.Add(Roles.Transportadora);
                }
                else
                {
                    rolesNovas.Add(Roles.Entregador);
                }
                var result = await _userManager.AddToRolesAsync(user, rolesNovas);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return CustomResponse(result.Errors);

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