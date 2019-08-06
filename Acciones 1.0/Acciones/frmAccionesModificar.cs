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
    public partial class frmAccionesModificar : Form
    {
        private String clave;
        public frmAccionesModificar(String c, String n, String s, String d)
        {
            InitializeComponent();
            clave = c;
            txbClave.Text = c;
            txbStock.Text = n;
            txbPrecio.Text = s;
            txbDesc.Text = d;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txbClave.Text != "" && txbStock.Text != "" && txbPrecio.Text != "")
            {
                try
                {
                    int s = int.Parse(txbStock.Text);
                    float p = float.Parse(txbPrecio.Text);
                    My_SQL a = new My_SQL();
                    int nq = a.hazNoConsulta("update acciones set clave='" + txbClave.Text.ToUpper() + "',stock='" + txbStock.Text + "',precio='" + txbPrecio.Text + "',descripcion='" + txbDesc.Text + "' where clave='" + clave + "'");
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
