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


        /// <summary>
        /// dar de alta una furgoneta
        /// </summary>
        /// <param name="furgoneta"></param>
        public void AltaFurgoneta(Furgoneta furgoneta)
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
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }


        /// <summary>
        /// eliminar del sistema una furgoneta
        /// </summary>
        /// <param name="idFurgoneta"></param>
        public void BajaFurgoneta(int idFurgoneta)
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }

        /// <summary>
        /// modificar una furgoneta
        /// </summary>
        /// <param name="Furgoneta"></param>
        public void ModificacionFurgoneta(Furgoneta Furgoneta)
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
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
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

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from furgonetas", conexion.retornarCN()))
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgonetas;
        }

        public List<Vehiculo> GetFurgonetasDeSupervisor(int idSupervisor)
        {
            List<Vehiculo> listaFurgonetas = new List<Vehiculo>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            double capacidad;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from furgonetas where fk_idSupervisorF = @idSupervisor ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgoneta;
        }

        //Buscar vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetFurgonetas(int anioCompra, int idSupervisor)
        {
            List<Vehiculo> listaFurgoneta = new List<Vehiculo>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from furgonetas where ( year(fechaCompra) = @anioCompra AND fk_idSupervisorF = @idSupervisor )", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgonetas;
        }

        //Buscar Vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetFurgonetas(string modeloToSerch, int idSupervisor)
        {
            List<Vehiculo> listaFurgonetas = new List<Vehiculo>();
            Furgoneta nuevaFurgoneta;
            int idFurgoneta;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from furgonetas where ( modelo = @modeloToSerch and fk_idSupervisorF = @idSupervisor )", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgonetas;
        }

        /// <summary>
        /// relaciona un supervisor con una furgoneta
        /// </summary>
        /// <param name="idFurgoneta"></param>
        /// <param name="idSup"></param>
        public void agregarSupervisor(int idFurgoneta, int idSupervisor)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE furgonetas SET fk_idSupervisorF = @idSupervisor WHERE idFurgoneta = @idFurgoneta", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    cmd.Parameters.AddWithValue("@idFurgoneta", idFurgoneta);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehiculo asociado con éxito");
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }
    }
}