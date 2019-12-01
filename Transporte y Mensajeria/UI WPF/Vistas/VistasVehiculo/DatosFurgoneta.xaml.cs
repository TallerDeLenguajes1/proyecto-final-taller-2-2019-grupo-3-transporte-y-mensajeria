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
    /// Lógica de interacción para DatosFurgoneta.xaml
    /// </summary>
    public partial class DatosFurgoneta : Page
    {
        public DatosFurgoneta(Furgoneta furgoneta)
        {
            InitializeComponent();
            this.furgoneta = furgoneta;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Furgoneta furgoneta;

        private void RellenarCampos()
        {
            tbxModelo.Text = furgoneta.Modelo;
            tbxFechaCompra.Text = Convert.ToString(furgoneta.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(furgoneta.PrecioCompra);
            tbxCapacidad.Text = Convert.ToString(furgoneta.CapacidadCarga);
        }
    }
}
