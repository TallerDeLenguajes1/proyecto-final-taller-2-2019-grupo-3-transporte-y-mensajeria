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

        //Setters and Getters
        //public int IdMercancia { get => idMercancia; set => idMercancia = value; }
        //public string Contenido { get => contenido; set => contenido = value; }
        //public bool Asegurada { get => asegurada; set => asegurada = value; }
        //public static double AumSeguro { get => aumSeguro; set => aumSeguro = value; }
        //public bool LargoRecorrido { get => largoRecorrido; set => largoRecorrido = value; }
        //public double PrecioNeto { get => precioNeto; set => precioNeto = value; }
        //public List<Vehiculo> Vehiculos { get => vehiculos; set => vehiculos = value; }



        //Constructor
        public Mercancia()
        {

        }
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

        public Mercancia(int idMercancia, string contenido, bool asegurada, bool largoRecorrido, double aumSeguro, List<Vehiculo> vehiculos)
        {
            this.idMercancia = idMercancia;
            this.contenido = contenido;
            this.asegurada = asegurada;
            this.largoRecorrido = largoRecorrido;
            this.Vehiculos = vehiculos;
            this.aumSeguro = aumSeguro;
            this.precioNeto = CalcularPrecioNeto();
        }

        //Otros Metodos
        public void AsociarVehiculo(Vehiculo vehiculo)
        {
            Vehiculos.Add(vehiculo);
        }

        public void QuitarVehiculo(Vehiculo vehiculo)
        {
            Vehiculos.Remove(vehiculo);
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
