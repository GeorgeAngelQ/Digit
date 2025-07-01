using System.Data;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class DocumentoDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpDocumentoInsert = "UpDocumentoInsert";
        private const string UpDocumentoSelectById = "UpDocumentoSelectById";
        private const string UpDocumentoUpdate = "UpDocumentoUpdate";
        private const string UpDocumentoDelete = "UpDocumentoDelete";
        #endregion

        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion

        #region Constructores
        public DocumentoDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion

        #region Métodos

        public void Insert(Documento enDocumento)
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
                            sqlCmd.CommandText = UpDocumentoInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            if (enDocumento.IdProceso.HasValue)
                                sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = enDocumento.IdProceso;
                            else
                                sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enDocumento.NombreDocumento))
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = enDocumento.NombreDocumento;

                            if (string.IsNullOrWhiteSpace(enDocumento.TipoDocumento))
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = enDocumento.TipoDocumento;

                            if (string.IsNullOrWhiteSpace(enDocumento.FormatoDocumento))
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = enDocumento.FormatoDocumento;

                            if (enDocumento.FechaDigitalizacion.HasValue)
                                sqlCmd.Parameters.Add("@FechaDigitalizacion", SqlDbType.Date).Value = enDocumento.FechaDigitalizacion;
                            else
                                sqlCmd.Parameters.Add("@FechaDigitalizacion", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enDocumento.Estado_Documento))
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = enDocumento.Estado_Documento;

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

        public Documento? SelectById(int idDocumento)
        {
            Documento beDocumento = null;
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpDocumentoSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = idDocumento;
                        dr = sqlCmd.ExecuteReader();

                        while (dr.Read())
                        {
                            beDocumento = new Documento();
                            beDocumento.IdDocumento = Convert.ToInt32(dr["IdDocumento"]);

                            if (dr["IdProceso"] != DBNull.Value)
                                beDocumento.IdProceso = Convert.ToInt32(dr["IdProceso"]);

                            if (dr["NombreDocumento"] != DBNull.Value)
                                beDocumento.NombreDocumento = Convert.ToString(dr["NombreDocumento"]);

                            if (dr["TipoDocumento"] != DBNull.Value)
                                beDocumento.TipoDocumento = Convert.ToString(dr["TipoDocumento"]);

                            if (dr["FormatoDocumento"] != DBNull.Value)
                                beDocumento.FormatoDocumento = Convert.ToString(dr["FormatoDocumento"]);

                            if (dr["FechaDigitalizacion"] != DBNull.Value)
                                beDocumento.FechaDigitalizacion = Convert.ToDateTime(dr["FechaDigitalizacion"]);

                            if (dr["Estado_Documento"] != DBNull.Value)
                                beDocumento.Estado_Documento = Convert.ToString(dr["Estado_Documento"]);
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

            return beDocumento;
        }

        public void Update(int idDocumento, Documento enDocumento)
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
                            sqlCmd.CommandText = UpDocumentoUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = idDocumento;

                            if (enDocumento.IdProceso.HasValue)
                                sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = enDocumento.IdProceso;
                            else
                                sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enDocumento.NombreDocumento))
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = enDocumento.NombreDocumento;

                            if (string.IsNullOrWhiteSpace(enDocumento.TipoDocumento))
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = enDocumento.TipoDocumento;

                            if (string.IsNullOrWhiteSpace(enDocumento.FormatoDocumento))
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = enDocumento.FormatoDocumento;

                            if (enDocumento.FechaDigitalizacion.HasValue)
                                sqlCmd.Parameters.Add("@FechaDigitalizacion", SqlDbType.Date).Value = enDocumento.FechaDigitalizacion;
                            else
                                sqlCmd.Parameters.Add("@FechaDigitalizacion", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enDocumento.Estado_Documento))
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = enDocumento.Estado_Documento;

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

        public void Delete(int idDocumento)
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
                            sqlCmd.CommandText = UpDocumentoDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = idDocumento;
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