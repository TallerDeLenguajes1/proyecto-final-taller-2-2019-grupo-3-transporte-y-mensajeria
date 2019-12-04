using EntidadesDelProyecto;
using NLog;
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
using System.Windows.Shapes;

namespace UI_WPF.Vistas.VistasEnvio
{
    /// <summary>
    /// Lógica de interacción para EnvioExitoso.xaml
    /// </summary>
    public partial class EnvioExitoso : Window
    {
        public EnvioExitoso(Envio envio)
        {
            InitializeComponent();
            this.envio = envio;
            
        }

        Envio envio;

        HelperDeArchivos.Reporte reporte = new HelperDeArchivos.Reporte();

        private void btnImprimirFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reporte.generarBoleta();
                lblReporte.Content = "Boleta generada";
            }
            catch (Exception)
            {
                //Logger.Error("Error generando el reporte" + ex);
                lblReporte.Content = "Error generando boleta";
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
