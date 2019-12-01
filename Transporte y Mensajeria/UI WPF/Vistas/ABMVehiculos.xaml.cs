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
    /// Lógica de interacción para ABMVehiculos.xaml
    /// </summary>
    public partial class ABMVehiculos : Page
    {
        public ABMVehiculos()
        {
            InitializeComponent();

            //ListVehiculosEncontrados = motoBD.GetMotos("test");
            //lbxResultBusqueda.ItemsSource = ListVehiculosEncontrados;
            //test.ItemsSource = ListTest;

        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADMoto motoBD = new AccesoADatos.ADMoto();
        AccesoADatos.ADFurgoneta furgonetaBD = new AccesoADatos.ADFurgoneta();
        AccesoADatos.ADAvion avionBD = new AccesoADatos.ADAvion();
        List<Vehiculo> ListTest = new List<Vehiculo>();

        List<Vehiculo> ListVehiculosEncontrados = new List<Vehiculo>();
        List<Vehiculo> ListMotosEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListFurgonetasEncontradas = new List<Vehiculo>();
        List<Vehiculo> ListAvionesEncontrados = new List<Vehiculo>();

        public void ActualizarResultadosBusqueda()
        {
            ListVehiculosEncontrados.Clear();
            lbxResultBusqueda.Items.Clear();
        }

        private void rdoMoto_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaMoto pagina = new VistasVehiculo.AltaMoto(motoBD);
            frmNuevoVehiculo.Content = pagina;
        }

        private void rdoFurgoneta_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaFurgoneta pagina = new VistasVehiculo.AltaFurgoneta(furgonetaBD);
            frmNuevoVehiculo.Content = pagina;
        }

        private void rdoAvion_Checked(object sender, RoutedEventArgs e)
        {
            VistasVehiculo.AltaAvion pagina = new VistasVehiculo.AltaAvion(avionBD);
            frmNuevoVehiculo.Content = pagina;
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListTest = motoBD.GetMotos("C90");
            foreach (var item in ListTest)
            {
                test.Items.Add(item);
            }

        }

        //Ver datos de un vehiculo
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (vehiculoEncontrado != null)
            {
                //Segun el tipo de vehiculo cargo su pagina correspondiente (Hacer con switch)
                if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                {
                    frmAcciones.Content = new VistasVehiculo.DatosMoto((Moto)vehiculoEncontrado);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                {
                    frmAcciones.Content = new VistasVehiculo.DatosFurgoneta((Furgoneta)vehiculoEncontrado);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                {
                    frmAcciones.Content = new VistasVehiculo.DatosAvion((Avion)vehiculoEncontrado);
                }
                
            }
        }

        //Cargar formulario de modificacion de un vehiculo
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (vehiculoEncontrado != null)
            {
                //Segun el tipo de vehiculo cargo su pagina correspondiente (Hacer con switch)
                if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                {
                    frmAcciones.Content = new VistasVehiculo.ModificarMoto((Moto)vehiculoEncontrado, motoBD, this);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                {
                    frmAcciones.Content = new VistasVehiculo.ModificarFurgoneta((Furgoneta)vehiculoEncontrado, furgonetaBD, this);
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                {
                    frmAcciones.Content = new VistasVehiculo.ModificarAvion((Avion)vehiculoEncontrado, avionBD, this);
                }

            }
        }

        //Cargar formulario de eliminacion de vehiculo
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (vehiculoEncontrado != null)
            {
                VistasVehiculo.EliminarVehiculo pagina = new VistasVehiculo.EliminarVehiculo((Vehiculo)vehiculoEncontrado, motoBD, furgonetaBD, avionBD, this);
                frmAcciones.Content = pagina;
            }
        }
    }
}
