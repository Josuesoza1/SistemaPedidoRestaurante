using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de facturas.
    /// </summary>
    public class Facturas : List<Factura>
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
                    string sql = "SELECT idFactura, idPedido, numeroFactura, fechaEmision, estado, subtotal, descuento, impuestos, propina, total FROM factura ORDER BY fechaEmision DESC;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Add(new Factura
                            {
                                IdFactura = Convert.ToInt32(dr["idFactura"]),
                                IdPedido = Convert.ToInt32(dr["idPedido"]),
                                NumeroFactura = dr["numeroFactura"].ToString(),
                                FechaEmision = Convert.ToDateTime(dr["fechaEmision"]),
                                Estado = dr["estado"].ToString(),
                                Subtotal = Convert.ToDecimal(dr["subtotal"]),
                                Descuento = Convert.ToDecimal(dr["descuento"]),
                                Impuestos = Convert.ToDecimal(dr["impuestos"]),
                                Propina = Convert.ToDecimal(dr["propina"]),
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
