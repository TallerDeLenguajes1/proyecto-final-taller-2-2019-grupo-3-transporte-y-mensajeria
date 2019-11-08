using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public class Moto : Vehiculo
    {
        //Atributos
        int cilindrada;

        //Setters and Getters
        public int Cilindrada { get => cilindrada; set => cilindrada = value; }

        //Constructor
        protected Moto( string descripcion, DateTime fechaCompra, double precioCompra, int cilindrada) : base( descripcion, fechaCompra, precioCompra)
        {
            this.cilindrada = cilindrada;
        }

        //Otros Metodos
        public override double CalcularPrecio(double precioNeto, double peso)
        {
            if (peso > 300)
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
