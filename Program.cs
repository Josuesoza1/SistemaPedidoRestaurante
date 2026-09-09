using System;
using System.Windows.Forms;
using SistemaPedidoRestaurante.Loggin;

namespace SistemaPedidoRestaurante
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// Si ya tienes un Program.cs (el que crea Visual Studio por defecto),
        /// solo cambia "new Form1()" por "new FrmLogin()".
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
