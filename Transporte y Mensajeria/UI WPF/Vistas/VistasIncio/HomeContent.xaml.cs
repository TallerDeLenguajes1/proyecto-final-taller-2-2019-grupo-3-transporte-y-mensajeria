using LiveCharts;
using LiveCharts.Wpf;
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

namespace UI_WPF.Vistas.VistasIncio
{
    /// <summary>
    /// Lógica de interacción para HomeContent.xaml
    /// </summary>
    public partial class HomeContent : Page
    {
        public HomeContent()
        {
            InitializeComponent();

            //Total de clientes
            AccesoADatos.ADCliente clienteBD = new AccesoADatos.ADCliente();
            lblTotalClientes.Content = clienteBD.TotalClientes();

            //Total de envios mes actual
            lblTotalEnvios.Content = envioDB.TotalEnvios();

            //Rellenar un arreglo de Double con la recaudacion por mes en un determinado año

            GraficaAnioActual();
            Title = "Recaudacion\nMensual";

            Labels = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            YFormatter = value => value.ToString("C");


            DataContext = this;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADEnvio envioDB = new AccesoADatos.ADEnvio();
        int actualYear = DateTime.Now.Year;
        Double[] valores;

        //Inicializacion Grafico
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        //Cargar grafica con contenido perteneciente al año actual
        private void GraficaAnioActual()
        {
            lblRecaudacion.Content = "Recaudacion Anual (" + actualYear + ")";
            valores = envioDB.RecaudacionAnual(actualYear);
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Recaudación \n Mensual",
                    Values = new ChartValues<Double> {
                        valores[0],
                        valores[1],
                        valores[2],
                        valores[3],
                        valores[4],
                        valores[5],
                        valores[6],
                        valores[7],
                        valores[8],
                        valores[9],
                        valores[10],
                        valores[11]
                    }
                },
            };

        }

        private void btnVerRecaudacion_Click(object sender, RoutedEventArgs e)
        {
            string anio = tbxAnioRecaudacion.Text;
            if (anio != "" && !anio.Any(x => !char.IsNumber(x)))
            {
                lblRecaudacion.Content = "Recaudacion Anual (" + anio + ")";

                Grafica.Series.Clear();


                valores = envioDB.RecaudacionAnual(Convert.ToInt32(anio));

                Grafica.Series.Add(new LineSeries
                {
                    Values = new ChartValues<Double> {
                        valores[0],
                        valores[1],
                        valores[2],
                        valores[3],
                        valores[4],
                        valores[5],
                        valores[6],
                        valores[7],
                        valores[8],
                        valores[9],
                        valores[10],
                        valores[11]
                        },
                    Title = "Recaudacion\nMensual",
                });
            }
        }
    }
}
