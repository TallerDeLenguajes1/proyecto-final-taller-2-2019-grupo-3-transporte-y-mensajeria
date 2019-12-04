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
                    cmd = new MySqlCommand("Select * from paquetes where contenido=@contenido", conexion.retornarCN());
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

                        nuevoPaquete = new Paquete(idPaquete, contenido, asegurada, largoRecorrido, aumSeguro, precioM3, volumen);

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

            nuevoPaquete.AsociarVehiculo(listaVehiculos);
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
    }
}