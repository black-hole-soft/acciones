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
    public partial class frmAcciones : Formulario
    {
        private DataGridView dgv;
        private String clave, stock, precio, desc;
        private int sel = -1;
        private int p = 0;

        public frmAcciones()
        {
            InitializeComponent();
        }
        private void PopulateDataGridView(String cond)
        {
            My_SQL mysql = new My_SQL();
            OdbcDataReader res = mysql.hazConsulta("select * from acciones" + cond);
            int i;
            String[] row = new String[4];
            if (res.HasRows)
            {
                while (res.Read())
                {
                    for (i = 0; i < 4; i++)
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
            frmAccionesAgregar ca = new frmAccionesAgregar();
            ca.ShowDialog();
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView("");
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                clave = (String)dgv.Rows[sel].Cells[0].Value;
                stock = (String)dgv.Rows[sel].Cells[1].Value;
                precio = (String)dgv.Rows[sel].Cells[2].Value;
                desc = (String)dgv.Rows[sel].Cells[3].Value;
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
            dgv.Name = "Acciones";
            dgv.ReadOnly = true;
            dgv.Size = new Size(423, 314);
            dgv.TabIndex = 1;
            dgv.CellClick += new DataGridViewCellEventHandler(this.dgv_CellClick);
            dgv.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            dgv.ColumnCount = 4;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);

            dgv.GridColor = Color.Black;
            dgv.RowHeadersVisible = false;

            dgv.Columns[0].Name = "Clave";
            dgv.Columns[1].Name = "Stock";
            dgv.Columns[2].Name = "Precio";
            dgv.Columns[3].Name = "Descripcion";
            dgv.Columns[2].DefaultCellStyle.Font = new Font(dgv.DefaultCellStyle.Font, FontStyle.Italic);

            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }
        private void frmAcciones_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            PopulateDataGridView("");
            if (dgv.Rows.Count > 0)
            {
                sel = 0;
                clave = (String)dgv.Rows[sel].Cells[0].Value;
                stock = (String)dgv.Rows[sel].Cells[1].Value;
                precio = (String)dgv.Rows[sel].Cells[2].Value;
                desc = (String)dgv.Rows[sel].Cells[3].Value;
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridView ob = (DataGridView)sender;
                clave = (String)ob.Rows[e.RowIndex].Cells[0].Value;
                stock = (String)ob.Rows[e.RowIndex].Cells[1].Value;
                precio = (String)ob.Rows[e.RowIndex].Cells[2].Value;
                desc = (String)ob.Rows[e.RowIndex].Cells[3].Value;
                sel = e.RowIndex;
            }
        }
        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            My_SQL mysql = new My_SQL();
            int nq = mysql.hazNoConsulta("delete from acciones where clave = '" + clave + "'");
            int row = e.RowIndex;
            if (row >= e.RowCount)
                row = e.RowCount - 1;
            if (row >= 0 && e.RowCount > 1)
            {
                DataGridView ob = (DataGridView)sender;
                clave = (String)ob.Rows[e.RowIndex].Cells[0].Value;
                stock = (String)ob.Rows[e.RowIndex].Cells[1].Value;
                precio = (String)ob.Rows[e.RowIndex].Cells[2].Value;
                desc = (String)ob.Rows[e.RowIndex].Cells[3].Value;
                sel = row;
            }
            MessageBox.Show("Registro Borrado");
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                My_SQL mysql = new My_SQL();
                int nq = mysql.hazNoConsulta("delete from acciones where clave = '" + clave + "'");
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                if (dgv.Rows.Count > 0)
                {
                    sel = dgv.SelectedRows[0].Index;
                    clave = (String)dgv.Rows[sel].Cells[0].Value;
                    stock = (String)dgv.Rows[sel].Cells[1].Value;
                    precio = (String)dgv.Rows[sel].Cells[2].Value;
                    desc = (String)dgv.Rows[sel].Cells[3].Value;
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
                frmAccionesModificar cm = new frmAccionesModificar(clave, stock, precio, desc);
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                clave = (String)dgv.Rows[sel].Cells[0].Value;
                stock = (String)dgv.Rows[sel].Cells[1].Value;
                precio = (String)dgv.Rows[sel].Cells[2].Value;
                desc = (String)dgv.Rows[sel].Cells[3].Value;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmAccionesBuscar cb = new frmAccionesBuscar();
            cb.ShowDialog();
            String b = "", c = "", s = "", p = "";
            if (Sval1 != "")
            {
                b = " where";
                c = " clave = '" + Sval1 + "'";
            }
            if (Sval2 != "")
            {
                if(c != "")
                    s = "&&";
                else
                    b = " where";
                s += " stock = '" + Sval2 + "'";
            }
            if (Sval3 != "")
            {
                if (c != "" || s != "")
                    p = "&&";
                else
                    b = " where";
                p += " precio = '" + Sval3 + "'";
            }
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView(b + c + s + p);
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                clave = (String)dgv.Rows[sel].Cells[0].Value;
                stock = (String)dgv.Rows[sel].Cells[1].Value;
                precio = (String)dgv.Rows[sel].Cells[2].Value;
                desc = (String)dgv.Rows[sel].Cells[3].Value;
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
                clave = (String)dgv.Rows[sel].Cells[0].Value;
                stock = (String)dgv.Rows[sel].Cells[1].Value;
                precio = (String)dgv.Rows[sel].Cells[2].Value;
                desc = (String)dgv.Rows[sel].Cells[3].Value;
            }
            else
                sel = -1;
        }
    }
}
