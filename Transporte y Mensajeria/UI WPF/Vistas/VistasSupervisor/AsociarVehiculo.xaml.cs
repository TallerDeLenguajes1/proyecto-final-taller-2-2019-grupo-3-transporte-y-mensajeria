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
    /// Lógica de interacción para AsociarVehiculo.xaml
    /// </summary>
    public partial class AsociarVehiculo : Page
    {
        public AsociarVehiculo(Supervisor supervisor, AccesoADatos.ADSupervisor supervisorBD, ABMSupervisor mainPage)
        {
            InitializeComponent();
            this.supervisor = supervisor;
            this.supervisorBD = supervisorBD;
            this.mainPage = mainPage;
        }

        //Variables y colecciones de datos auxiliares
        Supervisor supervisor;
        ABMSupervisor mainPage;
        AccesoADatos.ADSupervisor supervisorBD;
        AccesoADatos.ADMoto motoBD = new AccesoADatos.ADMoto();
        AccesoADatos.ADFurgoneta furgonetaBD = new AccesoADatos.ADFurgoneta();
        AccesoADatos.ADAvion avionBD = new AccesoADatos.ADAvion();
        List<Vehiculo> ListVehiculosEncontrados = new List<Vehiculo>();
        List<Vehiculo> ListMotosEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListFurgonetasEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListAvionesEncontrados = new List<Vehiculo>();

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
            ListVehiculosEncontrados.Clear();
            lbxResultBusqueda.Items.Clear();

            string contenido = tbxBuscar.Text;

            //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
            if (contenido != "")
            {
                switch (cboBuscar.Text)
                {
                    case "Modelo":
                        ListAvionesEncontrados = avionBD.GetAviones(contenido);
                        ListMotosEncontradas = motoBD.GetMotos(contenido);
                        ListFurgonetasEncontradas = furgonetaBD.GetFurgonetas(contenido);
                        ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();
                        break;
                    case "Año de compra":
                        int anioCompra = Convert.ToInt32(contenido);
                        ListAvionesEncontrados = avionBD.GetAviones(anioCompra);
                        ListMotosEncontradas = motoBD.GetMotos(anioCompra);
                        ListFurgonetasEncontradas = furgonetaBD.GetFurgonetas(anioCompra);
                        ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();
                        break;
                    default:
                        break;
                }

                foreach (var item in ListVehiculosEncontrados)
                {
                    lbxResultBusqueda.Items.Add(item);
                }
            }
        }

        private void btnAsociar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (vehiculoEncontrado != null)
            {
                ////Consulta que pone en la tupla correspondiente al vehiculo seleccionado la pk del supervisor tomado
                if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                {
                    motoBD.agregarSupervisor(vehiculoEncontrado.IdVehiculo,supervisor.IdPersona);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                {
                    furgonetaBD.agregarSupervisor(vehiculoEncontrado.IdVehiculo, supervisor.IdPersona);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                {
                    avionBD.agregarSupervisor(vehiculoEncontrado.IdVehiculo, supervisor.IdPersona);
                }
            }
        }
    }
}
