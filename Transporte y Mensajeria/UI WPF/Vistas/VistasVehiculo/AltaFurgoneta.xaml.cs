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
    /// Lógica de interacción para AltaFurgoneta.xaml
    /// </summary>
    public partial class AltaFurgoneta : Page
    {
        AccesoADatos.ADFurgoneta furgonetaBD;
        public AltaFurgoneta(AccesoADatos.ADFurgoneta furgonetaBD)
        {
            InitializeComponent();
            this.furgonetaBD = furgonetaBD;
        }

        private void BtnAltaFurgoneta_Click(object sender, RoutedEventArgs e)
        {
            string modelo = tbxModelo.Text;
            double aumento = Convert.ToDouble(tbxAumento.Text);
            double capacidadCarga = Convert.ToDouble(tbxCapCarga.Text);
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            Furgoneta nuevafurgoneta = new Furgoneta(modelo, fechaCompra, precioCompra, capacidadCarga, aumento);
            furgonetaBD.AltaFurgoneta(nuevafurgoneta);
        }
    }
}
