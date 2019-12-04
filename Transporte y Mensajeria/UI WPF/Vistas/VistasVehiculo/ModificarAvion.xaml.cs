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

namespace UI_WPF.Vistas.VistasVehiculo
{
    /// <summary>
    /// Lógica de interacción para ModificarAvion.xaml
    /// </summary>
    public partial class ModificarAvion : Page
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public ModificarAvion(Avion avion, AccesoADatos.ADAvion avionBD, ABMVehiculos mainPage)
        {
            InitializeComponent();
            this.avion = avion;
            this.avionBD = avionBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Avion avion;
        AccesoADatos.ADAvion avionBD;
        ABMVehiculos mainPage;

        private void RellenarCampos()
        {
            tbxModelo.Text = avion.Modelo;
            dpFechaCompra.Text = Convert.ToString(avion.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(avion.PrecioCompra);
        }

        private void btnModificarAvion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Declaracion de variables para tomar el contenido del formulario
                string modelo = tbxModelo.Text;
                DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
                Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);

                Avion avionEncontrado = new Avion(avion.IdVehiculo, modelo, fechaCompra, precioCompra, 10);

                avionBD.ModificacionAvion(avionEncontrado);

                mainPage.ActualizarResultadosBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo dar Modificar al Vehiculo, quizas ingreso algun dato incorrectamente");
                Logger.Warn("No se pudo dar de Vehiculo" + ex);
            }
        }
    }
}
