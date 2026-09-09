using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa una mesa del restaurante.
    /// </summary>
    public class Mesa
    {
        public int IdMesa { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; }
        public string MensajeError { get; private set; }

        public Mesa()
        {
            Estado = "Disponible";
        }

        public Mesa(int idMesa, int numero, int capacidad, string estado)
        {
            IdMesa = idMesa;
            Numero = numero;
            Capacidad = capacidad;
            Estado = estado;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "INSERT INTO mesa (numero, capacidad, estado) VALUES (@numero, @capacidad, @estado); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@numero", Numero);
                        cmd.Parameters.AddWithValue("@capacidad", Capacidad);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        IdMesa = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = "UPDATE mesa SET numero = @numero, capacidad = @capacidad, estado = @estado WHERE idMesa = @idMesa;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@numero", Numero);
                        cmd.Parameters.AddWithValue("@capacidad", Capacidad);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        cmd.Parameters.AddWithValue("@idMesa", IdMesa);
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
                    string sql = "DELETE FROM mesa WHERE idMesa = @idMesa;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idMesa", IdMesa);
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

        public bool Buscar(int idMesa)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idMesa, numero, capacidad, estado FROM mesa WHERE idMesa = @idMesa;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idMesa", idMesa);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                IdMesa = Convert.ToInt32(dr["idMesa"]);
                                Numero = Convert.ToInt32(dr["numero"]);
                                Capacidad = Convert.ToInt32(dr["capacidad"]);
                                Estado = dr["estado"].ToString();
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
    }
}
