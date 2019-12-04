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
    public class ADPaquete
    {
        ADFurgoneta furgonetaBD = new ADFurgoneta();
        ADAvion avionBD = new ADAvion();
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar de Alta un Paquete.
        /// </summary>
        /// <param name="paquete"></param>
        public void AltaPaquete(Paquete paquete)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Insert into paquetes(precioNeto,contenido,asegurada,aumSeguro,largoRecorrido,volumen) values( @precioNeto, @contenido, @asegurada, @aumSeguro,@largoRecorrido, @volumen)", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@precioNeto", paquete.CalcularPrecioNeto());
                    cmd.Parameters.AddWithValue("@contenido", paquete.Contenido);
                    cmd.Parameters.AddWithValue("@asegurada", paquete.Asegurada);
                    cmd.Parameters.AddWithValue("@aumSeguro", paquete.AumSeguro);
                    cmd.Parameters.AddWithValue("@largoRecorrido", paquete.LargoRecorrido);
                    cmd.Parameters.AddWithValue("@volumen", paquete.Volumen);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Paquete agregado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Paquete {0}", ex.ToString());
                    MessageBox.Show("No se pudo dar el Alta el Paquete");
                }
            }

        }

        /// <summary>
        /// Metodo para buscar Paquetes segun Contenido
        /// </summary>
        /// <param name="contenidoToSearch"></param>
        /// <returns></returns>
        public List<Mercancia> GetPaquetes(string contenidoToSearch)
        {
            //Variables auxiliares
            List<Mercancia> ListPaquetes = new List<Mercancia>();
            List<Vehiculo> listVehiculos = new List<Vehiculo>();
            Paquete nuevoPaquete;

            int idPaquete;
            double volumen;
            double precioNeto;
            string contenido;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioM3 = this.GetPrecioM3();
            Vehiculo furgoneta;
            Vehiculo avion;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("select * from paquetes P inner join furgonetas F on P.fk_idFurgoneta = F.idFurgoneta inner join aviones A on P.fk_idAvionP = A.idAvion where P.contenido = @contenido", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@contenido", contenidoToSearch);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idPaquete = Convert.ToInt32(dr[0]);
                        precioNeto = Convert.ToInt32(dr[1]);
                        contenido = dr[2].ToString();
                        //En Mysql un bool es 1 o 0, por lo que se hace el control para que en VS se asigne true o false.
                        int aseguradaBD = Convert.ToInt32(dr[3]);
                        if (aseguradaBD == 1)
                        {
                            asegurada = true;
                        }
                        else
                        {
                            asegurada = false;
                        }

                        aumSeguro = Convert.ToInt32(dr[4]);

                        //En Mysql un bool es 1 o 0, por lo que se hace el control para que en VS se asigne true o false.
                        int largoRecorridoBD = Convert.ToInt32(dr[5]);
                        if (largoRecorridoBD == 1)
                        {
                            largoRecorrido = true;
                        }
                        else
                        {
                            largoRecorrido = false;
                        }

                        volumen = Convert.ToInt32(dr[6]);

                        //Si la mercancia tiene vehiculos asociados los rescato y construyo el objeto
                        if (!dr.IsDBNull(8))
                        {
                            furgoneta = furgonetaBD.GetFurgonetaPorID(Convert.ToInt32(dr[8]));
                            listVehiculos.Add(furgoneta);
                        }

                        if (!dr.IsDBNull(9))
                        {
                            avion = avionBD.GetAvionPorID(Convert.ToInt32(dr[9]));
                            listVehiculos.Add(avion);
                        }

                        nuevoPaquete = new Paquete(idPaquete, contenido, asegurada, largoRecorrido, aumSeguro, 150, volumen, listVehiculos);

                        //cargarVehiculos(nuevoPaquete);
                        //Esto funcionaba correctamente en la version q subi el lunes pero ahora aqui no.
                        //abria q ver si es necesario q tenga los vehiculos ,asociarlos en otro lado.

                        ListPaquetes.Add(nuevoPaquete);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
                return ListPaquetes;
            }
        }
        /// <summary>
        /// El metodo permite cargar los vehiculos que se le asocian a un Sobre
        /// esta carga es externa al sistema, y lo realizamos de esta forma para disminuir la complejidad del sistema.
        /// </summary>
        /// <param name="nuevoPaquete"></param>
        private static void cargarVehiculos(Paquete nuevoPaquete)
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();
            listaVehiculos.Add(new Furgoneta("Toyota", DateTime.Now, 900000, 500, 2));
            listaVehiculos.Add(new Furgoneta("Renault", DateTime.Now, 500000, 450, 2));
            listaVehiculos.Add(new Avion("Aerobus920", DateTime.Now, 2000000, 10));

            //nuevoPaquete.AsociarVehiculo(listaVehiculos);
        }

        /// <summary>
        /// Metodo para Modificar Paquete.
        /// </summary>
        /// <param name="paquete"></param>
        public void ModificacionPaquete(Paquete paquete)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("UPDATE paquetes SET precioNeto=@precioNeto, contenido=@contenido, asegurada=@asegurada, aumSeguro=@aumSeguro, largoRecorrido=@largoRecorrido, volumen=@volumen  WHERE idPaquete=@idPaquete", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@idPaquete", paquete.IdMercancia);
                    cmd.Parameters.AddWithValue("@precioNeto", paquete.CalcularPrecioNeto());
                    cmd.Parameters.AddWithValue("@contenido", paquete.Contenido);
                    cmd.Parameters.AddWithValue("@asegurada", paquete.Asegurada);
                    cmd.Parameters.AddWithValue("@aumSeguro", paquete.AumSeguro);
                    cmd.Parameters.AddWithValue("@largoRecorrido", paquete.LargoRecorrido);
                    cmd.Parameters.AddWithValue("@volumen", paquete.Volumen);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Paquete modificado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Modificicacion de Paquete {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo Modificar");
                }
            }
        }
        /// <summary>
        /// Metodo que permite dar la baja de un paquete , recibe como parametro el volumen del paquete.
        /// </summary>
        /// <param name="volumen"></param>
        public void BajaPaquete(int idPaquete)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("DELETE FROM paquetes WHERE idPaquete = @idPaquete", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@idPaquete", idPaquete);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Paquete eliminado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Baja de Paquete {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de baja el Paquete");
                }

            }


        }

        //-------------


        /// <summary>
        /// Metodo para devolver listado de Paquetes.
        /// </summary>
        /// <returns></returns>
        public List<Mercancia> GetPaquetes()
        {
            //Variables auxiliares
            List<Mercancia> ListPaquetes = new List<Mercancia>();
            Paquete nuevoPaquete;

            int idPaquete;
            double volumen;
            double precioNeto;
            string contenido;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioM3 = this.GetPrecioM3();

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Select * from paquetes", conexion.retornarCN());
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idPaquete = Convert.ToInt32(dr[0]);
                        precioNeto = Convert.ToInt32(dr[1]);
                        contenido = dr[2].ToString();
                        //En Mysql un bool es 1 o 0, por lo que se hace el control para que en VS se asigne true o false.
                        int aseguradaBD = Convert.ToInt32(dr[3]);
                        if (aseguradaBD == 1)
                        {
                            asegurada = true;
                        }
                        else
                        {
                            asegurada = false;
                        }

                        aumSeguro = Convert.ToInt32(dr[4]);

                        //En Mysql un bool es 1 o 0, por lo que se hace el control para que en VS se asigne true o false.
                        int largoRecorridoBD = Convert.ToInt32(dr[5]);
                        if (largoRecorridoBD == 1)
                        {
                            largoRecorrido = true;
                        }
                        else
                        {
                            largoRecorrido = false;
                        }

                        volumen = Convert.ToInt32(dr[6]);

                        nuevoPaquete = new Paquete(idPaquete, contenido, asegurada, largoRecorrido, aumSeguro, precioM3, volumen);

                        ListPaquetes.Add(nuevoPaquete);
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
            return ListPaquetes;
        }
        /// <summary>
        /// Metodo para obtener el precio del Metro cubico de un paquete.
        /// </summary>
        /// <returns></returns>
        public double GetPrecioM3()
        {
            double precioM3 = 0;
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("select precio from precio_unidad where unidad = 'm3' order by fecha desc limit 1;", conexion.retornarCN());
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        precioM3 = Convert.ToDouble(dr[0]);
                    }
                    dr.Close();


                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return precioM3;

        }

        /// <summary>
        /// Asociar una furgoneta a un paquete
        /// </summary>
        /// <param name="IdVehiculo"></param>
        /// <param name="IdMercancia"></param>
        public void AsociarFurgoneta(int IdVehiculo, int IdMercancia)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE paquetes SET fk_idFurgoneta = @IdVehiculo WHERE idPaquete = @IdMercancia", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@IdVehiculo", IdVehiculo);
                    cmd.Parameters.AddWithValue("@IdMercancia", IdMercancia);
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
        /// Asociar un avion a un paquete
        /// </summary>
        /// <param name="IdVehiculo"></param>
        /// <param name="IdMercancia"></param>
        public void AsociarAvion(int IdVehiculo, int IdMercancia)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE paquetes SET fk_idAvionP = @IdVehiculo WHERE idPaquete = @IdMercancia", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@IdVehiculo", IdVehiculo);
                    cmd.Parameters.AddWithValue("@IdMercancia", IdMercancia);
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
        /// Devuelve la furgoneta asociada a un paquete
        /// </summary>
        /// <param name="IdMercancia"></param>
        public Furgoneta FurgonetaAsociada(int idMercancia)
        {
            Furgoneta nuevaFurgoneta = null;
            int idFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            double capacidad;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("select F.* from paquetes P inner join furgonetas F on P.fk_idFurgoneta = F.idFurgoneta where idPaquete = @idMercancia ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idMercancia", idMercancia);
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
                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return nuevaFurgoneta;
        }

        /// <summary>
        /// Devuelve el avion asociado a un paquete
        /// </summary>
        /// <param name="IdMercancia"></param>
        public Avion AvionAsociado(int idMercancia)
        {
            Avion nuevoAvion = null;
            int idAvion;
            string modelo;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("select A.* from paquetes P inner join aviones A on P.fk_idAvionP = A.idAvion where idPaquete = @idMercancia ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idMercancia", idMercancia);
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

        /// <summary>
        /// Quita una furgoneta asociada a un paquete
        /// </summary>
        public void QuitarFurgoneta(Paquete paquete)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE paquetes SET fk_idFurgoneta = null WHERE idPaquete=@idPaquete", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idPaquete", paquete.IdMercancia);
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
        /// Quita un avion asociada a un paquete
        /// </summary>
        public void QuitarAvion(Paquete paquete)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE paquetes SET fk_idAvionP = null WHERE idPaquete=@idPaquete", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idPaquete", paquete.IdMercancia);
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
    }
}