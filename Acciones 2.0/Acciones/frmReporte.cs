using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;

namespace Acciones
{
    public partial class frmReporte : Formulario
    {
        private DataGridView dgv;
        private Label txtTotal, total;
        public frmReporte()
        {
            InitializeComponent();
        }
        public frmReporte(String[] v)
        {
            InitializeComponent();
            val = v;
        }
        private void SetupDataGridView()
        {
            dgv = new DataGridView();
            this.Controls.Add(dgv);
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(13, 13);
            dgv.ReadOnly = true;
            dgv.TabIndex = 4;
            dgv.ColumnCount = 4;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            dgv.GridColor = Color.Black;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            
            this.Text = "Reporte Cliente " + val[1];
            this.ClientSize = new Size(552, 369);
            dgv.AllowUserToDeleteRows = false;
            dgv.Size = new Size(523, 314);
            dgv.Columns[0].Name = "Clave";
            dgv.Columns[1].Name = "Descripcion";
            dgv.Columns[2].Name = "Cantidad";
            dgv.Columns[3].Name = "Total";

            txtTotal = new Label();
            this.Controls.Add(txtTotal);
            txtTotal.AutoSize = true;
            txtTotal.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            txtTotal.Location = new Point(13, 330);
            txtTotal.Size = new Size(55, 13);
            txtTotal.Text = "Total";
            total = new Label();
            this.Controls.Add(total);
            total.AutoSize = true;
            total.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            total.Location = new Point(50, 330);
            total.Size = new Size(55, 13);
        }
        private void PopulateDataGridView(String cond)
        {
            My_SQL mysql = new My_SQL();
            OdbcDataReader res;
            res = mysql.hazConsulta("select " + cam[0] + "," + cam[1] + "," + cam[2] + "," + cam[3] + " from clientes,acciones,detalle where clientes.curp=detalle.curp and acciones.clave=detalle.clave and clientes.curp='" + val[0] + "'");
            int i;
            float acc,tot=0;
            String[] row = new String[4];
            if (res.HasRows)
            {
                while (res.Read())
                {
                    for (i = 0; i < 3; i++)
                        row[i] = res.GetString(i);
                    acc = float.Parse(res.GetString(i)) * int.Parse(row[2]);
                    tot += acc;
                    row[3] = acc.ToString();
                    dgv.Rows.Add(row);
                }
            }
            total.Text = tot.ToString();
        }
        private void frmReporte_Load(object sender, EventArgs e)
        {
            cam = new String[4];
            cam[0] = "acciones.clave";
            cam[1] = "acciones.descripcion";
            cam[2] = "detalle.cant";
            cam[3] = "acciones.precio";
            SetupDataGridView();
            PopulateDataGridView("");
        }
    }
}
