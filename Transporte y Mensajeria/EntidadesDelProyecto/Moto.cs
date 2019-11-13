﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Moto : Vehiculo
    {
        //Atributos
        int cilindrada;

        //Setters and Getters
        //public int Cilindrada { get => cilindrada; set => cilindrada = value; }

        //Constructor
        public Moto(string descripcion, DateTime fechaCompra, double precioCompra, int cilindrada, double aumento) : base(descripcion, fechaCompra, precioCompra, aumento)
        {
            this.cilindrada = cilindrada;
        }

        public int Cilindrada
        {
            get
            {
                return cilindrada;
            }

            set
            {
                cilindrada = value;
            }
        }

        //Otros Metodos
        public override double CalcularPrecio(double precioNeto, double peso)
        {
            if (peso > 300)
            {
                return precioNeto * ((Aumento / 100) + 1);
            }
            else
            {
                return precioNeto;
            }
        }




    }

}
