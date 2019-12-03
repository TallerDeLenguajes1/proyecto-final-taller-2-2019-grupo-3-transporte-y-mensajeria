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
        string contenido;//
        bool asegurada;//
        bool largoRecorrido;//
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

        public string Contenido
        {
            get
            {
                return contenido;
            }

            set
            {
                contenido = value;
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


        /// <summary>
        /// Metodo Construcor Mercancia.
        /// </summary>
        /// <param name="contenido"></param>
        /// <param name="asegurada"></param>
        /// <param name="largoRecorrido"></param>
        /// <param name="aumSeguro"></param>
        public Mercancia(string contenido, bool asegurada, bool largoRecorrido, double aumSeguro)
        {
            this.contenido = contenido;
            this.asegurada = asegurada;
            this.largoRecorrido = largoRecorrido;
            this.Vehiculos = new List<Vehiculo>();
            this.aumSeguro = aumSeguro;
            this.precioNeto = CalcularPrecioNeto();
        }

        public Mercancia(int idMercancia, string contenido, bool asegurada, bool largoRecorrido, double aumSeguro)
        {
            this.idMercancia = idMercancia;
            this.contenido = contenido;
            this.asegurada = asegurada;
            this.largoRecorrido = largoRecorrido;
            this.Vehiculos = new List<Vehiculo>();
            this.aumSeguro = aumSeguro;
            this.precioNeto = CalcularPrecioNeto();
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

        public virtual double CalcularPrecioNeto() { return 0; }
        /// <summary>
        /// Este metodo permite obtener la unidad ya sea el peso o volumen segun  sea la Mercancia.
        /// </summary>
        /// <returns></returns>
        public virtual double GetUnidad() { return 0; }
        public double CalcularPrecioSegunVehiculos()
        {
            double suma = 0;
            for (int i = 0; i < Vehiculos.Count(); i++)
            {
                suma += Vehiculos[i].CalcularPrecio(this.CalcularPrecioNeto(), this.GetUnidad());
            }
            return suma;
        }

        public double CalcularPrecioFinal()
        {
            return CalcularPrecioSegunVehiculos();
        }

    }

}
