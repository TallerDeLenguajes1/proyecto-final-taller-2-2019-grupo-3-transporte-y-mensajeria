using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Furgoneta : Vehiculo
    {
        //Atributos
        double capacidadCarga;

        //Setters and Getters
       // public double CapacidadCarga { get => capacidadCarga; set => capacidadCarga = value; }

        //Constructor
        public Furgoneta(string modelo, DateTime fechaCompra, double precioCompra, double capacidadCarga, double aumento) : base(modelo, fechaCompra, precioCompra, aumento)
        {
            this.capacidadCarga = capacidadCarga;
        }

        public double CapacidadCarga
        {
            get
            {
                return capacidadCarga;
            }

            set
            {
                capacidadCarga = value;
            }
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
