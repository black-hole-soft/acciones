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
    public partial class frmAccionesAgregar : Form
    {
        public frmAccionesAgregar()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txbClave.Text != "" && txbStock.Text != "" && txbPrecio.Text != "")
            {
                try
                {
                    float p = float.Parse(txbPrecio.Text);
                    int s = int.Parse(txbStock.Text);
                    My_SQL c = new My_SQL();
                    int nq = c.hazNoConsulta("insert into acciones values('" + txbClave.Text.ToUpper() + "','" + txbStock.Text + "','" + txbPrecio.Text + "','" + txbDesc.Text + "')");
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
