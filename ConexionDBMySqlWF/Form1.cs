using ConexionDBMySqlWF.Data;
using ConexionDBMySqlWF.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDBMySqlWF
{
    public partial class Form1 : Form
    {
        ConexionMySql Clscone = new ConexionMySql();
        Usuario usr = new Usuario();
        List<Usuario> todos;
        ClsCursor cursor1 = new ClsCursor();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            Clscone.Insertar(textBoxNombre.Text, textBoxApellido.Text, dateTimePickerFechaNacimiento.Value, textBoxEmail.Text, decimal.Parse(textBoxSaldo.Text), checkBox1.Checked);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Clscone.Actualizar(int.Parse(textBoxID.Text), textBoxNombre.Text, textBoxApellido.Text, dateTimePickerFechaNacimiento.Value, textBoxEmail.Text, decimal.Parse(textBoxSaldo.Text), checkBox1.Checked);
        }

        private void buttonBuscarById_Click(object sender, EventArgs e)
        {
            DataRow resp = Clscone.LeerPorId(int.Parse(textBoxID.Text));
            if (resp != null)
            {
                textBoxNombre.Text = resp["Nombre"].ToString();
                textBoxApellido.Text = resp["Apellido"].ToString();
                dateTimePickerFechaNacimiento.Value = (DateTime)resp["FechaNacimiento"];
                textBoxEmail.Text = resp["Email"].ToString();
                textBoxSaldo.Text = resp["Saldo"].ToString();
                checkBox1.Checked = (bool)resp["Activo"];
            }
            else
            {
                MessageBox.Show("No se encontro el registro");
            }
        }

        private void buttonInsertarModel_Click(object sender, EventArgs e)
        {
            //insertar usando la clase Usuario
            usr.Nombre = textBoxNombre.Text;
            usr.Apellido = textBoxApellido.Text;
            usr.FechaNacimiento = dateTimePickerFechaNacimiento.Value;
            usr.Email = textBoxEmail.Text;
            usr.Saldo = decimal.Parse(textBoxSaldo.Text);
            usr.Activo = checkBox1.Checked;
            Clscone.Insertar(usr);
        }

        private void buttonTodos_Click(object sender, EventArgs e)
        {
            todos = Clscone.ObtenerTodosLosUsuarios();
            if (todos.Count > 0)
            {
                cursor1.totalRegistros = todos.Count;
                cursor1.current = 0;
                MostrarRegistro();
            }
            else
            {
                MessageBox.Show("No hay registros");
            }
        }

        //funcion para mostrar el registro actual
        private void MostrarRegistro()
        {
            if (cursor1.current >= 0 && cursor1.current < cursor1.totalRegistros)
            {
                Usuario u = todos[cursor1.current];
                textBoxID.Text = u.ID.ToString();
                textBoxNombre.Text = u.Nombre;
                textBoxApellido.Text = u.Apellido;
                dateTimePickerFechaNacimiento.Value = u.FechaNacimiento;
                textBoxEmail.Text = u.Email;
                textBoxSaldo.Text = u.Saldo.ToString();
                checkBox1.Checked = u.Activo;
                //incrementar el cursor y validar que no se pase del total de registros
                cursor1.current++;
                if (cursor1.current >= cursor1.totalRegistros)
                {
                    cursor1.current = 0;
                    MessageBox.Show("Fin de los registros");
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            MostrarRegistro();
        }
    }
}
