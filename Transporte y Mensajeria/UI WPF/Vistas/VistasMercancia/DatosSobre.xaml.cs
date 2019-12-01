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
    /// Lógica de interacción para DatosSobre.xaml
    /// </summary>
    public partial class DatosSobre : Page
    {
        public DatosSobre(Sobre sobre)
        {
            InitializeComponent();
            this.sobre = sobre;

            //Relleno los campos con los atributos de la mercancia recibida
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Sobre sobre;

        private void RellenarCampos()
        {
            tbxContenido.Text = sobre.Contenido;
            tbxPeso.Text = Convert.ToString(sobre.Peso);
            cbAsegurado.IsChecked = sobre.Asegurada ? true : false;
            cbLargoRecorrido.IsChecked = sobre.LargoRecorrido ? true : false;

        }
    }
}
