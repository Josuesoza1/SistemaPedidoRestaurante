using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaPedidoRestaurante.UIAdministracion;
using SistemaPedidoRestaurante.UICliente;


namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmMenuAdministracion : Form
    {
        public FrmMenuAdministracion()
        {
            InitializeComponent();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            FrmCategoria categoria = new FrmCategoria();

            categoria.Show();
            this.Hide();

        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FrmProducto productos= new FrmProducto();
            productos.Show();
            this.Hide();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            FrmRoles roles = new FrmRoles();
            roles.Show();
            this.Hide();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            FrmEmpleado empleados = new FrmEmpleado();
            empleados.Show();
            this.Hide();
        }

        private void FrmMenuAdministracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
