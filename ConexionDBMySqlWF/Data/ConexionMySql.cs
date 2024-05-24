using ConexionDBMySqlWF.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionDBMySqlWF.Data
{
    internal class ConexionMySql
    {
        string connectionString = "server=localhost;database=LaboratorioCRUD;user=root;password=";
        MySqlConnection connection;


        //constructor
        public ConexionMySql()
        {
            connection = new MySqlConnection(connectionString);
        }


        //funcion insertar con estos datos
        /*
         *  ID INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    FechaNacimiento DATE,
    Email VARCHAR(100),
    Saldo DECIMAL(10, 2),
    Activo BOOLEAN
         */
        public void Insertar(string nombre, string apellido, DateTime fechaNacimiento, string email, decimal saldo, bool activo)
        {
            try
            {
                string query = "INSERT INTO Usuarios (Nombre, Apellido, FechaNacimiento, Email, Saldo, Activo) VALUES (@Nombre, @Apellido, @FechaNacimiento, @Email, @Saldo, @Activo)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Saldo", saldo); //decimal.Parse(txtSaldo.Text)
                cmd.Parameters.AddWithValue("@Activo", activo);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        //funcion de actualizar
        public void Actualizar(int id, string nombre, string apellido, DateTime fechaNacimiento, string email, decimal saldo, bool activo)
        {
            try
            {
                string query = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Email = @Email, Saldo = @Saldo, Activo = @Activo WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Saldo", saldo);
                cmd.Parameters.AddWithValue("@Activo", activo);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        //funcion de leer todos los registros, retornando un data table
        public DataTable LeerTodos()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM Usuarios";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer los registros: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }


        //funcion de leer un registro por id
        public DataRow LeerPorId(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM Usuarios WHERE ID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ID", id);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                connection.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt.Rows[0];
        }




        //uso de la clase Usuario
        //insertar con un objeto de la clase Usuario
        public void Insertar(Usuario usr)
        {
            try
            {
                string query = "INSERT INTO Usuarios (Nombre, Apellido, FechaNacimiento, Email, Saldo, Activo) VALUES (@Nombre, @Apellido, @FechaNacimiento, @Email, @Saldo, @Activo)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Nombre", usr.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usr.Apellido);
                cmd.Parameters.AddWithValue("@FechaNacimiento", usr.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Email", usr.Email);
                cmd.Parameters.AddWithValue("@Saldo", usr.Saldo);
                cmd.Parameters.AddWithValue("@Activo", usr.Activo);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el registro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
          
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM Usuarios";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        (
                            id: reader.GetInt32("ID"),
                            nombre: reader.GetString("Nombre"),
                            apellido: reader.GetString("Apellido"),
                            fechaNacimiento: reader.GetDateTime("FechaNacimiento"),
                            email: reader.GetString("Email"),
                            saldo: reader.GetDecimal("Saldo"),
                            activo: reader.GetBoolean("Activo")
                        );

                        usuarios.Add(usuario);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return usuarios;
        }






    }
}
