using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa la factura emitida para un pedido (relación 1 a 1 con pedido).
    /// </summary>
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdPedido { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Propina { get; set; }
        public decimal Total { get; set; }
        public string MensajeError { get; private set; }

        public Factura()
        {
            FechaEmision = DateTime.Now;
            Estado = "Emitida";
            Subtotal = 0;
            Descuento = 0;
            Impuestos = 0;
            Propina = 0;
        }

        public Factura(int idFactura, int idPedido, string numeroFactura, DateTime fechaEmision, string estado,
            decimal subtotal, decimal descuento, decimal impuestos, decimal propina, decimal total)
        {
            IdFactura = idFactura;
            IdPedido = idPedido;
            NumeroFactura = numeroFactura;
            FechaEmision = fechaEmision;
            Estado = estado;
            Subtotal = subtotal;
            Descuento = descuento;
            Impuestos = impuestos;
            Propina = propina;
            Total = total;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = @"INSERT INTO factura (idPedido, numeroFactura, fechaEmision, estado, subtotal, descuento, impuestos, propina, total)
                                   VALUES (@idPedido, @numeroFactura, @fechaEmision, @estado, @subtotal, @descuento, @impuestos, @propina, @total);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        IdFactura = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = @"UPDATE factura SET idPedido = @idPedido, numeroFactura = @numeroFactura, fechaEmision = @fechaEmision,
                                   estado = @estado, subtotal = @subtotal, descuento = @descuento, impuestos = @impuestos,
                                   propina = @propina, total = @total
                                   WHERE idFactura = @idFactura;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@idFactura", IdFactura);
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
                    string sql = "DELETE FROM factura WHERE idFactura = @idFactura;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idFactura", IdFactura);
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

        public bool Buscar(int idFactura)
        {
            return BuscarPor("idFactura", idFactura);
        }

        /// <summary>
        /// Localiza la factura de un pedido específico (idPedido es UNIQUE en la tabla factura).
        /// </summary>
        public bool BuscarPorPedido(int idPedido)
        {
            return BuscarPor("idPedido", idPedido);
        }

        private bool BuscarPor(string columna, int valor)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = $"SELECT idFactura, idPedido, numeroFactura, fechaEmision, estado, subtotal, descuento, impuestos, propina, total FROM factura WHERE {columna} = @valor;";
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
            cmd.Parameters.AddWithValue("@idPedido", IdPedido);
            cmd.Parameters.AddWithValue("@numeroFactura", NumeroFactura);
            cmd.Parameters.AddWithValue("@fechaEmision", FechaEmision);
            cmd.Parameters.AddWithValue("@estado", Estado);
            cmd.Parameters.AddWithValue("@subtotal", Subtotal);
            cmd.Parameters.AddWithValue("@descuento", Descuento);
            cmd.Parameters.AddWithValue("@impuestos", Impuestos);
            cmd.Parameters.AddWithValue("@propina", Propina);
            cmd.Parameters.AddWithValue("@total", Total);
        }

        private void CargarDesdeLector(SqlDataReader dr)
        {
            IdFactura = Convert.ToInt32(dr["idFactura"]);
            IdPedido = Convert.ToInt32(dr["idPedido"]);
            NumeroFactura = dr["numeroFactura"].ToString();
            FechaEmision = Convert.ToDateTime(dr["fechaEmision"]);
            Estado = dr["estado"].ToString();
            Subtotal = Convert.ToDecimal(dr["subtotal"]);
            Descuento = Convert.ToDecimal(dr["descuento"]);
            Impuestos = Convert.ToDecimal(dr["impuestos"]);
            Propina = Convert.ToDecimal(dr["propina"]);
            Total = Convert.ToDecimal(dr["total"]);
        }
    }
}
