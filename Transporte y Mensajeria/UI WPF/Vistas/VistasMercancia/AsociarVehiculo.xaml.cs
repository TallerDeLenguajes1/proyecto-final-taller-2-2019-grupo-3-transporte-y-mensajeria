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

namespace UI_WPF.Vistas.VistasMercancia
{
    /// <summary>
    /// Lógica de interacción para AsociarVehiculo.xaml
    /// </summary>
    public partial class AsociarVehiculo : Page
    {
        public AsociarVehiculo(Mercancia mercancia, AccesoADatos.ADSobre sobreBD, AccesoADatos.ADPaquete paqueteBD, ABMMercancia mainPage)
        {
            InitializeComponent();
            this.mercancia = mercancia;
            this.sobreBD = sobreBD;
            this.paqueteBD = paqueteBD;
            this.mainPage = mainPage;
        }

        //Variables y colecciones de datos auxiliares
        Mercancia mercancia;
        ABMMercancia mainPage;
        AccesoADatos.ADSobre sobreBD;
        AccesoADatos.ADPaquete paqueteBD;
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
            tblError.Text = "";

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

            tblError.Text = "";
            if (vehiculoEncontrado != null)
            {
                if (mercancia is EntidadesDelProyecto.Sobre)
                {
                    if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                    {
                        tblError.Text = "";
                        sobreBD.AsociarMoto(vehiculoEncontrado.IdVehiculo, mercancia.IdMercancia);
                        mercancia.AsociarVehiculo(vehiculoEncontrado);
                    }

                    if (vehiculoEncontrado is EntidadesDelProyecto.Avion )
                    {
                        if (mercancia.LargoRecorrido)
                        {
                            tblError.Text = "";
                            sobreBD.AsociarAvion(vehiculoEncontrado.IdVehiculo, mercancia.IdMercancia);
                            mercancia.AsociarVehiculo(vehiculoEncontrado);
                        }
                        else
                        {
                            //tblError.Text = "No se puede asociar una avion a un \nsobre que no sea de largo recorrido";
                        }

                    }
                    

                    if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                    {
                        tblError.Text = "No se puede asociar una \nfurgoneta a un sobre";
                    }
                }

                if (mercancia is EntidadesDelProyecto.Paquete)
                {
                    if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                    {
                        tblError.Text = "";
                        paqueteBD.AsociarFurgoneta(vehiculoEncontrado.IdVehiculo, mercancia.IdMercancia);
                        mercancia.AsociarVehiculo(vehiculoEncontrado);
                    }

                    if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                    {
                        if (mercancia.LargoRecorrido)
                        {
                            tblError.Text = "";
                            paqueteBD.AsociarAvion(vehiculoEncontrado.IdVehiculo, mercancia.IdMercancia);
                            mercancia.AsociarVehiculo(vehiculoEncontrado);
                        }
                        else
                        {
                            //tblError.Text = "No se puede asociar una avion a un \npaquete que no sea de largo recorrido";
                        }
                    }
                    else
                    {
                        //tblError.Text = "No se puede asociar una avion a un \npaquete que no sea de largo recorrido";
                    }

                    if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                    {
                        tblError.Text = "No se puede asociar una \nmoto a un paquete";
                    }
                }
            }
        }
    }
}
