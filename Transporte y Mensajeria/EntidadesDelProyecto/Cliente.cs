﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesDelProyecto
{
    public class Cliente : Persona
    {
        //Atributos

        //Setters and Getters

        //Constructor
        public Cliente(int cuil, string nombre, string apellido, string direccion, string telefono) : base(cuil, nombre, apellido, direccion, telefono) { }
        public Cliente(int idPersona, int cuil, string nombre, string apellido, string direccion, string telefono) : base(idPersona, cuil, nombre, apellido, direccion, telefono) { }
        public Cliente() : base() { }

        //Otros Metodos

    }

}
