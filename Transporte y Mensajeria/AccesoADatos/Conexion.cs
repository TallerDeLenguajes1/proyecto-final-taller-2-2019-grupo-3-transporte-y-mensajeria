using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccesoADatos
{
    public class Conexion
    {

        MySqlConnection cn;

        public Conexion()
        {

            try
            {
                cn = new MySqlConnection("Server=localhost; Database=transporte_y_mensajeria; Uid=root; Pwd = 1234; Integrated Security =True");
                //MessageBox.Show("Conectado");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion: " + ex.ToString());
            }
        }

        public void abrir()
        {
            cn.Open();
        }

        public void cerrar()
        {
            cn.Close();
        }

        public MySqlConnection retornarCN()
        {
            return cn;
        }
    }
}
