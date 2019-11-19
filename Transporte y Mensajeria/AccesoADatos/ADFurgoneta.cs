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
    class ADFurgoneta
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;


        //Para dar de alta una moto
        public void AltaFurgoneta(Furgoneta furgoneta)
        {

            try
            {
                conexion.abrir();

                cmd = new MySqlCommand("Insert into furgonetas(descripcion,fechaCompra,precioCompra,aumento,capacidadCarga) values (@descripcion,@fechaCompra,@precioCompra,@aumento,@capacidadCarga)", conexion.retornarCN());
                //cmd = new MySqlCommand("Insert into furgonetas(descripcion,fechaCompra,precioCompra,aumento) values (@descripcion,@fechaCompra,@precioCompra,@aumento)", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@descripcion", furgoneta.Descripcion);
                cmd.Parameters.AddWithValue("@fechaCompra", furgoneta.FechaCompra);
                cmd.Parameters.AddWithValue("@precioCompra", furgoneta.PrecioCompra);
                cmd.Parameters.AddWithValue("@aumento", furgoneta.Aumento);
                cmd.Parameters.AddWithValue("@capacidadCarga", furgoneta.CapacidadCarga);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }
        public void agregarSupervisor(int idFurgoneta, int idSup)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE furgonetas SET fk_idSupervisor= @idSup WHERE idFurgoneta = @idFurgoneta", conexion.retornarCN());
                //UPDATE `transportemensajeria`.`motos` SET `fk_idSupervisor` = '12' WHERE (`idMoto` = '1');
                cmd.Parameters.AddWithValue("@idSup", idSup);
                cmd.Parameters.AddWithValue("@idFurgoneta", idFurgoneta);
                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        //Eliminar furgoneta
        public void BajaFurgoneta(int idFurgoneta)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("DELETE FROM motos WHERE idFurgoneta = @idFurgoneta", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@idFurgoneta", idFurgoneta);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }

        //////Devolver listado de motos
        public List<Furgoneta> GetFurgonetas()
        {
            List<Furgoneta> listaFurgonetas = new List<Furgoneta>();
            Furgoneta nuevaFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from furgoneta", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);

                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    capacidad = dr.GetDouble(5);
                    nuevaFurgoneta = new Furgoneta(descripcion, fechaCompra, precioCompra, capacidad, aumento);
                    listaFurgonetas.Add(nuevaFurgoneta);
                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgonetas;
        }
        //////Devolver listado de motos por año de comparacion
        public List<Furgoneta> GetFurgonetas(int anioCompra)
        {
            List<Furgoneta> listaFurgoneta = new List<Furgoneta>();
            Furgoneta nuevaFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from furgoneta where year(fechaCompra) = @anioCompra ", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);
                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    capacidad = dr.GetDouble(5);
                    nuevaFurgoneta = new Furgoneta(descripcion, fechaCompra, precioCompra, capacidad, aumento);
                    listaFurgoneta.Add(nuevaFurgoneta);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgoneta;
        }
        //////Devolver listado de furgoneta por marca
        public List<Furgoneta> GetFurgonetas(string descripcionMarca)
        {
            List<Furgoneta> listaFurgonetas = new List<Furgoneta>();
            Furgoneta nuevaFurgoneta;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            double capacidad;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from furgonetas where descripcion like '%@descripcionMarca%' ", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@descripcionMarca", descripcionMarca);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);
                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    capacidad = dr.GetInt32(5);
                    nuevaFurgoneta = new Furgoneta(descripcion, fechaCompra, precioCompra, capacidad, aumento);
                    listaFurgonetas.Add(nuevaFurgoneta);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaFurgonetas;
        }
    }
}
