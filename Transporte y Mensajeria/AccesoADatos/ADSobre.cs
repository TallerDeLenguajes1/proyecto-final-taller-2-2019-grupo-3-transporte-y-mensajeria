using EntidadesDelProyecto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

// Paquete Nuget Nlog permite logear los eventos del Sistema.
//using NLog;


namespace AccesoADatos
{
    public class ADSobre
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();
        //NLOG

        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        //Metodo alta Sobre.
        public void AltaSobre(Sobre sobre)
        {
            using (conexion.retornarCN())
            {
                try
                {

                    conexion.abrir();
                    cmd = new MySqlCommand("Insert into sobres(precioNeto,descripcion,asegurada,aumSeguro,largoRecorrido,peso) values( @precioNeto, @descripcion, @asegurada, @aumSeguro,@largoRecorrido, @peso)", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@precioNeto", sobre.CalcularPrecioNeto());
                    cmd.Parameters.AddWithValue("@descripcion", sobre.Contenido);
                    cmd.Parameters.AddWithValue("@asegurada", sobre.Asegurada);
                    cmd.Parameters.AddWithValue("@aumSeguro", sobre.AumSeguro);
                    cmd.Parameters.AddWithValue("@largoRecorrido", sobre.LargoRecorrido);
                    cmd.Parameters.AddWithValue("@peso", sobre.Peso);

                    cmd.ExecuteNonQuery();
                    //conexion.cerrar();
                    MessageBox.Show("Sobre agregado");
                }
                catch (Exception ex)
                {
                    //logger.Error("Ejemplo de mensaje de error{0}", ex.ToString());
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
            }

        }

        //---------------------
        //Buscar sobre segun Descripcion
        public Sobre GetSobres(string contenido, string metodoBusqueda)
        {
            //Variables auxiliares
            Sobre nuevoSobre = null;

            double peso;
            double precioNeto;
            string descripcion;
            bool asegurada;
            bool largoRecorrido;
            double aumSeguro;
            double precioGramo = 1;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Select * from sobres where descripcion=@descripcion", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@descripcion", contenido);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        precioNeto = Convert.ToInt32(dr[1]);
                        descripcion = dr[2].ToString();
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

                        nuevoSobre = new Sobre(descripcion, asegurada, largoRecorrido, aumSeguro, precioGramo, peso);

                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
                return nuevoSobre;
            }


        }



        //--------------


        //Modificar Sobre
        public void ModificacionSobre(Sobre sobre)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("UPDATE sobres SET precioNeto=@precioNeto, descripcion=@descripcion, asegurada=@asegurada, aumSeguro=@aumSeguro, largoRecorrido=@largoRecorrido, peso=@peso  WHERE descripcion=@descripcion", conexion.retornarCN());
                    //sobres(precioNeto,descripcion,asegurada,aumSeguro,largoRecorrido,peso) values( @precioNeto, @descripcion, @asegurada, @aumSeguro,@largoRecorrido, @peso)", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@precioNeto", sobre.PrecioNeto);
                    cmd.Parameters.AddWithValue("@descripcion", sobre.Contenido);
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
                    //Loguear el error
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
            }

        }

        /// <summary>
        /// El metodo BajaSobre permite eliminar un sobre de la BD de acuerdo al parametro de peso del sobre.
        /// </summary>
        /// <param name="peso"></param>
        public void BajaSobre(double peso)
        {
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("DELETE FROM sobres WHERE peso = @peso", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@peso", peso);

                    cmd.ExecuteNonQuery();
                    conexion.cerrar();
                    MessageBox.Show("Sobre eliminado");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }

            }


        }




        //Devolver listado de Sobres
        public List<Sobre> GetSobres()
        {
            //Variables auxiliares
            List<Sobre> ListSobres = new List<Sobre>();
            Sobre nuevoSobre;
            int idSobre;
            double precioNeto;
            string descripcion;
            bool asegurada;
            double aumSeguro;
            bool largoRecorrido;
            double peso;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("Select * from sobres", conexion.retornarCN());
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        idSobre = Convert.ToInt32(dr[0]);
                        precioNeto = Convert.ToInt32(dr[1]);
                        descripcion = dr[2].ToString();
                        asegurada = true;//Convert.ToInt32(dr[3]);
                        aumSeguro = Convert.ToInt32(dr[4]);
                        largoRecorrido = true;//dr[5].ToString();
                        peso = Convert.ToInt32(dr[6]);
                        nuevoSobre = new Sobre(descripcion, asegurada, largoRecorrido, aumSeguro, precioNeto, peso);
                        ListSobres.Add(nuevoSobre);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
            }



            return ListSobres;
        }

    }
}
