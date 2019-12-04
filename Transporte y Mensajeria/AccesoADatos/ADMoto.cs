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
    public class ADMoto
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// dar de alta una moto
        /// </summary>
        /// <param name="moto"></param>
        public void AltaMoto(Moto moto)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();

                    using (cmd = new MySqlCommand("Insert into motos(modelo,fechaCompra,precioCompra,aumento,cilindrada) values (@modelo,@fechaCompra,@precioCompra,@aumento,@cilindrada)", conexion.retornarCN()))
                    {

                        //cmd = new MySqlCommand("Insert into motos(descripcion,fechaCompra,precioCompra,cilindrada) values (@descripcion,@fechaCompra,@precioCompra,@cilindrada)", conexion.retornarCN());
                        cmd.Parameters.AddWithValue("@modelo", moto.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", moto.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", moto.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", moto.Aumento);
                        cmd.Parameters.AddWithValue("@cilindrada", moto.Cilindrada);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Moto {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de alta el Vehiculo");
                }
            }
        }



        /// <summary>
        /// elimina una moto del sistema con el id
        /// </summary>
        /// <param name="idMoto"></param>
        public void BajaMoto(int idMoto)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("DELETE FROM motos WHERE idMoto = @idMoto", conexion.retornarCN()))
                    {

                        cmd.Parameters.AddWithValue("@idMoto", idMoto);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.cerrar();
                    MessageBox.Show("Vehiculo eliminado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Baja de Moto {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de baja la Moto");
                }
            }

        }

        public void ModificacionMoto(Moto moto)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("UPDATE motos SET modelo=@modelo, fechaCompra=@fechaCompra, precioCompra=@precioCompra, aumento=@aumento, cilindrada=@cilindrada WHERE idMoto=@idMoto", conexion.retornarCN()))
                    {

                        cmd.Parameters.AddWithValue("@idMoto", moto.IdVehiculo);
                        cmd.Parameters.AddWithValue("@modelo", moto.Modelo);
                        cmd.Parameters.AddWithValue("@fechaCompra", moto.FechaCompra);
                        cmd.Parameters.AddWithValue("@precioCompra", moto.PrecioCompra);
                        cmd.Parameters.AddWithValue("@aumento", moto.Aumento);
                        cmd.Parameters.AddWithValue("@cilindrada", moto.Cilindrada);
                        cmd.ExecuteNonQuery();
                        conexion.cerrar();
                        MessageBox.Show("Moto modificada");
                    }


                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Moficicacion de Moto {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo Modificar");
                }
            }
        }

        /// <summary>
        /// Quita una moto a cargo de un supervisor
        /// </summary>
        /// <returns>lista de Motos</returns>
        public void QuitarSupervisor(Moto moto)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE motos SET fk_idSupervisorM = @idSupervisor WHERE idMoto=@idMoto", conexion.retornarCN()))
                {

                    cmd.Parameters.AddWithValue("@idMoto", moto.IdVehiculo);
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
        /// devuelve una lista de todas las motos del sistema
        /// </summary>
        /// <returns>lista de Motos</returns>
        public List<Moto> GetMotos()
        {
            List<Moto> listaMotos = new List<Moto>();
            Moto nuevaMoto;
            int idMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            int cilindrada;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from motos", conexion.retornarCN()))
                {
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        public List<Vehiculo> GetMotosDeSupervisor(int idSupervisor)
        {
            List<Vehiculo> listaMotos = new List<Vehiculo>();
            Moto nuevaMoto;
            int idMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from motos where fk_idSupervisorM = @idSupervisor ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        /// <summary>
        /// devuelve listado de motos del sistema, de acuedo al año de compra
        /// </summary>
        /// <param name="anioCompra"></param>
        /// <returns></returns>
        public List<Vehiculo> GetMotos(int anioCompra)
        {
            List<Vehiculo> listaMotos = new List<Vehiculo>();
            Moto nuevaMoto;
            int idMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from motos where year(fechaCompra) = @anioCompra ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        //Buscar Vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetMotos(int anioCompra, int idSupervisor)
        {
            List<Vehiculo> listaMotos = new List<Vehiculo>();
            Moto nuevaMoto;
            int idMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("Select * from motos where ( year(fechaCompra) = @anioCompra AND fk_idSupervisorM = @idSupervisor )", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        /// <summary>
        /// devuelve listado de motos del sistema, de acuedo al tipo de marca
        /// </summary>
        /// <param name="descripcionMarca"></param>
        /// <returns></returns>
        public List<Vehiculo> GetMotos(string modeloToSerch)
        {
            List<Vehiculo> listaMotos = new List<Vehiculo>();
            Moto nuevaMoto;
            int idMoto;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from motos where modelo = @modeloToSerch ", conexion.retornarCN()))
                {
                    //cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, modelo, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);
                    }
                    dr.Close();

                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        //Buscar Vehiculos asociados a un determinado supervisor
        public List<Vehiculo> GetMotos(string modeloToSerch, int idSupervisor)
        {
            List<Vehiculo> listaMotos = new List<Vehiculo>();
            Moto nuevaMoto;
            int idMoto;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from motos where ( modelo = @modeloToSerch AND fk_idSupervisorM = @idSupervisor )", conexion.retornarCN()))
                {
                    //cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@modeloToSerch", modeloToSerch);
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, modelo, fechaCompra, precioCompra, cilindrada, aumento);
                        listaMotos.Add(nuevaMoto);
                    }
                    dr.Close();

                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }

        /// <summary>
        /// relacionar un supervisor con una moto
        /// </summary>
        /// <param name="idMoto"></param>
        /// <param name="idSup"></param>
        public void agregarSupervisor(int idMoto, int idSupervisor)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE motos SET fk_idSupervisorM = @idSupervisor WHERE idMoto = @idMoto", conexion.retornarCN()))
                {
                    //UPDATE `transportemensajeria`.`motos` SET `fk_idSupervisor` = '12' WHERE (`idMoto` = '1');
                    cmd.Parameters.AddWithValue("@idSupervisor", idSupervisor);
                    cmd.Parameters.AddWithValue("@idMoto", idMoto);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehiculo asociado con éxito");
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error Busqueda {0}", ex.ToString());
            }
        }

        /// <summary>
        /// devolver moto por id
        /// </summary>
        /// <returns></>
        public Vehiculo GetMotoPorID(int idMoto)
        {
            Vehiculo nuevaMoto = null;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            int cilindrada;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from motos where idMoto = @idMoto ", conexion.retornarCN()))
                {
                    //cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@idMoto", idMoto);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idMoto = dr.GetInt16(0);
                        modelo = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(idMoto, modelo, fechaCompra, precioCompra, cilindrada, aumento);
                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return nuevaMoto;
        }
    }
}