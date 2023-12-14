using capa_entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace capa_datos
{
    public class D_ventas
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public void insertarVentas(E_Ventas venta)
        {
            SqlCommand cmd = new SqlCommand("SP_AgregarVenta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.Add(new SqlParameter("@Id_cliente", venta.Id_cliente));
            cmd.Parameters.Add(new SqlParameter("@Nu_Factura", venta.No_factura));
            cmd.Parameters.Add(new SqlParameter("@impuesto", venta.Impuesto));
            cmd.Parameters.Add(new SqlParameter("@subtotal", venta.Subtotal));
            cmd.Parameters.Add(new SqlParameter("@Montototal", venta.MontoTotal));
            cmd.Parameters.Add(new SqlParameter("@estado", venta.Estado));
            cmd.Parameters.Add(new SqlParameter("@fecha", venta.Fecha_venta));
            cmd.Parameters.Add(new SqlParameter("@tipo_venta", venta.TipoVenta1));

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void anularVentas(E_Ventas venta)
        {
            string Estado = string.Empty;

            SqlCommand Cmd = new SqlCommand("Select Estado From facturacion where idfac=" + venta.Id_facturacion + "", conexion);
            Cmd.CommandType = CommandType.Text;
            conexion.Open();

            SqlDataReader Dr = Cmd.ExecuteReader();
            if (Dr.Read())
            {
                Estado = Dr["Estado"].ToString();
            }
            Dr.Close();

            if (Estado == "Anulado")
            {
                MessageBox.Show("La venta ya ha sido anulada, seleccione otra venta por favor.");
                return;
            }
            else
            {
                Cmd = new SqlCommand("SP_EditarVenta", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                Cmd.Parameters.Add(new SqlParameter("@IdVentas", venta.Id_facturacion));
                Cmd.Parameters.Add(new SqlParameter("@Id_cliente", venta.Id_cliente));
                Cmd.Parameters.Add(new SqlParameter("@Nu_Factura", venta.No_factura));
                Cmd.Parameters.Add(new SqlParameter("@impuesto", venta.Impuesto));
                Cmd.Parameters.Add(new SqlParameter("@subtotal", venta.Subtotal));
                Cmd.Parameters.Add(new SqlParameter("@Montototal", venta.MontoTotal));
                Cmd.Parameters.Add(new SqlParameter("@estado", venta.Estado));
                Cmd.Parameters.Add(new SqlParameter("@fecha", venta.Fecha_venta));
                Cmd.Parameters.Add(new SqlParameter("@tipo_venta", venta.TipoVenta1));

                Cmd.ExecuteNonQuery();
                conexion.Close();


            }
            


        }


        public void eliminarFactura(E_Ventas ventas)
        {
            SqlCommand cmd = new SqlCommand("sp_eliminarFac", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@borrar", ventas.Id_facturacion);
          

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void eliminarFactura2(E_Ventas cliente)
        {
            SqlCommand cmd = new SqlCommand("EliminarFac", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@Id_fac", cliente.Id_facturacion);
            cmd.Parameters.AddWithValue("@estado", cliente.Estado);
           

            cmd.ExecuteNonQuery();
            conexion.Close();
        }


        //public DataTable MostrarTngresoVentas(string buscar)
        //{
        //    DataTable dt = new DataTable("Venta de Inventario");
        //    SqlCommand cmd = new SqlCommand("Mostrar_producto_venta", conexion);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@BUSCAR", buscar));
        //    conexion.Open();
           

            

        //    SqlDataReader dr = cmd.ExecuteReader();
        //    dt.Load(dr);

        //    dr.Close();
        //    conexion.Close();
        //    return dt;
        //}

        public List<E_inventario> MostrarProductoVentas(string buscar)
        {
            SqlDataReader leerfilas;
            SqlCommand cmd = new SqlCommand("Mostrar_producto_venta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerfilas = cmd.ExecuteReader();

            List<E_inventario> listar = new List<E_inventario>();

            while (leerfilas.Read())
            {
                listar.Add(new E_inventario
                {
                    ID_inventario = leerfilas.GetInt32(0),
                    Codigo = leerfilas.GetString(1),
                    Descripcion = leerfilas.GetString(2),
                    Precio = leerfilas.GetDecimal(3),
                    Excistencia = leerfilas.GetInt32(4)

                });
            }

            conexion.Close();
            leerfilas.Close();
            return listar;
        }
    }
}
