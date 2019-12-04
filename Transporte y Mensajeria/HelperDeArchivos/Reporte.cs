using AccesoADatos;
using EntidadesDelProyecto;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HelperDeArchivos
{

    public class Reporte
    {
        ADMoto motoBD = new ADMoto();
        ADFurgoneta furgonetaBD = new ADFurgoneta();
        ADAvion avionBD = new ADAvion();
        ADCliente clienteBD = new ADCliente();
        ADSobre sobreBD = new ADSobre();
        ADPaquete paqueteBD = new ADPaquete();
        ADSupervisor supervisorBD = new ADSupervisor();
        ADEnvio envioBD = new ADEnvio();
        string carpetaReportes = "Transporte y Mensajería/Reportes";
        //string carpetaReportesClientes = carpetaReportes +"Transporte y Mensajería/Reportes";
        //string carpetaReportesSupervisores = "Transporte y Mensajería/Reportes";
        //string carpetaReportesMotos = "Transporte y Mensajería/Reportes";
        //string carpetaReportesFurgonetas = "Transporte y Mensajería/Reportes";
        //string carpetaReportesAviones = "Transporte y Mensajería/Reportes";
        //string carpetaReportesSobres = "Transporte y Mensajería/Reportes";
        //string carpetaReportesPaquetes = "Transporte y Mensajería/Reportes";


        // metodo para crear las carpetas donde estaran los reportes
        public void crearCarpetas()
        {
            //Crear carpeta Transporte y Mensajeria
            try
            {
                if (!Directory.Exists("C:/" + carpetaReportes))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes);
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Clientes
            try
            {
                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Clientes"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Clientes");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Vehiculos
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Vehiculos"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Vehiculos");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Motos
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Vehiculos/Motos"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Vehiculos/Motos");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Furgonetas
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Vehiculos/Furgonetas"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Vehiculos/Furgonetas");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Aviones
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Vehiculos/Aviones"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Vehiculos/Aviones");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Supervisores
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Supervisores"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Supervisores");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Mercancias
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Mercancias"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Mercancias");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Sobres
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Mercancias/Sobres"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Mercancias/Sobres");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Paquetes
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Mercancias/Paquetes"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Mercancias/Paquetes");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }

            //Crear carpeta Envios
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Envios"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Envios");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }
            //Crear carpeta Boletas
            try
            {

                if (!Directory.Exists("C:/" + carpetaReportes + "/" + "Envios/Boletas"))
                {
                    Directory.CreateDirectory("C:/" + carpetaReportes + "/" + "Envios/Boletas");
                }
            }
            catch (Exception)
            {
                //Loguear el error de que no se pudo crear la carpeta
            }



        }
        public void generarReporteMotos()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Vehiculos/Motos/Reporte_Motos.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Motos   " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(5);// Esta es la primera fila

            table.AddCell("Id");
            table.AddCell("Modelo");
            table.AddCell("Fecha de compra");
            table.AddCell("Precio de compra");
            table.AddCell("cilindrada");

            //motosPdf = motoBD.GetMotos();

            foreach (Moto moto in motoBD.GetMotos())
            {
                table.AddCell(Convert.ToString(moto.IdVehiculo));
                table.AddCell(Convert.ToString(moto.Modelo));
                table.AddCell((moto.FechaCompra).ToString("dd/MM/yyyy"));
                table.AddCell(Convert.ToString(moto.PrecioCompra));
                table.AddCell(Convert.ToString(moto.Cilindrada));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
            
        }
        public void generarReporteFurgonetas()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Vehiculos/Furgonetas/Reporte_Furgonetas.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Furgonetas  " + fechaReporte);

            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(5);// Esta es la primera fila

            table.AddCell("Id");
            table.AddCell("Modelo");
            table.AddCell("Fecha de compra");
            table.AddCell("Precio de compra");
            table.AddCell("Capacidad de carga");


            foreach (Furgoneta furgoneta in furgonetaBD.GetFurgonetas())
            {
                table.AddCell(Convert.ToString(furgoneta.IdVehiculo));
                table.AddCell(Convert.ToString(furgoneta.Modelo));
                table.AddCell((furgoneta.FechaCompra).ToString("dd/MM/yyyy"));
                table.AddCell(Convert.ToString(furgoneta.PrecioCompra));
                table.AddCell(Convert.ToString(furgoneta.CapacidadCarga));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }
        public void generarReporteAviones()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Vehiculos/Aviones/Reporte_Aviones.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Aviones  " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(4);// Esta es la primera fila

            table.AddCell("Id");
            table.AddCell("Modelo");
            table.AddCell("Fecha de compra");
            table.AddCell("Precio de compra");


            foreach (Avion aviones in avionBD.GetAviones())
            {
                table.AddCell(Convert.ToString(aviones.IdVehiculo));
                table.AddCell(Convert.ToString(aviones.Modelo));
                table.AddCell((aviones.FechaCompra).ToString("dd/MM/yyyy"));
                table.AddCell(Convert.ToString(aviones.PrecioCompra));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }
        public void generarReporteClientes()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Clientes/Reporte_Clientes.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Clientes  " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(5);// Esta es la primera fila

            table.AddCell("Cuil");
            table.AddCell("Nombre");
            table.AddCell("Apellido");
            table.AddCell("Direccion");
            table.AddCell("Telefono");

            foreach (Cliente cliente in clienteBD.GetClientes())
            {
                table.AddCell(Convert.ToString(cliente.Cuil));
                table.AddCell(Convert.ToString(cliente.Nombre));
                table.AddCell(Convert.ToString(cliente.Apellido));
                table.AddCell(Convert.ToString(cliente.Direccion));
                table.AddCell(Convert.ToString(cliente.Telefono));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }

        public void generarReporteSupervisores()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Supervisores/Reporte_Supervisores.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Supervisores  " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(5);// Esta es la primera fila

            table.AddCell("Cuil");
            table.AddCell("Nombre");
            table.AddCell("Apellido");
            table.AddCell("Direccion");
            table.AddCell("Telefono");

            foreach (Supervisor supervisor in supervisorBD.GetSupervisores())
            {
                table.AddCell(Convert.ToString(supervisor.Cuil));
                table.AddCell(Convert.ToString(supervisor.Nombre));
                table.AddCell(Convert.ToString(supervisor.Apellido));
                table.AddCell(Convert.ToString(supervisor.Direccion));
                table.AddCell(Convert.ToString(supervisor.Telefono));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }

        public void generarReporteSobres()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Mercancias/Sobres/Reporte_Sobres.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Sobres  " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(7);// Esta es la primera fila
            table.WidthPercentage = 95f;
            table.AddCell("ID");
            table.AddCell("Contenido");
            table.AddCell("Asegurado");
            table.AddCell("Largo Recorrido");
            table.AddCell("Aumento por seguro");
            //table.AddCell("Precio gramo");
            table.AddCell("Peso");
            table.AddCell("Precio Neto");

            foreach (Sobre sobre in sobreBD.GetSobres())
            {
                table.AddCell(Convert.ToString(sobre.IdMercancia));
                table.AddCell(Convert.ToString(sobre.Contenido));

                table.AddCell(Convert.ToString(sobre.Asegurada));
                table.AddCell(Convert.ToString(sobre.LargoRecorrido));
                table.AddCell(Convert.ToString(sobre.AumSeguro));

                table.AddCell(Convert.ToString(sobre.Peso));
                table.AddCell(Convert.ToString(sobre.PrecioNeto));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }
        public void generarReportePaquete()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Mercancias/Paquetes/Reporte_Paquete.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Paquete  " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(7);// Esta es la primera fila
            table.WidthPercentage = 95f;
            table.AddCell("ID");
            table.AddCell("Contenido");
            table.AddCell("Asegurado");
            table.AddCell("Largo Recorrido");
            table.AddCell("Aumento por seguro");
            //table.AddCell("Precio gramo");
            table.AddCell("Volumen");
            table.AddCell("Precio Neto");

            foreach (Paquete paquete in paqueteBD.GetPaquetes())
            {
                table.AddCell(Convert.ToString(paquete.IdMercancia));
                table.AddCell(Convert.ToString(paquete.Contenido));

                table.AddCell(Convert.ToString(paquete.Asegurada));
                table.AddCell(Convert.ToString(paquete.LargoRecorrido));
                table.AddCell(Convert.ToString(paquete.AumSeguro));

                table.AddCell(Convert.ToString(paquete.Volumen));
                table.AddCell(Convert.ToString(paquete.PrecioNeto));

            }
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();
        }

        public void generarReporteEnvios()
        {
            crearCarpetas();
            string fechaReporte = DateTime.Now.ToString("dd/MM/yyyy");
            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Envios/Reporte_Envios.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();

            Paragraph title = new Paragraph();
            title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f);
            title.Add("Reporte de Envios   " + fechaReporte);
            doc.Add(title);
            doc.Add(new Paragraph(" "));//// Agregamos un parrafo vacio como separacion.
            PdfPTable table = new PdfPTable(6);// Esta es la primera fila
                                               ////
            PdfPCell cell = new PdfPCell(new Phrase("EMISOR"));

            //cell.BackgroundColor = new BaseColor(Color.DarkOrange );
            //cell.BorderWidthBottom = 1f;
            //cell.BorderWidthTop = 1f;
            //cell.BorderWidthRight = 1f;
            //cell.BorderWidthLeft = 1f;


            table.WidthPercentage = 95f;
            //table.AddCell(cell);
            table.AddCell(celdaConColor("ID"));
            table.AddCell(celdaConColor("FECHA"));
            table.AddCell(celdaConColor("EMISOR"));
            table.AddCell(celdaConColor("CUIL"));
            //table.AddCell(celdaConColor("RECEPTOR"));
            //table.AddCell(celdaConColor("CUIL"));
            table.AddCell(celdaConColor("CONTENIDO"));
            table.AddCell(celdaConColor("PRECIO"));
            //table.AddCell("CUIL");
            //table.AddCell("RECEPTOR");
            //table.AddCell("CUIL");
            //table.AddCell("CONTENIDO");
            //table.AddCell("PRECIO");

            //motosPdf = motoBD.GetMotos();


            foreach (Envio envio in envioBD.GetEnvios())
            {
                table.AddCell((Convert.ToString(envio.IdEnvio)));
                table.AddCell((Convert.ToString(envio.FechaEnvio.ToString("dd/MM/yyyy"))));
                table.AddCell((Convert.ToString(envio.Emisor.Apellido + " " + envio.Emisor.Nombre)));
                table.AddCell((Convert.ToString(envio.Emisor.Cuil)));
                //table.AddCell((Convert.ToString(envio.Receptor.Apellido + " " + envio.Receptor.Nombre)));
                //table.AddCell((Convert.ToString(envio.Receptor.Cuil)));
                table.AddCell((Convert.ToString(envio.Merc.Contenido)));
                table.AddCell((Convert.ToString(0)));
                //table.AddCell((Convert.ToString(envio.Merc.CalcularPrecioFinal())));
            }
            //foreach (Moto moto in motoBD.GetMotos())
            //{
            //    table.AddCell(Convert.ToString(moto.IdVehiculo));
            //    table.AddCell(Convert.ToString(moto.Modelo));
            //    table.AddCell((moto.FechaCompra).ToString("dd/MM/yyyy"));
            //    table.AddCell(Convert.ToString(moto.PrecioCompra));
            //    table.AddCell(Convert.ToString(moto.Cilindrada));

            //}
            //agregamos tabla a documento
            doc.Add(table);
            doc.Close();

        }
        public PdfPCell celdaConColor(string cont)
        {
            PdfPCell cell = new PdfPCell(new Phrase(cont));

            cell.BackgroundColor = new BaseColor(Color.DarkOrange);

            cell.BorderWidthBottom = 1f;
            cell.BorderWidthTop = 1f;
            //cell.BorderWidthRight = 1f;
            cell.BorderWidthLeft = 1f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            return cell;
        }

        public void generarBoleta()
        {
            crearCarpetas();

            //Envio envioCargar = envioBD.GetEnvio(idEnvio);
            Envio envioCargar = envioBD.GetEnvioUltimo();


            Document doc = new Document(PageSize.A4);

            PdfWriter.GetInstance(doc, new FileStream("C:/Transporte y Mensajería/Reportes/Envios/Boletas/Boleta_Envios.pdf", FileMode.Create));// asignamos el nombre de archivo reporte.pdf
            doc.Open();


            PdfPTable table = new PdfPTable(3);



            PdfPCell cell = new PdfPCell(new Phrase("EMPRESA DE MENSAJERIA Y TRANSPORTE"));
            cell.BorderWidthBottom = 1f;
            cell.BorderWidthTop = 1f;
            cell.BorderWidthRight = 1f;
            cell.BorderWidthLeft = 1f;
            cell.Colspan = 2;
            cell.Rowspan = 2;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            table.AddCell(Convert.ToString("ID: " + envioCargar.IdEnvio));
            table.AddCell("FECHA DEL ENVIO : " + envioCargar.FechaEnvio.ToString("dd/MM/yyyy"));

            table.AddCell("Emisor: " + envioCargar.Emisor.Apellido + " " + envioCargar.Emisor.Nombre);
            table.AddCell("Cuil: " + envioCargar.Emisor.Cuil);
            table.AddCell("Telefono: " + envioCargar.Emisor.Telefono);

            table.AddCell("Destinatario: " + envioCargar.Receptor.Apellido + " " + envioCargar.Receptor.Nombre);
            table.AddCell("Direccion: " + envioCargar.Receptor.Direccion);
            table.AddCell("Telefono: " + envioCargar.Receptor.Telefono);

            table.AddCell("Contenido: " + envioCargar.Merc.Contenido);
            table.AddCell("Cantidad: 1");


            PdfPCell cell2 = new PdfPCell(new Phrase("Precio Final: " + envioCargar.PrecioFinal));
            cell2.BackgroundColor = new BaseColor(Color.Gray);
            table.AddCell(cell2);
            //PdfPTable table = new PdfPTable(6);

            // Esta es la primera fila                                   
            //PdfPCell cell = new PdfPCell(new Phrase("EMISOR"));
            //cell.BackgroundColor = new BaseColor(Color.DarkOrange );
            //cell.BorderWidthBottom = 1f;
            //cell.BorderWidthTop = 1f;
            //cell.BorderWidthRight = 1f;
            //cell.BorderWidthLeft = 1f;


            table.WidthPercentage = 95f;

            ////agregamos tabla a documento
            doc.Add(table);
            doc.Close();

        }
    
    }
}
