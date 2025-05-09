using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Usuario;
using RastreamentoPedido.Core.Repositories.Interface.IUsuarioRepository;


namespace RastreamentoPedidos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDapperContext _dapper;

        public UsuarioRepository(IDapperContext dapper)
        {
            _dapper = dapper;
        }
        public async Task<Usuario> CarregarPorId(int id)
        {
            Usuario usuario = new Usuario();
            using (var connection = _dapper.ConnectionCreate())
            {
                string sql = "SELECT \"idUsuario\", email, ativo, nome, senha, \"senhaConfirmacao\", funcao, bio FROM usuario WHERE \"idUsuario\" = @id";
                var registro = await connection.QueryFirstOrDefaultAsync(sql, new { idUsuario = id });
                if (registro != null)
                {
                    usuario.idUsuario = registro.idUsuario;
                    usuario.email = registro.login;
                    usuario.ativo = registro.ativo;
                    usuario.senha = registro.senha;
                    usuario.senhaConfirmacao = registro.senhaConfirmacao;
                    usuario.nomeUsuario = registro.nomeUsuario;
                    usuario.funcao = registro.funcao;
                    usuario.bio = registro.bio;
                }
                return usuario;
            }
        }
    }
}