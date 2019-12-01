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
    /// Lógica de interacción para ModificarMoto.xaml
    /// </summary>
    public partial class ModificarMoto : Page
    {
        public ModificarMoto(Moto moto, AccesoADatos.ADMoto motoBD, ABMVehiculos mainPage)
        {
            InitializeComponent();
            this.moto = moto;
            this.motoBD = motoBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Moto moto;
        AccesoADatos.ADMoto motoBD;
        ABMVehiculos mainPage;

        private void RellenarCampos()
        {
            tbxModelo.Text = moto.Modelo;
            dpFechaCompra.Text = Convert.ToString(moto.FechaCompra);
            tbxPrecioCompra.Text = Convert.ToString(moto.PrecioCompra);
            tbxCilindrada.Text = Convert.ToString(moto.Cilindrada);
        }

        private void btnModificarMoto_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string modelo = tbxModelo.Text;
            DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
            Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
            int cilindrada = Convert.ToInt32(tbxCilindrada.Text);

            Moto motoEncontrada = new Moto(moto.IdVehiculo, modelo, fechaCompra, precioCompra, cilindrada, 2);

            motoBD.ModificacionMoto(motoEncontrada);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
