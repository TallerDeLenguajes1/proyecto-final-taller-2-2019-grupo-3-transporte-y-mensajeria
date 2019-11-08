using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public class Furgoneta : Vehiculo
    {
        //Atributos
        double capacidadCarga;

        //Setters and Getters
        public double CapacidadCarga { get => capacidadCarga; set => capacidadCarga = value; }

        //Constructor
        protected Furgoneta( string descripcion, DateTime fechaCompra, double precioCompra, double capacidadCarga) : base( descripcion, fechaCompra, precioCompra)
        {
            this.capacidadCarga = capacidadCarga;
        }

        //Otros Metodos
        public override double CalcularPrecio(double precioNeto, double volumen)
        {
            if (volumen > 1)
            {
                return precioNeto * ((Aumento / 100) + 1);
            }
            else
            {
                return precioNeto;
            }
        }




    }
}
