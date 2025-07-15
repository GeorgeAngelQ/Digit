using System.Data;
using System.Dynamic;
using Digitalizacion.EN;
using Digitalizacion.SEC;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class UsuarioSistemaDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpUsuarioSistemaInsert = "UpUsuarioSistemaInsert";
        private const string UpUsuarioSistemaSelectById = "UpUsuarioSistemaSelectById";
        private const string UpUsuarioSistemaUpdate = "UpUsuarioSistemaUpdate";
        private const string UpUsuarioSistemaDelete = "UpUsuarioSistemaDelete";
        private const string UpUsuarioSistemaPagination = "UpUsuarioSistemaPagination";
        #endregion
        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion 
        #region Constructores
        public UsuarioSistemaDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion
        #region Métodos
        public void Insert(UsuarioSistema enUsuarioSistema)
        {
            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand())
                {
                    using (var tran = sqlCon.BeginTransaction())
                    {
                        try
                        {
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.CommandText = UpUsuarioSistemaInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.Usuario))
                            {
                                sqlCmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = enUsuarioSistema.Usuario;
                            }
                            if (!string.IsNullOrWhiteSpace(enUsuarioSistema.Contrasenia))
                            {
                                enUsuarioSistema.Contrasenia = PasswordHasher.HashPassword(enUsuarioSistema.Contrasenia);
                                sqlCmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = enUsuarioSistema.Contrasenia;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.Rol))
                            {
                                sqlCmd.Parameters.Add("@Rol", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Rol", SqlDbType.VarChar).Value = enUsuarioSistema.Rol;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.NombreCompleto))
                            {
                                sqlCmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = enUsuarioSistema.NombreCompleto;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.CorreoElectronico))
                            {
                                sqlCmd.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar).Value = enUsuarioSistema.CorreoElectronico;
                            }
                            sqlCmd.ExecuteNonQuery();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }
        public UsuarioSistema? SelectById(int idUsuario)
        {
            UsuarioSistema beUsuarioSistema = null;
            SqlDataReader dr = null;
            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpUsuarioSistemaSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                        dr = sqlCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            beUsuarioSistema = new UsuarioSistema();
                            beUsuarioSistema.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            if (dr["Usuario"] != DBNull.Value)
                            {
                                beUsuarioSistema.Usuario = Convert.ToString(dr["Usuario"]);
                            }
                            if (dr["Contrasenia"] != DBNull.Value)
                            {
                                beUsuarioSistema.Contrasenia = Convert.ToString(dr["Contrasenia"]);
                            }
                            if (dr["Rol"] != DBNull.Value)
                            {
                                beUsuarioSistema.Rol = Convert.ToString(dr["Rol"]);
                            }
                            if (dr["NombreCompleto"] != DBNull.Value)
                            {
                                beUsuarioSistema.NombreCompleto = Convert.ToString(dr["NombreCompleto"]);
                            }
                            if (dr["CorreoElectronico"] != DBNull.Value)
                            {
                                beUsuarioSistema.CorreoElectronico = Convert.ToString(dr["CorreoElectronico"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();
            }
            return beUsuarioSistema;
        }
        public void Update(int idUsuario, UsuarioSistema enUsuarioSistema)
        {
            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand())
                {
                    using (var tran = sqlCon.BeginTransaction())
                    {
                        try
                        {
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.CommandText = UpUsuarioSistemaUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.Usuario))
                            {
                                sqlCmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = enUsuarioSistema.Usuario;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.Contrasenia))
                            {
                                sqlCmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar).Value = enUsuarioSistema.Contrasenia;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.Rol))
                            {
                                sqlCmd.Parameters.Add("@Rol", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Rol", SqlDbType.VarChar).Value = enUsuarioSistema.Rol;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.NombreCompleto))
                            {
                                sqlCmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreCompleto", SqlDbType.VarChar).Value = enUsuarioSistema.NombreCompleto;
                            }
                            if (string.IsNullOrWhiteSpace(enUsuarioSistema.CorreoElectronico))
                            {
                                sqlCmd.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar).Value = enUsuarioSistema.CorreoElectronico;
                            }
                            sqlCmd.ExecuteNonQuery();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }
        public void Delete(int idUsuario)
        {
            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand())
                {
                    using (var tran = sqlCon.BeginTransaction())
                    {
                        try
                        {
                            sqlCmd.Connection = sqlCon;
                            sqlCmd.CommandText = UpUsuarioSistemaDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                            sqlCmd.ExecuteNonQuery();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
        }
        public UsuarioSistema? Login(string usuario, string contrasenia)
        {
            UsuarioSistema? beUsuarioSistema = null;
            SqlDataReader dr = null;

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT * FROM UsuarioSistema WHERE Usuario = @Usuario", sqlCon))
                {
                    sqlCmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = usuario;

                    dr = sqlCmd.ExecuteReader();
                    if (dr.Read())
                    {
                        var contraseniaHash = Convert.ToString(dr["Contrasenia"]);
                        if (PasswordHasher.VerifyPassword(contrasenia, contraseniaHash))
                        {
                            beUsuarioSistema = new UsuarioSistema
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Usuario = Convert.ToString(dr["Usuario"]),
                                Rol = Convert.ToString(dr["Rol"]),
                                NombreCompleto = Convert.ToString(dr["NombreCompleto"]),
                                CorreoElectronico = Convert.ToString(dr["CorreoElectronico"])
                            };
                        }
                    }
                }
            }

            return beUsuarioSistema;
        }
        public List<UsuarioSistema> List()
        {
            var lista = new List<UsuarioSistema>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT IdUsuario, Usuario, Rol,NombreCompleto, CorreoElectronico FROM UsuarioSistema", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var usuario = new UsuarioSistema
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Usuario = Convert.ToString(dr["Usuario"]),
                                Rol = Convert.ToString(dr["Rol"]),
                                NombreCompleto = Convert.ToString(dr["NombreCompleto"]),
                                CorreoElectronico = Convert.ToString(dr["CorreoElectronico"])
                            };
                            lista.Add(usuario);
                        }
                    }
                }
            }

            return lista;
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var beUsuarioSistemaLst = new List<ExpandoObject>();
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = "UpUsuarioSistemaPagination";
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        if (string.IsNullOrWhiteSpace(texto))
                        {
                            sqlCmd.Parameters.Add("@Texto", SqlDbType.NVarChar).Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCmd.Parameters.Add("@Texto", SqlDbType.NVarChar).Value = texto;
                        }
                        sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                        sqlCmd.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = currentPage;
                        sqlCmd.Parameters.Add("@OrderBy", SqlDbType.VarChar).Value = orderBy;

                        if (sortOrder.HasValue)
                        {
                            sqlCmd.Parameters.Add("@SortOrder", SqlDbType.Bit).Value = sortOrder;
                        }
                        else
                        {
                            sqlCmd.Parameters.Add("@SortOrder", SqlDbType.Bit).Value = DBNull.Value;
                        }
                        dr = sqlCmd.ExecuteReader();

                        while (dr.Read())
                        {
                            dynamic usuario = new ExpandoObject();

                            usuario.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            usuario.Usuario = Convert.ToString(dr["Usuario"]);
                            usuario.Rol = Convert.ToString(dr["Rol"]);
                            usuario.NombreCompleto = Convert.ToString(dr["NombreCompleto"]);
                            usuario.CorreoElectronico = Convert.ToString(dr["CorreoElectronico"]);
                            usuario.TotalRegistros = Convert.ToInt32(dr["TotalRegistros"]);

                            beUsuarioSistemaLst.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dr?.Close();
                dr?.Dispose();
            }

            return beUsuarioSistemaLst;
        }


        #endregion
    }
}