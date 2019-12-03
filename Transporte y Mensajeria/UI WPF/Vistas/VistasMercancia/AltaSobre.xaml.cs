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

            Sobre nuevoSobre = new Sobre( contenido, asegurado, largoRecorrido, 10, 1, peso);
            sobreBD.AltaSobre(nuevoSobre);
        }

    }
}
