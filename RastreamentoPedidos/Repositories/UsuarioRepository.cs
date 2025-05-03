using Dapper;
using RastreamentoPedidos.Data.Interface;
using RastreamentoPedidos.Model.Usuario;
using RastreamentoPedidos.Repositories.Interface.IUsuarioRepository;

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
                string sql = "SELECT \"idUsuario\", login, ativo, nome, senha, bio FROM usuario WHERE \"idUsuario\" = @id";
                var registro = await connection.QueryFirstOrDefaultAsync(sql, new { idUsuario = id });
                if (registro != null)
                {
                    usuario.idUsuario = registro.idUsuario;
                    usuario.login = registro.login;
                    usuario.ativo = registro.ativo;
                    usuario.senha = registro.senha;
                    usuario.bio = registro.bio;
                }
                return usuario;
            }
        }
    }
}