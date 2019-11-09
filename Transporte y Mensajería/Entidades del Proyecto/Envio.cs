using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public class Envio
    {
        //Atributos
        int idEnvio;
        DateTime fechaEnvio;
        Cliente emisor;
        Cliente receptor;
        Mercancia merc;

        //Setters and Getters
        public int IdEnvio { get => idEnvio; set => idEnvio = value; }
        public DateTime FechaEnvio { get => fechaEnvio; set => fechaEnvio = value; }
        public Cliente Emisor { get => emisor; set => emisor = value; }
        public Cliente Receptor { get => receptor; set => receptor = value; }
        public Mercancia Merc { get => merc; set => merc = value; }

        //Constructor
        public Envio(int idEnvio, DateTime fechaEnvio, Cliente emisor, Cliente receptor, Mercancia merc)
        {
            this.idEnvio = idEnvio;
            this.fechaEnvio = fechaEnvio;
            this.emisor = emisor;
            this.receptor = receptor;
            this.merc = merc;
        }

        //Otros Metodos
        public double CalcularPrecioEnvio()
        {
            return merc.CalcularPrecioFinal();
        }

    }
}
