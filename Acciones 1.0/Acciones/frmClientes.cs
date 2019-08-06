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
    public partial class frmClientes : Formulario
    {
        private DataGridView dgv;
        private String curp, nombre, saldo;
        private int sel = -1;
        private int p = 0;

        public frmClientes()
        {
            InitializeComponent();
        }
        private void PopulateDataGridView(String cond)
        {
            My_SQL mysql = new My_SQL();
            OdbcDataReader res = mysql.hazConsulta("select * from clientes" + cond);
            int i;
            String[] row = new String[3];
            if (res.HasRows)
            {
                while (res.Read())
                {
                    for (i = 0; i < 3; i++)
                    {
                        row[i] = res.GetString(i);
                    }
                    dgv.Rows.Add(row);
                    p++;
                }
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmClientesAgregar ca = new frmClientesAgregar();
            ca.ShowDialog();
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView("");
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                curp = (String)dgv.Rows[sel].Cells[0].Value;
                nombre = (String)dgv.Rows[sel].Cells[1].Value;
                saldo = (String)dgv.Rows[sel].Cells[2].Value;
            }
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
            dgv.Name = "Clientes";
            dgv.ReadOnly = true;
            dgv.Size = new Size(323, 314);
            dgv.TabIndex = 1;
            dgv.CellClick += new DataGridViewCellEventHandler(this.dgv_CellClick);
            dgv.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            dgv.ColumnCount = 3;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);

            dgv.GridColor = Color.Black;
            dgv.RowHeadersVisible = false;

            dgv.Columns[0].Name = "CURP";
            dgv.Columns[1].Name = "Nombre";
            dgv.Columns[2].Name = "Saldo";
            dgv.Columns[2].DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Italic);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }
        private void frmClientes_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            PopulateDataGridView("");
            if (dgv.Rows.Count > 0)
            {
                sel = 0;
                curp = (String)dgv.Rows[sel].Cells[0].Value;
                nombre = (String)dgv.Rows[sel].Cells[1].Value;
                saldo = (String)dgv.Rows[sel].Cells[2].Value;
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0)
            {
                DataGridView ob = (DataGridView)sender;
                curp = (String)ob.Rows[e.RowIndex].Cells[0].Value;
                nombre = (String)ob.Rows[e.RowIndex].Cells[1].Value;
                saldo = (String)ob.Rows[e.RowIndex].Cells[2].Value;
                sel = e.RowIndex;
            }
        }
        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            My_SQL mysql = new My_SQL();
            int nq = mysql.hazNoConsulta("delete from clientes where curp = '" + curp + "'");
            int row = e.RowIndex;
            if (row >= e.RowCount)
                row = e.RowCount - 1;
            if (row >= 0 && e.RowCount > 1)
            {
                DataGridView ob = (DataGridView)sender;
                curp = (String)ob.Rows[e.RowIndex].Cells[0].Value;
                nombre = (String)ob.Rows[e.RowIndex].Cells[1].Value;
                saldo = (String)ob.Rows[e.RowIndex].Cells[2].Value;
                sel = row;
            }
            MessageBox.Show("Registro Borrado");
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                My_SQL mysql = new My_SQL();
                int nq = mysql.hazNoConsulta("delete from clientes where curp = '" + curp + "'");
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                if (dgv.Rows.Count > 0)
                {
                    sel = dgv.SelectedRows[0].Index;
                    curp = (String)dgv.Rows[sel].Cells[0].Value;
                    nombre = (String)dgv.Rows[sel].Cells[1].Value;
                    saldo = (String)dgv.Rows[sel].Cells[2].Value;
                }
                else
                    sel = -1;
                MessageBox.Show("Registro Borrado");
            }
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                frmClientesModificar cm = new frmClientesModificar(curp, nombre, saldo);
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                sel = dgv.SelectedRows[0].Index;
                curp = (String)dgv.Rows[sel].Cells[0].Value;
                nombre = (String)dgv.Rows[sel].Cells[1].Value;
                saldo = (String)dgv.Rows[sel].Cells[2].Value;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmClientesBuscar cb = new frmClientesBuscar();
            cb.ShowDialog();
            String b = "", c = "", n = "", s = "";
            if (Sval1 != "")
            {
                b = " where";
                c = " curp = '" + Sval1 + "'";
            }
            if (Sval2 != "")
            {
                if(c != "")
                    n = "&&";
                else
                    b = " where";
                n += " nom = '" + Sval2 + "'";
            }
            if (Sval3 != "")
            {
                if (c != "" || n != "")
                    s = "&&";
                else
                    b = " where";
                s += " saldo = '" + Sval3 + "'";
            }
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView(b + c + n + s);
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                curp = (String)dgv.Rows[sel].Cells[0].Value;
                nombre = (String)dgv.Rows[sel].Cells[1].Value;
                saldo = (String)dgv.Rows[sel].Cells[2].Value;
            }
            else
                sel = -1;
        }
        private void btnActualiza_Click(object sender, EventArgs e)
        {
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView("");
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                curp = (String)dgv.Rows[sel].Cells[0].Value;
                nombre = (String)dgv.Rows[sel].Cells[1].Value;
                saldo = (String)dgv.Rows[sel].Cells[2].Value;
            }
            else
                sel = -1;
        }
    }
}
