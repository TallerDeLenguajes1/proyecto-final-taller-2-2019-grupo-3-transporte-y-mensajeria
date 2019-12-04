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
    /// Lógica de interacción para VehiculosAsociados.xaml
    /// </summary>
    public partial class VehiculosAsociados : Page
    {
        public VehiculosAsociados(Mercancia mercancia, ABMMercancia mainPage)
        {
            InitializeComponent();
            this.mercancia = mercancia;
            this.mainPage = mainPage;


            CargarTodosVehiculos();
        }

        //Variables y colecciones de datos auxiliares
        Mercancia mercancia;
        ABMMercancia mainPage;
        AccesoADatos.ADSobre sobreBD = new AccesoADatos.ADSobre();
        AccesoADatos.ADPaquete paqueteBD = new AccesoADatos.ADPaquete();
        Moto motoAsociada;
        Furgoneta furgonetaAsociada;
        Avion avionAsociado;
        List<Vehiculo> ListVehiculosAsociados = new List<Vehiculo>();


        private void CargarTodosVehiculos()
        {
            ListVehiculosAsociados.Clear();
            lbxResultBusqueda.Items.Clear();
            if (mercancia is EntidadesDelProyecto.Sobre)
            {
                motoAsociada = sobreBD.MotoAsociada(mercancia.IdMercancia);
                avionAsociado = sobreBD.AvionAsociado(mercancia.IdMercancia);
                ListVehiculosAsociados.Add(motoAsociada);
                ListVehiculosAsociados.Add(avionAsociado);
            }

            if (mercancia is EntidadesDelProyecto.Paquete)
            {
                furgonetaAsociada = paqueteBD.FurgonetaAsociada(mercancia.IdMercancia);
                avionAsociado = paqueteBD.AvionAsociado(mercancia.IdMercancia);
                ListVehiculosAsociados.Add(furgonetaAsociada);
                ListVehiculosAsociados.Add(avionAsociado);
            }

            foreach (var item in ListVehiculosAsociados)
            {
                lbxResultBusqueda.Items.Add(item);
            }
        }

        private void btnQuitar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo el vehiculo seleccionado del listbox de resultados de busqueda
            Vehiculo vehiculoEncontrado = (Vehiculo)lbxResultBusqueda.SelectedItem;

            if (mercancia is EntidadesDelProyecto.Sobre)
            {
                if (vehiculoEncontrado is EntidadesDelProyecto.Moto)
                {
                    sobreBD.QuitarMoto((Sobre)mercancia);
                    mercancia.QuitarVehiculo(vehiculoEncontrado);
                    CargarTodosVehiculos();
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                {
                    sobreBD.QuitarAvion((Sobre)mercancia);
                    mercancia.QuitarVehiculo(vehiculoEncontrado);
                    CargarTodosVehiculos();
                }
            }

            if (mercancia is EntidadesDelProyecto.Paquete)
            {
                if (vehiculoEncontrado is EntidadesDelProyecto.Furgoneta)
                {
                    paqueteBD.QuitarFurgoneta((Paquete)mercancia);
                    mercancia.QuitarVehiculo(vehiculoEncontrado);
                    CargarTodosVehiculos();
                }

                if (vehiculoEncontrado is EntidadesDelProyecto.Avion)
                {
                    paqueteBD.QuitarAvion((Paquete)mercancia);
                    mercancia.QuitarVehiculo(vehiculoEncontrado);
                    CargarTodosVehiculos();
                }
            }
        }
    }
}
