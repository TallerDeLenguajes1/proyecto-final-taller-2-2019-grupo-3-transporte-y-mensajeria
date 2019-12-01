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
        public AltaMoto(AccesoADatos.ADMoto motoBD)
        {
            InitializeComponent();
            this.motoBD = motoBD;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADMoto motoBD;

        private void btnAltaMoto_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string modelo = tbxModelo.Text;
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            int cilindrada = Convert.ToInt32(tbxCilindrada.Text);

            Moto nuevaMoto = new Moto( modelo, fechaCompra, precioCompra, cilindrada, 2);
            motoBD.AltaMoto(nuevaMoto);
        }


    }
}
