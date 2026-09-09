using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa un pago aplicado a una factura (una factura puede tener varios pagos, ej. pago dividido).
    /// </summary>
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdFactura { get; set; }
        public string MetodoDePago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MensajeError { get; private set; }

        public Pago()
        {
            FechaPago = DateTime.Now;
        }

        public Pago(int idPago, int idFactura, string metodoDePago, decimal monto, DateTime fechaPago)
        {
            IdPago = idPago;
            IdFactura = idFactura;
            MetodoDePago = metodoDePago;
            Monto = monto;
            FechaPago = fechaPago;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = @"INSERT INTO pago (idFactura, metodoDePago, monto, fechaPago)
                                   VALUES (@idFactura, @metodoDePago, @monto, @fechaPago);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        IdPago = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = @"UPDATE pago SET idFactura = @idFactura, metodoDePago = @metodoDePago, monto = @monto, fechaPago = @fechaPago
                                   WHERE idPago = @idPago;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@idPago", IdPago);
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
                    string sql = "DELETE FROM pago WHERE idPago = @idPago;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idPago", IdPago);
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

        public bool Buscar(int idPago)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idPago, idFactura, metodoDePago, monto, fechaPago FROM pago WHERE idPago = @idPago;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idPago", idPago);
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
            cmd.Parameters.AddWithValue("@idFactura", IdFactura);
            cmd.Parameters.AddWithValue("@metodoDePago", MetodoDePago);
            cmd.Parameters.AddWithValue("@monto", Monto);
            cmd.Parameters.AddWithValue("@fechaPago", FechaPago);
        }

        private void CargarDesdeLector(SqlDataReader dr)
        {
            IdPago = Convert.ToInt32(dr["idPago"]);
            IdFactura = Convert.ToInt32(dr["idFactura"]);
            MetodoDePago = dr["metodoDePago"].ToString();
            Monto = Convert.ToDecimal(dr["monto"]);
            FechaPago = Convert.ToDateTime(dr["fechaPago"]);
        }
    }
}
