using SistemaPedidoRestaurante.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPedidoRestaurante.Loggin
{
    public partial class FrmCrearCuenta : Form
    {
        public FrmCrearCuenta()
        {
            InitializeComponent();
        }

        private void btn_Salir(object sender, EventArgs e)
        {
            FrmLogin loginForm = new FrmLogin();
            loginForm.Show();
            this.Hide();
        }

        private void btnCrearCuenta(object sender, EventArgs e)
        { 

        }

        private void FrmCrearCuenta_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}