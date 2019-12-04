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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ABMVehiculos()
        {
            InitializeComponent();

            //ListVehiculosEncontrados = motoBD.GetMotos("test");
            //lbxResultBusqueda.ItemsSource = ListVehiculosEncontrados;
            //test.ItemsSource = ListTest;

        }

        //Variables y colecciones de datos auxiliares
        HelperDeArchivos.Reporte reporte = new HelperDeArchivos.Reporte();
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
            try
            {
                VistasVehiculo.AltaMoto pagina = new VistasVehiculo.AltaMoto(motoBD);
                frmNuevoVehiculo.Content = pagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo dar de alta al Vehiculo, quizas ingreso algun dato incorrectamente");
                Logger.Warn("No se pudo dar de Vehiculo" + ex);
            }
        }

        private void rdoFurgoneta_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                VistasVehiculo.AltaFurgoneta pagina = new VistasVehiculo.AltaFurgoneta(furgonetaBD);
                frmNuevoVehiculo.Content = pagina;
            }
            catch (Exception ex)
            {
                Logger.Warn("No se pudo dar de Vehiculo" + ex);
            }
        }

        private void rdoAvion_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                VistasVehiculo.AltaAvion pagina = new VistasVehiculo.AltaAvion(avionBD);
                frmNuevoVehiculo.Content = pagina;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo dar de alta al Vehiculo, quizas ingreso algun dato incorrectamente");
                Logger.Warn("No se pudo dar de Vehiculo" + ex);
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el Vehiculo");
                Logger.Warn("Error al buscar Vehiculo" + ex);
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
            try
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
            catch (Exception ex)
            {

                Logger.Warn("Ver Vehiculo" + ex);
            }
        }

        //Cargar formulario de modificacion de un vehiculo
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al Modificar el Vehiculo");
                Logger.Warn("Modificar Vehiculo" + ex);
            }
        }

        //Cargar formulario de eliminacion de vehiculo
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
                Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

                if (vehiculoEncontrado != null)
                {
                    VistasVehiculo.EliminarVehiculo pagina = new VistasVehiculo.EliminarVehiculo((Vehiculo)vehiculoEncontrado, motoBD, furgonetaBD, avionBD, this);
                    frmAcciones.Content = pagina;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar el Vehiculo");
                Logger.Warn("Eliminar Vehiculo" + ex);
            }
        }

        private void btnAsociarSupervisor_Click(object sender, RoutedEventArgs e)
        {

        }

        //Exportar tabla Motos pdf
        private void btnExportarMotos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reporte.generarReporteMotos();
                MessageBox.Show("Reporte de motos generado");
            }
            catch (Exception ex)
            {
                Logger.Error("Error generando el reporte" + ex);
                MessageBox.Show("Error generando el reporte \n");
            }
        }

        //Exportar tabla Furgonetas pdf
        private void btnExportarFurgonetas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reporte.generarReporteFurgonetas();
                MessageBox.Show("Reporte de furgonetas generado");
            }
            catch (Exception ex)
            {
                Logger.Error("Error generando el reporte" + ex);
                MessageBox.Show("Error generando el reporte \n");
            }
        }

        //Exportar tabla Aviones pdf
        private void btnExportarAviones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reporte.generarReporteAviones();
                MessageBox.Show("Reporte de aviones generado");
            }
            catch (Exception ex)
            {
                Logger.Error("Error generando el reporte" + ex);
                MessageBox.Show("Error generando el reporte \n");
            }
        }
    }
   
}
