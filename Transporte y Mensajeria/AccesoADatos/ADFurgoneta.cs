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
    public class ADFurgoneta
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar de alta una furgoneta.
        /// </summary>
        /// <param name="furgoneta"></param>
        public void AltaFurgoneta(Furgoneta furgoneta)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();

                    using (cmd = new MySqlCommand("Insert into furgonetas(modelo,fechaCompra,precioCompra,aumento,capacidadCarga) values (@modelo,@fechaCompra,@precioCompra,@aumento,@capacidadCarga)", conexion.retornarCN()))
                    {

                        cmd.Parameters.AddWithValue("@modelo", furgoneta.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", furgoneta.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", furgoneta.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", furgoneta.Aumento);
                        cmd.Parameters.AddWithValue("@capacidadCarga", furgoneta.CapacidadCarga);

                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Furgoneta {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de alta el Vehiculo");
                }
            }
        }


        /// <summary>
        /// eliminar del sistema una furgoneta
        /// </summary>
        /// <param name="idFurgoneta"></param>
        public void BajaFurgoneta(int idFurgoneta)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("DELETE FROM furgonetas WHERE idFurgoneta = @idFurgoneta", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@idFurgoneta", idFurgoneta);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Baja de Furgoneta {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de baja la Furgoneta");
                }
            }

        }
        /// <summary>
        /// modificar una furgoneta
        /// </summary>
        /// <param name="Furgoneta"></param>
        public void ModificacionFurgoneta(Furgoneta Furgoneta)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("UPDATE furgonetas SET modelo=@modelo, fechaCompra=@fechaCompra, precioCompra=@precioCompra, aumento=@aumento, capacidadCarga=@CapacidadCarga WHERE idFurgoneta=@idFurgoneta", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@IdFurgoneta", Furgoneta.IdVehiculo);
                        cmd.Parameters.AddWithValue("@modelo", Furgoneta.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", Furgoneta.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", Furgoneta.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", Furgoneta.Aumento);
                        cmd.Parameters.AddWithValue("@capacidadCarga", Furgoneta.CapacidadCarga);
                        cmd.ExecuteNonQuery();
                        conexion.cerrar();
                        MessageBox.Show("Furgoneta modificada");
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Moficicacion de Furgoneta {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo Modificar");
                }
            }
        }

        /// <summary>
        /// devuelve todas las furgonetas del sistema
        /// </summary>
        /// <returns></returns>
        public List<Furgoneta> GetFurgonetas()
        {
            List<Furgoneta> listaFurgonetas = new List<Furgoneta>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            double capacidad;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("Select * from furgoneta", conexion.retornarCN()))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            idFurgoneta = dr.GetInt16(0);
                            descripcion = dr.GetString(1);
                            aumento = dr.GetDouble(2);
                            fechaCompra = dr.GetDateTime(3);
                            precioCompra = dr.GetDouble(4);
                            capacidad = dr.GetDouble(5);
                            nuevaFurgoneta = new Furgoneta(idFurgoneta, descripcion, fechaCompra, precioCompra, capacidad, aumento);
                            listaFurgonetas.Add(nuevaFurgoneta);
                        }
                        dr.Close();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return listaFurgonetas;
        }
        /// <summary>
        /// devuelve una lista de las furgonetas en cierto año
        /// </summary>
        /// <param name="anioCompra"></param>
        /// <returns></returns>
        public List<Vehiculo> GetFurgonetas(int anioCompra)
        {
            List<Vehiculo> listaFurgoneta = new List<Vehiculo>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("Select * from furgonetas where year(fechaCompra) = @anioCompra ", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            idFurgoneta = dr.GetInt16(0);
                            descripcion = dr.GetString(1);
                            aumento = dr.GetDouble(2);
                            fechaCompra = dr.GetDateTime(3);
                            precioCompra = dr.GetDouble(4);
                            capacidad = dr.GetDouble(5);
                            nuevaFurgoneta = new Furgoneta(idFurgoneta, descripcion, fechaCompra, precioCompra, capacidad, aumento);
                            listaFurgoneta.Add(nuevaFurgoneta);

                        }
                        dr.Close();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return listaFurgoneta;
        }
        /// <summary>
        /// devuelve lista de acuerdo a cierta marca
        /// </summary>
        /// <param name="descripcionMarca"></param>
        /// <returns></returns>
        public List<Vehiculo> GetFurgonetas(string modeloToSerch)
        {
            List<Vehiculo> listaFurgonetas = new List<Vehiculo>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Select * from furgonetas where modelo = @modeloToSerch ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idFurgoneta = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        capacidad = dr.GetInt32(5);
                        nuevaFurgoneta = new Furgoneta(idFurgoneta, modelo, fechaCompra, precioCompra, capacidad, aumento);
                        listaFurgonetas.Add(nuevaFurgoneta);

                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return listaFurgonetas;
        }
        /// <summary>
        /// relaciona un supervisor con una furgoneta
        /// </summary>
        /// <param name="idFurgoneta"></param>
        /// <param name="idSup"></param>
        public void agregarSupervisor(int idFurgoneta, int idSup)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("UPDATE furgonetas SET fk_idSupervisor= @idSup WHERE idFurgoneta = @idFurgoneta", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@idSup", idSup);
                        cmd.Parameters.AddWithValue("@idFurgoneta", idFurgoneta);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error AgregarSupervisor {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
        }
    }
}