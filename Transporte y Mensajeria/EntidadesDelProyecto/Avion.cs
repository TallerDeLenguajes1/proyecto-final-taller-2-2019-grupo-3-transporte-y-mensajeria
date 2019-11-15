using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Avion : Vehiculo
    {
        //Atributos

        //Setters and Getters

        //Constructor
        public Avion(string modelo, DateTime fechaCompra, double precioCompra, double aumento) : base(modelo, fechaCompra, precioCompra,aumento) { }

        //Otros Metodos
        public override double CalcularPrecio(double precioNeto, double unidad)
        {
            return precioNeto * ((Aumento / 100) + 1);
        }



    }

}
