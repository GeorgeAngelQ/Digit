using System.Data;
using System.Dynamic;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class ResponsableDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpResponsableInsert = "UpResponsableInsert";
        private const string UpResponsableSelectById = "UpResponsableSelectById";
        private const string UpResponsableUpdate = "UpResponsableUpdate";
        private const string UpResponsableDelete = "UpResponsableDelete";
        private const string UpResponsablePagination = "UpResponsablePagination";
        #endregion

        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion 

        #region Constructores
        public ResponsableDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion

        #region Métodos
        public void Insert(Responsable enResponsable)
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
                            sqlCmd.CommandText = UpResponsableInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            if (string.IsNullOrWhiteSpace(enResponsable.NombreResponsable))
                            {
                                sqlCmd.Parameters.Add("@NombreResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreResponsable", SqlDbType.VarChar).Value = enResponsable.NombreResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.ApellidoResponsable))
                            {
                                sqlCmd.Parameters.Add("@ApellidoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ApellidoResponsable", SqlDbType.VarChar).Value = enResponsable.ApellidoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.CorreoResponsable))
                            {
                                sqlCmd.Parameters.Add("@CorreoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CorreoResponsable", SqlDbType.VarChar).Value = enResponsable.CorreoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.TelefonoResponsable))
                            {
                                sqlCmd.Parameters.Add("@TelefonoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TelefonoResponsable", SqlDbType.VarChar).Value = enResponsable.TelefonoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.CargoResponsable))
                            {
                                sqlCmd.Parameters.Add("@CargoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CargoResponsable", SqlDbType.VarChar).Value = enResponsable.CargoResponsable;
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
        public Responsable? SelectById(int idResponsable)
        {
            Responsable beResponsable = null;
            SqlDataReader dr = null;
            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpResponsableSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = idResponsable;
                        dr = sqlCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            beResponsable = new Responsable();
                            beResponsable.IdResponsable = Convert.ToInt32(dr["IdResponsable"]);
                            if (dr["NombreResponsable"] != DBNull.Value)
                            {
                                beResponsable.NombreResponsable = Convert.ToString(dr["NombreResponsable"]);
                            }
                            if (dr["ApellidoResponsable"] != DBNull.Value)
                            {
                                beResponsable.ApellidoResponsable = Convert.ToString(dr["ApellidoResponsable"]);
                            }
                            if (dr["CorreoResponsable"] != DBNull.Value)
                            {
                                beResponsable.CorreoResponsable = Convert.ToString(dr["CorreoResponsable"]);
                            }
                            if (dr["TelefonoResponsable"] != DBNull.Value)
                            {
                                beResponsable.TelefonoResponsable = Convert.ToString(dr["TelefonoResponsable"]);
                            }
                            if (dr["CargoResponsable"] != DBNull.Value)
                            {
                                beResponsable.CargoResponsable = Convert.ToString(dr["CargoResponsable"]);
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
            return beResponsable;
        }
        public void Update(int idResponsable, Responsable enResponsable)
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
                            sqlCmd.CommandText = UpResponsableUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = idResponsable;
                            if (string.IsNullOrWhiteSpace(enResponsable.NombreResponsable))
                            {
                                sqlCmd.Parameters.Add("@NombreResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreResponsable", SqlDbType.VarChar).Value = enResponsable.NombreResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.ApellidoResponsable))
                            {
                                sqlCmd.Parameters.AddWithValue("@ApellidoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ApellidoResponsable", SqlDbType.VarChar).Value = enResponsable.ApellidoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.CorreoResponsable))
                            {
                                sqlCmd.Parameters.Add("@CorreoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CorreoResponsable", SqlDbType.VarChar).Value = enResponsable.CorreoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.TelefonoResponsable))
                            {
                                sqlCmd.Parameters.Add("@TelefonoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TelefonoResponsable", SqlDbType.VarChar).Value = enResponsable.TelefonoResponsable;
                            }
                            if (string.IsNullOrWhiteSpace(enResponsable.CargoResponsable))
                            {
                                sqlCmd.Parameters.Add("@CargoResponsable", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@CargoResponsable", SqlDbType.VarChar).Value = enResponsable.CargoResponsable;
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
        public void Delete(int idResponsable)
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
                            sqlCmd.CommandText = UpResponsableDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = idResponsable;
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
        public List<Responsable> List()
        {
            var lista = new List<Responsable>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT IdResponsable, NombreResponsable, ApellidoResponsable, CorreoResponsable, TelefonoResponsable, CargoResponsable FROM dbo.Responsable", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var responsable = new Responsable
                            {
                                IdResponsable = Convert.ToInt32(dr["IdResponsable"]),
                                NombreResponsable = Convert.ToString(dr["NombreResponsable"]),
                                ApellidoResponsable = Convert.ToString(dr["ApellidoResponsable"]),
                                CorreoResponsable = Convert.ToString(dr["CorreoResponsable"]),
                                TelefonoResponsable = Convert.ToString(dr["TelefonoResponsable"]),
                                CargoResponsable = Convert.ToString(dr["CargoResponsable"])
                            };
                            lista.Add(responsable);
                        }
                    }
                }
            }

            return lista;
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var beResponsableLst = new List<ExpandoObject>();
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = "UpResponsablePagination";
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
                            dynamic responsable = new ExpandoObject();

                            responsable.IdResponsable = Convert.ToInt32(dr["IdResponsable"]);
                            responsable.NombreResponsable = Convert.ToString(dr["NombreResponsable"]);
                            responsable.ApellidoResponsable = Convert.ToString(dr["ApellidoResponsable"]);
                            responsable.CorreoResponsable = Convert.ToString(dr["CorreoResponsable"]);
                            responsable.TelefonoResponsable = Convert.ToString(dr["TelefonoResponsable"]);
                            responsable.CargoResponsable = Convert.ToString(dr["CargoResponsable"]);
                            responsable.TotalRegistros = Convert.ToInt32(dr["TotalRegistros"]);

                            beResponsableLst.Add(responsable);
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

            return beResponsableLst;
        }
        #endregion
    }
}