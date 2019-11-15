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

namespace UI_WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para ABMMercancia.xaml
    /// </summary>
    public partial class ABMMercancia : Page
    {
        public ABMMercancia()
        {
            InitializeComponent();
        }

        private void rdoSobre_Checked(object sender, RoutedEventArgs e)
        {
            VistasMercancia.AltaSobre pagina = new VistasMercancia.AltaSobre();
            frmNuevaMercancia.Content = pagina;
        }

        private void rdoPaquete_Checked(object sender, RoutedEventArgs e)
        {
            VistasMercancia.AltaPaquete pagina = new VistasMercancia.AltaPaquete();
            frmNuevaMercancia.Content = pagina;
        }

        private void lbxResultCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
