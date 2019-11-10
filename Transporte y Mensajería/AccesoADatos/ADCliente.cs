using EntidadesDelProyecto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccesoADatos
{
    public class ABMAlumno
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        //Alta cliente
        public void AltaCliente(Cliente cliente)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Insert into clientes(cuil,nombre,apellido,direccion,telefono) values( @cuil, @nombre, @apellido, @direccion, @telefono)", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@cuil", cliente.Cuil);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        //Buscar cliente segun cuil
        public Cliente GetCliente(int cuil)
        {
            //Variables auxiliares
            Cliente nuevoCliente = null;
            int idCliente;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from clientes where cuil=" + cuil + "", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idCliente = Convert.ToInt32(dr[0]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(cuil, nombre, apellido, direccion, telefono);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return nuevoCliente;


        }


        //Devolver listado de clientes
        public List<Cliente> GetClientes()
        {
            //Variables auxiliares
            List<Cliente> ListClientes = new List<Cliente>();
            Cliente nuevoCliente;
            int cuil;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            //Consulta que devuelve todos los alumnos de la BD
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from clientes", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cuil = Convert.ToInt32(dr[1]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(cuil, nombre, apellido, direccion, telefono);
                    ListClientes.Add(nuevoCliente);
                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

            return ListClientes;
        }

        //Eliminar Cliente
        public void BajaCliente(int cuil)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("DELETE FROM clientes WHERE cuil = @cuil", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@cuil", cuil);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }
    }
}
