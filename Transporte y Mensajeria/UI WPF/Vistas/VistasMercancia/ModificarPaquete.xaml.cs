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
    /// Lógica de interacción para ModificarPaquete.xaml
    /// </summary>
    public partial class ModificarPaquete : Page
    {
        public ModificarPaquete(Paquete paquete, AccesoADatos.ADPaquete paqueteBD, ABMMercancia mainPage)
        {
            InitializeComponent();
            this.paquete = paquete;
            this.paqueteBD = paqueteBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Paquete paquete;
        AccesoADatos.ADPaquete paqueteBD;
        ABMMercancia mainPage;

        private void RellenarCampos()
        {
            tbxContenido.Text = paquete.Contenido;
            tbxVolumen.Text = Convert.ToString(paquete.Volumen);
            cbAsegurado.IsChecked = paquete.Asegurada ? true : false;
            cbLargoRecorrido.IsChecked = paquete.LargoRecorrido ? true : false;
        }

        private void btnModificarPaquete_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string contenido = tbxContenido.Text;
            double volumen = Convert.ToDouble(tbxVolumen.Text);
            bool asegurado = Convert.ToBoolean(cbAsegurado.IsChecked);
            bool largoRecorrido = Convert.ToBoolean(cbLargoRecorrido.IsChecked);
            double precioM3 = paqueteBD.GetPrecioM3();

            Paquete paqueteEncontrado = new Paquete(paquete.IdMercancia, contenido, asegurado, largoRecorrido, 10, precioM3, volumen);

            paqueteBD.ModificacionPaquete(paqueteEncontrado);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
