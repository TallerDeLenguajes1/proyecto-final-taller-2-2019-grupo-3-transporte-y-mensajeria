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

namespace UI_WPF.Vistas.VistasMercancia
{
    /// <summary>
    /// Lógica de interacción para EliminarMercancia.xaml
    /// </summary>
    public partial class EliminarMercancia : Page
    {
        public EliminarMercancia(Mercancia mercancia, AccesoADatos.ADSobre sobreBD, AccesoADatos.ADPaquete paqueteBD, ABMMercancia mainPage)
        {
            InitializeComponent();
            this.mercancia = mercancia;
            this.sobreBD = sobreBD;
            this.paqueteBD = paqueteBD;
            this.mainPage = mainPage;

            Rellenar();
        }

        //Variables y colecciones de datos auxiliares
        Mercancia mercancia;
        AccesoADatos.ADSobre sobreBD;
        AccesoADatos.ADPaquete paqueteBD;
        ABMMercancia mainPage;

        //Rellenar el label lblEliminar segun el tipo de vehiculo con el que se este trabajando
        private void Rellenar()
        {
            if (mercancia is Sobre)
            {
                lblEliminar.Content = "ELIMINAR SOBRE";
            }

            if (mercancia is Paquete)
            {
                lblEliminar.Content = "ELIMINAR PAQUETE";
            }
        }

        //Si eliminar Mercancia
        private void btnSiEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Segun el tipo de mercancia cargo su pagina correspondiente (Hacer con switch)
            if (mercancia is Sobre)
            {
                sobreBD.BajaSobre(mercancia.IdMercancia);
            }

            if (mercancia is Paquete)
            {
                paqueteBD.BajaPaquete(mercancia.IdMercancia);
            }

            grdContent.Visibility = Visibility.Collapsed;
            mainPage.ActualizarResultadosBusqueda();
        }

        //No eliminar Mercancia
        private void btnNoEliminar_Click(object sender, RoutedEventArgs e)
        {
            grdContent.Visibility = Visibility.Collapsed;
        }
    }
}
