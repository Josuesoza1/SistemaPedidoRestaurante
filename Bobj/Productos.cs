using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaPedidoRestaurante.Conexion;

namespace SistemaPedidoRestaurante.Bobj
{
    /// <summary>
    /// Clase plural: colección de productos.
    /// </summary>
    public class Productos : List<Producto>
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
                    string sql = "SELECT idProducto, idCategoria, nombre, precio, codigo, disponible FROM producto ORDER BY nombre;";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["idProducto"]),
                                IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                                Nombre = dr["nombre"].ToString(),
                                Precio = Convert.ToDecimal(dr["precio"]),
                                Codigo = dr["codigo"].ToString(),
                                Disponible = Convert.ToBoolean(dr["disponible"])
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
