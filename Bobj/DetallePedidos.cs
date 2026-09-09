using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de líneas de detalle de pedidos.
    /// </summary>
    public class DetallePedidos : List<DetallePedido>
    {
        public string MensajeError { get; private set; }

        public bool Listar()
        {
            return Cargar("SELECT idDetalle, idPedido, idProducto, cantidad, notasCocina, precioUnitario, subtotal, total FROM detallePedido;", null);
        }

        /// <summary>
        /// Lista solo las líneas de detalle de un pedido específico (uso más frecuente:
        /// armar la comanda o la factura de una orden puntual).
        /// </summary>
        public bool ListarPorPedido(int idPedido)
        {
            return Cargar("SELECT idDetalle, idPedido, idProducto, cantidad, notasCocina, precioUnitario, subtotal, total FROM detallePedido WHERE idPedido = @idPedido;", idPedido);
        }

        private bool Cargar(string sql, int? idPedido)
        {
            try
            {
                Clear();
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        if (idPedido.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idPedido", idPedido.Value);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Add(new DetallePedido
                                {
                                    IdDetalle = Convert.ToInt32(dr["idDetalle"]),
                                    IdPedido = Convert.ToInt32(dr["idPedido"]),
                                    IdProducto = Convert.ToInt32(dr["idProducto"]),
                                    Cantidad = Convert.ToInt32(dr["cantidad"]),
                                    NotasCocina = dr["notasCocina"] == DBNull.Value ? null : dr["notasCocina"].ToString(),
                                    PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]),
                                    Subtotal = Convert.ToDecimal(dr["subtotal"]),
                                    Total = Convert.ToDecimal(dr["total"])
                                });
                            }
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
