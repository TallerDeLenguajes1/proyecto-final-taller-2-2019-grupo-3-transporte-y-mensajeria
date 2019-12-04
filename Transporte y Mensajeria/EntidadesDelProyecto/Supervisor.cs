using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Supervisor : Persona
    {
        //Atributos
        List<Vehiculo> vehiculos;

        public List<Vehiculo> Vehiculos
        {
            get
            {
                return vehiculos;
            }

            set
            {
                vehiculos = value;
            }
        }

        //Setters and Getters
        // public List<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }

        //Constructor
        public Supervisor(int cuil, string nombre, string apellido, string direccion, string telefono) : base(cuil, nombre, apellido, direccion, telefono)
        {
            this.Vehiculos = new List<Vehiculo>();
        }

        public Supervisor(int idPersona, int cuil, string nombre, string apellido, string direccion, string telefono) : base(idPersona, cuil, nombre, apellido, direccion, telefono)
        {
            this.Vehiculos = new List<Vehiculo>();
        }

        //Otros Metodos
        public void AsignarVehiculo(Vehiculo vehiculo)
        {
            vehiculos.Add(vehiculo);
        }

    }

}
