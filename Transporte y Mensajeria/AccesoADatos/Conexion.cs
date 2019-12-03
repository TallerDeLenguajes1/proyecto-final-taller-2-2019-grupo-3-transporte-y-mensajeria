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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        MySqlConnection cn;

        public Conexion()
        {
            string cadenaConexion = "Server=localhost;" +
                " Database=transporte_mensajeria;" + 
                //"Pwd = 1234" +
                " Uid=root;" +
                " Integrated Security =True";
            cn = new MySqlConnection(cadenaConexion);
        }

        public void abrir()
        {
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                Logger.Error("Error CONEXION PARA ABRIR LA CONEXION {0}", ex.ToString());
                MessageBox.Show("Error de conexion");
            }
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
