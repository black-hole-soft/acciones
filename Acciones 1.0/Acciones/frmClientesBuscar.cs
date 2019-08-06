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
    public partial class frmClientesBuscar : Formulario
    {
        public frmClientesBuscar()
        {
            InitializeComponent();
            Sval1 = "";
            Sval2 = "";
            Sval3 = "";
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txbCURP.Text != "")
            {
                Sval1 = txbCURP.Text.ToUpper();
            }
            if (txbNombre.Text != "")
            {
                Sval2 = txbNombre.Text;
            }
            if(txbSaldo.Text != "")
            {
                try
                {
                    float s = float.Parse(txbSaldo.Text);
                    Sval3 = s.ToString();
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
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
