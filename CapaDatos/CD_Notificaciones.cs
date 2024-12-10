using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Notificaciones
    {
        // Obtener correos según el módulo
        public List<E_Correo> ObtenerCorreos(string modulo)
        {
            List<E_Correo> correos = new List<E_Correo>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GestionCorreosYNotificaciones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Accion", 1); // Acción para obtener correos
                        cmd.Parameters.AddWithValue("@Modulo", modulo);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                correos.Add(new E_Correo
                                {
                                    Modulo = dr["Modulo"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellidos"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener correos para el módulo {modulo}: {ex.Message}", ex);
            }

            return correos;
        }

        // Registrar una nueva notificación
        public bool RegistrarNotificacion(E_Notificacion notificacion, out string mensaje)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GestionCorreosYNotificaciones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Accion", 2); // Acción para registrar notificación
                        cmd.Parameters.AddWithValue("@Modulo", notificacion.Modulo);
                        cmd.Parameters.AddWithValue("@Correo", notificacion.Correo);
                        cmd.Parameters.AddWithValue("@TipoEvento", notificacion.TipoEvento);
                        cmd.Parameters.AddWithValue("@Asunto", notificacion.Asunto);
                        cmd.Parameters.AddWithValue("@Mensaje", notificacion.Mensaje);

                        con.Open();
                        int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                        mensaje = "Notificación registrada exitosamente.";
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al registrar la notificación: {ex.Message}";
                return false;
            }
        }

        // Obtener notificaciones pendientes
        public List<E_Notificacion> ObtenerNotificacionesPendientes()
        {
            List<E_Notificacion> notificaciones = new List<E_Notificacion>();
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GestionCorreosYNotificaciones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Accion", 3); // Acción para obtener notificaciones pendientes
                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                notificaciones.Add(new E_Notificacion
                                {
                                    IdNotificacion = Convert.ToInt32(dr["IdNotificacion"]),
                                    Modulo = dr["Modulo"].ToString(),
                                    TipoEvento = dr["TipoEvento"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Asunto = dr["Asunto"].ToString(),
                                    Mensaje = dr["Mensaje"].ToString(),
                                    FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                                    Estado = dr["Estado"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener notificaciones pendientes", ex);
            }
            return notificaciones;
        }

        // Actualizar el estado de una notificación
        public bool ActualizarEstadoNotificacion(int idNotificacion, out string mensaje)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GestionCorreosYNotificaciones", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Accion", 4); // Acción para actualizar el estado
                        cmd.Parameters.AddWithValue("@IdNotificacion", idNotificacion);

                        con.Open();
                        int resultado = cmd.ExecuteNonQuery();
                        mensaje = "Estado actualizado exitosamente.";
                        return resultado > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = $"Error al actualizar el estado: {ex.Message}";
                return false;
            }
        }
    }
}
