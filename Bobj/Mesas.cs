using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de mesas.
    /// </summary>
    public class Mesas : List<Mesa>
    {
        public string MensajeError { get; private set; }

        public bool Listar()
        {
            try
            {
                Clear();
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idMesa, numero, capacidad, estado FROM mesa ORDER BY numero;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Add(new Mesa
                            {
                                IdMesa = Convert.ToInt32(dr["idMesa"]),
                                Numero = Convert.ToInt32(dr["numero"]),
                                Capacidad = Convert.ToInt32(dr["capacidad"]),
                                Estado = dr["estado"].ToString()
                            });
                        }
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
    }
}
