using Dapper;
using Datos.Interfaces;
using Microsoft.SqlServer.Management.Smo;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private string CadenaConexion;
        public UsuarioRepositorio (string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }
    private MySqlConnection Conexion()
    {
        return new MySqlConnection(CadenaConexion);
    }
    public Task<bool> Nuevo(Usuario usuario)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ValidarUsuario(Login login)
    {
        bool valido = false;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT 1 FROM usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña= @Contraseña;";
            valido = await conexion.ExecuteScalarAsync<bool>(sql, new { login.NombreUsuario, login.Contraseña });
        }
        catch (Exception ex)
        {
        }
        return valido;
    }
}
