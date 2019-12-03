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
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }
        //public Envio cargarEnvio(int idEnvio, DateTime fechaEnvio, int idEmisor, int idReceptor,int idMercancia, string tipo, double precioFinal)
        //{
        //    Envio nuevoEnvio = new Envio(idEnvio, fechaEnvio, GetClientes(idEmisor), GetClientes(idReceptor),GetMercancia(idMercancia,tipo) , precioFinal);
        //    //nuevoEnvio.Emisor = GetClientes(idEmisor);

        //    return(nuevoEnvio);

        //}
        //public Cliente GetClientes(int idCliente)
        //{
        //    //Variables auxiliares
        //    Cliente nuevoCliente = null;
        //    int cuil;
        //    string nombre;
        //    string apellido;
        //    string direccion;
        //    string telefono;

        //    try
        //    {
        //        conexion.abrir();
        //        //dr.Close();
        //        cmd = new MySqlCommand("Select * from clientes where idCliente=@idCliente", conexion.retornarCN());
        //        cmd.Parameters.AddWithValue("@idCliente", idCliente);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            idCliente = Convert.ToInt32(dr[0]);
        //            cuil= Convert.ToInt32(dr[1]);
        //            nombre = dr[2].ToString();
        //            apellido = dr[3].ToString();
        //            direccion = dr[4].ToString();
        //            telefono = dr[5].ToString();
        //            nuevoCliente = new Cliente(idCliente, cuil, nombre, apellido, direccion, telefono);

        //        }
        //        dr.Close();
        //        conexion.cerrar();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error en la consulta" + ex.ToString());
        //    }
        //    return nuevoCliente;
        //}

        //public Mercancia GetMercancia(int idMercancia, string tipo)
        //{
        //    Paquete nuevoPaquete;
        //    Sobre nuevoSobre;
        //    Mercancia nuevaMercancia= null;
        //    double precioNeto;
        //    string descripcion;
        //    bool asegurada;
        //    double aumSeguro;
        //    bool largoRecorrido;
        //    double volumen;
        //    double peso;


        //    if (tipo == "paquete")
        //    {
        //        conexion.abrir();
        //        cmd = new MySqlCommand("Select * from paquetes where idPaquete=@idMercancia ", conexion.retornarCN());
        //        cmd.Parameters.AddWithValue("@idMercancia", idMercancia);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            precioNeto = Convert.ToInt32(dr[1]);
        //            descripcion = dr[2].ToString();
        //            asegurada = true;//Convert.ToInt32(dr[3]);
        //            aumSeguro = Convert.ToInt32(dr[4]);
        //            largoRecorrido = true;//dr[5].ToString();
        //            volumen = Convert.ToInt32(dr[6]);
        //            nuevoPaquete = new Paquete(descripcion, asegurada, largoRecorrido, aumSeguro, precioNeto, volumen);
        //            nuevaMercancia = nuevoPaquete;
        //        }
        //        dr.Close();
        //        conexion.cerrar();
        //    }
        //    else
        //    {
        //        conexion.abrir();
        //        cmd = new MySqlCommand("Select * from sobres where idSobre=@idMercancia ", conexion.retornarCN());
        //        cmd.Parameters.AddWithValue("@idMercancia", idMercancia);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            precioNeto = Convert.ToInt32(dr[1]);
        //            descripcion = dr[2].ToString();
        //            asegurada = true;//Convert.ToInt32(dr[3]);
        //            aumSeguro = Convert.ToInt32(dr[4]);
        //            largoRecorrido = true;//dr[5].ToString();
        //            peso = Convert.ToInt32(dr[6]);
        //            nuevoSobre = new Sobre(descripcion, asegurada, largoRecorrido, aumSeguro, precioNeto, peso);
        //            nuevaMercancia = nuevoSobre;
        //        }
        //        dr.Close();
        //        conexion.cerrar();
        //    }

        //    return (nuevaMercancia);
        //}



        //public Envio getEnvio(int idEnvio)
        //{
        //    Envio nuevoEnvio= null;
        //    Cliente aux1=null;
        //    Cliente aux2=null;
        //    string tipoMercancia;
        //    DateTime fechaEnvio;
        //    int idEmisor;
        //    int idReceptor;
        //    //int idSobre;
        //    //int idPaquete;
        //    int idMercancia;
        //    double precioFinal; 

        //    try
        //    {
        //        conexion.abrir();
        //        cmd = new MySqlCommand("Select * from envios where idEnvios=@idEnvio", conexion.retornarCN());
        //        cmd.Parameters.AddWithValue("@idEnvio", idEnvio);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            idEnvio = dr.GetInt16(0);
        //            fechaEnvio = dr.GetDateTime(1);
        //            idEmisor = dr.GetInt32(2);
        //            idReceptor = dr.GetInt32(3);                  
        //            if (dr.IsDBNull(4))
        //            {
        //                //idSobre = dr.GetInt32(5);
        //                //Console.WriteLine("idS", idSobre);// solo para probar en consola
        //                idMercancia = dr.GetInt32(5);
        //                tipoMercancia = "sobre";
        //            }
        //            else
        //            {
        //                //idPaquete = dr.GetInt32(4);
        //                //Console.WriteLine("idP", idPaquete);// solo para probar en consola
        //                idMercancia = dr.GetInt32(4);
        //                tipoMercancia = "Paquete";
        //            }
        //            precioFinal = dr.GetDouble(6);
        //            dr.Close();
        //            conexion.cerrar();
        //            nuevoEnvio = cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idMercancia, tipoMercancia, precioFinal);
        //            //aux1.IdPersona = idEmisor;
        //            //aux2.IdPersona = idReceptor;
        //            //nuevoEnvio = new Envio(idEnvio, fechaEnvio,aux1,aux2, )
        //        }

        //        //dr.Close();
        //        //conexion.cerrar();
        //        //nuevoEnvio = cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idMercancia, tipoMercancia, precioFinal);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error en la consulta" + ex.ToString());
        //    }
        //    return (nuevoEnvio);

        //}

        //public List<Envio> GetEnvios()
        //{
        //    List<Envio> listaEnvio = new List<Envio>();
        //    string tipoMercancia;

        //    int idEnvio;
        //    Cliente Emisor ;
        //    Cliente Receptor ;
        //    DateTime fechaEnvio;
        //    int idEmisor;
        //    int idReceptor;
        //    double precioFinal;

        //    try
        //    {
        //        conexion.abrir();
        //        using (cmd = new MySqlCommand("select * from envios e inner join clientes ce on(e.fk_idClienteEmisor = ce.idCliente) inner join clientes cr on (e.fk_idClienteReceptor= cr.idCliente) left join sobres sob on (e.fk_idSobre= sob.idSobre ) left join paquetes paq on (e.fk_idPaquete =paq.idPaquete  );", conexion.retornarCN()))
        //        {

        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                Emisor = new Cliente();
        //                idEnvio = dr.GetInt16(0);
        //                fechaEnvio = dr.GetDateTime(1);
        //                idEmisor = dr.GetInt32(2);
        //                idReceptor = dr.GetInt32(3);
        //                precioFinal = dr.GetDouble(6);

        //                if (dr.IsDBNull(4))
        //                {
        //                    idSobre = dr.GetInt32(5);
        //                    Console.WriteLine("idS", idSobre);// solo para probar en consola
        //                    tipoMercancia = "sobre";
        //                    listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idSobre, tipoMercancia, precioFinal));
        //                }
        //                else
        //                {
        //                    idPaquete = dr.GetInt32(4);
        //                    Console.WriteLine("idP", idPaquete);// solo para probar en consola
        //                    tipoMercancia = "Paquete";
        //                    listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idPaquete, tipoMercancia, precioFinal));
        //                }

        //                Envio nuevoEnvio = new Envio();
        //            }
        //            dr.Close();
        //        }
        //        conexion.cerrar();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error en la consulta" + ex.ToString());
        //    }


        //    return (listaEnvio);

        //}
        //public List<Envio> GetEnvios()
        //{
        //    List<Envio> listaEnvio = new List<Envio>();
        //    string tipoMercancia;

        //    DateTime fechaEnvio;
        //    int idEnvio;
        //    int idEmisor;
        //    int idReceptor;
        //    int idSobre;
        //    int idPaquete;
        //    double precioFinal;

        //    try
        //    {
        //        conexion.abrir();
        //        using (cmd = new MySqlCommand("Select * from envios", conexion.retornarCN()))
        //        {
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {

        //                idEnvio = dr.GetInt16(0);
        //                fechaEnvio = dr.GetDateTime(1);
        //                idEmisor = dr.GetInt32(2);
        //                idReceptor = dr.GetInt32(3);
        //                precioFinal = dr.GetDouble(6);

        //                if (dr.IsDBNull(4))
        //                {
        //                    idSobre = dr.GetInt32(5);
        //                    Console.WriteLine("idS", idSobre);// solo para probar en consola
        //                    tipoMercancia = "sobre";
        //                    listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor,idSobre,tipoMercancia,precioFinal));
        //                }
        //                else
        //                {
        //                    idPaquete = dr.GetInt32(4);
        //                    Console.WriteLine("idP", idPaquete);// solo para probar en consola
        //                    tipoMercancia = "Paquete";
        //                    listaEnvio.Add(cargarEnvio(idEnvio, fechaEnvio, idEmisor, idReceptor, idPaquete, tipoMercancia, precioFinal));
        //                }
        //            }
        //            dr.Close();
        //        }
        //        conexion.cerrar();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error en la consulta" + ex.ToString());
        //    }


        //    return (listaEnvio);

        //    //////try
        //    //////{
        //    //////    conexion.abrir();
        //    //////    cmd = new MySqlCommand("Select fk_idPaquete from envios where fk_idPaquete is null and idEnvios = 105", conexion.retornarCN());
        //    //////    //SELECT fk_idPaquete FROM transporte_y_mensajeria.motos where  fk_idPaquete is null and idEenvios = 10;
        //    //////    dr = cmd.ExecuteReader();
        //    //////    if (dr.Read)
        //    //////    {
        //    //////        Console.WriteLine("bien");
        //    //////    }
        //    //////    conexion.cerrar();
        //    //////}
        //    //////catch (Exception ex)
        //    //////{
        //    //////    MessageBox.Show("Error en la consulta" + ex.ToString());
        //    //////}
        //}
    }
}

