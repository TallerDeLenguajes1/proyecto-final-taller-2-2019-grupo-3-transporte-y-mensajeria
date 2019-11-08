using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_del_Proyecto
{
    public abstract class Persona
    {
        //Atributos
        int idPersona;
        int cuil;
        string nombre;
        string direccion;
        string telefono;

        //Setters and Getters
        public int IdPersona { get => idPersona; set => idPersona = value; }
        public int Cuil { get => cuil; set => cuil = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        //Constructor
        protected Persona( int cuil, string nombre, string direccion, string telefono)
        {
            this.cuil = cuil;
            this.nombre = nombre;
            this.direccion = direccion;
            this.telefono = telefono;
        }

        //Otros Metodos


    }
}
