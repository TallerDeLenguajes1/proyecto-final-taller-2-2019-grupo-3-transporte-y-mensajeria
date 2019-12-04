﻿using EntidadesDelProyecto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccesoADatos
{
    public class ADAvion
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar de alta un avion.
        /// </summary>
        /// <param name="avion"></param>
        public void AltaAvion(Avion avion)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();

                    using (cmd = new MySqlCommand("Insert into aviones(modelo,fechaCompra,precioCompra,aumento) values (@modelo,@fechaCompra,@precioCompra,@aumento)", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@modelo", avion.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", avion.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", avion.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", avion.Aumento);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Avion {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de alta el Avion");
                }
            }
        }


        /// <summary>
        /// Metodo para eliminar un avion.
        /// </summary>
        /// <param name="idAvion"></param>
        public void BajaAvion(int idAvion)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("DELETE FROM aviones WHERE idAvion = @idAvion", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@idAvion", idAvion);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                    MessageBox.Show("Vehiculo eliminado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Baja de Avion {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de baja el Avion");
                }
            }

        }
        /// <summary>
        /// modificar un avion del sistema
        /// </summary>
        /// <param name="Avion"></param>
        public void ModificacionAvion(Avion Avion)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("UPDATE aviones SET modelo=@modelo, fechaCompra=@fechaCompra, precioCompra=@precioCompra, aumento=@aumento WHERE idAvion=@idAvion", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@idAvion", Avion.IdVehiculo);
                        cmd.Parameters.AddWithValue("@modelo", Avion.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", Avion.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", Avion.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", Avion.Aumento);
                        cmd.ExecuteNonQuery();
                        conexion.cerrar();
                        MessageBox.Show("Avion modificado");
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Modificicacion de Avion {0}", ex.ToString());
                }
            }
        }

        /// <summary>
        /// Metodo para listar todos los aviones del sistema.
        /// </summary>
        /// <returns></returns>
        public List<Avion> GetAviones()
        {
            List<Avion> listaAviones = new List<Avion>();
            Avion nuevoAvion;
            int idAvion;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from aviones", conexion.retornarCN()))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(idAvion, modelo, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevoAvion);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        public List<Vehiculo> GetAvionesDeSupervisor(int idSupervisor)
        {
            List<Vehiculo> listaAviones = new List<Vehiculo>();
            Avion nuevoAvion;
            int idAvion;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from aviones where fk_idSupervisorA = @idSupervisor ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(idAvion, modelo, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevoAvion);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        /// <summary>
        /// lista de aviones segun año de compra
        /// </summary>
        /// <param name="anioCompra"></param>
        /// <returns></returns>
        public List<Vehiculo> GetAviones(int anioCompra)
        {
            List<Vehiculo> listaAviones = new List<Vehiculo>();
            Avion nuevaAvion;
            int idAvion;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from aviones where year(fechaCompra) = @anioCompra ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevaAvion = new Avion(idAvion, descripcion, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevaAvion);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        //Buscar Vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetAviones(int anioCompra, int idSupervisor)
        {
            List<Vehiculo> listaAviones = new List<Vehiculo>();
            Avion nuevaAvion;
            int idAvion;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from aviones where ( year(fechaCompra) = @anioCompra AND fk_idSupervisorA = @idSupervisor )", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevaAvion = new Avion(idAvion, descripcion, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevaAvion);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        /// <summary>
        /// lista de aviones segun marca
        /// </summary>
        /// <param name="descripcionMarca"></param>
        /// <returns></returns>
        public List<Vehiculo> GetAviones(string modeloToSerch)
        {
            List<Vehiculo> listaAviones = new List<Vehiculo>();
            Avion nuevoAvion;
            int idAvion;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from aviones where modelo = @modeloToSerch ", conexion.retornarCN()))
                {

                    cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(idAvion, modelo, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevoAvion);

                    }
                    dr.Close();

                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        //Buscar Vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetAviones(string modeloToSerch, int idSupervisor)
        {
            List<Vehiculo> listaAviones = new List<Vehiculo>();
            Avion nuevoAvion;
            int idAvion;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from aviones where ( modelo = @modeloToSerch AND fk_idSupervisorA = @idSupervisor )", conexion.retornarCN()))
                {

                    cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(idAvion, modelo, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevoAvion);

                    }
                    dr.Close();

                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
            return listaAviones;
        }

        /// <summary>
        /// relacionar un supervisor con un avion
        /// </summary>
        /// <param name="idAvion"></param>
        /// <param name="idSup"></param>
        public void agregarSupervisor(int idAvion, int idSupervisor)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE aviones SET fk_idSupervisorA= @idSupervisor WHERE idAvion = @idAvion", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                cmd.Parameters.AddWithValue("@idAvion", idAvion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehiculo asociado con éxito");
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
        }

        /// <summary>
        /// Quita un avion a cargo de un supervisor
        /// </summary>
        /// <returns>lista de Motos</returns>
        public void QuitarSupervisor(Avion avion)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE aviones SET fk_idSupervisorA = @idSupervisor WHERE idAvion=@idAvion", conexion.retornarCN()))
                {

                    cmd.Parameters.AddWithValue("@idAvion", avion.IdVehiculo);
                    cmd.Parameters.AddWithValue("@idSupervisor", null);
                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Vehiculo quitado con éxito");
                }
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        /// <summary>
        /// devolver avion por id
        /// </summary>
        /// <returns></>
        public Vehiculo GetAvionPorID(int idAvion)
        {
            Avion nuevoAvion = null;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from aviones where idAvion = @idAvion ", conexion.retornarCN()))
                {
                    //cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@idAvion", idAvion);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idAvion = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(idAvion, modelo, fechaCompra, precioCompra, aumento);
                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return nuevoAvion;
        }
    }
}