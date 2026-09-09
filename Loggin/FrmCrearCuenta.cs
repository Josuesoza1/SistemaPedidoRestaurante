using SistemaPedidoRestaurante.Entities;
using SistemaPedidoRestaurante.Conexion;
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
        { /*
            if (string.IsNullOrEmpty(txt_Usuario.Text) || string.IsNullOrEmpty(txtContrasena.Text) || string.IsNullOrEmpty(txtPnombre.Text))
            {
                MessageBox.Show("Por favor, llena los campos obligatorios (Usuario, Contraseña y Primer Nombre).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtContrasena.Text != txt_ConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string tipoSeleccionado = "1";
                int idRol = (tipoSeleccionado == "Empleado") ? 1 : 2;

                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.NombreUsuario = txt_Usuario.Text;
                nuevoUsuario.Contrasena = txtContrasena.Text;
                nuevoUsuario.IdRol = idRol;

                if (!nuevoUsuario.Insertar())
                {
                    MessageBox.Show("Error al crear la credencial de usuario: " + nuevoUsuario.MensajeError, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tipoSeleccionado == "Empleado")
                {
                    Empleado nuevoEmpleado = new Empleado(
                        0,
                        txtPnombre.Text,
                        txtSnombre.Text,
                        txtPapellido.Text,
                        txtSapellido.Text,
                        txtTelefono.Text,
                        true,
                        nuevoUsuario.IdUsuario
                    );

                    if (nuevoEmpleado.Insertar())
                    {
                        MessageBox.Show("¡Empleado registrado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar empleado: " + nuevoEmpleado.MensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (tipoSeleccionado == "Cliente")
                {
                    Cliente nuevoCliente = new Cliente(
                        0,
                        txtPnombre.Text,
                        txtSnombre.Text,
                        txtPapellido.Text,
                        txtSapellido.Text,
                        txtCedula.Text,
                        txtTelefono.Text,
                        txtEmail.Text,
                        nuevoUsuario.IdUsuario
                    );

                    if (nuevoCliente.Insertar())
                    {
                        MessageBox.Show("¡Cuenta de cliente creada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FrmLogin loginForm = new FrmLogin();
                        loginForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error al registrar cliente: " + nuevoCliente.MensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } */
        }

        private void cmbTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
    }
}