using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Paquete : Mercancia
    {
        //Atributos
        double volumen;
        double precioM3;

        public double Volumen
        {
            get
            {
                return volumen;
            }

            set
            {
                volumen = value;
            }
        }

        public double PrecioM3
        {
            get
            {
                return precioM3;
            }

            set
            {
                precioM3 = value;
            }
        }

        //Setters and Getters
        //public double Volumen { get => volumen; set => volumen = value; }
        //public static double PrecioM3 { get => precioM3; set => precioM3 = value; }

        //Constructor
        public Paquete()
        {

        }
        public Paquete(string contenido, bool asegurada, bool largoRecorrido, double aumSeguro, double precioM3, double volumen) : base(contenido, asegurada, largoRecorrido, aumSeguro)
        {
            this.volumen = volumen;
            this.precioM3 = precioM3;
        }

        public Paquete(int idMercancia, string contenido, bool asegurada, bool largoRecorrido, double aumSeguro, double precioM3, double volumen) : base(idMercancia, contenido, asegurada, largoRecorrido, aumSeguro)
        {
            this.volumen = volumen;
            this.precioM3 = precioM3;
        }

        //Otros Metodos
        public override double CalcularPrecioNeto()
        {
            double aux = volumen * precioM3;
            return PrecioMercanciaAsegurada(aux);
        }

        public override double GetUnidad()
        {
            return this.volumen;
        }

        public override string ToString()
        {
            return "(Paquete) - " + Contenido;
        }
    }

}
