using EntidadesDelProyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_WPF.Vistas.VistasVehiculo
{
    /// <summary>
    /// Lógica de interacción para DatosMoto.xaml
    /// </summary>
    public partial class DatosMoto : Page
    {
        public DatosMoto(Moto moto)
        {
            InitializeComponent();
            this.moto = moto;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Moto moto;

        private void RellenarCampos()
        {
            tbxModelo.Text = moto.Modelo;
            tbxFechaCompra.Text = Convert.ToString(moto.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(moto.PrecioCompra);
            tbxCilindrada.Text = Convert.ToString(moto.Cilindrada);
        }
    }
}
