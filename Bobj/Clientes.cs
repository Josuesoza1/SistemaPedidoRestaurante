using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de clientes.
    /// </summary>
    public class Clientes : List<Cliente>
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
                    string sql = "SELECT idCliente, PNombreCl, SNombreCl, PApellidoCl, SApellidoCl, cedula, telefono, email, idUsuario FROM cliente ORDER BY PApellidoCl, PNombreCl;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Add(new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["idCliente"]),
                                PNombreCl = dr["PNombreCl"].ToString(),
                                SNombreCl = dr["SNombreCl"] == DBNull.Value ? null : dr["SNombreCl"].ToString(),
                                PApellidoCl = dr["PApellidoCl"].ToString(),
                                SApellidoCl = dr["SApellidoCl"] == DBNull.Value ? null : dr["SApellidoCl"].ToString(),
                                Cedula = dr["cedula"] == DBNull.Value ? null : dr["cedula"].ToString(),
                                Telefono = dr["telefono"] == DBNull.Value ? null : dr["telefono"].ToString(),
                                Email = dr["email"] == DBNull.Value ? null : dr["email"].ToString(),
                                IdUsuario = dr["idUsuario"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["idUsuario"])
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
