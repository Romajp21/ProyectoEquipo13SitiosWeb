using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class CD_Artista
    {
        private readonly string connectionString;

        // Constructor para inicializar la conexión desde la configuración
        public CD_Artista(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        /// <summary>
        /// Obtiene una lista de todos los artistas activos de la base de datos.
        /// </summary>
        /// <returns>Lista de artistas activos.</returns>
        /// 
        //VISTA ADMIN
        public List<E_Artista> ObtenerArtistas()
        {
            List<E_Artista> artistas = new List<E_Artista>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Quitar el filtro por Estado
                string query = "SELECT IdArtista, Nombre, Apellidos, Telefono, Correo, Estado, IdRole FROM Artista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artistas.Add(new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"]?.ToString(),
                                Correo = reader["Correo"]?.ToString(),
                                Estado = (bool)reader["Estado"],
                                IdRole = (int)reader["IdRole"]
                            });
                        }
                    }
                }
            }

            return artistas;
        }



        /// <summary>
        /// Obtiene los detalles de un artista específico por su ID.
        /// </summary>
        /// <param name="idArtista">ID del artista.</param>
        /// <returns>Objeto E_Artista con la información del artista.</returns>
        public E_Artista ObtenerDetalleArtista(int idArtista)
        {
            E_Artista artista = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Artista WHERE IdArtista = @IdArtista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artista = new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"]?.ToString(),
                                Correo = reader["Correo"]?.ToString(),
                                Estado = (bool)reader["Estado"]
                            };
                        }
                    }
                }

                if (artista != null)
                {
                    // Obtener especialidades
                    string especialidadesQuery = @"
                        SELECT e.NombreEspecialidad 
                        FROM Especialidades e
                        INNER JOIN EspecialidadArtistas ea ON e.IdEspecialidad = ea.IdEspecialidad
                        WHERE ea.IdArtista = @IdArtista";

                    using (SqlCommand cmd = new SqlCommand(especialidadesQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                artista.Especialidades.Add(reader["NombreEspecialidad"].ToString());
                            }
                        }
                    }

                    
                }
            }

            return artista;
        }

        // Método para insertar un artista
        public int InsertarArtista(E_Artista artista)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertarArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros de entrada
                    cmd.Parameters.AddWithValue("@Nombre", artista.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", artista.Apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", artista.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", artista.Correo);
                    cmd.Parameters.AddWithValue("@Estado", artista.Estado);
                    cmd.Parameters.AddWithValue("@IdRole", artista.IdRole);

                    // Convertir las especialidades a un formato CSV
                    string especialidades = string.Join(",", artista.Especialidades);
                    cmd.Parameters.AddWithValue("@Especialidades", string.IsNullOrEmpty(especialidades) ? (object)DBNull.Value : especialidades);

                    // Parámetro de salida
                    SqlParameter outputId = new SqlParameter("@NuevoIdArtista", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputId);

                    // Ejecutar el procedimiento
                    cmd.ExecuteNonQuery();

                    // Devolver el ID del artista creado
                    return Convert.ToInt32(outputId.Value);
                }
            }
        }


        // Método para editar un artista
        public void EditarArtista(E_Artista artista)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditarArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdArtista", artista.IdArtista);
                    cmd.Parameters.AddWithValue("@Nombre", artista.Nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", artista.Apellidos);
                    cmd.Parameters.AddWithValue("@Telefono", artista.Telefono ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Correo", artista.Correo);
                    cmd.Parameters.AddWithValue("@Estado", artista.Estado);
                    cmd.Parameters.AddWithValue("@IdRole", artista.IdRole);

                    // Convertir lista de especialidades a formato CSV
                    string especialidades = string.Join(",", artista.Especialidades);
                    cmd.Parameters.AddWithValue("@Especialidades", string.IsNullOrEmpty(especialidades) ? (object)DBNull.Value : especialidades);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para eliminar un artista
        public void EliminarArtista(int idArtista)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EliminarArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<E_Roles> ObtenerRolesPorArtista(int idArtista)
        {
            List<E_Roles> roles = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT r.IdRole, r.Role, r.Estado
            FROM Roles r
            INNER JOIN Artista a ON r.IdRole = a.IdRole
            WHERE a.IdArtista = @IdArtista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new E_Roles
                            {
                                IdRole = (int)reader["IdRole"],
                                Role = reader["Role"].ToString(),
                                Estado = (bool)reader["Estado"]
                            });
                        }
                    }
                }
            }
            return roles;
        }

        // Obtener especialidades asignadas a un artista específico
        public List<E_Especialidades> ObtenerEspecialidadesPorArtista(int idArtista)
        {
            List<E_Especialidades> especialidades = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
    SELECT e.IdEspecialidad, e.NombreEspecialidad 
    FROM Especialidades e
    INNER JOIN EspecialidadArtistas ea ON e.IdEspecialidad = ea.IdEspecialidad
    WHERE ea.IdArtista = @IdArtista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            especialidades.Add(new E_Especialidades
                            {
                                IdEspecialidad = reader.GetInt32(reader.GetOrdinal("IdEspecialidad")),
                                NombreEspecialidad = reader.GetString(reader.GetOrdinal("NombreEspecialidad"))
                            });
                        }
                    }
                }
            }
            return especialidades;
        }


        public int CrearArtistaConEspecialidades(E_Artista artista, List<int> especialidadesIds)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insertar el nuevo artista
                    string queryArtista = @"
                INSERT INTO Artista (Nombre, Apellidos, Telefono, Correo, Estado, IdRole)
                OUTPUT INSERTED.IdArtista
                VALUES (@Nombre, @Apellidos, @Telefono, @Correo, @Estado, @IdRole)";

                    int idArtista;
                    using (SqlCommand cmd = new SqlCommand(queryArtista, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", artista.Nombre);
                        cmd.Parameters.AddWithValue("@Apellidos", artista.Apellidos);
                        cmd.Parameters.AddWithValue("@Telefono", artista.Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Correo", artista.Correo);
                        cmd.Parameters.AddWithValue("@Estado", artista.Estado);
                        cmd.Parameters.AddWithValue("@IdRole", artista.IdRole);

                        idArtista = (int)cmd.ExecuteScalar();
                    }

                    // Asignar las especialidades al artista
                    if (especialidadesIds != null && especialidadesIds.Any())
                    {
                        foreach (int idEspecialidad in especialidadesIds)
                        {
                            string queryEspecialidad = @"
                        INSERT INTO EspecialidadArtistas (IdArtista, IdEspecialidad, Estado)
                        VALUES (@IdArtista, @IdEspecialidad, 1)";

                            using (SqlCommand cmd = new SqlCommand(queryEspecialidad, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@IdArtista", idArtista);
                                cmd.Parameters.AddWithValue("@IdEspecialidad", idEspecialidad);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    return idArtista;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        // Método para asignar especialidades a un artista
        public void AsignarEspecialidadesAArtista(int idArtista, List<int> especialidadesIds)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Eliminar especialidades previas
                    string deleteQuery = "DELETE FROM EspecialidadArtistas WHERE IdArtista = @IdArtista";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@IdArtista", idArtista);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Insertar nuevas especialidades
                    foreach (int idEspecialidad in especialidadesIds)
                    {
                        string insertQuery = @"
                    INSERT INTO EspecialidadArtistas (IdArtista, IdEspecialidad, Estado)
                    VALUES (@IdArtista, @IdEspecialidad, 1)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@IdArtista", idArtista);
                            insertCmd.Parameters.AddWithValue("@IdEspecialidad", idEspecialidad);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar especialidades: " + ex.Message);
            }
        }


        public List<E_Artista> ObtenerArtistasPendientes()
        {
            List<E_Artista> artistas = new List<E_Artista>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerArtistasPendientes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artistas.Add(new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"]?.ToString(),
                                Correo = reader["Correo"]?.ToString(),
                                Estado = (bool)reader["Estado"],
                                IdRole = (int)reader["IdRole"]
                            });
                        }
                    }
                }
            }

            return artistas;
        }

        public void AprobarArtista(int idArtista)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_AprobarArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RechazarArtista(int idArtista)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_RechazarArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<E_Especialidades> ObtenerEspecialidades()
        {
            List<E_Especialidades> especialidades = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IdEspecialidad, NombreEspecialidad FROM Especialidades";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            especialidades.Add(new E_Especialidades
                            {
                                IdEspecialidad = (int)reader["IdEspecialidad"],
                                NombreEspecialidad = reader["NombreEspecialidad"].ToString()
                            });
                        }
                    }
                }
            }
            return especialidades;
        }


        public void ActualizarEstadoArtista(int idArtista, bool nuevoEstado)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "UPDATE Artista SET Estado = @Estado WHERE IdArtista = @IdArtista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<E_Artista> ObtenerTodosLosArtistas()
        {
            List<E_Artista> artistas = new List<E_Artista>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT IdArtista, Nombre, Apellidos, Telefono, Correo, Estado, IdRole FROM Artista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artistas.Add(new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Telefono = reader["Telefono"]?.ToString(),
                                Correo = reader["Correo"]?.ToString(),
                                Estado = (bool)reader["Estado"],
                                IdRole = (int)reader["IdRole"]
                            });
                        }
                    }
                }
            }

            return artistas;
        }



        // WEBAPP

        //Necesario para cargar imagenes al index de cliente
        public List<E_Artista> ObtenerArtistas1()
        {
            var artistas = new List<E_Artista>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IdArtista, Nombre, Apellidos, ImagenPerfil, Estado FROM Artista", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artistas.Add(new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                ImagenPerfil = reader["ImagenPerfil"] != DBNull.Value
                                    ? reader["ImagenPerfil"].ToString()
                                    : null,
                                Estado = (bool)reader["Estado"]
                            });
                        }
                    }
                }
            }

            return artistas;
        }


        //Para rellenar los datos del perfil del artista cliente
        public E_Artista ObtenerDetalleArtista1(int idArtista)
        {
            E_Artista artista = null;
            List<string> especialidades = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerDetalleArtista", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Leer la información principal del artista
                        if (reader.Read())
                        {
                            artista = new E_Artista
                            {
                                IdArtista = (int)reader["IdArtista"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                ImagenPerfil = reader["ImagenPerfil"]?.ToString(),
                                Bio = reader["Bio"]?.ToString()
                            };
                        }

                        // Leer las especialidades
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                especialidades.Add(reader["NombreEspecialidad"].ToString());
                            }
                        }
                    }
                }
            }

            if (artista != null)
            {
                artista.Especialidades = especialidades;
            }

            return artista;
        }


        public List<E_Portafolio> ObtenerPortafolio(int idArtista)
        {
            var portafolio = new List<E_Portafolio>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerPortafolio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            portafolio.Add(new E_Portafolio
                            {
                                IdImagen = (int)reader["IdImagen"],
                                RutaImagen = reader["RutaImagen"].ToString(),
                                DescripcionTatuaje = reader["DescripcionTatuaje"]?.ToString(),
                                Fecha = (DateTime)reader["Fecha"]
                            });
                        }
                    }
                }
            }

            return portafolio;
        }



        public void GuardarImagenPerfil(int idArtista, string imagenBase64)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Artista SET ImagenPerfil = @ImagenPerfil WHERE IdArtista = @IdArtista";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ImagenPerfil", imagenBase64);
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void ActualizarBio(int idArtista, string bio)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarBio", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdArtista", idArtista);
                    cmd.Parameters.AddWithValue("@Bio", bio);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public string ObtenerImagenPerfil(int idArtista)
        {
            string rutaImagen = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerImagenPerfil", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdArtista", idArtista);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read() && dr["ImagenPerfil"] != DBNull.Value)
                            {
                                rutaImagen = dr["ImagenPerfil"].ToString(); // Ruta como string
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la imagen de perfil: {ex.Message}", ex);
            }

            return rutaImagen;
        }








    }





}
