using System.Data.SqlClient;

namespace SistemaPedidoRestaurante.Conexion
{
    /// <summary>
    /// Capa Conexión: entrega una conexión nueva (cerrada) hacia la base de datos.
    /// Todas las clases del Bobj llaman a ConexionBD.ObtenerConexion() dentro de un "using".
    /// </summary>
    public static class ConexionBD
    {
        private static readonly string cadenaConexion =
        @"Server=(localdb)\MSSQLLocalDB;Database=RestaurantSM;User Id=2M6-SIS;Password=sasa;";
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
