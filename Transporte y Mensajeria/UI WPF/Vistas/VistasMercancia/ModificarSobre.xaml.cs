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
    /// Lógica de interacción para ModificarSobre.xaml
    /// </summary>
    public partial class ModificarSobre : Page
    {
        public ModificarSobre(Sobre sobre, AccesoADatos.ADSobre sobreBD, ABMMercancia mainPage)
        {
            InitializeComponent();
            this.sobre = sobre;
            this.sobreBD = sobreBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del vehiculo recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Sobre sobre;
        AccesoADatos.ADSobre sobreBD;
        ABMMercancia mainPage;

        private void RellenarCampos()
        {
            tbxContenido.Text = sobre.Contenido;
            tbxPeso.Text = Convert.ToString(sobre.Peso);
            cbAsegurado.IsChecked = sobre.Asegurada ? true : false;
            cbLargoRecorrido.IsChecked = sobre.LargoRecorrido ? true : false;
        }

        private void btnModificarSobre_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            string contenido = tbxContenido.Text;
            double peso = Convert.ToDouble(tbxPeso.Text);
            bool asegurado = Convert.ToBoolean(cbAsegurado.IsChecked);
            bool largoRecorrido = Convert.ToBoolean(cbLargoRecorrido.IsChecked);

            Sobre sobreEncontrado = new Sobre(sobre.IdMercancia, contenido, asegurado, largoRecorrido, 10, 1, peso);

            sobreBD.ModificacionSobre(sobreEncontrado);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
