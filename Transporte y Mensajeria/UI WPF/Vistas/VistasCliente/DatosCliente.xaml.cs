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
    /// Lógica de interacción para DatosCliente.xaml
    /// </summary>
    public partial class DatosCliente : Page
    {
        public DatosCliente(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;

            //Relleno los campos con los atributos del cliente recibido
            RellenarCampos();

        }

        //Variables y colecciones de datos auxiliares
        Cliente cliente;

        private void RellenarCampos()
        {
            tbxCuil.Text = Convert.ToString(cliente.Cuil);
            tbxNombre.Text = cliente.Nombre;
            tbxApellido.Text = cliente.Apellido;
            tbxDireccion.Text = cliente.Direccion;
            tbxTelefono.Text = cliente.Telefono;

        }

        
    }
}
