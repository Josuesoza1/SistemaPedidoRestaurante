using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmEmpleado : Form
    {
        private readonly EmpleadoBLL _empleadoBLL = new EmpleadoBLL();

        public FrmEmpleado()
        {
            InitializeComponent();
        }



        private void CargarDatos()
        {
            try
            {
                dgvEmpleados.AutoGenerateColumns = false;

                dgvEmpleados.Columns[0].DataPropertyName = "IdEmpleado";
                dgvEmpleados.Columns[1].DataPropertyName = "PrimerNombre";
                dgvEmpleados.Columns[2].DataPropertyName = "PrimerApellido";
                dgvEmpleados.Columns[3].DataPropertyName = "Cargo";
                dgvEmpleados.Columns[4].DataPropertyName = "Cedula";

                dgvEmpleados.DataSource = _empleadoBLL.ObtenerEmpleados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}