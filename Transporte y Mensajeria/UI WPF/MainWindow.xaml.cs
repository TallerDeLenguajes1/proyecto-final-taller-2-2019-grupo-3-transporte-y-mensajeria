﻿using System;
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
using AccesoADatos;
using EntidadesDelProyecto;
using UI_WPF.Vistas;

namespace UI_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Instanciaciones necesaria para conexion a base de datos
        AccesoADatos.ADCliente clienteBD = new AccesoADatos.ADCliente();
        AccesoADatos.ADSupervisor supervisorBD = new AccesoADatos.ADSupervisor();

        //********************* Botones de control de la ventana *********************//

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            btnMax.Visibility = Visibility.Collapsed;
            btnVent.Visibility = Visibility.Visible;
            WindowState = WindowState.Maximized;
        }

        private void btnVent_Click(object sender, RoutedEventArgs e)
        {
            btnMax.Visibility = Visibility.Visible;
            btnVent.Visibility = Visibility.Collapsed;
            WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        //****************** Control del menu de contenido principal *****************//

        //Cargar vista Clientes
        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ABMCliente(clienteBD);
        }

        //Cargar vista Supervisores
        private void btnSupervisores_Click(object sender, RoutedEventArgs e) 
        {
            frmMain.Content = new ABMSupervisor(supervisorBD);
        }

        //Cargar vista Vehiculos
        private void btnVehiculos_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ABMVehiculos();
        }

        //Cargar vista Mercancia
        private void btnMercancia_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ABMMercancia();
        }

        //Cargar vista Envios
        private void btnEnvios_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Content = new ABMEnvio();
        }


        //Cargar una pagina en un frame
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //NavigationService.Navigate(new Uri("Vistas/AltaCliente.xaml",UriKind.RelativeOrAbsolute));
        //    frmAltaCliente.Content = new AltaCliente(clienteBD);
        //}
    }
}
