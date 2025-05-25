using System;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int IdUsuario {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Usuario()
        {
        }

        public Usuario(string nombre, string apellido, string cedula, string telefono, string direccion, DateTime fechaNacimiento)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cedula = cedula;
            Telefono = telefono;
            Direccion = direccion;
            FechaNacimiento = fechaNacimiento;
        }



        

    }
}
