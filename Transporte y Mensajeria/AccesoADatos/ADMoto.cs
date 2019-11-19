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
    class ADMoto
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;


        //Para dar de alta una moto
        public void AltaMoto(Moto moto)
        {

            try
            {
                conexion.abrir();

                cmd = new MySqlCommand("Insert into motos(descripcion,fechaCompra,precioCompra,aumento,cilindrada) values (@descripcion,@fechaCompra,@precioCompra,@aumento,@cilindrada)", conexion.retornarCN());

                //cmd = new MySqlCommand("Insert into motos(descripcion,fechaCompra,precioCompra,cilindrada) values (@descripcion,@fechaCompra,@precioCompra,@cilindrada)", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@descripcion", moto.Descripcion);
                cmd.Parameters.AddWithValue("@fechaCompra", moto.FechaCompra);
                cmd.Parameters.AddWithValue("@precioCompra", moto.PrecioCompra);
                cmd.Parameters.AddWithValue("@aumento", moto.Aumento);
                cmd.Parameters.AddWithValue("@cilindrada", moto.Cilindrada);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }
        //metodo para agregar clave de supervisor a cargo del vehiculo
        public void agregarSupervisor(int idMoto, int idSup)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE motos SET fk_idSupervisor= @idSup WHERE idMoto = @idMoto", conexion.retornarCN());
                //UPDATE `transportemensajeria`.`motos` SET `fk_idSupervisor` = '12' WHERE (`idMoto` = '1');
                cmd.Parameters.AddWithValue("@idSup", idSup);
                cmd.Parameters.AddWithValue("@idMoto", idMoto);
                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }
        //Eliminar moto
        public void BajaMoto(int idMoto)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("DELETE FROM motos WHERE idMoto = @idMoto", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@idMoto", idMoto);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }

        //////Devolver listado de motos
        public List<Moto> GetMotos()
        {
            List<Moto> listaMotos = new List<Moto>();
            Moto nuevaMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from motos", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);

                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    cilindrada = dr.GetInt32(5);
                    nuevaMoto = new Moto(descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                    listaMotos.Add(nuevaMoto);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }
        //////Devolver listado de motos por año de compra
        public List<Moto> GetMotos(int anioCompra)
        {
            List<Moto> listaMotos = new List<Moto>();
            Moto nuevaMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from motos where year(fechaCompra) = @anioCompra ", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);
                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    cilindrada = dr.GetInt32(5);
                    nuevaMoto = new Moto(descripcion, fechaCompra, precioCompra, cilindrada, aumento);
                    listaMotos.Add(nuevaMoto);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaMotos;
        }
        //////Devolver listado de motos por marca
        public List<Moto> GetMotos(string descripcionMarca)
        {
            List<Moto> listaMotos = new List<Moto>();
            Moto nuevaMoto;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            int cilindrada;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN()))
                {
                    //cmd = new MySqlCommand("Select * from motos where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                    cmd.Parameters.AddWithValue("@descripcionMarca", descripcionMarca);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        cilindrada = dr.GetInt32(5);
                        nuevaMoto = new Moto(descripcion, fechaCompra, precioCompra, cilindrada, aumento);
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
    }
}
