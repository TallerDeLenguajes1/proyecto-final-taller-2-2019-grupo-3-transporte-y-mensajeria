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
    /// Lógica de interacción para DatosAvion.xaml
    /// </summary>
    public partial class DatosAvion : Page
    {
        public DatosAvion(Avion avion)
        {
            InitializeComponent();
            this.avion = avion;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }


        //Variables y colecciones de datos auxiliares
        Avion avion;

        private void RellenarCampos()
        {
            tbxModelo.Text = avion.Modelo;
            tbxFechaCompra.Text = Convert.ToString(avion.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(avion.PrecioCompra);
        }
    }
}
