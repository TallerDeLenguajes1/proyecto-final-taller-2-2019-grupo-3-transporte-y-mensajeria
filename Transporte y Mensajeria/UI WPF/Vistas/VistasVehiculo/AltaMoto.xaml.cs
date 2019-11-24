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
    /// Lógica de interacción para AltaMoto.xaml
    /// </summary>
    public partial class AltaMoto : Page
    {
        AccesoADatos.ADMoto motoBD;
        public AltaMoto(AccesoADatos.ADMoto motoBD)
        {
            InitializeComponent();
            this.motoBD = motoBD;
        }
        /// <summary>
        /// alta de moto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAltaMoto_Click(object sender, RoutedEventArgs e)
        {
            string modelo = tbxModelo.Text;
            double aumento = Convert.ToDouble(tbxAumento.Text);
            int cilindrada = Convert.ToInt16( tbxCilindrada.Text);
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            Moto nuevaMoto = new Moto(modelo, fechaCompra,precioCompra,cilindrada,aumento);
            motoBD.AltaMoto(nuevaMoto);

        }
    }
}
