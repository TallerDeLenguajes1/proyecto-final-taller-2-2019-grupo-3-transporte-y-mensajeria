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

        /// <summary>
        /// El paquete NLog permite registrar datos de interes.
        /// </summary>
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar el alta de un Cliente.
        /// </summary>
        /// <param name="cliente"></param>
        public void AltaCliente(Cliente cliente)
        {
            using (conexion.retornarCN())
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
                    MessageBox.Show("Error, no se pudo dar de alta al Cliente, Ya existe");
                    Logger.Error("Error de alta de Cliente {0}", ex.ToString());
                }
            }

        }

        /// <summary>
        /// Metodo para Buscar un cliente segun el su nro de CUIL.
        /// </summary>
        /// <param name="cuil"></param>
        /// <returns></returns>
        public Cliente GetClientes(int cuil)
        {
            //Variables auxiliares
            Cliente nuevoCliente = null;
            int idCliente;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            using (conexion.retornarCN())
            {
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
                        nuevoCliente = new Cliente(idCliente, cuil, nombre, apellido, direccion, telefono);

                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return nuevoCliente;
        }

        /// <summary>
        /// Metodo para buscar clientes segun Nombre Completo.
        /// </summary>
        /// <param name="nombreBuscado"></param>
        /// <param name="apellidoBuscado"></param>
        /// <returns></returns>
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

            using (conexion.retornarCN())
            {
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
                    Logger.Error("Error Buscar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListClientes;
        }

        /// <summary>
        ///  Metodo para buscar clientes segun NOMBRE O APELLIDO.
        /// </summary>
        /// <param name="nombreOApellido"></param>
        /// <returns></returns>
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

            using (conexion.retornarCN())
            {
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
                    Logger.Error("Error Buscar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListClientes;
        }

        /// <summary>
        /// Metodo para devolver listado de clientes.
        /// </summary>
        /// <returns></returns>
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

            using (conexion.retornarCN())
            {
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
                        nuevoCliente = new Cliente(idCliente, cuil, nombre, apellido, direccion, telefono);
                        ListClientes.Add(nuevoCliente);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListClientes;
        }

        /// <summary>
        /// Metodo para modificar Cliente.
        /// </summary>
        /// <param name="cliente"></param>
        public void ModificacionCliente(Cliente cliente)
        {
            using (conexion.retornarCN())
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
                    Logger.Error("Error Modificar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error no se pudo modificar");
                }
            }
        }

        /// <summary>
        /// Metodo para Eliminar Cliente.
        /// </summary>
        /// <param name="cuil"></param>
        public void BajaCliente(int cuil)
        {
            using (conexion.retornarCN())
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
                    Logger.Error("Error Eliminar Cliente {0}", ex.ToString());
                    MessageBox.Show("Error no se pudo Eliminar");
                }
            }

        }
    }
}
