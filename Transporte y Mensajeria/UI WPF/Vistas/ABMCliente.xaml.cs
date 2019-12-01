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
    /// Lógica de interacción para ABMCliente.xaml
    /// </summary>
    public partial class ABMCliente : Page
    {
        public ABMCliente(AccesoADatos.ADCliente clienteBD)
        {
            InitializeComponent();
            this.clienteBD = clienteBD;

            lbxResultBusqueda.ItemsSource = ListClientesEncontrados;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADCliente clienteBD;
        List<Cliente> ListClientesEncontrados = new List<Cliente>();

        //Alta cliente
        private void btnAltaCliente_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            int cuil = Convert.ToInt32(tbxCuil.Text);
            string nombre = tbxNombre.Text;
            string apellido = tbxApellido.Text;
            string direccion = tbxDireccion.Text;
            string telefono = tbxTelefono.Text;
            Cliente nuevoCliente = new Cliente(cuil, nombre, apellido, direccion, telefono);
            clienteBD.AltaCliente(nuevoCliente);

        }

        //Buscar un cliente segun el metodo elegido
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
            ListClientesEncontrados.Clear();
            lbxResultBusqueda.Items.Refresh();
            string contenido = tbxBuscar.Text;

            //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
            if (contenido != "")
            {
                Cliente clienteBuscado= clienteBD.GetClientes(Convert.ToInt32(contenido));
                if (clienteBuscado != null)
                {
                    ListClientesEncontrados.Add(clienteBuscado);
                    lbxResultBusqueda.Items.Refresh();
                }
            }
        }

        //Ver datos de un cliente
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

            if (clienteEncontrado!=null)
            {
                frmAcciones.Content = new VistasCliente.DatosCliente((Cliente)lbxResultBusqueda.SelectedItem);
            }
            
        }

        //Modificar datos de un cliente
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

            if (clienteEncontrado != null)
            {
                frmAcciones.Content = new VistasCliente.ModificarCliente((Cliente)lbxResultBusqueda.SelectedItem,clienteBD, this);
            }
        }

        //Eliminar Cliente
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Cliente clienteEncontrado = (Cliente)lbxResultBusqueda.SelectedItem;

            if (clienteEncontrado != null)
            {
                VistasCliente.EliminarCliente pagina = new VistasCliente.EliminarCliente((Cliente)lbxResultBusqueda.SelectedItem, clienteBD, this);
                frmAcciones.Content = pagina;
            }
        }

        public void ActualizarResultadosBusqueda()
        {
            ListClientesEncontrados.Clear();
            lbxResultBusqueda.Items.Refresh();
        }

    }
}
