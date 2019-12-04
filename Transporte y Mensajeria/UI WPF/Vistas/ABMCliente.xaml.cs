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
    /// Lógica de interacción para ABMCliente.xaml
    /// </summary>
    public partial class ABMCliente : Page
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ABMCliente(AccesoADatos.ADCliente clienteBD)
        {
            InitializeComponent();
            this.clienteBD = clienteBD;

            //lbxResultBusqueda.ItemsSource = ListClientesEncontrados;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADCliente clienteBD;
        HelperDeArchivos.Reporte reporte = new HelperDeArchivos.Reporte();
        
        List<Cliente> ListClientesEncontrados = new List<Cliente>();

        //Alta cliente
        private void btnAltaCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Declaracion de variables para tomar el contenido del formulario
                int cuil = Convert.ToInt32(tbxCuil.Text);
                string nombre = tbxNombre.Text;
                string apellido = tbxApellido.Text;
                string direccion = tbxDireccion.Text;
                string telefono = tbxTelefono.Text;
                Cliente nuevoCliente = new Cliente(cuil, nombre, apellido, direccion, telefono);
                clienteBD.AltaCliente(nuevoCliente);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo dar de alta al Cliente, quizas ingreso algun dato incorrectamente");
                Logger.Warn("No se pudo dar de Alta" + ex);
            }

        }

        //Buscar un cliente segun el metodo elegido
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
                ListClientesEncontrados.Clear();
                lbxResultBusqueda.Items.Clear();
                string contenido = tbxBuscar.Text;

                //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
                if (contenido != "")
                {
                    if (cboBuscar.Text == "Cuil")
                    {
                        Cliente clienteBuscado = clienteBD.GetClientes(Convert.ToInt32(contenido));
                        if (clienteBuscado != null)
                        {
                            ListClientesEncontrados.Add(clienteBuscado);

                        }
                    }
                    

                    if (cboBuscar.Text == "Nombre")
                    {
                        ListClientesEncontrados = clienteBD.GetClientes(contenido);
                    }

                    foreach (var item in ListClientesEncontrados)
                    {
                        lbxResultBusqueda.Items.Add(item);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar al cliente"+ex.ToString());
                Logger.Warn("Error al buscar al cliente");
            }
        }

        //Ver datos de un cliente
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Tomo el cliente seleccionado del listbox de resultados de busqueda
                Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

                if (clienteEncontrado != null)
                {
                    frmAcciones.Content = new VistasCliente.DatosCliente((Cliente)lbxResultBusqueda.SelectedItem);
                }

            }
            catch (Exception ex)
            {
                Logger.Warn("Ver Cliente" + ex);
            }
            
        }

        //Modificar datos de un cliente
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Tomo el cliente seleccionado del listbox de resultados de busqueda
                Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

                if (clienteEncontrado != null)
                {
                    frmAcciones.Content = new VistasCliente.ModificarCliente((Cliente)lbxResultBusqueda.SelectedItem, clienteBD, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Modificar al cliente");
                Logger.Warn("Modificar Cliente" + ex);
            }
        }

        //Eliminar Cliente
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Tomo el cliente seleccionado del listbox de resultados de busqueda
                Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

                if (clienteEncontrado != null)
                {
                    VistasCliente.EliminarCliente pagina = new VistasCliente.EliminarCliente((Cliente)lbxResultBusqueda.SelectedItem, clienteBD, this);
                    frmAcciones.Content = pagina;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar al cliente");
                Logger.Warn("Eliminar Cliente" + ex);
            }
        }

        //Actualizar los resultados de buequeda luego de realizar una accion
        public void ActualizarResultadosBusqueda()
        {
            ListClientesEncontrados.Clear();
            lbxResultBusqueda.Items.Refresh();
        }

        //Exportar tabla Clientes pdf
        private void btnExportarClientes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reporte.generarReporteClientes();
                MessageBox.Show("Reporte de clientes generado");
            }
            catch (Exception ex)
            {
                Logger.Error("Error generando el reporte" + ex);
                MessageBox.Show("Error generando el reporte \n");
            }  
        }
    }
}
