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

namespace UI_WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para ABMMercancia.xaml
    /// </summary>
    public partial class ABMMercancia : Page
    {
        public ABMMercancia()
        {
            InitializeComponent();
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADSobre sobreBD = new AccesoADatos.ADSobre();
        AccesoADatos.ADPaquete paqueteBD = new AccesoADatos.ADPaquete();

        List<Mercancia> ListSobresEncontrados = new List<Mercancia>();
        List<Mercancia> ListPaquetesEncontrados = new List<Mercancia>();
        List<Mercancia> ListMercanciasEncontradas = new List<Mercancia>();

        private void rdoSobre_Checked(object sender, RoutedEventArgs e)
        {
            VistasMercancia.AltaSobre pagina = new VistasMercancia.AltaSobre(sobreBD);
            frmNuevaMercancia.Content = pagina;
        }

        private void rdoPaquete_Checked(object sender, RoutedEventArgs e)
        {
            VistasMercancia.AltaPaquete pagina = new VistasMercancia.AltaPaquete(paqueteBD);
            frmNuevaMercancia.Content = pagina;
        }

        public void ActualizarResultadosBusqueda()
        {
            ListMercanciasEncontradas.Clear();
            lbxResultBusqueda.Items.Clear();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
            ListMercanciasEncontradas.Clear();
            lbxResultBusqueda.Items.Clear();

            string contenido = tbxBuscar.Text;

            //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
            if (contenido != "")
            {
                switch (cboBuscar.Text)
                {
                    case "Contenido":
                        ListSobresEncontrados = sobreBD.GetSobres(contenido);
                        ListPaquetesEncontrados = paqueteBD.GetPaquetes(contenido);
                        ListMercanciasEncontradas = ListSobresEncontrados.Concat(ListPaquetesEncontrados).ToList();
                        break;
                    case "Codigo":
                        break;
                    default:
                        break;
                }

                foreach (var item in ListMercanciasEncontradas)
                {
                    lbxResultBusqueda.Items.Add(item);
                }

            }
        }

        //Ver datos de un mercancia
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            // Tomo la mercancia seleccionada del listbox de resultados de busqueda
            Mercancia mercanciaEncontrada = (Mercancia)lbxResultBusqueda.SelectedItem;

            if (mercanciaEncontrada != null)
            {
                //Segun el tipo de mercancia cargo su pagina correspondiente (Hacer con switch)
                if (mercanciaEncontrada is Sobre)
                {
                    frmAcciones.Content = new VistasMercancia.DatosSobre((Sobre)mercanciaEncontrada);
                }

                if (mercanciaEncontrada is Paquete)
                {
                    frmAcciones.Content = new VistasMercancia.DatosPaquete((Paquete)mercanciaEncontrada);
                }
            }
        }

        //Cargar formulario de modificacion de una mercancia
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo la mercancia seleccionada del listbox de resultados de busqueda
            Mercancia mercanciaEncontrada = (Mercancia)lbxResultBusqueda.SelectedItem;

            if (mercanciaEncontrada != null)
            {
                //Segun el tipo de mercancia cargo su pagina correspondiente (Hacer con switch)
                if (mercanciaEncontrada is Sobre)
                {
                    frmAcciones.Content = new VistasMercancia.ModificarSobre((Sobre)mercanciaEncontrada, sobreBD, this);
                }

                if (mercanciaEncontrada is Paquete)
                {
                    frmAcciones.Content = new VistasMercancia.ModificarPaquete((Paquete)mercanciaEncontrada, paqueteBD, this);
                }
            }

        }

        //Cargar formulario de eliminacion de mercancia 
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Tomo la mercancia seleccionada del listbox de resultados de busqueda
            Mercancia mercanciaEncontrada = (Mercancia)lbxResultBusqueda.SelectedItem;

            if (mercanciaEncontrada != null)
            {
                frmAcciones.Content = new VistasMercancia.EliminarMercancia((Mercancia)mercanciaEncontrada, sobreBD, paqueteBD, this);
            }
        }
    }
}
