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
    public class D_Detalle_venta
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public void insertarDetalleVentas(E_DetalleVentas venta)
        {
            SqlCommand cmd = new SqlCommand("SP_AgregarDetallesVentas", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

           
            cmd.Parameters.AddWithValue("@id_Inve", venta.Id_Inventario);
            cmd.Parameters.AddWithValue("@idfac", venta.Id_facturacion);
            cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
            cmd.Parameters.AddWithValue("@precio", venta.Precio);
            cmd.Parameters.AddWithValue("@impuesto", venta.Impuesto);
            cmd.Parameters.AddWithValue("@Estado", venta.Estado);
            cmd.Parameters.AddWithValue("@sub_total", venta.Subtotal);
            cmd.Parameters.AddWithValue("@monto_total", venta.MontoTotal);
            


            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void editarDetalleVentas(E_DetalleVentas venta)
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

            if(Estado == "Anulado")
            {
                return;
            }
            else
            {
                Cmd = new SqlCommand("SP_editarDetalleFactura", conexion);
                Cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                Cmd.Parameters.AddWithValue("@id_inve", venta.Id_Inventario);
                Cmd.Parameters.AddWithValue("@id_fac", venta.Id_facturacion);
                Cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                Cmd.Parameters.AddWithValue("@precio", venta.Precio);
                Cmd.Parameters.AddWithValue("@impuesto", venta.Impuesto);
                Cmd.Parameters.AddWithValue("@subtotal", venta.Subtotal);
                Cmd.Parameters.AddWithValue("@estado", venta.Estado);
                Cmd.Parameters.AddWithValue("@montototal", venta.MontoTotal);
                Cmd.Parameters.AddWithValue("@id_detalle", venta.Id_detalle);

                Cmd.ExecuteNonQuery();
                conexion.Close();
            }
         



        }

        public void eliminarDetalleFactura2(E_DetalleVentas cliente)
        {
            SqlCommand cmd = new SqlCommand("EliminarDetalleFac", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@Id_fac", cliente.Id_facturacion);
            cmd.Parameters.AddWithValue("@id_Inve", cliente.Id_Inventario);
            cmd.Parameters.AddWithValue("@estado", cliente.Estado);
            cmd.Parameters.AddWithValue("@cantidad", cliente.Cantidad);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable MostrarDetalleVentasCO2(int buscar)
        {
            DataTable dt = new DataTable("Detalle venta");
            SqlCommand cmd = new SqlCommand("Mostrar_dellate_venta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id_venta", buscar));
            conexion.Open();
            cmd.ExecuteNonQuery();

            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(dt);

            conexion.Close();
            return dt;
        }


        public DataTable MostrarDetalleVentasCO(string buscar)
        {
            DataTable dt = new DataTable("Detalle venta");
            SqlCommand cmd = new SqlCommand("BuscarVentasCliente", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@buscar", buscar));
            conexion.Open();
            cmd.ExecuteNonQuery();

            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(dt);

            conexion.Close();
            return dt;
        }


    }
}
