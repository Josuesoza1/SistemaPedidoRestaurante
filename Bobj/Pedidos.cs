using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de pedidos.
    /// </summary>
    public class Pedidos : List<Pedido>
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
                    string sql = "SELECT idPedido, idCliente, idMesa, idEmpleado, fechaHora, estado, tipoPedido, subtotal, total FROM pedido ORDER BY fechaHora DESC;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Add(new Pedido
                            {
                                IdPedido = Convert.ToInt32(dr["idPedido"]),
                                IdCliente = Convert.ToInt32(dr["idCliente"]),
                                IdMesa = dr["idMesa"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["idMesa"]),
                                IdEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                                FechaHora = Convert.ToDateTime(dr["fechaHora"]),
                                Estado = dr["estado"].ToString(),
                                TipoPedido = dr["tipoPedido"].ToString(),
                                Subtotal = Convert.ToDecimal(dr["subtotal"]),
                                Total = Convert.ToDecimal(dr["total"])
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
