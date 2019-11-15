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

namespace UI_WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para ABMVehiculos.xaml
    /// </summary>
    public partial class ABMVehiculos : Page
    {
        public ABMVehiculos()
        {
            InitializeComponent();
        }

        private void rdoMoto_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaMoto pagina = new VistasVehiculo.AltaMoto();
            frmNuevoVehiculo.Content = pagina;
        }

        private void rdoFurgoneta_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaFurgoneta pagina = new VistasVehiculo.AltaFurgoneta();
            frmNuevoVehiculo.Content = pagina;
        }

        private void rdoAvion_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaAvion pagina = new VistasVehiculo.AltaAvion();
            frmNuevoVehiculo.Content = pagina;
        }
    }
}
