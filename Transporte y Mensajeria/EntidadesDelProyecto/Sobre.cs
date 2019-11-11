using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Sobre : Mercancia
    {
        //Atribtos
        double peso;
        static double precioGramo;

        //Setters and Getters
        public double Peso { get => peso; set => peso = value; }
        public static double PrecioGramo { get => precioGramo; set => precioGramo = value; }

        //Constructor
        protected Sobre(string descripcion, bool asegurada, bool largoRecorrido, double peso) : base(descripcion, asegurada, largoRecorrido)
        {
            this.peso = peso;
        }

        //Otros Metodos
        protected override double CalcularPrecioNeto()
        {
            double aux = peso * precioGramo;
            return PrecioMercanciaAsegurada(aux);
        }

        protected override double CalcularPrecioSegunVehiculos()
        {
            double suma = 0;
            for (int i = 0; i < Vehiculos.Count(); i++)
            {
                suma += Vehiculos[i].CalcularPrecio(PrecioNeto, peso);
            }
            return suma;
        }
    }

}
