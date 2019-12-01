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
    public class ADAvion
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;


        /// <summary>
        /// dar de alta un avion
        /// </summary>
        /// <param name="avion"></param>
        public void AltaAvion(Avion avion)
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
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }


        /// <summary>
        /// eliminar un avion
        /// </summary>
        /// <param name="idAvion"></param>
        public void BajaAvion(int idAvion)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }
        /// <summary>
        /// modificar un avion del sistema
        /// </summary>
        /// <param name="Avion"></param>
        public void ModificacionAvion(Avion Avion)
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
                    MessageBox.Show("Moto modificada");
                }
               
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        /// <summary>
        /// lista de todos los aviones del sistema
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
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
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaAviones;
        }

        /// <summary>
        /// relacionar un supervisor con un avion
        /// </summary>
        /// <param name="idAvion"></param>
        /// <param name="idSup"></param>
        public void agregarSupervisor(int idAvion, int idSup)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE aviones SET fk_idSupervisor= @idSup WHERE idAvion = @idAvion", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@idSup", idSup);
                cmd.Parameters.AddWithValue("@idAvion", idAvion);
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