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
    /// Lógica de interacción para DatosPaquete.xaml
    /// </summary>
    public partial class DatosPaquete : Page
    {
        public DatosPaquete(Paquete paquete)
        {
            InitializeComponent();
            this.paquete = paquete;

            //Relleno los campos con los atributos de la mercancia recibida
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Paquete paquete;

        private void RellenarCampos()
        {
            tbxContenido.Text = paquete.Contenido;
            tbxVolumen.Text = Convert.ToString(paquete.Volumen);
            cbAsegurado.IsChecked = paquete.Asegurada ? true : false;
            cbLargoRecorrido.IsChecked = paquete.LargoRecorrido ? true : false;

        }
    }
}
