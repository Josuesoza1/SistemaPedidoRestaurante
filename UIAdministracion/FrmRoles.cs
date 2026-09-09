using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmRoles : Form
    {
        private readonly RolBLL _rolBLL = new RolBLL();

        public FrmRoles()
        {
            InitializeComponent();
        }



        private void CargarDatos()
        {
            try
            {
                
                dgvClientes.AutoGenerateColumns = false;

                
                dgvClientes.Columns[0].DataPropertyName = "IdRol";
                dgvClientes.Columns[1].DataPropertyName = "Nombre";
                dgvClientes.Columns[2].DataPropertyName = "Descripcion";
                
                dgvClientes.DataSource = _rolBLL.ObtenerRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de roles: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmRoles_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}