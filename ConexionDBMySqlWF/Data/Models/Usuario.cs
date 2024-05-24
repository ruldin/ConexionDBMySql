using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionDBMySqlWF.Data.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public decimal Saldo { get; set; }
        public bool Activo { get; set; }

        // Constructor sin parámetros
        public Usuario() { }

        // Constructor con parámetros
        public Usuario(int id, string nombre, string apellido, DateTime fechaNacimiento, string email, decimal saldo, bool activo)
        {
            ID = id;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
            Email = email;
            Saldo = saldo;
            Activo = activo;
        }

        // Método para obtener la edad del usuario
        public int ObtenerEdad()
        {
            int edad = DateTime.Now.Year - FechaNacimiento.Year;
            if (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear)
            {
                edad--;
            }
            return edad;
        }

        // Método para mostrar información del usuario
        public override string ToString()
        {
            return $"ID: {ID}, Nombre: {Nombre} {Apellido}, Fecha de Nacimiento: {FechaNacimiento.ToShortDateString()}, Email: {Email}, Saldo: {Saldo:C}, Activo: {Activo}";
        }
    }
}
