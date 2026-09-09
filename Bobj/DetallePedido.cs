using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa una línea de detalle (un producto) dentro de un pedido.
    /// </summary>
    public class DetallePedido
    {
        public int IdDetalle { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NotasCocina { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string MensajeError { get; private set; }

        public DetallePedido() { }

        public DetallePedido(int idDetalle, int idPedido, int idProducto, int cantidad, string notasCocina,
            decimal precioUnitario, decimal subtotal, decimal total)
        {
            IdDetalle = idDetalle;
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
            NotasCocina = notasCocina;
            PrecioUnitario = precioUnitario;
            Subtotal = subtotal;
            Total = total;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = @"INSERT INTO detallePedido (idPedido, idProducto, cantidad, notasCocina, precioUnitario, subtotal, total)
                                   VALUES (@idPedido, @idProducto, @cantidad, @notasCocina, @precioUnitario, @subtotal, @total);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        IdDetalle = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = @"UPDATE detallePedido SET idPedido = @idPedido, idProducto = @idProducto, cantidad = @cantidad,
                                   notasCocina = @notasCocina, precioUnitario = @precioUnitario, subtotal = @subtotal, total = @total
                                   WHERE idDetalle = @idDetalle;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@idDetalle", IdDetalle);
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
                    string sql = "DELETE FROM detallePedido WHERE idDetalle = @idDetalle;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idDetalle", IdDetalle);
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

        public bool Buscar(int idDetalle)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idDetalle, idPedido, idProducto, cantidad, notasCocina, precioUnitario, subtotal, total FROM detallePedido WHERE idDetalle = @idDetalle;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idDetalle", idDetalle);
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
            cmd.Parameters.AddWithValue("@idProducto", IdProducto);
            cmd.Parameters.AddWithValue("@cantidad", Cantidad);
            cmd.Parameters.AddWithValue("@notasCocina", (object)NotasCocina ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@precioUnitario", PrecioUnitario);
            cmd.Parameters.AddWithValue("@subtotal", Subtotal);
            cmd.Parameters.AddWithValue("@total", Total);
        }

        private void CargarDesdeLector(SqlDataReader dr)
        {
            IdDetalle = Convert.ToInt32(dr["idDetalle"]);
            IdPedido = Convert.ToInt32(dr["idPedido"]);
            IdProducto = Convert.ToInt32(dr["idProducto"]);
            Cantidad = Convert.ToInt32(dr["cantidad"]);
            NotasCocina = dr["notasCocina"] == DBNull.Value ? null : dr["notasCocina"].ToString();
            PrecioUnitario = Convert.ToDecimal(dr["precioUnitario"]);
            Subtotal = Convert.ToDecimal(dr["subtotal"]);
            Total = Convert.ToDecimal(dr["total"]);
        }
    }
}
