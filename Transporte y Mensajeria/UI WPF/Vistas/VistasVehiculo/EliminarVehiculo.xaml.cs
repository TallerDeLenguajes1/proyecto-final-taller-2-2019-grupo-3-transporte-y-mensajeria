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
    /// Lógica de interacción para EliminarMoto.xaml
    /// </summary>
    public partial class EliminarVehiculo : Page
    {
        public EliminarVehiculo(Vehiculo vehiculo, AccesoADatos.ADMoto motoBD, AccesoADatos.ADFurgoneta furgonetaBD, AccesoADatos.ADAvion avionBD, ABMVehiculos mainPage)
        {
            InitializeComponent();
            this.vehiculo = vehiculo;
            this.motoBD = motoBD;
            this.furgonetaBD = furgonetaBD;
            this.avionBD = avionBD;
            this.mainPage = mainPage;

            Rellenar();

        }

        //Variables y colecciones de datos auxiliares
        Vehiculo vehiculo;
        AccesoADatos.ADMoto motoBD;
        AccesoADatos.ADFurgoneta furgonetaBD;
        AccesoADatos.ADAvion avionBD;
        ABMVehiculos mainPage;

        //Rellenar el label lblEliminar segun el tipo de vehiculo con el que se este trabajando
        private void Rellenar()
        {
            if (vehiculo is EntidadesDelProyecto.Moto)
            {
                lblEliminar.Content = "ELIMINAR MOTO";
            }

            if (vehiculo is EntidadesDelProyecto.Furgoneta)
            {
                lblEliminar.Content = "ELIMINAR FURGONETA";
            }

            if (vehiculo is EntidadesDelProyecto.Avion)
            {
                lblEliminar.Content = "ELIMINAR AVION";
            }
        }

        //Si eliminar Vehiculo
        private void btnSiEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Segun el tipo de vehiculo cargo su pagina correspondiente (Hacer con switch)
            if (vehiculo is EntidadesDelProyecto.Moto)
            {
                motoBD.BajaMoto(vehiculo.IdVehiculo);
            }

            if (vehiculo is EntidadesDelProyecto.Furgoneta)
            {
                furgonetaBD.BajaFurgoneta(vehiculo.IdVehiculo);
            }

            if (vehiculo is EntidadesDelProyecto.Avion)
            {
                avionBD.BajaAvion(vehiculo.IdVehiculo);
            }

            grdContent.Visibility = Visibility.Collapsed;
            mainPage.ActualizarResultadosBusqueda();
        }

        //No eliminar Vehiculo
        private void btnNoEliminar_Click(object sender, RoutedEventArgs e)
        {
            grdContent.Visibility = Visibility.Collapsed;
        }
    }
}
