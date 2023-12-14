using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using capa_entidad;
using System.Data;

namespace capa_datos
{
    public class D_inventario
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public void insertarProductos(E_inventario inventario)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERTARPRODUCTOS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@CODIGO", inventario.Codigo);
            cmd.Parameters.AddWithValue("@DESCRIPCION", inventario.Descripcion);
            cmd.Parameters.AddWithValue("@PRECIO", inventario.Precio);
            cmd.Parameters.AddWithValue("@EXCISTENCIA", inventario.Excistencia);
            cmd.Parameters.AddWithValue("@ESTADO", inventario.Estado);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void EditarInventario(E_inventario inventario)
        {
            SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTOS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@ID_INVENT", inventario.ID_inventario);
            cmd.Parameters.AddWithValue("@CODIGO", inventario.Codigo);
            cmd.Parameters.AddWithValue("@DESCRIPCION", inventario.Descripcion);
            cmd.Parameters.AddWithValue("@PRECIO", inventario.Precio);
            cmd.Parameters.AddWithValue("@EXCISTENCIA", inventario.Excistencia);
            cmd.Parameters.AddWithValue("@ESTADO", inventario.Estado);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void EliminarInventario(E_inventario inventario)
        {
            SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTOS", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@ID_INVENT", inventario.ID_inventario);
            cmd.Parameters.AddWithValue("@ESTADO", inventario.Estado);


            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        
        public List<E_inventario> datosInventario(string buscar)
        {
            SqlDataReader leerfilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARPRODUCTOS", conexion);
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
                    Excistencia = leerfilas.GetInt32(4),
                   

                });
            }

            conexion.Close();
            leerfilas.Close();
            return listar;
        }

        public List<E_inventario> datosInventarioCodigo(string buscar)
        {
            SqlDataReader leerfilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARPRODUCTOSCODIGO", conexion);
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
                    Excistencia = leerfilas.GetInt32(4),


                });
            }

            conexion.Close();
            leerfilas.Close();
            return listar;
        }
    }
}
