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
    public partial class frmAccionesBuscar : Formulario
    {
        public frmAccionesBuscar()
        {
            InitializeComponent();
            Sval1 = "";
            Sval2 = "";
            Sval3 = "";
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txbClave.Text != "")
            {
                Sval1 = txbClave.Text.ToUpper();
            }
            if (txbStock.Text != "")
            {
                try
                {
                    int s = int.Parse(txbStock.Text);
                    Sval2 = s.ToString();
                }
                catch (FormatException formato1)
                {
                    MessageBox.Show("El saldo debe ser de tipo flotante " + formato1.ToString());
                }
                catch (Exception exc1)
                {
                    MessageBox.Show(exc1.ToString());
                }
                
            }
            if(txbPrecio.Text != "")
            {
                try
                {
                    float p = float.Parse(txbPrecio.Text);
                    Sval3 = p.ToString();
                }
                catch (FormatException formato2)
                {
                    MessageBox.Show("El saldo debe ser de tipo flotante " + formato2.ToString());
                }
                catch (Exception exc2)
                {
                    MessageBox.Show(exc2.ToString());
                }
            }
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
