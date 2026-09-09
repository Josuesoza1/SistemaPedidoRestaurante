using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa un pedido/orden. IdMesa es opcional (pedidos "Para llevar" no usan mesa).
    /// </summary>
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public int? IdMesa { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
        public string TipoPedido { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string MensajeError { get; private set; }

        public Pedido()
        {
            FechaHora = DateTime.Now;
            Estado = "Abierto";
            TipoPedido = "Local";
            Subtotal = 0;
            Total = 0;
        }

        public Pedido(int idPedido, int idCliente, int? idMesa, int idEmpleado, DateTime fechaHora,
            string estado, string tipoPedido, decimal subtotal, decimal total)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            IdMesa = idMesa;
            IdEmpleado = idEmpleado;
            FechaHora = fechaHora;
            Estado = estado;
            TipoPedido = tipoPedido;
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
                    string sql = @"INSERT INTO pedido (idCliente, idMesa, idEmpleado, fechaHora, estado, tipoPedido, subtotal, total)
                                   VALUES (@idCliente, @idMesa, @idEmpleado, @fechaHora, @estado, @tipoPedido, @subtotal, @total);
                                   SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        IdPedido = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = @"UPDATE pedido SET idCliente = @idCliente, idMesa = @idMesa, idEmpleado = @idEmpleado,
                                   fechaHora = @fechaHora, estado = @estado, tipoPedido = @tipoPedido, subtotal = @subtotal, total = @total
                                   WHERE idPedido = @idPedido;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        AgregarParametros(cmd);
                        cmd.Parameters.AddWithValue("@idPedido", IdPedido);
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
                    string sql = "DELETE FROM pedido WHERE idPedido = @idPedido;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idPedido", IdPedido);
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

        public bool Buscar(int idPedido)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idPedido, idCliente, idMesa, idEmpleado, fechaHora, estado, tipoPedido, subtotal, total FROM pedido WHERE idPedido = @idPedido;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idPedido", idPedido);
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
            cmd.Parameters.AddWithValue("@idCliente", IdCliente);
            cmd.Parameters.AddWithValue("@idMesa", (object)IdMesa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@idEmpleado", IdEmpleado);
            cmd.Parameters.AddWithValue("@fechaHora", FechaHora);
            cmd.Parameters.AddWithValue("@estado", Estado);
            cmd.Parameters.AddWithValue("@tipoPedido", TipoPedido);
            cmd.Parameters.AddWithValue("@subtotal", Subtotal);
            cmd.Parameters.AddWithValue("@total", Total);
        }

        private void CargarDesdeLector(SqlDataReader dr)
        {
            IdPedido = Convert.ToInt32(dr["idPedido"]);
            IdCliente = Convert.ToInt32(dr["idCliente"]);
            IdMesa = dr["idMesa"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["idMesa"]);
            IdEmpleado = Convert.ToInt32(dr["idEmpleado"]);
            FechaHora = Convert.ToDateTime(dr["fechaHora"]);
            Estado = dr["estado"].ToString();
            TipoPedido = dr["tipoPedido"].ToString();
            Subtotal = Convert.ToDecimal(dr["subtotal"]);
            Total = Convert.ToDecimal(dr["total"]);
        }
    }
}
