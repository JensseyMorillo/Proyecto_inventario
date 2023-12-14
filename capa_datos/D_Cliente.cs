using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using capa_entidad;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capa_datos
{
    public class D_Cliente
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public void insertarCliente( E_cliente cliente)
        {
            SqlCommand cmd = new SqlCommand("SP_AGREGARCLIENTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
            cmd.Parameters.AddWithValue("@CEDULA", cliente.Cedula);
            cmd.Parameters.AddWithValue("@TELEFONO", cliente.Telefono);
            cmd.Parameters.AddWithValue("@CARRO", cliente.Carro);
            cmd.Parameters.AddWithValue("@MODELO", cliente.Modelo);
            cmd.Parameters.AddWithValue("@AÑO", cliente.Año);
            cmd.Parameters.AddWithValue("@PLACA", cliente.Placa);
            cmd.Parameters.AddWithValue("@DESCRIPCION", cliente.Descripcion);
            cmd.Parameters.AddWithValue("@FECHA", cliente.Fecha);
            cmd.Parameters.AddWithValue("@ESTADO", cliente.Estado);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void editarCliente(E_cliente cliente)
        {
            SqlCommand cmd = new SqlCommand("SP_EDITARCLIENTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@ID_CLIENTE", cliente.IdCliente);
            cmd.Parameters.AddWithValue("@NOMBRE", cliente.Nombre);
            cmd.Parameters.AddWithValue("@APELLIDO", cliente.Apellido);
            cmd.Parameters.AddWithValue("@CEDULA", cliente.Cedula);
            cmd.Parameters.AddWithValue("@TELEFONO", cliente.Telefono);
            cmd.Parameters.AddWithValue("@CARRO", cliente.Carro);
            cmd.Parameters.AddWithValue("@MODELO", cliente.Modelo);
            cmd.Parameters.AddWithValue("@AÑO", cliente.Año);
            cmd.Parameters.AddWithValue("@PLACA", cliente.Placa);
            cmd.Parameters.AddWithValue("@DESCRIPCION", cliente.Descripcion);
            cmd.Parameters.AddWithValue("@FECHA", cliente.Fecha);
            cmd.Parameters.AddWithValue("@ESTADO", cliente.Estado);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public void eliminarCliente(E_cliente cliente)
        {
            SqlCommand cmd = new SqlCommand("SP_ELIMINARCLIENTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@ID_CLIENTE", cliente.IdCliente);
            cmd.Parameters.AddWithValue("@ESTADO", cliente.Estado);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        public List<E_cliente> datosCliente(string buscar)
        {
            SqlDataReader leerfilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARCLIENTE", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerfilas = cmd.ExecuteReader();

            List<E_cliente> listar = new List<E_cliente>();

            while (leerfilas.Read())
            {
                listar.Add(new E_cliente
                {
                    IdCliente=leerfilas.GetInt32(0),
                    Nombre=leerfilas.GetString(1),
                    Apellido=leerfilas.GetString(2),
                    Cedula=leerfilas.GetString(3),
                    Telefono=leerfilas.GetString(4),
                    Carro=leerfilas.GetString(5),
                    Modelo=leerfilas.GetString(6),
                    Año=leerfilas.GetString(7),
                    Placa=leerfilas.GetString(8),
                    Descripcion=leerfilas.GetString(9),
                    Fecha=leerfilas.GetDateTime(10),
                    Estado = leerfilas.GetString(11)

                });
            }

            conexion.Close();
            leerfilas.Close();
            return listar;
        
        
        }

        public List<E_cliente> datosCliente2(string buscar)
        {
            SqlDataReader leerfilas;
            SqlCommand cmd = new SqlCommand("SP_BUSCARCLIENTE2", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@BUSCAR", buscar);
            leerfilas = cmd.ExecuteReader();

            List<E_cliente> listar = new List<E_cliente>();

            while (leerfilas.Read())
            {
                listar.Add(new E_cliente
                {
                    IdCliente = leerfilas.GetInt32(0),
                    Nombre = leerfilas.GetString(1),
                    Apellido = leerfilas.GetString(2),
                    Cedula = leerfilas.GetString(3),
                    Telefono = leerfilas.GetString(4),
                    Carro = leerfilas.GetString(5),
                    Modelo = leerfilas.GetString(6),
                    Año = leerfilas.GetString(7),
                    Placa = leerfilas.GetString(8),
                    Descripcion = leerfilas.GetString(9),
                    Fecha = leerfilas.GetDateTime(10),
                    Estado = leerfilas.GetString(11)

                });
            }

            conexion.Close();
            leerfilas.Close();
            return listar;


        }

    }
}
