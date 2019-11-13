using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public abstract class Mercancia
    {
        //Atributos
        int idMercancia;
        string descripcion;
        bool asegurada;
        bool largoRecorrido;
        double precioNeto;
        List<Vehiculo> vehiculos;
        double aumSeguro;

        public int IdMercancia
        {
            get
            {
                return idMercancia;
            }

            set
            {
                idMercancia = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public bool Asegurada
        {
            get
            {
                return asegurada;
            }

            set
            {
                asegurada = value;
            }
        }

        public bool LargoRecorrido
        {
            get
            {
                return largoRecorrido;
            }

            set
            {
                largoRecorrido = value;
            }
        }

        public double PrecioNeto
        {
            get
            {
                return precioNeto;
            }

            set
            {
                precioNeto = value;
            }
        }

        public List<Vehiculo> Vehiculos
        {
            get
            {
                return vehiculos;
            }

            set
            {
                vehiculos = value;
            }
        }

        public double AumSeguro
        {
            get
            {
                return aumSeguro;
            }

            set
            {
                aumSeguro = value;
            }
        }

        //Setters and Getters
        //public int IdMercancia { get => idMercancia; set => idMercancia = value; }
        //public string Descripcion { get => descripcion; set => descripcion = value; }
        //public bool Asegurada { get => asegurada; set => asegurada = value; }
        //public static double AumSeguro { get => aumSeguro; set => aumSeguro = value; }
        //public bool LargoRecorrido { get => largoRecorrido; set => largoRecorrido = value; }
        //public double PrecioNeto { get => precioNeto; set => precioNeto = value; }
        //public List<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }



        //Constructor
        public Mercancia(string descripcion, bool asegurada, bool largoRecorrido, double aumSeguro)
        {
            this.descripcion = descripcion;
            this.asegurada = asegurada;
            this.largoRecorrido = largoRecorrido;
            this.precioNeto = CalcularPrecioNeto();
            this.Vehiculos = new List<Vehiculo>();
            this.aumSeguro = aumSeguro;
        }

        //Otros Metodos
        public void AsociarVehiculo(List<Vehiculo> VehiculosDisponibles)
        {
            if (largoRecorrido)
            {
                for (int i = 0; i < VehiculosDisponibles.Count(); i++)
                {
                    vehiculos.Add(VehiculosDisponibles[i]);
                }
            }
            else
            {
                vehiculos.Add(VehiculosDisponibles[1]);
            }
        }

        protected double PrecioMercanciaAsegurada(double precio)
        {
            if (asegurada)
            {
                return precio * ((aumSeguro / 100) + 1);
            }
            else
            {
                return precio;
            }
        }

        protected virtual double CalcularPrecioNeto() { return 0; }

        protected virtual double CalcularPrecioSegunVehiculos() { return 0; }

        public double CalcularPrecioFinal()
        {
            return CalcularPrecioSegunVehiculos();
        }












    }

}
