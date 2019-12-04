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
    public class ADSupervisor
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar de Alta un Supervisor.
        /// </summary>
        /// <param name="supervisor"></param>
        public void AltaSupervisor(Supervisor supervisor)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Insert into supervisores(cuil,nombre,apellido,direccion,telefono) values( @cuil, @nombre, @apellido, @direccion, @telefono)", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@cuil", supervisor.Cuil);
                    cmd.Parameters.AddWithValue("@nombre", supervisor.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", supervisor.Apellido);
                    cmd.Parameters.AddWithValue("@direccion", supervisor.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", supervisor.Telefono);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de alta al Supervisor");
                }
            }

        }

        /// <summary>
        /// Metodo para buscar supervisor segun cuil.
        /// </summary>
        /// <param name="cuil"></param>
        /// <returns></returns>
        public Supervisor GetSupervisores(int cuil)
        {
            //Variables auxiliares
            Supervisor nuevoSupervisor = null;
            int idSupervisor;
            string nombre;
            string apellido;
            string direccion;
            string telefono;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Select * from supervisores where cuil=@cuil", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@cuil", cuil);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idSupervisor = Convert.ToInt32(dr[0]);
                        nombre = dr[2].ToString();
                        apellido = dr[3].ToString();
                        direccion = dr[4].ToString();
                        telefono = dr[5].ToString();
                        nuevoSupervisor = new Supervisor(idSupervisor, cuil, nombre, apellido, direccion, telefono);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return nuevoSupervisor;
        }

        /// <summary>
        /// Metodo para Buscar supervisores segun Nombre Completo
        /// </summary>
        /// <param name="nombreBuscado"></param>
        /// <param name="apellidoBuscado"></param>
        /// <returns></returns>
        public List<Supervisor> GetSupervisores(string nombreBuscado, string apellidoBuscado)
        {
            //Variables auxiliares
            List<Supervisor> ListSupervisores = new List<Supervisor>();
            Supervisor nuevoSupervisor;
            int idSupervisor;
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
                    cmd = new MySqlCommand("Select * from supervisores where nombre=@nombre and apellido=@apellido", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@nombre", nombreBuscado);
                    cmd.Parameters.AddWithValue("@apellido", apellidoBuscado);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idSupervisor = Convert.ToInt32(dr[0]);
                        cuil = Convert.ToInt32(dr[1]);
                        nombre = dr[2].ToString();
                        apellido = dr[3].ToString();
                        direccion = dr[4].ToString();
                        telefono = dr[5].ToString();
                        nuevoSupervisor = new Supervisor(idSupervisor, cuil, nombre, apellido, direccion, telefono);
                        ListSupervisores.Add(nuevoSupervisor);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListSupervisores;
        }

        /// <summary>
        /// Metodo para Buscar supervisores segun NOMBRE O APELLIDO.
        /// </summary>
        /// <param name="nombreOApellido"></param>
        /// <returns></returns>
        public List<Supervisor> GetSupervisores(string nombreOApellido)
        {
            //Variables auxiliares
            List<Supervisor> ListSupervisores = new List<Supervisor>();
            Supervisor nuevoSupervisor;
            int idSupervisor;
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
                    cmd = new MySqlCommand("Select * from supervisores where nombre=@nombreOApellido or apellido=@nombreOApellido", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@nombreOApellido", nombreOApellido);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idSupervisor = Convert.ToInt32(dr[0]);
                        cuil = Convert.ToInt32(dr[1]);
                        nombre = dr[2].ToString();
                        apellido = dr[3].ToString();
                        direccion = dr[4].ToString();
                        telefono = dr[5].ToString();
                        nuevoSupervisor = new Supervisor(idSupervisor, cuil, nombre, apellido, direccion, telefono);
                        ListSupervisores.Add(nuevoSupervisor);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListSupervisores;
        }

        /// <summary>
        /// Meotodo para Devolver listado de supervisores.
        /// </summary>
        /// <returns></returns>
        public List<Supervisor> GetSupervisores()
        {
            //Variables auxiliares
            List<Supervisor> ListSupervisores = new List<Supervisor>();
            Supervisor nuevoSupervisor;
            int idSupervisor;
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
                    cmd = new MySqlCommand("Select * from supervisores", conexion.retornarCN());
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idSupervisor = Convert.ToInt32(dr[0]);
                        cuil = Convert.ToInt32(dr[1]);
                        nombre = dr[2].ToString();
                        apellido = dr[3].ToString();
                        direccion = dr[4].ToString();
                        telefono = dr[5].ToString();
                        nuevoSupervisor = new Supervisor(idSupervisor, cuil, nombre, apellido, direccion, telefono);
                        ListSupervisores.Add(nuevoSupervisor);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Buscar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return ListSupervisores;
        }

        /// <summary>
        /// Metodo para Modificar Supervisor
        /// </summary>
        /// <param name="supervisor"></param>
        public void ModificacionSupervisor(Supervisor supervisor)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("UPDATE supervisores SET cuil=@cuil, nombre=@nombre, apellido=@apellido, direccion=@direccion, telefono=@telefono WHERE cuil=@cuil", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@cuil", supervisor.Cuil);
                    cmd.Parameters.AddWithValue("@nombre", supervisor.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", supervisor.Apellido);
                    cmd.Parameters.AddWithValue("@direccion", supervisor.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", supervisor.Telefono);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Supervisor modificado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Modificar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error no se pudo modificar");
                }
            }
        }

        /// <summary>
        /// Metodo para Eliminar Supervisor.
        /// </summary>
        /// <param name="cuil"></param>
        public void BajaSupervisor(int cuil)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("DELETE FROM supervisores WHERE cuil = @cuil", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@cuil", cuil);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Eliminar Supervisor {0}", ex.ToString());
                    MessageBox.Show("Error no se pudo Eliminar");
                }
            }
        }
    }
}
