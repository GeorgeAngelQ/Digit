using System.Data;
using Digitalizacion.EN;
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
        #endregion
    }
}