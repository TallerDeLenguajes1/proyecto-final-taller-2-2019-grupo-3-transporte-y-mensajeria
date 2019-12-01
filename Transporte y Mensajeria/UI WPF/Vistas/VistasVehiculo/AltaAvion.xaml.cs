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
    /// Lógica de interacción para AltaAvion.xaml
    /// </summary>
    public partial class AltaAvion : Page
    {
        public AltaAvion(AccesoADatos.ADAvion avionBD)
        {
            InitializeComponent();
            this.avionBD = avionBD;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADAvion avionBD;

        private void btnAltaAvion_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string modelo = tbxModelo.Text;
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);

            Avion nuevoAvion = new Avion(modelo, fechaCompra, precioCompra, 10);
            avionBD.AltaAvion(nuevoAvion);
        }
    }
}
