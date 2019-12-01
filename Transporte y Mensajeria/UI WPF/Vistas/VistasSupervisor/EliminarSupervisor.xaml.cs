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

namespace UI_WPF.Vistas.VistasSupervisor
{
    /// <summary>
    /// Lógica de interacción para EliminarSupervisor.xaml
    /// </summary>
    public partial class EliminarSupervisor : Page
    {
        public EliminarSupervisor(Supervisor supervisor, AccesoADatos.ADSupervisor supervisorBD, ABMSupervisor mainPage)
        {
            InitializeComponent();
            this.supervisor = supervisor;
            this.supervisorBD = supervisorBD;
            this.mainPage = mainPage;
        }

        //Variables y colecciones de datos auxiliares
        Supervisor supervisor;
        AccesoADatos.ADSupervisor supervisorBD;
        ABMSupervisor mainPage;

        //Si eliminar supervisor
        private void btnSiEliminar_Click(object sender, RoutedEventArgs e)
        {
            supervisorBD.BajaSupervisor(supervisor.Cuil);
            grdContent.Visibility = Visibility.Collapsed;
            mainPage.ActualizarResultadosBusqueda();
        }

        //No eliminar supervisor
        private void btnNoEliminar_Click(object sender, RoutedEventArgs e)
        {
            grdContent.Visibility = Visibility.Collapsed;
        }
    }
}
