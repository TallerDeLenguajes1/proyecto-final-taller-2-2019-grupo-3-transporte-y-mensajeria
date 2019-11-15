using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public abstract class Vehiculo
    {
        //Atributos
        int idVehiculo;
        string modelo;
        DateTime fechaCompra;
        double precioCompra;
        double aumento;

        public int IdVehiculo
        {
            get
            {
                return idVehiculo;
            }

            set
            {
                idVehiculo = value;
            }
        }

        public string Modelo
        {
            get
            {
                return modelo;
            }

            set
            {
                modelo = value;
            }
        }

        public DateTime FechaCompra
        {
            get
            {
                return fechaCompra;
            }

            set
            {
                fechaCompra = value;
            }
        }

        public double PrecioCompra
        {
            get
            {
                return precioCompra;
            }

            set
            {
                precioCompra = value;
            }
        }

        public  double Aumento
        {
            get
            {
                return aumento;
            }

            set
            {
                aumento = value;
            }
        }

        //Setters and Getters
        //public int IdVehiculo { get => idVehiculo; set => idVehiculo = value; }
        //public string Modelo { get => modelo; set => modelo = value; }
        //public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        //public double PrecioCompra { get => precioCompra; set => precioCompra = value; }
        //public static double Aumento { get => aumento; set => aumento = value; }

        //Constructor
        public Vehiculo(string modelo, DateTime fechaCompra, double precioCompra, double aumento)
        {
            this.modelo = modelo;
            this.fechaCompra = fechaCompra;
            this.precioCompra = precioCompra;
            this.aumento = aumento;
        }

        //Otros Metodos
        public virtual double CalcularPrecio(double precioMercancia, double unidad) { return 0; }

    }
}
