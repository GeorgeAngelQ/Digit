using System.Data;
using System.Dynamic;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class DepartamentoDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpDepartamentoInsert = "UpDepartamentoInsert";
        private const string UpDepartamentoSelectById = "UpDepartamentoSelectById";
        private const string UpDepartamentoUpdate = "UpDepartamentoUpdate";
        private const string UpDepartamentoDelete = "UpDepartamentoDelete";
        #endregion
        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion 
        #region Constructores
        public DepartamentoDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion
        #region Metodos
        public void Insert(Departamento enDepartamento)
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
                            sqlCmd.CommandText = UpDepartamentoInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            if (string.IsNullOrWhiteSpace(enDepartamento.NombreDepartamento))
                            {
                                sqlCmd.Parameters.Add("@NombreDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreDepartamento", SqlDbType.VarChar).Value = enDepartamento.NombreDepartamento;
                            }
                            if (string.IsNullOrWhiteSpace(enDepartamento.UbicacionDepartamento))
                            {
                                sqlCmd.Parameters.Add("@UbicacionDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@UbicacionDepartamento", SqlDbType.VarChar).Value = enDepartamento.UbicacionDepartamento;
                            }
                            if (string.IsNullOrWhiteSpace(enDepartamento.ExtensionDepartamento))
                            {
                                sqlCmd.Parameters.Add("@ExtensionDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ExtensionDepartamento", SqlDbType.VarChar).Value = enDepartamento.ExtensionDepartamento;
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
        public Departamento? SelectById(int idDepartamento)
        {
            Departamento? beDepartamento = null;
            SqlDataReader dr = null;
            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpDepartamentoSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;
                        sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = idDepartamento;
                        dr = sqlCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            beDepartamento = new Departamento();
                            beDepartamento.IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]);
                            if (dr["NombreDepartamento"] != DBNull.Value)
                            {
                                beDepartamento.NombreDepartamento = dr["NombreDepartamento"].ToString();
                            }
                            if (dr["UbicacionDepartamento"] != DBNull.Value)
                            {
                                beDepartamento.UbicacionDepartamento = dr["UbicacionDepartamento"].ToString();
                            }
                            if (dr["ExtensionDepartamento"] != DBNull.Value)
                            {
                                beDepartamento.ExtensionDepartamento = dr["ExtensionDepartamento"].ToString();
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
            return beDepartamento;
        }
        public void Update(int idDepartamento, Departamento enDepartamento)
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
                            sqlCmd.CommandText = UpDepartamentoUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = idDepartamento;
                            if (string.IsNullOrWhiteSpace(enDepartamento.NombreDepartamento))
                            {
                                sqlCmd.Parameters.Add("@NombreDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@NombreDepartamento", SqlDbType.VarChar).Value = enDepartamento.NombreDepartamento;
                            }
                            if (string.IsNullOrWhiteSpace(enDepartamento.UbicacionDepartamento))
                            {
                                sqlCmd.Parameters.Add("@UbicacionDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@UbicacionDepartamento", SqlDbType.VarChar).Value = enDepartamento.UbicacionDepartamento;
                            }
                            if (string.IsNullOrWhiteSpace(enDepartamento.ExtensionDepartamento))
                            {
                                sqlCmd.Parameters.Add("@ExtensionDepartamento", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ExtensionDepartamento", SqlDbType.VarChar).Value = enDepartamento.ExtensionDepartamento;
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
        public void Delete(int idDepartamento)
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
                            sqlCmd.CommandText = UpDepartamentoDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = idDepartamento;
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
        public List<Departamento> List()
        {
            var lista = new List<Departamento>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT IdDepartamento, NombreDepartamento, UbicacionDepartamento, ExtensionDepartamento FROM dbo.Departamento", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var departamento = new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                NombreDepartamento = Convert.ToString(dr["NombreDepartamento"]),
                                UbicacionDepartamento = Convert.ToString(dr["UbicacionDepartamento"]),
                                ExtensionDepartamento = Convert.ToString(dr["ExtensionDepartamento"]),
                            };
                            lista.Add(departamento);
                        }
                    }
                }
            }

            return lista;
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var beDepartamentoLst = new List<ExpandoObject>();
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = "UpEquipoDigitalizacionPagination";
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
                            dynamic departamento = new ExpandoObject();

                            departamento.IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]);
                            departamento.NombreDepartamento = Convert.ToString(dr["NombreDepartamento"]);
                            departamento.UbicacionDepartamento = Convert.ToString(dr["UbicacionDepartamento"]);
                            departamento.ExtensionDepartamento = Convert.ToString(dr["ExtensionDepartamento"]);
                            beDepartamentoLst.Add(departamento);
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

            return beDepartamentoLst;
        }   
        #endregion
    }
}