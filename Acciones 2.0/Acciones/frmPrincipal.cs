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
            My_SQL mys = new My_SQL();
            InitializeComponent();
        }
        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmGrafica c = new frmGrafica("clientes",3);
            c.ShowDialog();
        }
        private void btnAcciones_Click(object sender, EventArgs e)
        {
            frmGrafica a = new frmGrafica("acciones",4);
            a.ShowDialog();
        }
        private void btnCompra_Click(object sender, EventArgs e)
        {
            frmGrafica a = new frmGrafica("comprasClientes", 3);
            a.ShowDialog();
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            frmGrafica reporte = new frmGrafica("ventasReporte", 5);
            reporte.ShowDialog();
        }
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
