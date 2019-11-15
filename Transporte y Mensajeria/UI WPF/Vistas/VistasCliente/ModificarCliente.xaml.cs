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

namespace UI_WPF.Vistas.VistasCliente
{
    /// <summary>
    /// Lógica de interacción para ModificarCliente.xaml
    /// </summary>
    public partial class ModificarCliente : Page
    {
        public ModificarCliente(Cliente cliente,AccesoADatos.ADCliente clienteBD, ABMCliente mainPage)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.clienteBD = clienteBD;
            this.mainPage = mainPage;

            //Relleno los campos con los atributos del cliente recibido
            RellenarCampos();
        }

        //Variables y colecciones de datos auxiliares
        Cliente cliente;
        AccesoADatos.ADCliente clienteBD;
        ABMCliente mainPage;

        private void RellenarCampos()
        {
            tbxCuil.Text = Convert.ToString(cliente.Cuil);
            tbxNombre.Text = cliente.Nombre;
            tbxApellido.Text = cliente.Apellido;
            tbxDireccion.Text = cliente.Direccion;
            tbxTelefono.Text = cliente.Telefono;

        }

        private void btnModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            int cuil = Convert.ToInt32(tbxCuil.Text);
            string nombre = tbxNombre.Text;
            string apellido = tbxApellido.Text;
            string direccion = tbxDireccion.Text;
            string telefono = tbxTelefono.Text;

            Cliente clienteEncontrado = new Cliente(cuil, nombre, apellido, direccion, telefono);

            clienteBD.ModificacionCliente(clienteEncontrado);

            mainPage.ActualizarResultadosBusqueda();
        }
    }
}
