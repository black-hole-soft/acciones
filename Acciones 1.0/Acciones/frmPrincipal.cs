using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acciones
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes c = new frmClientes();
            c.ShowDialog();
        }
        private void btnAcciones_Click(object sender, EventArgs e)
        {
            frmAcciones a = new frmAcciones();
            a.ShowDialog();
        }
        private void btnCompra_Click(object sender, EventArgs e)
        {

        }
        private void btnVenta_Click(object sender, EventArgs e)
        {

        }
    }
}
