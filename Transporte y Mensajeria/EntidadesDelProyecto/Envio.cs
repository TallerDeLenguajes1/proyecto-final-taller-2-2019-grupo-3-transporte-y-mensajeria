using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Envio
    {
        //Atributos
        int idEnvio;
        DateTime fechaEnvio;
        Cliente emisor;
        Cliente receptor;
        Mercancia merc;
        Double precioFinal;

        public Double PrecioFinal
        {
            get
            {
                return precioFinal;
            }

            set
            {
                precioFinal = value;
            }
        }

        public int IdEnvio
        {
            get
            {
                return idEnvio;
            }

            set
            {
                idEnvio = value;
            }
        }

        public DateTime FechaEnvio
        {
            get
            {
                return fechaEnvio;
            }

            set
            {
                fechaEnvio = value;
            }
        }

        public Cliente Emisor
        {
            get
            {
                return emisor;
            }

            set
            {
                emisor = value;
            }
        }

        public Cliente Receptor
        {
            get
            {
                return receptor;
            }

            set
            {
                receptor = value;
            }
        }

        public Mercancia Merc
        {
            get
            {
                return merc;
            }

            set
            {
                merc = value;
            }
        }

        //Setters and Getters
        //public int IdEnvio { get => idEnvio; set => idEnvio = value; }
        //public DateTime FechaEnvio { get => fechaEnvio; set => fechaEnvio = value; }
        //public Cliente Emisor { get => emisor; set => emisor = value; }
        //public Cliente Receptor { get => receptor; set => receptor = value; }
        //public Mercancia Merc { get => merc; set => merc = value; }

        //Constructor
        public Envio(DateTime fechaEnvio, Cliente emisor, Cliente receptor, Mercancia merc)
        {
            this.fechaEnvio = fechaEnvio;
            this.emisor = emisor;
            this.receptor = receptor;
            this.merc = merc;
            precioFinal = CalcularPrecioEnvio();
        }

        public Envio(int idEnvio, DateTime fechaEnvio, Cliente emisor, Cliente receptor, Mercancia merc)
        {
            this.idEnvio = idEnvio;
            this.fechaEnvio = fechaEnvio;
            this.emisor = emisor;
            this.receptor = receptor;
            this.merc = merc;
            precioFinal = CalcularPrecioEnvio();
        }

        //Otros Metodos
        public double CalcularPrecioEnvio()
        {
            return merc.CalcularPrecioFinal();
        }

    }

}
