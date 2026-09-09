using System;
using System.Data;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase singular: representa un producto del menú.
    /// </summary>
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Codigo { get; set; }
        public bool Disponible { get; set; }
        public string MensajeError { get; private set; }

        public Producto()
        {
            Disponible = true;
        }

        public Producto(int idProducto, int idCategoria, string nombre, decimal precio, string codigo, bool disponible)
        {
            IdProducto = idProducto;
            IdCategoria = idCategoria;
            Nombre = nombre;
            Precio = precio;
            Codigo = codigo;
            Disponible = disponible;
        }

        public bool Insertar()
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "INSERT INTO producto (idCategoria, nombre, precio, codigo, disponible) VALUES (@idCategoria, @nombre, @precio, @codigo, @disponible); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idCategoria", IdCategoria);
                        cmd.Parameters.AddWithValue("@nombre", Nombre);
                        cmd.Parameters.AddWithValue("@precio", Precio);
                        cmd.Parameters.AddWithValue("@codigo", Codigo);
                        cmd.Parameters.AddWithValue("@disponible", Disponible);
                        IdProducto = Convert.ToInt32(cmd.ExecuteScalar());
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
                    string sql = "UPDATE producto SET idCategoria = @idCategoria, nombre = @nombre, precio = @precio, codigo = @codigo, disponible = @disponible WHERE idProducto = @idProducto;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idCategoria", IdCategoria);
                        cmd.Parameters.AddWithValue("@nombre", Nombre);
                        cmd.Parameters.AddWithValue("@precio", Precio);
                        cmd.Parameters.AddWithValue("@codigo", Codigo);
                        cmd.Parameters.AddWithValue("@disponible", Disponible);
                        cmd.Parameters.AddWithValue("@idProducto", IdProducto);
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
                    string sql = "DELETE FROM producto WHERE idProducto = @idProducto;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idProducto", IdProducto);
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

        public bool Buscar(int idProducto)
        {
            try
            {
                using (SqlConnection cn = ConexionBD.ObtenerConexion())
                {
                    cn.Open();
                    string sql = "SELECT idProducto, idCategoria, nombre, precio, codigo, disponible FROM producto WHERE idProducto = @idProducto;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@idProducto", idProducto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                IdProducto = Convert.ToInt32(dr["idProducto"]);
                                IdCategoria = Convert.ToInt32(dr["idCategoria"]);
                                Nombre = dr["nombre"].ToString();
                                Precio = Convert.ToDecimal(dr["precio"]);
                                Codigo = dr["codigo"].ToString();
                                Disponible = Convert.ToBoolean(dr["disponible"]);
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
    }
}
