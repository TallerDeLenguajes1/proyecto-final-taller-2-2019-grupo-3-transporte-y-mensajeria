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
    /// Lógica de interacción para EliminarCliente.xaml
    /// </summary>
    public partial class EliminarCliente : Page
    {
        public EliminarCliente(Cliente cliente, AccesoADatos.ADCliente clienteBD)
        {
            InitializeComponent();
            this.cliente = cliente;
            this.clienteBD = clienteBD;
        }

        //Variables y colecciones de datos auxiliares
        Cliente cliente;
        AccesoADatos.ADCliente clienteBD;
        bool estado = false;

        //Si eliminar cliente
        private void btnSiEliminar_Click(object sender, RoutedEventArgs e)
        {
            clienteBD.BajaCliente(cliente.Cuil);
            grdContent.Visibility = Visibility.Collapsed;
            estado = true;

            //ListClientesEncontrados.Clear();
            //lbxResultCliente.Items.Refresh();
        }

        //No eliminar cliente
        private void btnNoEliminar_Click(object sender, RoutedEventArgs e)
        {
            grdContent.Visibility = Visibility.Collapsed;
        }

        //Devolver estado de si el cliente fue eliminado o no
        public bool GetEstado()
        {
            return estado;
        }
    }
}
