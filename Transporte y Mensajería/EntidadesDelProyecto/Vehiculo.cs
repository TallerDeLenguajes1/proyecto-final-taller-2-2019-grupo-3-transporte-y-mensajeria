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
        string descripcion;
        DateTime fechaCompra;
        double precioCompra;
        static double aumento;

        //Setters and Getters
        public int IdVehiculo { get => idVehiculo; set => idVehiculo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaCompra { get => fechaCompra; set => fechaCompra = value; }
        public double PrecioCompra { get => precioCompra; set => precioCompra = value; }
        public static double Aumento { get => aumento; set => aumento = value; }

        //Constructor
        protected Vehiculo(string descripcion, DateTime fechaCompra, double precioCompra)
        {
            this.descripcion = descripcion;
            this.fechaCompra = fechaCompra;
            this.precioCompra = precioCompra;
        }

        //Otros Metodos
        public virtual double CalcularPrecio(double precioMercancia, double unidad) { return 0; }

    }
