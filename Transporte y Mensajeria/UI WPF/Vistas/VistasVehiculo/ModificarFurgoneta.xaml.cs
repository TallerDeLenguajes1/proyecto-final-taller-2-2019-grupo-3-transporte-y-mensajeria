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
    /// Lógica de interacción para ModificarFurgoneta.xaml
    /// </summary>
    public partial class ModificarFurgoneta : Page
    {
        public ModificarFurgoneta(Furgoneta furgoneta, AccesoADatos.ADFurgoneta furgonetaBD, ABMVehiculos mainPage)
        {
            InitializeComponent();
            this.furgoneta = furgoneta;
            this.furgonetaBD = furgonetaBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Furgoneta furgoneta;
        AccesoADatos.ADFurgoneta furgonetaBD;
        ABMVehiculos mainPage;

        private void RellenarCampos()
        {
            tbxModelo.Text = furgoneta.Modelo;
            dpFechaCompra.Text = Convert.ToString(furgoneta.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(furgoneta.PrecioCompra);
            tbxCapacidad.Text = Convert.ToString(furgoneta.CapacidadCarga);
        }

        private void btnModificarFurgoneta_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string modelo = tbxModelo.Text;
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            int capacidad = Convert.ToInt32(tbxCapacidad.Text);

            Furgoneta furgonetaEncontrada = new Furgoneta(furgoneta.IdVehiculo, modelo, fechaCompra, precioCompra, capacidad, 5);

            furgonetaBD.ModificacionFurgoneta(furgonetaEncontrada);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
