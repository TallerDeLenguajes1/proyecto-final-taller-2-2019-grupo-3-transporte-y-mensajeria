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
    /// Lógica de interacción para ABMEnvio.xaml
    /// </summary>
    public partial class ABMEnvio : Page
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ABMEnvio()
        {
            InitializeComponent();
        }

        //Variables y colecciones de datos auxiliares
        Cliente emisorSeleccionado = null;
        Cliente receptorSeleccionado = null;
        Mercancia mercanciaSeleccionada = null;
        AccesoADatos.ADSobre sobreBD = new AccesoADatos.ADSobre();
        AccesoADatos.ADPaquete paqueteBD = new AccesoADatos.ADPaquete();
        AccesoADatos.ADCliente clienteBD = new AccesoADatos.ADCliente();
        AccesoADatos.ADEnvio envioBD = new AccesoADatos.ADEnvio();

        List<Mercancia> ListSobresEncontrados = new List<Mercancia>();
        List<Mercancia> ListPaquetesEncontrados = new List<Mercancia>();
        List<Mercancia> ListMercanciasEncontradas = new List<Mercancia>();
        List<Cliente> ListEmisoresEncontrados = new List<Cliente>();
        List<Cliente> ListReceptoresEncontrados = new List<Cliente>();

        //Buscar cliente emisor del pedido
        private void btnBuscarEmisor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
                ListEmisoresEncontrados.Clear();
                lbxResultBusquedaEmisor.Items.Clear();
                string contenido = tbxBuscarEmisor.Text;

                //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
                if (contenido != "")
                {
                    switch (cboBuscarEmisor.Text)
                    {
                        case "Cuil":
                            Cliente clienteBuscado = clienteBD.GetClientes(Convert.ToInt32(contenido));
                            if (clienteBuscado != null)
                            {
                                ListEmisoresEncontrados.Add(clienteBuscado);
                            }
                            break;
                        case "Nombre":
                            //Hacer busqueda por nombre
                            break;
                        default:
                            break;
                    }

                    foreach (var item in ListEmisoresEncontrados)
                    {
                        lbxResultBusquedaEmisor.Items.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar al cliente Emisor \n Verifique los datos Ingresados");
                Logger.Warn("Error al buscar Cliente emisor ENVIO" + ex);
            }
        }

        //Buscar cliente receptor del pedido
        private void btnBuscarReceptor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
                ListEmisoresEncontrados.Clear();
                lbxResultBusquedaReceptor.Items.Clear();
                string contenido = tbxBuscarReceptor.Text;

                //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
                if (contenido != "")
                {
                    switch (cboBuscarReceptor.Text)
                    {
                        case "Cuil":
                            Cliente clienteBuscado = clienteBD.GetClientes(Convert.ToInt32(contenido));
                            if (clienteBuscado != null)
                            {
                                ListEmisoresEncontrados.Add(clienteBuscado);
                            }
                            break;
                        case "Nombre":
                            //Hacer busqueda por nombre
                            break;
                        default:
                            break;
                    }

                    foreach (var item in ListEmisoresEncontrados)
                    {
                        lbxResultBusquedaReceptor.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar al cliente Receptor\n Verifique los datos Ingresados");
                Logger.Warn("Error al buscar Cliente Receptor ENVIO" + ex);
            }
        }

        //Buscar mercancia a incluir en el pedido
        private void btnBuscarMercancia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
                ListMercanciasEncontradas.Clear();
                lbxResultBusquedaMercancia.Items.Clear();

                string contenido = tbxBuscarMercancia.Text;

                //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
                if (contenido != "")
                {
                    switch (cboBuscarMercancia.Text)
                    {
                        case "Contenido":
                            ListSobresEncontrados = sobreBD.GetSobres(contenido);
                            ListPaquetesEncontrados = paqueteBD.GetPaquetes(contenido);
                            ListMercanciasEncontradas = ListSobresEncontrados.Concat(ListPaquetesEncontrados).ToList();
                            break;
                        case "Codigo":
                            break;
                        default:
                            break;
                    }

                    foreach (var item in ListMercanciasEncontradas)
                    {
                        lbxResultBusquedaMercancia.Items.Add(item);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar Mercancia \n Verifique los datos Ingresados");
                Logger.Warn("Error al buscar Mercancia ENVIO" + ex);
            }
        }

        //Seleccionar cliente emisor
        private void AgregarEmisor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emisorSeleccionado = (Cliente)lbxResultBusquedaEmisor.SelectedItem;

                if (emisorSeleccionado != null)
                {
                    tbxEmisorSeleccionado.Text = emisorSeleccionado.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio no se pudo seleccionar" + ex);
            }
            
        }

        //Quitar cliente emisor seleccionado
        private void QuitarEmisor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emisorSeleccionado = null;
                tbxEmisorSeleccionado.Clear();
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Seleccionar cliente emisor
        private void AgregarReceptor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                receptorSeleccionado = (Cliente)lbxResultBusquedaReceptor.SelectedItem;

                if (receptorSeleccionado != null)
                {
                    tbxReceptorSeleccionado.Text = receptorSeleccionado.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Quitar cliente emisor seleccionado
        private void QuitarReceptor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                receptorSeleccionado = null;
                tbxReceptorSeleccionado.Clear();

            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Seleccionar mercancia
        private void AgregarMercancia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mercanciaSeleccionada = (Mercancia)lbxResultBusquedaMercancia.SelectedItem;

                if (mercanciaSeleccionada != null)
                {
                    tbxMercanciaSeleccionada.Text = mercanciaSeleccionada.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Quitar mercancia seleccionada
        private void QuitarMercancia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mercanciaSeleccionada = null;
                tbxMercanciaSeleccionada.Clear();
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Mostra costo de envio
        private void btnCalcularPrecio_Click(object sender, RoutedEventArgs e)
        {
            tbxPrecioEnvio.Text = "";
            try
            {
                if (mercanciaSeleccionada != null)
                {
                    //Todavia da error porque las mercancias no tienen vehiculos asignados
                    tbxPrecioEnvio.Text = Convert.ToString(mercanciaSeleccionada.CalcularPrecioFinal());
                    //tbxPrecioEnvio.Text = "1500";
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Envio " + ex);
            }
        }

        //Dar de alta un envio
        private void btnAltaEnvio_Click(object sender, RoutedEventArgs e)
        {
            Envio envio;
            try
            {
                DateTime fechaEnvio = Convert.ToDateTime(dtpFechaEnvio.Text);
                if ((emisorSeleccionado != null) && (receptorSeleccionado != null) && (mercanciaSeleccionada != null) && (fechaEnvio != null))
                {
                    if (mercanciaSeleccionada is Sobre)
                    {
                        envio = new Envio(fechaEnvio, emisorSeleccionado, receptorSeleccionado, mercanciaSeleccionada);
                        envioBD.AltaEnvio(emisorSeleccionado, receptorSeleccionado, mercanciaSeleccionada, fechaEnvio, "sobre");
                        VistasEnvio.EnvioExitoso ventana = new VistasEnvio.EnvioExitoso(envio);
                        ventana.Show();
                    }

                    if (mercanciaSeleccionada is Paquete)
                    {
                        envio = new Envio(fechaEnvio, emisorSeleccionado, receptorSeleccionado, mercanciaSeleccionada);
                        envioBD.AltaEnvio(emisorSeleccionado, receptorSeleccionado, mercanciaSeleccionada, fechaEnvio, "paquete");
                        VistasEnvio.EnvioExitoso ventana = new VistasEnvio.EnvioExitoso(envio);
                        ventana.Show();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al dar de Alta el Envio");
                Logger.Warn("Error al dar de Alta el Envio" + ex);
            }
        }
    }
}
