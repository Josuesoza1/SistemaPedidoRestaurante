using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.BLL;

namespace SistemaPedidoRestaurante.UIAdministracion
{
    public partial class FrmCategoria : Form
    {
        private readonly CategoriaBLL _categoriaBLL = new CategoriaBLL();

        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                dgvCategorias.AutoGenerateColumns = false;

                dgvCategorias.Columns[0].DataPropertyName = "IdCategoria";
                dgvCategorias.Columns[1].DataPropertyName = "Nombre";
                dgvCategorias.Columns[2].DataPropertyName = "Descripcion";

                dgvCategorias.DataSource = _categoriaBLL.ObtenerCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}