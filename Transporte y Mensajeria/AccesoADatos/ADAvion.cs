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
    class ADAvion
    {
        Conexion conexion = new Conexion();
        MySqlCommand cmd;
        MySqlDataReader dr;


        //Para dar de alta un avion
        public void AltaAvion(Avion avion)
        {

            try
            {
                conexion.abrir();

                cmd = new MySqlCommand("Insert into aviones(descripcion,fechaCompra,precioCompra,aumento) values (@descripcion,@fechaCompra,@precioCompra,@aumento)", conexion.retornarCN());


                cmd.Parameters.AddWithValue("@descripcion", avion.Descripcion);
                cmd.Parameters.AddWithValue("@fechaCompra", avion.FechaCompra);
                cmd.Parameters.AddWithValue("@precioCompra", avion.PrecioCompra);
                cmd.Parameters.AddWithValue("@aumento", avion.Aumento);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                //Loguear el error
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }
        //asignar a un supervisor el avion
        public void agregarSupervisor(int idAvion, int idSup)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("UPDATE aviones SET fk_idSupervisor= @idSup WHERE idAvion = @idAvion", conexion.retornarCN());
                //UPDATE `transportemensajeria`.`motos` SET `fk_idSupervisor` = '12' WHERE (`idMoto` = '1');
                cmd.Parameters.AddWithValue("@idSup", idSup);
                cmd.Parameters.AddWithValue("@idAvion", idAvion);
                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
        }

        //Eliminar avion
        public void BajaAvion(int idAvion)
        {
            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("DELETE FROM motos WHERE idAvion = @idAvion", conexion.retornarCN());

                cmd.Parameters.AddWithValue("@idAvion", idAvion);

                cmd.ExecuteNonQuery();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }

        }
        //////Devolver listado de aviones
        public List<Avion> GetAviones()
        {
            List<Avion> listaAviones = new List<Avion>();
            Avion nuevoAvion;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from aviones", conexion.retornarCN());
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);

                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    nuevoAvion = new Avion(descripcion, fechaCompra, precioCompra, aumento);
                    listaAviones.Add(nuevoAvion);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaAviones;
        }
        //////Devolver listado de aviones por año de compra
        public List<Avion> GetAviones(int anioCompra)
        {
            List<Avion> listaAviones = new List<Avion>();
            Avion nuevaAvion;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;


            try
            {
                conexion.abrir();
                cmd = new MySqlCommand("Select * from aviones where year(fechaCompra) = @anioCompra ", conexion.retornarCN());
                cmd.Parameters.AddWithValue("@anioCompra", anioCompra);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    descripcion = dr.GetString(1);
                    aumento = dr.GetDouble(2);
                    fechaCompra = dr.GetDateTime(3);
                    precioCompra = dr.GetDouble(4);
                    nuevaAvion = new Avion(descripcion, fechaCompra, precioCompra, aumento);
                    listaAviones.Add(nuevaAvion);

                }
                dr.Close();
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaAviones;
        }
        //////Devolver listado de motos por marca
        public List<Avion> GetAviones(string descripcionMarca)
        {
            List<Avion> listaAviones = new List<Avion>();
            Avion nuevoAvion;
            string descripcion;
            double aumento;
            DateTime fechaCompra;
            double precioCompra;

            try
            {
                conexion.abrir();

                using (cmd = new MySqlCommand("Select * from aviones where descripcion like '%@descripcionMarca%' ", conexion.retornarCN()))
                {

                    cmd.Parameters.AddWithValue("@descripcionMarca", descripcionMarca);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        descripcion = dr.GetString(1);
                        aumento = dr.GetDouble(2);
                        fechaCompra = dr.GetDateTime(3);
                        precioCompra = dr.GetDouble(4);
                        nuevoAvion = new Avion(descripcion, fechaCompra, precioCompra, aumento);
                        listaAviones.Add(nuevoAvion);

                    }
                    dr.Close();

                }
                conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la consulta" + ex.ToString());
            }
            return listaAviones;
        }
    }
}
