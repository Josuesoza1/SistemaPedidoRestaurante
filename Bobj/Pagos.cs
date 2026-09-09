using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de pagos.
    /// </summary>
    public class Pagos : List<Pago>
    {
        public string MensajeError { get; private set; }

        public bool Listar()
        {
            return Cargar("SELECT idPago, idFactura, metodoDePago, monto, fechaPago FROM pago ORDER BY fechaPago DESC;", null);
        }

        /// <summary>
        /// Lista los pagos aplicados a una factura específica (útil para pagos divididos).
        /// </summary>
        public bool ListarPorFactura(int idFactura)
        {
            return Cargar("SELECT idPago, idFactura, metodoDePago, monto, fechaPago FROM pago WHERE idFactura = @idFactura;", idFactura);
        }

        private bool Cargar(string sql, int? idFactura)
        {
            try
            {
                Clear();
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        if (idFactura.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@idFactura", idFactura.Value);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Add(new Pago
                                {
                                    IdPago = Convert.ToInt32(dr["idPago"]),
                                    IdFactura = Convert.ToInt32(dr["idFactura"]),
                                    MetodoDePago = dr["metodoDePago"].ToString(),
                                    Monto = Convert.ToDecimal(dr["monto"]),
                                    FechaPago = Convert.ToDateTime(dr["fechaPago"])
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
