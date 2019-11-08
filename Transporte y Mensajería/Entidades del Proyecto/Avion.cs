using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public class Avion : Vehiculo
    {
        //Atributos

        //Setters and Getters
        
        //Constructor
        protected Avion( string descripcion, DateTime fechaCompra, double precioCompra) : base( descripcion, fechaCompra, precioCompra){ }

        //Otros Metodos
        public override double CalcularPrecio(double precioNeto, double unidad)
        {
            return precioNeto * ((Aumento / 100) + 1);
        }



    }
}
