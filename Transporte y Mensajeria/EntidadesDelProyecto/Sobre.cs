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
        double precioGramo;

        public double Peso
        {
            get
            {
                return peso;
            }

            set
            {
                peso = value;
            }
        }

        public double PrecioGramo
        {
            get
            {
                return precioGramo;
            }

            set
            {
                precioGramo = value;
            }
        }

        //Setters and Getters
        //public double Peso { get => peso; set => peso = value; }
        //public static double PrecioGramo { get => precioGramo; set => precioGramo = value; }

        //Constructor
        public Sobre(string contenido, bool asegurada, bool largoRecorrido, double aumSeguro, double precioGramo, double peso) : base(contenido, asegurada, largoRecorrido, aumSeguro)
        {
            this.peso = peso;
            this.precioGramo = precioGramo;
        }

        public Sobre(int idMercancia, string contenido, bool asegurada, bool largoRecorrido, double aumSeguro, double precioGramo, double peso) : base(idMercancia, contenido, asegurada, largoRecorrido, aumSeguro)
        {
            this.peso = peso;
            this.precioGramo = precioGramo;
        }

        //Otros Metodos
        public override double CalcularPrecioNeto()
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

        public override string ToString()
        {
            return "(Sobre) - " + Contenido;
        }
    }

}
