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

namespace UI_WPF.Vistas.VistasSupervisor
{
    /// <summary>
    /// Lógica de interacción para ModificarSupervisor.xaml
    /// </summary>
    public partial class ModificarSupervisor : Page
    {
        public ModificarSupervisor(Supervisor supervisor, AccesoADatos.ADSupervisor supervisorBD, ABMSupervisor mainPage)
        {
            InitializeComponent();
            this.supervisor = supervisor;
            this.supervisorBD = supervisorBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del cliente recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Supervisor supervisor;
        AccesoADatos.ADSupervisor supervisorBD;
        ABMSupervisor mainPage;

        private void RellenarCampos()
        {
            tbxCuil.Text = Convert.ToString(supervisor.Cuil);
            tbxNombre.Text = supervisor.Nombre;
            tbxApellido.Text = supervisor.Apellido;
            tbxDireccion.Text = supervisor.Direccion;
            tbxTelefono.Text = supervisor.Telefono;
        }

        private void btnModificarSupervisor_Click(object sender, RoutedEventArgs e)
        {
            int cuil = Convert.ToInt32(tbxCuil.Text);
            string nombre = tbxNombre.Text;
            string apellido = tbxApellido.Text;
            string direccion = tbxDireccion.Text;
            string telefono = tbxTelefono.Text;

            Supervisor supervisorEncontrado = new Supervisor(cuil, nombre, apellido, direccion, telefono);

            supervisorBD.ModificacionSupervisor(supervisorEncontrado);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
