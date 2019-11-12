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
    /// Lógica de interacción para AltaCliente.xaml
    /// </summary>
    public partial class AltaCliente : Page
    {
        public AltaCliente(AccesoADatos.ADCliente clienteBD)
        {
            InitializeComponent();
            this.clienteBD = clienteBD;
        }
        //Variables auxiliares
        AccesoADatos.ADCliente clienteBD;

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





    }
}
