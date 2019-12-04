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
    /// Lógica de interacción para DatosCliente.xaml
    /// </summary>
    public partial class DatosSupervisor : Page
    {
        public DatosSupervisor(Supervisor supervisor)
        {
            InitializeComponent();
            this.supervisor = supervisor;

            //Relleno los campos con los atributos del cliente recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Supervisor supervisor;

        private void RellenarCampos()
        {
            tbxCuil.Text = Convert.ToString(supervisor.Cuil);
            tbxNombre.Text = supervisor.Nombre;
            tbxApellido.Text = supervisor.Apellido;
            tbxDireccion.Text = supervisor.Direccion;
            tbxTelefono.Text = supervisor.Telefono;
        }
    }
}
