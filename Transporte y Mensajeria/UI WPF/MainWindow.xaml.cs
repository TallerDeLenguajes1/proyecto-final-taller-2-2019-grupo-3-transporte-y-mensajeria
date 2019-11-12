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
using AccesoADatos;
using EntidadesDelProyecto;
using UI_WPF.Vistas;

namespace UI_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Instanciaciones necesaria para conexion a base de datos
        AccesoADatos.ADCliente clienteBD = new AccesoADatos.ADCliente();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("Vistas/AltaCliente.xaml",UriKind.RelativeOrAbsolute));
            frmAltaCliente.Content = new AltaCliente(clienteBD);
        }
    }
}
