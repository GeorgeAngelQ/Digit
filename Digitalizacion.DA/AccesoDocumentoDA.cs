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
        private const string UpAccesoDocumentoPagination = "UpAccesoDocumentoPagination";
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

                            sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = enAccesoDocumento.IdDocumento;
                            sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = enAccesoDocumento.IdUsuario;
                            if (string.IsNullOrWhiteSpace(enAccesoDocumento.TipoAcceso))
                            {
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = enAccesoDocumento.TipoAcceso;
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
                            beAccesoDocumento.IdDocumento = Convert.ToInt32(dr["IdDocumento"]);
                            beAccesoDocumento.IdUsuario = Convert.ToInt32(dr["IdUsuario"]);
                            if (dr["FechaAcceso"] != DBNull.Value)
                            {
                                beAccesoDocumento.FechaAcceso = Convert.ToDateTime(dr["FechaAcceso"]);
                            }
                            if (dr["TipoAcceso"] != DBNull.Value) { 
                            beAccesoDocumento.TipoAcceso = Convert.ToString(dr["TipoAcceso"]);
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
                            sqlCmd.Parameters.Add("@IdDocumento", SqlDbType.Int).Value = enAccesoDocumento.IdDocumento;
                            sqlCmd.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = enAccesoDocumento.IdUsuario;
                            if (string.IsNullOrWhiteSpace(enAccesoDocumento.TipoAcceso))
                            {
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoAcceso", SqlDbType.VarChar).Value = enAccesoDocumento.TipoAcceso;
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
        public List<AccesoDocumentoDTO> List()
        {
            var lista = new List<AccesoDocumentoDTO>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT A.IdAcceso,A.IdDocumento,D.TipoDocumento,D.NombreDocumento,A.IdUsuario,U.Usuario,U.NombreCompleto,A.FechaAcceso,A.TipoAcceso FROM AccesoDocumento A INNER JOIN Documento D ON A.IdDocumento = D.IdDocumento INNER JOIN UsuarioSistema U ON A.IdUsuario = U.IdUsuario;", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var accesoDocumentoDTO = new AccesoDocumentoDTO
                            {
                                IdAcceso = Convert.ToInt32(dr["IdAcceso"]),
                                IdDocumento = Convert.ToInt32(dr["IdDocumento"]),
                                NombreDocumento = dr["NombreDocumento"]?.ToString() ?? "",
                                TipoDocumento = dr["TipoDocumento"]?.ToString() ?? "",
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Usuario = Convert.ToString(dr["Usuario"]) ?? "",
                                NombreCompleto = dr["NombreCompleto"]?.ToString() ?? "",
                                FechaAcceso = Convert.ToDateTime(dr["FechaAcceso"]),
                                TipoAcceso = dr["TipoAcceso"]?.ToString() ?? ""
                            };
                            lista.Add(accesoDocumentoDTO);
                        }
                    }
                }
            }

            return lista;
        }
        public List<AccesoDocumentoDTO> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var lista = new List<AccesoDocumentoDTO>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("UpAccesoDocumentoPagination", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@Texto", string.IsNullOrWhiteSpace(texto) ? (object)DBNull.Value : texto);
                    sqlCmd.Parameters.AddWithValue("@PageSize", pageSize);
                    sqlCmd.Parameters.AddWithValue("@CurrentPage", currentPage);
                    sqlCmd.Parameters.AddWithValue("@OrderBy", orderBy);
                    sqlCmd.Parameters.AddWithValue("@SortOrder", sortOrder ?? (object)DBNull.Value);

                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var item = new AccesoDocumentoDTO
                            {
                                IdAcceso = Convert.ToInt32(dr["IdAcceso"]),
                                IdDocumento = Convert.ToInt32(dr["IdDocumento"]),
                                NombreDocumento = dr["NombreDocumento"]?.ToString() ?? "",
                                TipoDocumento = dr["TipoDocumento"]?.ToString() ?? "",
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Usuario = Convert.ToString(dr["Usuario"]) ?? "",
                                NombreCompleto = dr["NombreCompleto"]?.ToString() ?? "",
                                FechaAcceso = Convert.ToDateTime(dr["FechaAcceso"]),
                                TipoAcceso = dr["TipoAcceso"]?.ToString() ?? "",
                                TotalRegistros = Convert.ToInt32(dr["TotalRegistros"])
                            };

                            lista.Add(item);
                        }
                    }
                }
            }

            return lista;
        }

        #endregion
    }
}