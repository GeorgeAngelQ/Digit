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
        private const string UpDocumentoPagination = "UpDocumentoPagination";
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
                            
                            sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = enDocumento.IdProceso;
                            if (string.IsNullOrWhiteSpace(enDocumento.NombreDocumento))
                            {
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = enDocumento.NombreDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.TipoDocumento))
                            {
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = enDocumento.TipoDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.FormatoDocumento))
                            {
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = enDocumento.FormatoDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.Estado_Documento))
                            {
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = enDocumento.Estado_Documento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.RutaArchivo))
                            {
                                sqlCmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar).Value = enDocumento.RutaArchivo;
                            }
                            if(string.IsNullOrWhiteSpace(enDocumento.NombreOriginal))
                            {
                                sqlCmd.Parameters.Add("@NombreOriginal", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreOriginal", SqlDbType.VarChar).Value = enDocumento.NombreOriginal;
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
                            {
                                beDocumento.IdProceso = Convert.ToInt32(dr["IdProceso"]);
                            }
                            if (dr["NombreDocumento"] != DBNull.Value)
                            {
                                beDocumento.NombreDocumento = Convert.ToString(dr["NombreDocumento"]);
                            }
                            if (dr["TipoDocumento"] != DBNull.Value)
                            {
                                beDocumento.TipoDocumento = Convert.ToString(dr["TipoDocumento"]);
                            }
                            if (dr["FormatoDocumento"] != DBNull.Value)
                            {
                                beDocumento.FormatoDocumento = Convert.ToString(dr["FormatoDocumento"]);
                            }
                            if (dr["FechaDigitalizacion"] != DBNull.Value)
                            {
                                beDocumento.FechaDigitalizacion = Convert.ToDateTime(dr["FechaDigitalizacion"]);
                            }
                            if (dr["Estado_Documento"] != DBNull.Value)
                            {
                                beDocumento.Estado_Documento = Convert.ToString(dr["Estado_Documento"]);
                            }
                            if (dr["RutaArchivo"] != DBNull.Value)
                            {
                                beDocumento.RutaArchivo = Convert.ToString(dr["RutaArchivo"]);
                            }
                            if (dr["NombreOriginal"] != DBNull.Value)
                            {
                                beDocumento.NombreOriginal = Convert.ToString(dr["NombreOriginal"]);
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
                            sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = enDocumento.IdProceso;
                            if (string.IsNullOrWhiteSpace(enDocumento.NombreDocumento))
                            {
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreDocumento", SqlDbType.VarChar).Value = enDocumento.NombreDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.TipoDocumento))
                            {
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = enDocumento.TipoDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.FormatoDocumento))
                            {
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@FormatoDocumento", SqlDbType.VarChar).Value = enDocumento.FormatoDocumento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.Estado_Documento))
                            {
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Estado_Documento", SqlDbType.VarChar).Value = enDocumento.Estado_Documento;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.RutaArchivo))
                            {
                                sqlCmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@RutaArchivo", SqlDbType.VarChar).Value = enDocumento.RutaArchivo;
                            }
                            if (string.IsNullOrWhiteSpace(enDocumento.NombreOriginal))
                            {
                                sqlCmd.Parameters.Add("@NombreOriginal", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreOriginal", SqlDbType.VarChar).Value = enDocumento.NombreOriginal;
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
        public List<DocumentoDTO> List()
        {
            var lista = new List<DocumentoDTO>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT D.IdDocumento, P.IdProceso, P.Prioridad,D.NombreDocumento,D.TipoDocumento,D.FormatoDocumento,D.FechaDigitalizacion,D.Estado_Documento,D.RutaArchivo,D.NombreOriginal FROM Documento D INNER JOIN Proceso P ON D.IdProceso = P.IdProceso", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var documentoDTO = new DocumentoDTO
                            {
                                IdDocumento = Convert.ToInt32(dr["IdDocumento"]),
                                IdProceso = Convert.ToInt32(dr["IdProceso"]),
                                NombreDocumento = Convert.ToString(dr["NombreDocumento"]) ?? "",
                                TipoDocumento = Convert.ToString(dr["TipoDocumento"]) ?? "",
                                FormatoDocumento = Convert.ToString(dr["FormatoDocumento"]) ?? "",
                                FechaDigitalizacion = dr["FechaDigitalizacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaDigitalizacion"]) : DateTime.MinValue,
                                Estado_Documento = Convert.ToString(dr["Estado_Documento"]) ?? "",
                                Prioridad = Convert.ToString(dr["Prioridad"]) ?? "",
                                RutaArchivo = Convert.ToString(dr["RutaArchivo"]) ?? "",
                                NombreOriginal = Convert.ToString(dr["NombreOriginal"]) ?? ""

                            }; 
                            lista.Add(documentoDTO);
                        }
                    }
                }
            }

            return lista;
        }

        public List<DocumentoDTO> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var lista = new List<DocumentoDTO>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("UpDocumentoPagination", sqlCon))
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
                            var item = new DocumentoDTO
                            {
                                IdDocumento = Convert.ToInt32(dr["IdDocumento"]),
                                IdProceso = Convert.ToInt32(dr["IdProceso"]),
                                NombreDocumento = Convert.ToString(dr["NombreDocumento"]) ?? "",
                                TipoDocumento = Convert.ToString(dr["TipoDocumento"]) ?? "",
                                FormatoDocumento = Convert.ToString(dr["FormatoDocumento"]) ?? "",
                                FechaDigitalizacion = dr["FechaDigitalizacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaDigitalizacion"]) : DateTime.MinValue,
                                Estado_Documento = Convert.ToString(dr["Estado_Documento"]) ?? "",
                                Prioridad = Convert.ToString(dr["Prioridad"]) ?? "",
                                RutaArchivo = Convert.ToString(dr["RutaArchivo"]) ?? "",
                                NombreOriginal = Convert.ToString(dr["NombreOriginal"]) ?? "",

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