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
        AccesoADatos.ADAvion avionBD;
        public AltaAvion(AccesoADatos.ADAvion avionBD)
        {
            InitializeComponent();
            this.avionBD = avionBD;
        }

        private void BtnAltaAvion_Click(object sender, RoutedEventArgs e)
        {
            string modelo = tbxModelo.Text;
            double aumento = Convert.ToDouble(tbxAumento.Text);
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            Avion nuevoAvion = new Avion(modelo, fechaCompra, precioCompra, aumento);
            avionBD.AltaAvion(nuevoAvion);
        }
    }
}
