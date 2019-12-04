﻿using EntidadesDelProyecto;
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
    /// Lógica de interacción para AltaFurgoneta.xaml
    /// </summary>
    public partial class AltaFurgoneta : Page
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public AltaFurgoneta(AccesoADatos.ADFurgoneta furgonetaBD)
        {
            InitializeComponent();
            this.furgonetaBD = furgonetaBD;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADFurgoneta furgonetaBD;

        private void btnAltaFurgoneta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Declaracion de variables para tomar el contenido del formulario
                string modelo = tbxModelo.Text;
                DateTime fechaCompra = Convert.ToDateTime(dpFechaCompra.Text);
                Double precioCompra = Convert.ToDouble(tbxPrecioCompra.Text);
                int capacidad = Convert.ToInt32(tbxCapCarga.Text);

                Furgoneta nuevaFurgoneta = new Furgoneta(modelo, fechaCompra, precioCompra, capacidad, 5);
                furgonetaBD.AltaFurgoneta(nuevaFurgoneta);
                MessageBox.Show("Se agrego correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo dar de alta al Vehiculo, quizas ingreso algun dato incorrectamente");
                Logger.Warn("No se pudo dar de Vehiculo" + ex);
            }
        }
    }
}
