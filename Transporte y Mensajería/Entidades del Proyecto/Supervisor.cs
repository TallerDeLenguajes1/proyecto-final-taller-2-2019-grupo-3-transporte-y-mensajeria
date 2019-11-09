using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public class Supervisor : Persona
    {
        //Atributos
        List<Vehiculo> vehiculos;

        //Setters and Getters
        public List<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }

        //Constructor
        protected Supervisor(int cuil, string nombre, string apellido, string direccion, string telefono) : base(cuil, nombre, apellido, direccion, telefono)
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
