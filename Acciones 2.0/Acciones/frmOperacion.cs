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
    public partial class frmOperacion : Formulario
    {
        private Label[] lblCampo;
        private TextBox[] txbCampo;
        private Button btnAceptar;
        private Button btnCancelar;
        private String uso;

        public frmOperacion(String r, int c, String u)
        {
            InitializeComponent();
            roll = r;
            uso = u;
            colums = c;
            if (uso == "Buscar")
            {
                if(roll == "acciones" || roll == "Reporte" || roll == "ventasReporte")
                    colums--;
                Sval[0] = "";
                Sval[1] = "";
                Sval[2] = "";
                Sval[3] = "";
                Sval[4] = "";
            }
        }
        public frmOperacion(String r, int c, String[] v, String u)
        {
            InitializeComponent();
            roll = r;
            colums = c;
            val = v;
            uso = u;
        }
        private void iniciaVentana()
        {
            int i;
            lblCampo = new Label[colums];
            txbCampo = new TextBox[colums];
            btnAceptar = new Button();
            btnCancelar = new Button();
            this.Controls.Add(btnAceptar);
            this.Controls.Add(btnCancelar);
            for (i = 0; i < colums; i++)
            {
                lblCampo[i] = new Label();
                this.Controls.Add(lblCampo[i]);
                lblCampo[i].AutoSize = true;
                lblCampo[i].Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                lblCampo[i].Location = new Point(10, i * 24 + 10);
                lblCampo[i].Name = "lblCampo" + i;
                lblCampo[i].Size = new Size(55, 13);
                txbCampo[i] = new TextBox();
                this.Controls.Add(txbCampo[i]);
                txbCampo[i].Location = new Point(68, i * 24 + 10);
                txbCampo[i].Name = "txbCampo" + i;
                txbCampo[i].Size = new Size(100, 20);
                txbCampo[i].TabIndex = i + 1;
            }
            if (roll == "clientes" || roll == "comprasClientes")
            {
                this.Text = uso + " Clientes";
                lblCampo[0].Text = "CURP";
                txbCampo[0].MaxLength = 10;
                lblCampo[1].Text = "Nombre";
                txbCampo[1].MaxLength = 20;
                lblCampo[2].Text = "Saldo";
            }
            if (roll == "acciones" || roll == "comprasAcciones")
            {
                this.Text = uso + " Acciones";
                lblCampo[0].Text = "Clave";
                txbCampo[0].MaxLength = 10;
                lblCampo[1].Text = "Stock";
                lblCampo[2].Text = "Precio";
                if (uso != "Buscar")
                {
                    lblCampo[3].Text = "Desc";
                    txbCampo[3].MaxLength = 50;
                }
            }
            if (roll == "Reporte" || roll == "ventasReporte")
            {
                this.Text = uso + " Compras";
                lblCampo[0].Text = "CURP";
                txbCampo[0].MaxLength = 10;
                lblCampo[1].Text = "Nombre";
                txbCampo[1].MaxLength = 20;
                lblCampo[2].Text = "Clave";
                txbCampo[2].MaxLength = 10;
                lblCampo[3].Text = "Cantidad";
                txbCampo[3].MaxLength = 10;
            }
            if (roll == "Acciones" && (uso == "Comprar" || uso == "Vender"))
            {
                this.Text = uso + roll;
                lblCampo[0].Text = "Cantidad";
            }
            if(uso == "Modificar")
                for (i = 0; i < colums; i++)
                    txbCampo[i].Text = val[i];
            btnAceptar.Location = new Point(13, i * 24 + 15);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 23);
            btnAceptar.TabIndex = 5;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += new EventHandler(this.btnAceptar_Click);

            btnCancelar.Location = new Point(101, i * 24 + 15);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += new EventHandler(this.btnCancelar_Click);
            i++;
            this.ClientSize = new Size(188, i * 24 + 20);
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (uso == "Agregar" || uso == "Modificar")
            {
                if (txbCampo[0].Text != "" && txbCampo[1].Text != "" && txbCampo[2].Text != "")
                {
                    if (roll == "clientes")
                    {
                        try
                        {
                            float sa = float.Parse(txbCampo[2].Text);
                            My_SQL c = new My_SQL();
                            int nq;
                            if (uso == "Agregar")
                                nq = c.hazNoConsulta("insert into clientes values('" + txbCampo[0].Text.ToUpper() + "','" + txbCampo[1].Text + "','" + txbCampo[2].Text + "')");
                            if (uso == "Modificar")
                                nq = c.hazNoConsulta("update clientes set curp='" + txbCampo[0].Text.ToUpper() + "',nom='" + txbCampo[1].Text + "',saldo='" + txbCampo[2].Text + "' where curp='" + val[0] + "'");
                            this.Close();
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
                    if (roll == "acciones")
                    {
                        try
                        {
                            int st = int.Parse(txbCampo[1].Text);
                            float p = float.Parse(txbCampo[2].Text);
                            My_SQL a = new My_SQL();
                            int nq;
                            if (uso == "Agregar")
                                nq = a.hazNoConsulta("insert into acciones values('" + txbCampo[0].Text.ToUpper() + "','" + txbCampo[1].Text + "','" + txbCampo[2].Text + "','" + txbCampo[3].Text + "')");
                            if (uso == "Modificar")
                                nq = a.hazNoConsulta("update acciones set clave='" + txbCampo[0].Text.ToUpper() + "',stock='" + txbCampo[1].Text + "',precio='" + txbCampo[2].Text + "',descripcion='" + txbCampo[3].Text + "' where clave='" + val[0] + "'");
                            this.Close();
                        }
                        catch (FormatException formato2)
                        {
                            MessageBox.Show("Uno de los campos contiene datos inválidos " + formato2.ToString());
                        }
                        catch (Exception exc2)
                        {
                            MessageBox.Show(exc2.ToString());
                        }
                    }
                }
                else
                    MessageBox.Show("No se admiten campos vacios");
            }
            if (uso == "Buscar")
            {
                if (txbCampo[0].Text != "")
                    Sval[0] = txbCampo[0].Text.ToUpper();
                if (roll == "clientes" || roll == "comprasClientes")
                {
                    if (txbCampo[1].Text != "")
                        Sval[1] = txbCampo[1].Text;
                    if (txbCampo[2].Text != "")
                    {
                        try
                        {
                            float s1 = float.Parse(txbCampo[2].Text);
                            Sval[2] = s1.ToString();
                        }
                        catch (FormatException formato31)
                        {
                            MessageBox.Show("El saldo debe ser de tipo flotante " + formato31.ToString());
                        }
                        catch (Exception exc31)
                        {
                            MessageBox.Show(exc31.ToString());
                        }
                    }
                }
                if (roll == "acciones")
                {
                    if (txbCampo[1].Text != "")
                    {
                        try
                        {
                            int s1 = int.Parse(txbCampo[1].Text);
                            Sval[1] = s1.ToString();
                        }
                        catch (FormatException formato11)
                        {
                            MessageBox.Show("El stock debe ser de tipo entero " + formato11.ToString());
                        }
                        catch (Exception exc11)
                        {
                            MessageBox.Show(exc11.ToString());
                        }
                    }
                    if (txbCampo[2].Text != "")
                    {
                        try
                        {
                            float p1 = float.Parse(txbCampo[2].Text);
                            Sval[2] = p1.ToString();
                        }
                        catch (FormatException formato21)
                        {
                            MessageBox.Show("El precio debe ser de tipo flotante " + formato21.ToString());
                        }
                        catch (Exception exc21)
                        {
                            MessageBox.Show(exc21.ToString());
                        }
                    }
                }
                if (roll == "Reporte" || roll == "ventasReporte")
                {
                    if (txbCampo[1].Text != "")
                        Sval[1] = txbCampo[1].Text;
                    if (txbCampo[2].Text != "")
                        Sval[2] = txbCampo[2].Text;
                    if (txbCampo[3].Text != "")
                    {
                        try
                        {
                            int s11 = int.Parse(txbCampo[3].Text);
                            Sval[3] = s11.ToString();
                        }
                        catch (FormatException formato61)
                        {
                            MessageBox.Show("La cantidad debe ser de tipo entero " + formato61.ToString());
                        }
                        catch (Exception exc61)
                        {
                            MessageBox.Show(exc61.ToString());
                        }
                    }
                }
                this.Close();
            }
            if (uso == "Comprar" || uso == "Vender")
            {
                if (txbCampo[0].Text != "")
                {
                    try
                    {
                        if (uso == "Comprar")
                            movimiento(int.Parse(txbCampo[0].Text), "select * from clientes where curp='" + Scurp + "'", "select * from acciones where clave='" + val[0] + "'");
                        else
                            movimiento(int.Parse(txbCampo[0].Text), "select * from clientes where curp='" + val[0] + "'", "select * from acciones where clave='" + val[2] + "'");
                    }
                    catch (FormatException formato41)
                    {
                        MessageBox.Show("La cantidad debe ser de tipo entero " + formato41.ToString());
                    }
                    catch (Exception exc41)
                    {
                        MessageBox.Show(exc41.ToString());
                    }
                }
                else
                    MessageBox.Show("Debe introducir una Cantidad");
            }
        }
        private void movimiento(int cant,String c, String a)
        {
            int i, nq;
            My_SQL buy;
            OdbcDataReader res;


            buy = new My_SQL();
            res = buy.hazConsulta(c);
            String[] cliente = new String[3];
            if (res.HasRows)
                while (res.Read())
                    for (i = 0; i < 3; i++)
                        cliente[i] = res.GetString(i);
            else
            {
                MessageBox.Show("El cliente" + val[0] + " dejo de existir");
                return;
            }
            buy = new My_SQL();
            res = buy.hazConsulta(a);
            String[] accion = new String[4];
            if (res.HasRows)
                while (res.Read())
                    for (i = 0; i < 4; i++)
                        accion[i] = res.GetString(i);
            else
            {
                MessageBox.Show("Las acciones" + val[2] + " dejaron de existir");
                return;
            }
            float saldo = float.Parse(cliente[2]);
            float precio = float.Parse(accion[2]);
            int stock = int.Parse(accion[1]);
            if (uso == "Comprar")
            {
                if (cant * precio > saldo)
                    MessageBox.Show(cliente[1] + " no tiene saldo Suficiente");
                else
                {
                    if (stock < cant)
                        MessageBox.Show(accion[0] + " no tiene acciones suficientes");
                    else
                    {
                        buy = new My_SQL();
                        nq = buy.hazNoConsulta("update clientes set saldo='" + (saldo - (precio * cant)).ToString() + "' where curp='" + cliente[0] + "'");
                        buy = new My_SQL();
                        nq = buy.hazNoConsulta("update acciones set stock='" + (stock - cant).ToString() + "' where clave='" + accion[0] + "'");
                        buy = new My_SQL();
                        res = buy.hazConsulta("select * from detalle where curp='" + cliente[0] + "' and clave='" + accion[0] + "'");
                        String[] detalle = new String[3];
                        if (res.HasRows)
                        {
                            while (res.Read())
                                for (i = 0; i < 3; i++)
                                    detalle[i] = res.GetString(i);
                            buy = new My_SQL();
                            nq = int.Parse(detalle[0]);
                            nq = buy.hazNoConsulta("update detalle set cant='" + (nq + cant).ToString() + "' where curp='" + cliente[0] + "' && clave='" + accion[0] + "'");
                        }
                        else
                        {
                            buy = new My_SQL();
                            nq = buy.hazNoConsulta("insert into detalle values('" + cant.ToString() + "','" + cliente[0] + "','" + accion[0] + "')");
                        }
                        frmGrafica reporte = new frmGrafica("Reporte", 5);
                        reporte.Show();
                        this.Close();
                    }
                }
            }
            else
            { 
                nq = int.Parse(val[4]);
                if (cant > nq)
                    MessageBox.Show(cliente[1] + " no tiene Acciones Suficientes");
                else
                {
                    buy = new My_SQL();
                    nq = buy.hazNoConsulta("update clientes set saldo='" + (saldo + (precio * cant)).ToString() + "' where curp='" + cliente[0] + "'");
                    buy = new My_SQL();
                    nq = buy.hazNoConsulta("update acciones set stock='" + (stock + cant).ToString() + "' where clave='" + accion[0] + "'");
                    nq = int.Parse(val[4]);
                    nq -= cant;
                    if (nq > 0)
                    {
                        buy = new My_SQL();
                        nq = buy.hazNoConsulta("update detalle set cant='" + nq.ToString() + "' where curp='" + cliente[0] + "' and clave='" + accion[0] + "'");
                    }
                    else
                    {
                        buy = new My_SQL();
                        nq = buy.hazNoConsulta("delete from detalle where curp='" + val[0] + "' and clave='" + val[2] + "'");
                    }
                    this.Close();
                }
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmAgregar_Load(object sender, EventArgs e)
        {
            iniciaVentana();
        }
    }
}
