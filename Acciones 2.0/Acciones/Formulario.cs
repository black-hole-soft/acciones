using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acciones
{
    public class Formulario : Form
    {
        public static String[] Sval = new String[5];
        public static String Scurp;
        public String[] cam, val;
        public String roll, nombre;
        public int colums;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
