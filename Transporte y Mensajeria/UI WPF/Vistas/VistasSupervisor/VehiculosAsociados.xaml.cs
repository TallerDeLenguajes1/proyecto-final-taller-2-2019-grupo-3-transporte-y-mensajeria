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
    /// Lógica de interacción para VehiculosAsociados.xaml
    /// </summary>
    public partial class VehiculosAsociados : Page
    {
        public VehiculosAsociados(Supervisor supervisor, ABMSupervisor mainPage)
        {
            InitializeComponent();
            this.supervisor = supervisor;
            this.mainPage = mainPage;


            CargarTodosVehiculos();
        }

        //Variables y colecciones de datos auxiliares
        Supervisor supervisor;
        ABMSupervisor mainPage;
        AccesoADatos.ADMoto motoBD = new AccesoADatos.ADMoto();
        AccesoADatos.ADFurgoneta furgonetaBD = new AccesoADatos.ADFurgoneta();
        AccesoADatos.ADAvion avionBD = new AccesoADatos.ADAvion();
        List<Vehiculo> ListVehiculosEncontrados = new List<Vehiculo>();
        List<Vehiculo> ListMotosEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListFurgonetasEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListAvionesEncontrados = new List<Vehiculo>();

        private void CargarTodosVehiculos()
        {
            ListVehiculosEncontrados.Clear();
            lbxResultBusqueda.Items.Clear();
            ListAvionesEncontrados = avionBD.GetAvionesDeSupervisor(supervisor.IdPersona);
            ListMotosEncontradas = motoBD.GetMotosDeSupervisor(supervisor.IdPersona);
            ListFurgonetasEncontradas = furgonetaBD.GetFurgonetasDeSupervisor(supervisor.IdPersona);
            ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();

            foreach (var item in ListVehiculosEncontrados)
            {
                lbxResultBusqueda.Items.Add(item);
            }
        }

        //Buscar vehiculos asociados al supervisor elegido
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
            ListVehiculosEncontrados.Clear();
            lbxResultBusqueda.Items.Clear();

            string contenido = tbxBuscar.Text;

            if (cboBuscar.Text == "Todos")
            {
                ListAvionesEncontrados = avionBD.GetAvionesDeSupervisor(supervisor.IdPersona);
                ListMotosEncontradas = motoBD.GetMotosDeSupervisor(supervisor.IdPersona);
                ListFurgonetasEncontradas = furgonetaBD.GetFurgonetasDeSupervisor(supervisor.IdPersona);
                ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();
            }

            //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
            if (contenido != "")
            {
                switch (cboBuscar.Text)
                {
                    case "Modelo":
                        ListAvionesEncontrados = avionBD.GetAviones(contenido, supervisor.IdPersona);
                        ListMotosEncontradas = motoBD.GetMotos(contenido, supervisor.IdPersona);
                        ListFurgonetasEncontradas = furgonetaBD.GetFurgonetas(contenido, supervisor.IdPersona);
                        ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();
                        break;
                    case "Año de compra":
                        int anioCompra = Convert.ToInt32(contenido);
                        ListAvionesEncontrados = avionBD.GetAviones(anioCompra, supervisor.IdPersona);
                        ListMotosEncontradas = motoBD.GetMotos(anioCompra, supervisor.IdPersona);
                        ListFurgonetasEncontradas = furgonetaBD.GetFurgonetas(anioCompra, supervisor.IdPersona);
                        ListVehiculosEncontrados = ListMotosEncontradas.Concat(ListFurgonetasEncontradas).Concat(ListAvionesEncontrados).ToList();
                        break;
                    default:
                        break;
                }
            }

            foreach (var item in ListVehiculosEncontrados)
            {
                lbxResultBusqueda.Items.Add(item);
            }
        }

        //Quitar vehiculo del cargo del supervisor elegido
        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
            {
                motoBD.QuitarSupervisor((Moto)vehiculoEncontrado);
                CargarTodosVehiculos();
            }

            if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
            {
                furgonetaBD.QuitarSupervisor((Furgoneta)vehiculoEncontrado);
                CargarTodosVehiculos();
            }

            if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
            {
                avionBD.QuitarSupervisor((Avion)vehiculoEncontrado);
                CargarTodosVehiculos();
            }
        }


    }
}
