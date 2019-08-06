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
    public partial class frmGrafica : Formulario
    {
        private Button[] btn;
        private DataGridView dgv;
        private int sel = -1, btns;

        public frmGrafica(String r, int c)
        {
            InitializeComponent();
            roll = r;
            colums = c;
        }
        private void inicializaVentana()
        {
            if (roll == "clientes" || roll == "comprasClientes")
            {
                if(roll=="clientes")
                {
                    btns = 6;
                    nombre = "Clientes";
                }
                else
                {
                    btns = 3;
                    nombre = "Compras Clientes";    
                }
                cam = new String[3];
                val = new String[3];
                cam[0] = "curp";
                cam[1] = "nom";
                cam[2] = "saldo";
            }
            if(roll == "acciones" || roll == "comprasAcciones")
            {
                if(roll == "acciones")
                {
                    btns = 5;
                    nombre = "Acciones";
                }
                else
                {
                    btns = 3;
                    nombre = "Compras Acciones";
                }
                cam = new String[4];
                val = new String[4];
                cam[0] = "clave";
                cam[1] = "stock";
                cam[2] = "precio";
                cam[3] = "descripcion";
            }
            if (roll == "Reporte" || roll == "ventasReporte")
            {
                if (roll == "Reporte")
                {
                    btns = 2;
                    nombre = "Reporte";
                }
                else 
                {
                    btns = 3;
                    nombre = "Ventas Acciones";
                }
                cam = new String[5];
                val = new String[5];
                cam[0] = "clientes.curp";
                cam[1] = "clientes.nom";
                cam[2] = "acciones.clave";
                cam[3] = "acciones.descripcion";
                cam[4] = "detalle.cant";
            }
            btn = new Button[btns];
            for (int i = 0; i < btns; i++)
            {
                btn[i] = new Button();
                btn[i].Location = new Point(55 * i + 12, 334);
                btn[i].Size = new Size(55, 23);
                btn[i].TabIndex = i;
                btn[i].UseVisualStyleBackColor = true;
                this.Controls.Add(btn[i]);
            }
            if (roll == "clientes" || roll == "acciones")
            {
                btn[0].Text = "Buscar";
                btn[1].Text = "Agregar";
                btn[2].Text = "Modificar";
                btn[3].Text = "Eliminar";
                btn[4].Text = "Actualizar";
                if (roll == "clientes")
                {
                    btn[5].Text = "Reporte";
                    btn[5].Click += new EventHandler(this.btnReporte_Click);
                }
                btn[0].Click += new EventHandler(this.btnBuscar_Click);
                btn[1].Click += new EventHandler(this.btnAgregar_Click);
                btn[2].Click += new EventHandler(this.btnModificar_Click);
                btn[3].Click += new EventHandler(this.btnEliminar_Click);
                btn[4].Click += new EventHandler(this.btnActualizar_Click);
            }
            if (roll == "comprasClientes" || roll == "comprasAcciones")
            {
                btn[0].Text = "Buscar";
                btn[2].Text = "Actualizar";
                btn[0].Click += new EventHandler(this.btnBuscar_Click);
                btn[2].Click += new EventHandler(this.btnActualizar_Click);
                if (roll == "comprasClientes")
                {
                    btn[1].Text = "Acciones";
                    btn[1].Click += new EventHandler(this.btnAcciones_Click);
                }
                else
                {
                    btn[1].Text = "Comprar";
                    btn[1].Click += new EventHandler(this.btnComprar_Click);
                }
            }
            if (roll == "Reporte" || roll == "ventasReporte")
            {
                btn[0].Text = "Buscar";
                btn[1].Text = "Actualizar";
                btn[0].Click += new EventHandler(this.btnBuscar_Click);
                btn[1].Click += new EventHandler(this.btnActualizar_Click);
                if (roll == "ventasReporte")
                {
                    btn[2].Text = "Vender";
                    btn[2].Click += new EventHandler(this.btnVender_Click);
                }
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
            dgv.ReadOnly = true;
            dgv.TabIndex = 6;
            dgv.ColumnCount = colums;
            dgv.CellClick += new DataGridViewCellEventHandler(this.dgv_CellClick);
            dgv.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgv_RowsRemoved);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            dgv.GridColor = Color.Black;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            this.Text = nombre;
            if (roll == "clientes" || roll == "comprasClientes")
            {
                this.ClientSize = new Size(352, 369);
                dgv.Size = new Size(323, 314);
                dgv.Columns[0].Name = "CURP";
                dgv.Columns[1].Name = "Nombre";
                dgv.Columns[2].Name = "Saldo";
                if (roll == "clientes")
                    dgv.AllowUserToDeleteRows = true;
                else
                    dgv.AllowUserToDeleteRows = false;
            }
            if(roll == "acciones" || roll == "comprasAcciones")
            {
                this.ClientSize = new Size(452, 369);
                dgv.Size = new Size(423, 314);
                dgv.Columns[0].Name = "Clave";
                dgv.Columns[1].Name = "Stock";
                dgv.Columns[2].Name = "Precio";
                dgv.Columns[3].Name = "Descripcion";
                if (roll == "acciones")
                    dgv.AllowUserToDeleteRows = true;
                else
                    dgv.AllowUserToDeleteRows = false;
            }
            if (roll == "Reporte" || roll == "ventasReporte")
            {
                this.ClientSize = new Size(552, 369);
                dgv.AllowUserToDeleteRows = false;
                dgv.Size = new Size(523, 314);
                dgv.Columns[0].Name = "CURP";
                dgv.Columns[1].Name = "Nombre";
                dgv.Columns[2].Name = "Clave";
                dgv.Columns[3].Name = "Descripcion";
                dgv.Columns[4].Name = "Cantidad";
            }
        }
        private void PopulateDataGridView(String cond)
        {
            My_SQL mysql = new My_SQL();
            OdbcDataReader res;
            if (roll == "clientes" || roll == "acciones")
                res = mysql.hazConsulta("select * from " + roll + "" + cond);
            else
            {
                if (roll == "comprasClientes")
                    res = mysql.hazConsulta("select * from clientes" + cond);
                else
                {
                    if(roll == "comprasAcciones")
                        res = mysql.hazConsulta("select * from acciones" + cond);
                    else
                        res = mysql.hazConsulta("select " + cam[0] + "," + cam[1] + "," + cam[2] + "," + cam[3] + "," + cam[4] + " from clientes,acciones,detalle where clientes.curp=detalle.curp and acciones.clave=detalle.clave" + cond);
                }
            }
            int i;
            String[] row = new String[colums];
            if (res.HasRows)
            {
                while (res.Read())
                {
                    for (i = 0; i < colums; i++)
                        row[i] = res.GetString(i);
                    dgv.Rows.Add(row);
                }
            }
        }
        private void frmClientes_Load(object sender, EventArgs e)
        {
            inicializaVentana();
            SetupDataGridView();
            PopulateDataGridView("");
            actualiza();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >=0)
            {
                DataGridView ob = (DataGridView)sender;
                for (int i = 0; i < colums; i++)
                    val[i] = (String)ob.Rows[e.RowIndex].Cells[i].Value;
                sel = e.RowIndex;
            }
        }
        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            My_SQL mysql;
            OdbcDataReader res;
            int nq;
            mysql = new My_SQL();
            res = mysql.hazConsulta("select * from detalle where " + cam[0] + "='" + val[0] + "'");
            if (res.HasRows)
                MessageBox.Show("El registro esta vinculado a una transaccion");
            else
            {
                mysql = new My_SQL();
                nq = mysql.hazNoConsulta("delete from " + roll + " where " + cam[0] + " = '" + val[0] + "'");
                int row = e.RowIndex;
                if (row >= e.RowCount)
                    row = e.RowCount - 1;
                if (row >= 0 && e.RowCount > 1)
                {
                    DataGridView ob = (DataGridView)sender;
                    for (int i = 0; i < colums; i++)
                        val[i] = (String)ob.Rows[e.RowIndex].Cells[i].Value;
                    sel = row;
                }
                MessageBox.Show("Registro Borrado");
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmOperacion b = new frmOperacion(roll, colums, "Buscar");
            b.ShowDialog();
            String[] c = new String[4];
            for(int i = 0; i < 4; i++)
                c[i] = "";
            if (roll == "Reporte" || roll == "ventasReporte")
            {
                int i;
                for ( i = 0; i < 3; i++ )
                    if (Sval[i] != "")
                        c[i] = " and " + cam[i] + " = '" + Sval[i] + "'";
                if(Sval[i] != "")
                    c[i] = " and " + cam[i + 1] + " = '" + Sval[i] + "'";
            }
            else
            {
                if (Sval[0] != "")
                {
                    c[0] = " where";
                    c[1] = " " + cam[0] + " = '" + Sval[0] + "'";
                }
                if (Sval[1] != "")
                {
                    if (c[1] != "")
                        c[2] = " and ";
                    else
                        c[0] = " where";
                    c[2] += " " + cam[1] + " = '" + Sval[1] + "'";
                }
                if (Sval[2] != "")
                {
                    if (c[1] != "" || c[2] != "")
                        c[3] = " and ";
                    else
                        c[0] = " where";
                    c[3] += " " + cam[2] + " = '" + Sval[2] + "'";
                }
            }
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView(c[0] + c[1] + c[2] + c[3]);
            actualiza();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmOperacion ca = new frmOperacion(roll, colums, "Agregar");
            ca.ShowDialog();
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView("");
            actualiza();
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                frmOperacion cm = new frmOperacion(roll, colums, val, "Modificar");
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                actualiza();
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                My_SQL mysql;
                OdbcDataReader res;
                int nq;
                mysql = new My_SQL();
                res = mysql.hazConsulta("select * from detalle where " + cam[0] + "='" + val[0] + "'");
                if (res.HasRows)
                    MessageBox.Show("El registro esta vinculado a una transaccion");
                else
                {
                    mysql = new My_SQL();
                    nq = mysql.hazNoConsulta("delete from " + roll + " where " + cam[0] + " = '" + val[0] + "'");
                    dgv.Hide();
                    SetupDataGridView();
                    PopulateDataGridView("");
                    actualiza();
                    MessageBox.Show("Registro Borrado");
                }
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            dgv.Hide();
            SetupDataGridView();
            PopulateDataGridView("");
            actualiza();
        }
        private void btnAcciones_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                Scurp = val[0];
                frmGrafica a = new frmGrafica("comprasAcciones", 4);
                a.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                actualiza();
            }
            else
                MessageBox.Show("Selecciona un Cliente");
        }
        private void btnComprar_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                frmOperacion cm = new frmOperacion("Acciones", 1, val, "Comprar");
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                actualiza();
            }
            else
                MessageBox.Show("Selecciona o agrega una Accion");
        }
        private void btnVender_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                frmOperacion cm = new frmOperacion("Acciones", 1, val, "Vender");
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                actualiza();
            }
            else
                MessageBox.Show("Selecciona o realiza una Compra");
        }
        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (sel >= 0)
            {
                frmReporte cm = new frmReporte(val);
                cm.ShowDialog();
                dgv.Hide();
                SetupDataGridView();
                PopulateDataGridView("");
                actualiza();
            }
            else
                MessageBox.Show("Selecciona un cliente");
        }
        private void actualiza()
        {
            if (dgv.Rows.Count > 0)
            {
                sel = dgv.SelectedRows[0].Index;
                for (int i = 0; i < colums; i++)
                    val[i] = (String)dgv.Rows[sel].Cells[i].Value;
            }
            else
                sel = -1;
        }
    }
}
