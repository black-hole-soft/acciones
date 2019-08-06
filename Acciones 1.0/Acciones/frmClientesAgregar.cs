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
    public partial class frmClientesAgregar : Form
    {
        public frmClientesAgregar()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txbCURP.Text != "" && txbNombre.Text != "" && txbSaldo.Text != "")
            {
                try
                {
                    float saldo = float.Parse(txbSaldo.Text);
                    My_SQL c = new My_SQL();
                    int nq = c.hazNoConsulta("insert into clientes values('" + txbCURP.Text.ToUpper() + "','" + txbNombre.Text + "','" + txbSaldo.Text + "')");
                    this.Close();
                }
                catch (FormatException formato)
                {
                    MessageBox.Show("El saldo debe ser de tipo flotante " + formato.ToString());
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
            else
                MessageBox.Show("No se admiten campos vacios");
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
