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
    /// Lógica de interacción para AltaPaquete.xaml
    /// </summary>
    public partial class AltaPaquete : Page
    {
        public AltaPaquete(AccesoADatos.ADPaquete paqueteBD)
        {
            InitializeComponent();
            this.paqueteBD = paqueteBD;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADPaquete paqueteBD;

        private void btnAltaPaquete_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string contenido = tbxContenido.Text;
            Double volumen = Convert.ToDouble(tbxVolumen.Text);
            bool asegurado = Convert.ToBoolean(cbAsegurado.IsChecked);
            bool largoRecorrido = Convert.ToBoolean(cbLargoRecorrido.IsChecked);
            double precioM3 = paqueteBD.GetPrecioM3();

            Paquete nuevoPaquete = new Paquete(contenido, asegurado, largoRecorrido, 10, precioM3, volumen);
            paqueteBD.AltaPaquete(nuevoPaquete);
        }

    }
}
