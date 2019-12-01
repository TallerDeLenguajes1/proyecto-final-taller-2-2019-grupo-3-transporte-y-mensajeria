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

namespace UI_WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para ABMSupervisor.xaml
    /// </summary>
    public partial class ABMSupervisor : Page
    {
        public ABMSupervisor(AccesoADatos.ADSupervisor supervisorBD)
        {
            InitializeComponent();
            this.supervisorBD = supervisorBD;

            lbxResultBusqueda.ItemsSource = ListSupervisoresEncontrados;
        }

        //Variables y colecciones de datos auxiliares
        AccesoADatos.ADSupervisor supervisorBD;
        List<Supervisor> ListSupervisoresEncontrados = new List<Supervisor>();

        //Actualizar resultados de busqueda
        public void ActualizarResultadosBusqueda()
        {
            ListSupervisoresEncontrados.Clear();
            lbxResultBusqueda.Items.Refresh();
        }

        //Alta supervisor
        private void btnAltaSupervisor_Click(object sender, RoutedEventArgs e)
        {
            //Declaracion de variables para tomar el contenido del formulario
            int cuil = Convert.ToInt32(tbxCuil.Text);
            string nombre = tbxNombre.Text;
            string apellido = tbxApellido.Text;
            string direccion = tbxDireccion.Text;
            string telefono = tbxTelefono.Text;
            Supervisor nuevoSupervisor = new Supervisor(cuil, nombre, apellido, direccion, telefono);
            supervisorBD.AltaSupervisor(nuevoSupervisor);
        }

        //Buscar un supervisor segun el metodo elegido
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //Primero limpio la lista y el listbox para que los resultados de busquedas anteriores no interfieran con los nuevos
            ListSupervisoresEncontrados.Clear();
            lbxResultBusqueda.Items.Refresh();
            string contenido = tbxBuscar.Text;

            //Si el contenido del cuadro de busqueda esta vacio no se ejecuta la consulta a la db
            if (contenido != "")
            {
                Supervisor supervisorBuscado = supervisorBD.GetSupervisores(Convert.ToInt32(contenido));
                if (supervisorBuscado != null)
                {
                    ListSupervisoresEncontrados.Add(supervisorBuscado);
                    lbxResultBusqueda.Items.Refresh();
                }
            }
        }

        //Ver datos de un supervisor
        private void btnVer_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Supervisor supervisorEncontrado = (Supervisor)lbxResultBusqueda.SelectedItem;

            if (supervisorEncontrado != null)
            {
               frmAcciones.Content = new VistasSupervisor.DatosSupervisor(supervisorEncontrado);
            }
        }

        //Modificar datos de un supervisor
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Supervisor supervisorEncontrado = (Supervisor)lbxResultBusqueda.SelectedItem;

            if (supervisorEncontrado != null)
            {
                frmAcciones.Content = new VistasSupervisor.ModificarSupervisor(supervisorEncontrado, supervisorBD, this);
            }
        }

        //Eliminar supervisor
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            //Tomo el cliente seleccionado del listbox de resultados de busqueda
            Supervisor supervisorEncontrado = (Supervisor)lbxResultBusqueda.SelectedItem;

            if (supervisorEncontrado != null)
            {
                frmAcciones.Content = new VistasSupervisor.EliminarSupervisor(supervisorEncontrado, supervisorBD, this);
            }
        }
    }
}
