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
    /// Lógica de interacción para AltaSobre.xaml
    /// </summary>
    public partial class AltaSobre : Page
    {
        List<Vehiculo> listaVehiculos = new List<Vehiculo>();
        public AltaSobre(AccesoADatos.ADSobre sobreBD)
        {
            InitializeComponent();
            this.sobreBD = sobreBD;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADSobre sobreBD;

        private void btnAltaSobre_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string contenido = tbxContenido.Text;
            Double peso = Convert.ToDouble(tbxPeso.Text);
            bool asegurado = Convert.ToBoolean(cbAsegurado.IsChecked);
            bool largoRecorrido = Convert.ToBoolean(cbLargoRecorrido.IsChecked);
            double precioGramo = sobreBD.GetPrecioGramo();

            Sobre nuevoSobre = new Sobre(contenido, asegurado, largoRecorrido, 10, precioGramo, peso);
            
            cargarVehiculos(nuevoSobre);
            
            nuevoSobre.CalcularPrecioFinal();
            sobreBD.AltaSobre(nuevoSobre);
        }
        /// <summary>
        /// El metodo permite cargar los vehiculos que se le asocian a un Sobre
        /// esta carga es externa al sistema, y lo realizamos de esta forma para disminuir la complejidad del sistema.
        /// </summary>
        /// <param name="nuevoSobre"></param>
        private void cargarVehiculos(Sobre nuevoSobre)
        {
            listaVehiculos.Add(new Moto("yamaha", DateTime.Now, 700000, 500, 2));
            listaVehiculos.Add(new Moto("honda", DateTime.Now, 500000, 400, 2));
            listaVehiculos.Add(new Avion("Aerobus920", DateTime.Now, 2000000, 10));

            nuevoSobre.AsociarVehiculo(listaVehiculos);
        }
    }
}
