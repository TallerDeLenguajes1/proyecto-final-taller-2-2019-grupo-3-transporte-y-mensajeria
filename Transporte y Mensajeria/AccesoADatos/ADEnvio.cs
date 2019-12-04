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
    public class ADEnvio
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Metodo permite dar el alta de un Envio.
        /// </summary>
        /// <param name="emisor"></param>
        /// <param name="receptor"></param>
        /// <param name="mercanciaEnviada"></param>
        /// <param name="fechaEnvio"></param>
        /// <param name="tipoMercancia"></param>
        public void AltaEnvio(Cliente emisor, Cliente receptor, Mercancia mercanciaEnviada, DateTime fechaEnvio, string tipoMercancia)
        {

            try
            {
                conexion.abrir();
                if (tipoMercancia == "paquete")
                {
                    cmd = new MySqlCommand("Insert into envios(fechaEnvio,fk_idClienteEmisor,fk_idClienteReceptor,fk_idPaquete,fk_idSobre,precioFinal) values (@fechaEnvio,@fk_idClienteEmisor,@fk_idClienteReceptor,@fk_idPaquete,NULL,@precioFinal)", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@fechaEnvio", fechaEnvio);
                    cmd.Parameters.AddWithValue("@fk_idClienteEmisor", emisor.IdPersona);
                    cmd.Parameters.AddWithValue("@fk_idClienteReceptor", receptor.IdPersona);
                    cmd.Parameters.AddWithValue("@fk_idPaquete", mercanciaEnviada.IdMercancia);
                    cmd.Parameters.AddWithValue("@precioFinal", mercanciaEnviada.CalcularPrecioFinal());
                }
                else
                {
                    cmd = new MySqlCommand("Insert into envios(fechaEnvio,fk_idClienteEmisor,fk_idClienteReceptor,fk_idPaquete,fk_idSobre,precioFinal) values (@fechaEnvio,@fk_idClienteEmisor,@fk_idClienteReceptor,NULL,@fk_idSobre,@precioFinal)", conexion.retornarCN());

                    cmd.Parameters.AddWithValue("@fechaEnvio", fechaEnvio);
                    cmd.Parameters.AddWithValue("@fk_idClienteEmisor", emisor.IdPersona);
                    cmd.Parameters.AddWithValue("@fk_idClienteReceptor", receptor.IdPersona);
                    cmd.Parameters.AddWithValue("@fk_idSobre", mercanciaEnviada.IdMercancia);
                    cmd.Parameters.AddWithValue("@precioFinal", mercanciaEnviada.CalcularPrecioFinal());
                }

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error de alta de Sobre {0}", ex.ToString());
            }

        }

        public List<Envio> GetEnvios()
        {
            List<Envio> listaEnvio = new List<Envio>();

            int idEnvio;
            Cliente Emisor;
            Cliente Receptor;
            Sobre SobreEnv;
            Paquete PaqueteEnv;
            Mercancia MercanciaEnv;
            DateTime fechaEnvio;
            /// int idEmisor;
            // int idReceptor;
            double precioFinal;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("select * from envios e inner join clientes ce on(e.fk_idClienteEmisor = ce.idCliente) inner join clientes cr on (e.fk_idClienteReceptor= cr.idCliente) left join sobres sob on (e.fk_idSobre= sob.idSobre ) left join paquetes paq on (e.fk_idPaquete =paq.idPaquete  );", conexion.retornarCN()))
                {

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Emisor = new Cliente();
                        Receptor = new Cliente();

                        idEnvio = dr.GetInt16(0);
                        fechaEnvio = dr.GetDateTime(1);
                        //idEmisor = dr.GetInt32(2);
                        //idReceptor = dr.GetInt32(3);
                        precioFinal = dr.GetDouble(6);

                        if (dr.IsDBNull(4))
                        {


                            SobreEnv = new Sobre();
                            SobreEnv.IdMercancia = dr.GetInt16(19);
                            SobreEnv.PrecioNeto = dr.GetDouble(20);
                            SobreEnv.Contenido = dr.GetString(21);
                            SobreEnv.Asegurada = dr.GetBoolean(22);
                            SobreEnv.AumSeguro = dr.GetDouble(23);
                            SobreEnv.LargoRecorrido = dr.GetBoolean(24);
                            SobreEnv.Peso = dr.GetDouble(25);
                            MercanciaEnv = SobreEnv;


                            //idSobre = dr.GetInt32(5);
                            //Console.WriteLine("idS", idSobre);// solo para probar en consola
                            //tipoMercancia = "sobre";
                            //listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idSobre, tipoMercancia, precioFinal));
                        }
                        else
                        {
                            PaqueteEnv = new Paquete();
                            PaqueteEnv.IdMercancia = dr.GetInt16(29);
                            PaqueteEnv.PrecioNeto = dr.GetDouble(30);
                            PaqueteEnv.Contenido = dr.GetString(31);
                            PaqueteEnv.Asegurada = dr.GetBoolean(32);
                            PaqueteEnv.AumSeguro = dr.GetDouble(33);
                            PaqueteEnv.LargoRecorrido = dr.GetBoolean(34);
                            PaqueteEnv.Volumen = dr.GetDouble(35);
                            MercanciaEnv = PaqueteEnv;
                            //idPaquete = dr.GetInt32(4);
                            //Console.WriteLine("idP", idPaquete);// solo para probar en consola
                            //tipoMercancia = "Paquete";
                            //listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idPaquete, tipoMercancia, precioFinal));
                        }

                        //cargo el cliente emisor, tambien se puede hacer con el constructor. Pero asi sera mas sencillo corregir errores
                        Emisor.IdPersona = dr.GetInt16(7);
                        Emisor.Cuil = dr.GetInt16(8);
                        Emisor.Nombre = dr.GetString(9);
                        Emisor.Apellido = dr.GetString(10);
                        Emisor.Direccion = dr.GetString(11);
                        Emisor.Telefono = dr.GetString(12);
                        //cargo el cliente receptor, tambien se puede hacer con el constructor. Pero asi sera mas sencillo corregir errores
                        Receptor.IdPersona = dr.GetInt16(13);
                        Receptor.Cuil = dr.GetInt32(14);
                        Receptor.Nombre = dr.GetString(15);
                        Receptor.Apellido = dr.GetString(16);
                        Receptor.Direccion = dr.GetString(17);
                        Receptor.Telefono = dr.GetString(18);


                        Envio nuevoEnvio = new Envio(idEnvio, fechaEnvio, Emisor, Receptor, MercanciaEnv);
                        //Envio nuevoEnvio = new Envio(idEnvio, fechaEnvio, Emisor, Receptor, MercanciaEnv, precioFinal);
                        listaEnvio.Add(nuevoEnvio);
                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error de alta de Sobre {0}", ex.ToString());
            }


            return (listaEnvio);

        }
        public Envio GetEnvio(int idEnvio)
        {
            Envio EnvioBuscado = null;
            Cliente Emisor;
            Cliente Receptor;
            Sobre SobreEnv;
            Paquete PaqueteEnv;
            Mercancia MercanciaEnv;
            DateTime fechaEnvio;
            /// int idEmisor;
            // int idReceptor;
            double precioFinal;

            try
            {
                conexion.abrir();
                using (cmd = new MySqlCommand("select * from envios e inner join clientes ce on(e.fk_idClienteEmisor = ce.idCliente) inner join clientes cr on (e.fk_idClienteReceptor= cr.idCliente) left join sobres sob on (e.fk_idSobre= sob.idSobre ) left join paquetes paq on (e.fk_idPaquete =paq.idPaquete  ) where idEnvios=@idEnvio;", conexion.retornarCN()))
                {
                    //idEnvio
                    cmd.Parameters.AddWithValue("@idEnvio", idEnvio);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Emisor = new Cliente();
                        Receptor = new Cliente();

                        //idEnvio = dr.GetInt16(0);
                        fechaEnvio = dr.GetDateTime(1);
                        //idEmisor = dr.GetInt32(2);
                        //idReceptor = dr.GetInt32(3);
                        precioFinal = dr.GetDouble(6);

                        if (dr.IsDBNull(4))
                        {

                            SobreEnv = new Sobre();
                            //SobreEnv = new Sobre(dr.GetString(21), dr.GetBoolean(22), dr.GetBoolean(24), dr.GetDouble(23),dr.GetDouble(20), dr.GetDouble(25));
                            SobreEnv.IdMercancia = dr.GetInt16(19);
                            SobreEnv.PrecioNeto = dr.GetDouble(20);
                            SobreEnv.Contenido = dr.GetString(21);
                            SobreEnv.Asegurada = dr.GetBoolean(22);
                            SobreEnv.AumSeguro = dr.GetDouble(23);
                            SobreEnv.LargoRecorrido = dr.GetBoolean(24);
                            SobreEnv.Peso = dr.GetDouble(25);
                            MercanciaEnv = SobreEnv;
                        }
                        else
                        {
                            PaqueteEnv = new Paquete();
                            PaqueteEnv.IdMercancia = dr.GetInt16(29);
                            PaqueteEnv.PrecioNeto = dr.GetDouble(30);
                            PaqueteEnv.Contenido = dr.GetString(31);
                            PaqueteEnv.Asegurada = dr.GetBoolean(32);
                            PaqueteEnv.AumSeguro = dr.GetDouble(33);
                            PaqueteEnv.LargoRecorrido = dr.GetBoolean(34);
                            PaqueteEnv.Volumen = dr.GetDouble(35);
                            MercanciaEnv = PaqueteEnv;
                        }

                        //cargo el cliente emisor, tambien se puede hacer con el constructor. Pero asi sera mas sencillo corregir errores
                        Emisor.IdPersona = dr.GetInt16(7);
                        Emisor.Cuil = dr.GetInt16(8);
                        Emisor.Nombre = dr.GetString(9);
                        Emisor.Apellido = dr.GetString(10);
                        Emisor.Direccion = dr.GetString(11);
                        Emisor.Telefono = dr.GetString(12);
                        //cargo el cliente receptor, tambien se puede hacer con el constructor. Pero asi sera mas sencillo corregir errores
                        Receptor.IdPersona = dr.GetInt16(13);
                        Receptor.Cuil = dr.GetInt32(14);
                        Receptor.Nombre = dr.GetString(15);
                        Receptor.Apellido = dr.GetString(16);
                        Receptor.Direccion = dr.GetString(17);
                        Receptor.Telefono = dr.GetString(18);


                        EnvioBuscado = new Envio(idEnvio, fechaEnvio, Emisor, Receptor, MercanciaEnv);
                        //EnvioBuscado = new Envio(idEnvio, fechaEnvio, Emisor, Receptor, MercanciaEnv, precioFinal);

                    }
                    dr.Close();
                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                Logger.Error("Error de alta de Sobre {0}", ex.ToString());
            }


            return (EnvioBuscado);

        }

        //Obtener recaudacion anual para la grafica
        public Double[] RecaudacionAnual(int year)
        {
            //Variables auxiliares
            Double[] valores = new Double[12];

            int i = 0;

            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("select sum(precioFinal) as recaudacion from envios where year(fechaEnvio) = @year group by month(fechaEnvio)", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@year", year);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        valores[i] = Convert.ToDouble(dr[0]);
                        i++;
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la consulta" + ex.ToString());
                }
                return valores;
            }


        }


        /// <summary>
        /// Cantidad total de envios en el mes actual
        /// </summary>
        public int TotalEnvios()
        {
            int total = 0;
            using (conexion.retornarCN())
            {
                try
                {
                    conexion.abrir();
                    cmd = new MySqlCommand("select count(idEnvios) from envios where (month(fechaEnvio) = month(curdate())) and (year(fechaEnvio) = year(curdate()))", conexion.retornarCN());
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        total = Convert.ToInt32(dr[0]);
                    }
                    dr.Close();
                    conexion.cerrar();
                }
                catch (Exception ex)
                {
                    Logger.Error("Error total envios {0}", ex.ToString());
                    //MessageBox.Show("Error en la consulta");
                }
            }
            return total;

        }

    }
}

