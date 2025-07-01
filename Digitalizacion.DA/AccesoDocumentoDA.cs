using System.Data;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class AccesoDocumentoDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpAccesoDocumentoInsert = "UpAccesoDocumentoInsert";
        private const string UpAccesoDocumentoSelectById = "UpAccesoDocumentoSelectById";
        private const string UpAccesoDocumentoUpdate = "UpAccesoDocumentoUpdate";
        private const string UpAccesoDocumentoDelete = "UpAccesoDocumentoDelete";
        #endregion

        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion

        #region Constructores
        public AccesoDocumentoDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion

        #region Métodos
        public void Insert(AccesoDocumento enAccesoDocumento)
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
                            sqlCmd.CommandText = UpAccesoDocumentoInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdAcceso", SqlDbType.Int).Value = enAccesoDocumento.IdAcceso;

                            if (enAccesoDocumento.IdDocumento.HasValue)
                            {
                                sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = enAccesoDocumento.IdDocumento;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = DBNull.Value;
                            }
                            if (enAccesoDocumento.IdUsuario.HasValue)
                                sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = enAccesoDocumento.IdUsuario;
                            else
                                sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = DBNull.Value;

                            if (enAccesoDocumento.FechaAcceso.HasValue)
                                sqlCmd.Parameters.Add("@FechaAcceso", SqlDbType.Date).Value = enAccesoDocumento.FechaAcceso;
                            else
                                sqlCmd.Parameters.Add("@FechaAcceso", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enAccesoDocumento.TipoAcceso))
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = enAccesoDocumento.TipoAcceso;

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

        public AccesoDocumento? SelectById(int idAcceso)
        {
            AccesoDocumento beAccesoDocumento = null;
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpAccesoDocumentoSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        sqlCmd.Parameters.Add("@IdAcceso", SqlDbType.Int).Value = idAcceso;
                        dr = sqlCmd.ExecuteReader();

                        while (dr.Read())
                        {
                            beAccesoDocumento = new AccesoDocumento();
                            beAccesoDocumento.IdAcceso = Convert.ToInt32(dr["IdAcceso"]);

                            if (dr["IdDocumento"] != DBNull.Value)
                                beAccesoDocumento.IdDocumento = Convert.ToInt32(dr["IdDocumento"]);

                            if (dr["IdUsuario"] != DBNull.Value)
                                beAccesoDocumento.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);

                            if (dr["FechaAcceso"] != DBNull.Value)
                                beAccesoDocumento.FechaAcceso = Convert.ToDateTime(dr["FechaAcceso"]);

                            if (dr["TipoAcceso"] != DBNull.Value)
                                beAccesoDocumento.TipoAcceso = Convert.ToString(dr["TipoAcceso"]);
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

            return beAccesoDocumento;
        }
        public void Update(int idAcceso, AccesoDocumento enAccesoDocumento)
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
                            sqlCmd.CommandText = UpAccesoDocumentoUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdAcceso", SqlDbType.Int).Value = idAcceso;

                            if (enAccesoDocumento.IdDocumento.HasValue)
                                sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = enAccesoDocumento.IdDocumento;
                            else
                                sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = DBNull.Value;

                            if (enAccesoDocumento.IdUsuario.HasValue)
                                sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = enAccesoDocumento.IdUsuario;
                            else
                                sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = DBNull.Value;

                            if (enAccesoDocumento.FechaAcceso.HasValue)
                                sqlCmd.Parameters.Add("@FechaAcceso", SqlDbType.Date).Value = enAccesoDocumento.FechaAcceso;
                            else
                                sqlCmd.Parameters.Add("@FechaAcceso", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enAccesoDocumento.TipoAcceso))
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = enAccesoDocumento.TipoAcceso;

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

        public void Delete(int idAcceso)
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
                            sqlCmd.CommandText = UpAccesoDocumentoDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdAcceso", SqlDbType.Int).Value = idAcceso;
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