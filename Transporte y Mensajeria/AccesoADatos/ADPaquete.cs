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

        //Metodo alta Paquete.
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
                    //conexion.cerrar();
                    MessageBox.Show("Paquete agregado");
                }
                catch (Exception ex)
                {
                    //Loguear el error
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
            }

        }

        //Buscar Paquete segun Contenido
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
            double precioGramo = 1;

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

                        nuevoPaquete = new Paquete(idPaquete, contenido, asegurada, largoRecorrido, aumSeguro, precioGramo, volumen);
                        ListPaquetes.Add(nuevoPaquete);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
                return ListPaquetes;
            }
        }

        //Modificar Paquete
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
                    //Loguear el error
                    MessageBox.Show("Error en la consulta" + ex.ToString());
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
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }

            }


        }


        //-----------------
        //Devolver listado de Paquetes.
        public List<Paquete> GetPaquetes()
        {
            //Variables auxiliares
            List<Paquete> ListPaquetes = new List<Paquete>();
            Paquete nuevoPaquete;
            int idPaquete;
            double precioNeto;
            string descripcion;
            bool asegurada;
            double aumSeguro;
            bool largoRecorrido;
            double volumen;

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
                        descripcion = dr[2].ToString();
                        asegurada = true;//Convert.ToInt32(dr[3]);
                        aumSeguro = Convert.ToInt32(dr[4]);
                        largoRecorrido = true;//dr[5].ToString();
                        volumen = Convert.ToInt32(dr[6]);
                        nuevoPaquete = new Paquete(descripcion, asegurada, largoRecorrido, aumSeguro, precioNeto, volumen);
                        ListPaquetes.Add(nuevoPaquete);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
            }



            return ListPaquetes;
        }
    }
}