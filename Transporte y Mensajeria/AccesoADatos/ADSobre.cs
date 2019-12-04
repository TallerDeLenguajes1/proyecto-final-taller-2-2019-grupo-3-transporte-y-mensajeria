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
    public class ADSobre
    {
        ADMoto motoBD = new ADMoto();
        ADAvion avionBD = new ADAvion();
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metodo para dar de alta un Sobre.
        /// </summary>
        /// <param name="sobre"></param>
        public void AltaSobre(Sobre sobre)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Insert into sobres(precioNeto,contenido,asegurada,aumSeguro,largoRecorrido,peso) values( @precioNeto, @contenido, @asegurada, @aumSeguro,@largoRecorrido, @peso)", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@precioNeto", sobre.CalcularPrecioNeto());
                    cmd.Parameters.AddWithValue("@contenido", sobre.Contenido);
                    cmd.Parameters.AddWithValue("@asegurada", sobre.Asegurada);
                    cmd.Parameters.AddWithValue("@aumSeguro", sobre.AumSeguro);
                    cmd.Parameters.AddWithValue("@largoRecorrido", sobre.LargoRecorrido);
                    cmd.Parameters.AddWithValue("@peso", sobre.Peso);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sobre agregado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de alta de Sobre {0}", ex.ToString());
                    MessageBox.Show("No se pudo dar el Alta el Sobre");
                }
            }
        }

        /// <summary>
        /// Metodo para buscar Sobres segun Contenido.
        /// </summary>
        /// <param name="contenidoToSearch"></param>
        /// <returns></returns>
        public List<Mercancia> GetSobres(string contenidoToSearch)
        {
            //Variables auxiliares
            List<Mercancia> ListSobres = new List<Mercancia>();
            List<Vehiculo> listVehiculos = new List<Vehiculo>();
            Sobre nuevoSobre;
            int idSobre;
            double peso;
            double precioNeto;
            string contenido;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioGramo = this.GetPrecioGramo();
            Vehiculo moto;
            Vehiculo avion;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();

                    using (cmd = new MySqlCommand("select * from sobres S inner join motos M on S.fk_idMoto = M.idMoto inner join aviones A on S.fk_idAvionS = A.idAvion where S.contenido = @contenido", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@contenido", contenidoToSearch);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            idSobre = Convert.ToInt32(dr[0]);
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

                            peso = Convert.ToInt32(dr[6]);

                            //Si la mercancia tiene vehiculos asociados los rescato y construyo el objeto
                            if (!dr.IsDBNull(8))
                            {
                                moto = motoBD.GetMotoPorID(Convert.ToInt32(dr[8]));
                                listVehiculos.Add(moto);
                            }

                            if (!dr.IsDBNull(9))
                            {
                                avion = avionBD.GetAvionPorID(Convert.ToInt32(dr[9]));
                                listVehiculos.Add(avion);
                            }



                            if (listVehiculos.Count()==0)
                            {

                                nuevoSobre = new Sobre(idSobre, contenido, asegurada, largoRecorrido, aumSeguro, 100, peso);
                            }
                            else
                            {
                                nuevoSobre = new Sobre(idSobre, contenido, asegurada, largoRecorrido, aumSeguro, 100, peso, listVehiculos);
                            }

                            


                            ListSobres.Add(nuevoSobre);
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
                return ListSobres;
            }
        }

        public List<Mercancia> GetSobres2(string contenidoToSearch)
        {
            //Variables auxiliares
            List<Mercancia> ListSobres = new List<Mercancia>();
            List<Vehiculo> listVehiculos = new List<Vehiculo>();
            Sobre nuevoSobre;
            int idSobre;
            double peso;
            double precioNeto;
            string contenido;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioGramo = this.GetPrecioGramo();
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();

                    using (cmd = new MySqlCommand("select * from sobres where contenido = @contenido", conexion.retornarCN()))
                    {
                        cmd.Parameters.AddWithValue("@contenido", contenidoToSearch);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            idSobre = Convert.ToInt32(dr[0]);
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

                            peso = Convert.ToInt32(dr[6]);

                            nuevoSobre = new Sobre(idSobre, contenido, asegurada, largoRecorrido, aumSeguro, 100, peso);
         



                            ListSobres.Add(nuevoSobre);
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
                return ListSobres;
            }
        }


        /// <summary>
        /// El metodo permite cargar los vehiculos que se le asocian a un Sobre
        /// esta carga es externa al sistema, y lo realizamos de esta forma para disminuir la complejidad del sistema.
        /// </summary>
        /// <param name="nuevoSobre"></param>
        private static void cargarVehiculos(Sobre nuevoSobre)
        {
            List<Vehiculo> listaVehiculos = new List<Vehiculo>();
            listaVehiculos.Add(new Moto("yamaha", DateTime.Now, 700000, 500, 2));
            listaVehiculos.Add(new Moto("honda", DateTime.Now, 500000, 400, 2));
            listaVehiculos.Add(new Avion("Aerobus920", DateTime.Now, 2000000, 10));

            //nuevoSobre.AsociarVehiculo(listaVehiculos);
        }

        /// <summary>
        /// Metodo para Modificar un Sobre.
        /// </summary>
        /// <param name="sobre"></param>
        public void ModificacionSobre(Sobre sobre)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("UPDATE sobres SET precioNeto=@precioNeto, contenido=@contenido, asegurada=@asegurada, aumSeguro=@aumSeguro, largoRecorrido=@largoRecorrido, peso=@peso  WHERE idSobre=@idSobre", conexion.retornarCN());


                    cmd.Parameters.AddWithValue("@idSobre", sobre.IdMercancia);
                    cmd.Parameters.AddWithValue("@precioNeto", sobre.CalcularPrecioNeto());
                    cmd.Parameters.AddWithValue("@contenido", sobre.Contenido);
                    cmd.Parameters.AddWithValue("@asegurada", sobre.Asegurada);
                    cmd.Parameters.AddWithValue("@aumSeguro", sobre.AumSeguro);
                    cmd.Parameters.AddWithValue("@largoRecorrido", sobre.LargoRecorrido);
                    cmd.Parameters.AddWithValue("@peso", sobre.Peso);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Sobre modificado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Modificicacion de Sobre {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo Modificar");
                }
            }

        }

        /// <summary>
        /// El metodo BajaSobre permite eliminar un sobre de la BD de acuerdo al parametro de peso del sobre.
        /// </summary>
        /// <param name="peso"></param>
        public void BajaSobre(int idSobre)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("DELETE FROM sobres WHERE idSobre = @idSobre", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@idSobre", idSobre);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Sobre eliminado");
                }
                catch (Exception ex)
                {
                    Logger.Error("Error de Baja de Sobre {0}", ex.ToString());
                    MessageBox.Show("Error, no se pudo dar de baja el Sobre");
                }

            }
        }

        /// <summary>
        /// Metodo para obtener lista de Sobres.
        /// </summary>
        /// <returns></returns>
        public List<Sobre> GetSobres()
        {
            //Variables auxiliares
            List<Sobre> ListSobres = new List<Sobre>();
            Sobre nuevoSobre;

            int idSobre;
            double peso;
            double precioNeto;
            string contenido;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioGramo = this.GetPrecioGramo();

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    using (cmd = new MySqlCommand("Select * from sobres", conexion.retornarCN()))
                    {
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            idSobre = Convert.ToInt32(dr[0]);
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

                            peso = Convert.ToInt32(dr[6]);

                            nuevoSobre = new Sobre(idSobre, contenido, asegurada, largoRecorrido, aumSeguro, precioGramo, peso);
                            nuevoSobre.PrecioNeto = precioNeto;
                            ListSobres.Add(nuevoSobre);
                        }
                        dr.Close();
                        conexion.cerrar();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                }
            }
            return ListSobres;
        }


        /// <summary>
        /// Metodo para obtener el precio del gramo de un Sobre.
        /// </summary>
        /// <returns></returns>
        public double GetPrecioGramo()
        {
            double precioGramo = 0;
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("select precio from precio_unidad where unidad = 'gramo' order by fecha desc limit 1", conexion.retornarCN());
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        precioGramo = Convert.ToDouble(dr[0]);
                    }
                    dr.Close();


                }
                catch (Exception ex)
                {
                    Logger.Error("Error Busqueda {0}", ex.ToString());
                    MessageBox.Show("Error en la consulta");
                }
            }
            return precioGramo;

        }

        /// <summary>
        /// Asociar una moto a un sobre
        /// </summary>
        /// <param name="IdVehiculo"></param>
        /// <param name="IdMercancia"></param>
        public void AsociarMoto(int IdVehiculo, int IdMercancia)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE sobres SET fk_idMoto = @IdVehiculo WHERE idSobre = @IdMercancia", conexion.retornarCN()))
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
        /// Asociar un avion a un sobre
        /// </summary>
        /// <param name="IdVehiculo"></param>
        /// <param name="IdMercancia"></param>
        public void AsociarAvion(int IdVehiculo, int IdMercancia)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE sobres SET fk_idAvionS = @IdVehiculo WHERE idSobre = @IdMercancia", conexion.retornarCN()))
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
        /// Devuelve la moto asociada a un sobre
        /// </summary>
        /// <param name="IdMercancia"></param>
        public Moto MotoAsociada(int idMercancia)
        {
            Moto nuevaMoto = null;
            int idMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;
            int cilindrada;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("select M.* from sobres S inner join motos M on S.fk_idMoto = M.idMoto where idSobre = @idMercancia ", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idMercancia", idMercancia);
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

        /// <summary>
        /// Devuelve el avion asociado a un sobre
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
                using (cmd = new MySqlCommand("select A.* from sobres S inner join aviones A on S.fk_idAvionS = A.idAvion where idSobre = @idMercancia", conexion.retornarCN()))
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
        /// Quita una moto asociada a un sobre
        /// </summary>
        public void QuitarMoto(Sobre sobre)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE sobres SET fk_idMoto = null WHERE idSobre=@idSobre", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSobre", sobre.IdMercancia);
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
        /// Quita un avion asociada a un sobre
        /// </summary>
        public void QuitarAvion(Sobre sobre)
        {
            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("UPDATE sobres SET fk_idAvionS = null WHERE idSobre=@idSobre", conexion.retornarCN()))
                {
                    cmd.Parameters.AddWithValue("@idSobre", sobre.IdMercancia);
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