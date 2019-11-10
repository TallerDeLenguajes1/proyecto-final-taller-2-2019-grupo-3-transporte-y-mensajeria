using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Paquete : Mercancia
    {
        //Atribtos
        double volumen;
        static double precioM3;

        //Setters and Getters
        public double Volumen { get => volumen; set => volumen = value; }
        public static double PrecioM3 { get => precioM3; set => precioM3 = value; }

        //Constructor
        protected Paquete(string descripcion, bool asegurada, bool largoRecorrido, double volumen) : base( descripcion, asegurada, largoRecorrido)
        {
            this.volumen = volumen;
        }

        //Otros Metodos
        protected override double CalcularPrecioNeto()
        {
            double aux = volumen * precioM3;
            return PrecioMercanciaAsegurada(aux);
        }

        protected override double CalcularPrecioSegunVehiculos()
        {
            double suma = 0;
            for (int i = 0; i < Vehiculos.Count(); i++)
            {
                suma += Vehiculos[i].CalcularPrecio(PrecioNeto, volumen);
            }
            return suma;
        }
    }

}
