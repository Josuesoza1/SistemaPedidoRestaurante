using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa a un cliente del restaurante. IdUsuario es opcional
    /// (un cliente puede existir sin cuenta de acceso, por ejemplo si el pedido lo registra un empleado).
    /// </summary>
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string PNombreCl { get; set; }
        public string SNombreCl { get; set; }
        public string PApellidoCl { get; set; }
        public string SApellidoCl { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? IdUsuario { get; set; }
        public string MensajeError { get; private set; }

        public Cliente() { }

        public Cliente(int idCliente, string pNombreCl, string sNombreCl, string pApellidoCl, string sApellidoCl,
            string cedula, string telefono, string email, int? idUsuario)
        {
            IdCliente = idCliente;
            PNombreCl = pNombreCl;
            SNombreCl = sNombreCl;
            PApellidoCl = pApellidoCl;
            SApellidoCl = sApellidoCl;
            Cedula = cedula;
            Telefono = telefono;
            Email = email;
            IdUsuario = idUsuario;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = @"INSERT INTO cliente (PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, cedula, telefono, email, idUsuario)
                                   VALUES (@pNombreCl, @sNombreCl, @pApellidoCl, @sApellidoCl, @cedula, @telefono, @email, @idUsuario);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        IdCliente = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return false;
            }
        }

        public bool Modificar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = @"UPDATE cliente SET PNombreCl = @pNombreCl, SNombreCl = @sNombreCl, PApellidoCl = @pApellidoCl,
                                   SApellidoCl = @sApellidoCl, cedula = @cedula, telefono = @telefono, email = @email, idUsuario = @idUsuario
                                   WHERE idCliente = @idCliente;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@idCliente", IdCliente);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return false;
            }
        }

        public bool Eliminar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "DELETE FROM cliente WHERE idCliente = @idCliente;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", IdCliente);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return false;
            }
        }

        public bool Buscar(int idCliente)
        {
            return BuscarPor("idCliente", idCliente);
        }

        /// <summary>
        /// Localiza al cliente asociado a una cuenta de usuario (útil tras el login).
        /// </summary>
        public bool BuscarPorIdUsuario(int idUsuario)
        {
            return BuscarPor("idUsuario", idUsuario);
        }

        private bool BuscarPor(string columna, int valor)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = $"SELECT idCliente, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, cedula, telefono, email, idUsuario FROM cliente WHERE {columna} = @valor;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@valor", valor);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                CargarDesdeLector(dr);
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                return false;
            }
        }

        private void AgregarParametros(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@pNombreCl", PNombreCl);
            cmd.Parameters.AddWithValue("@sNombreCl", (object)SNombreCl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pApellidoCl", PApellidoCl);
            cmd.Parameters.AddWithValue("@sApellidoCl", (object)SApellidoCl ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@cedula", (object)Cedula ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@telefono", (object)Telefono ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@email", (object)Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idUsuario", (object)IdUsuario ?? DBNull.Value);
        }

        private void CargarDesdeLector(SqlDataReader dr)
        {
            IdCliente = Convert.ToInt32(dr["idCliente"]);
            PNombreCl = dr["PNombreCl"].ToString();
            SNombreCl = dr["SNombreCl"] == DBNull.Value ? null : dr["SNombreCl"].ToString();
            PApellidoCl = dr["PApellidoCl"].ToString();
            SApellidoCl = dr["SApellidoCl"] == DBNull.Value ? null : dr["SApellidoCl"].ToString();
            Cedula = dr["cedula"] == DBNull.Value ? null : dr["cedula"].ToString();
            Telefono = dr["telefono"] == DBNull.Value ? null : dr["telefono"].ToString();
            Email = dr["email"] == DBNull.Value ? null : dr["email"].ToString();
            IdUsuario = dr["idUsuario"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["idUsuario"]);
        }
    }
}
