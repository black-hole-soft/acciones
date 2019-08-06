using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acciones
{
    public class Formulario : Form
    {
        public static String Sval1, Sval2, Sval3;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Formulario
            // 
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Name = "Formulario";
            this.Load += new System.EventHandler(this.Formulario_Load);
            this.ResumeLayout(false);

        }

        private void Formulario_Load(object sender, EventArgs e)
        {

        }
    }
}
