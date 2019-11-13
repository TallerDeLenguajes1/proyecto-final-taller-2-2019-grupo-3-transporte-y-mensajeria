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
    public class ADCliente
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
                MessageBox.Show("Cliente agregado");
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        //Buscar cliente segun cuil
        public Cliente GetClientes(int cuil, string metodoBusqueda)
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
                cmd = new MySqlCommand("Select * from clientes where cuil=@cuil", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@cuil", cuil);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idCliente = Convert.ToInt32(dr[0]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(idCliente,cuil, nombre, apellido, direccion, telefono);

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

        //Buscar clientes segun Nombre Completo
        public List<Cliente> GetClientes(string nombreBuscado, string apellidoBuscado)
        {
            //Variables auxiliares
            List<Cliente> ListClientes = new List<Cliente>();
            Cliente nuevoCliente;
            int idCliente;
            int cuil;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            //string[] descomponer;
            //descomponer = nombreCompleto.Split(' ');
            //string nombreABuscar = descomponer[0];
            //string apellidoABuscar = descomponer[1];

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from clientes where nombre=@nombre and apellido=@apellido", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@nombre", nombreBuscado);
                cmd.Parameters.AddWithValue("@apellido", apellidoBuscado);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idCliente = Convert.ToInt32(dr[0]);
                    cuil = Convert.ToInt32(dr[1]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(idCliente, cuil, nombre, apellido, direccion, telefono);
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

        //Buscar clientes segun NOMBRE O APELLIDO
        public List<Cliente> GetClientes(string nombreOApellido)
        {
            //Variables auxiliares
            List<Cliente> ListClientes = new List<Cliente>();
            Cliente nuevoCliente;
            int idCliente;
            int cuil;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from clientes where nombre=@nombreOApellido or apellido=@nombreOApellido", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@nombreOApellido", nombreOApellido);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idCliente = Convert.ToInt32(dr[0]);
                    cuil = Convert.ToInt32(dr[1]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(idCliente, cuil, nombre, apellido, direccion, telefono);
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

        //Devolver listado de clientes
        public List<Cliente> GetClientes()
        {
            //Variables auxiliares
            List<Cliente> ListClientes = new List<Cliente>();
            Cliente nuevoCliente;
            int idCliente;
            int cuil;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from clientes", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idCliente = Convert.ToInt32(dr[0]);
                    cuil = Convert.ToInt32(dr[1]);
                    nombre = dr[2].ToString();
                    apellido = dr[3].ToString();
                    direccion = dr[4].ToString();
                    telefono = dr[5].ToString();
                    nuevoCliente = new Cliente(idCliente,cuil, nombre, apellido, direccion, telefono);
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

        //Modificar Cliente
        public void ModificacionCliente(Cliente cliente)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE clientes SET cuil=@cuil, nombre=@nombre, apellido=@apellido, direccion=@direccion, telefono=@telefono WHERE cuil=@cuil", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@cuil", cliente.Cuil);
                cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
                MessageBox.Show("Cliente modificado");
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
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
                MessageBox.Show("Cliente eliminado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }
    }
}
