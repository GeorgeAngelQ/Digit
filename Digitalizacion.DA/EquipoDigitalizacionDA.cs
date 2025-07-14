using System.Data;
using System.Dynamic;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class EquipoDigitalizacionDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpEquipoDigitalizacionInsert = "UpEquipoDigitalizacionInsert";
        private const string UpEquipoDigitalizacionSelectById = "UpEquipoDigitalizacionSelectById";
        private const string UpEquipoDigitalizacionUpdate = "UpEquipoDigitalizacionUpdate";
        private const string UpEquipoDigitalizacionDelete = "UpEquipoDigitalizacionDelete";
        private const string UpEquipoDigitalizacionPagination = "UpEquipoDigitalizacionPagination";
        #endregion
        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion 
        #region Constructores
        public EquipoDigitalizacionDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion
        #region Metodos
        public void Insert(EquipoDigitalizacion enEquipoDigitalizacion)
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
                            sqlCmd.CommandText = UpEquipoDigitalizacionInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.TipoEquipo))
                            {
                                sqlCmd.Parameters.Add("@TipoEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.TipoEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.MarcaEquipo))
                            {
                                sqlCmd.Parameters.Add("@MarcaEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@MarcaEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.MarcaEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.ModeloEquipo))
                            {
                                sqlCmd.Parameters.Add("@ModeloEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ModeloEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.ModeloEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.EstadoEquipo))
                            {
                                sqlCmd.Parameters.Add("@EstadoEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@EstadoEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.EstadoEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.UbicacionEquipo))
                            {
                                sqlCmd.Parameters.Add("@UbicacionEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@UbicacionEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.UbicacionEquipo;
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
        public EquipoDigitalizacion? SelectById(int idEquipo)
        {
            EquipoDigitalizacion beEquipoDigitalizacion = null;
            SqlDataReader dr = null;
            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpEquipoDigitalizacionSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = idEquipo;
                        dr = sqlCmd.ExecuteReader();
                        while (dr.Read())
                        {
                            beEquipoDigitalizacion = new EquipoDigitalizacion();
                            beEquipoDigitalizacion.IdEquipo = Convert.ToInt32(dr["IdEquipo"]);
                            if (dr["TipoEquipo"] != DBNull.Value)
                            {
                                beEquipoDigitalizacion.TipoEquipo = Convert.ToString(dr["TipoEquipo"]);
                            }
                            if (dr["MarcaEquipo"] != DBNull.Value)
                            {
                                beEquipoDigitalizacion.MarcaEquipo = Convert.ToString(dr["MarcaEquipo"]);
                            }
                            if (dr["ModeloEquipo"] != DBNull.Value)
                            {
                                beEquipoDigitalizacion.ModeloEquipo = Convert.ToString(dr["ModeloEquipo"]);
                            }
                            if (dr["EstadoEquipo"] != DBNull.Value)
                            {
                                beEquipoDigitalizacion.EstadoEquipo = Convert.ToString(dr["EstadoEquipo"]);
                            }
                            if (dr["UbicacionEquipo"] != DBNull.Value)
                            {
                                beEquipoDigitalizacion.UbicacionEquipo = Convert.ToString(dr["UbicacionEquipo"]);
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
            return beEquipoDigitalizacion;
        }
        public void Update(int idEquipo, EquipoDigitalizacion enEquipoDigitalizacion)
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
                            sqlCmd.CommandText = UpEquipoDigitalizacionUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = idEquipo;
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.TipoEquipo))
                            {
                                sqlCmd.Parameters.Add("@TipoEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@TipoEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.TipoEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.MarcaEquipo))
                            {
                                sqlCmd.Parameters.Add("@MarcaEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@MarcaEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.MarcaEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.ModeloEquipo))
                            {
                                sqlCmd.Parameters.Add("@ModeloEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@ModeloEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.ModeloEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.EstadoEquipo))
                            {
                                sqlCmd.Parameters.Add("@EstadoEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@EstadoEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.EstadoEquipo;
                            }
                            if (string.IsNullOrWhiteSpace(enEquipoDigitalizacion.UbicacionEquipo))
                            {
                                sqlCmd.Parameters.Add("@UbicacionEquipo", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@UbicacionEquipo", SqlDbType.VarChar).Value = enEquipoDigitalizacion.UbicacionEquipo;
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
        public void Delete(int idEquipo)
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
                            sqlCmd.CommandText = UpEquipoDigitalizacionDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandTimeout = 0;
                            sqlCmd.Transaction = tran;

                            sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = idEquipo;
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

        public List<EquipoDigitalizacion> List()
        {
            var lista = new List<EquipoDigitalizacion>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT IdEquipo, TipoEquipo, MarcaEquipo, ModeloEquipo, EstadoEquipo, UbicacionEquipo FROM dbo.EquipoDigitalizacion", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var equipo = new EquipoDigitalizacion
                            {
                                IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                                TipoEquipo = Convert.ToString(dr["TipoEquipo"]),
                                MarcaEquipo = Convert.ToString(dr["MarcaEquipo"]),
                                ModeloEquipo = Convert.ToString(dr["ModeloEquipo"]),
                                EstadoEquipo = Convert.ToString(dr["EstadoEquipo"]),
                                UbicacionEquipo = Convert.ToString(dr["UbicacionEquipo"])
                            };
                            lista.Add(equipo);
                        }
                    }
                }
            }

            return lista;
        }
        public List<ExpandoObject> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var beEquipoDigitalizacionLst = new List<ExpandoObject>();
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
                            dynamic equipo = new ExpandoObject();

                            equipo.IdEquipo = Convert.ToInt32(dr["IdEquipo"]);
                            equipo.TipoEquipo = Convert.ToString(dr["TipoEquipo"]);
                            equipo.MarcaEquipo = Convert.ToString(dr["MarcaEquipo"]);
                            equipo.ModeloEquipo = Convert.ToString(dr["ModeloEquipo"]);
                            equipo.EstadoEquipo = Convert.ToString(dr["EstadoEquipo"]);
                            equipo.UbicacionEquipo = Convert.ToString(dr["UbicacionEquipo"]);
                            equipo.TotalRegistros = Convert.ToInt32(dr["TotalRegistros"]);

                            beEquipoDigitalizacionLst.Add(equipo);
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

            return beEquipoDigitalizacionLst;
        }
        #endregion
    }
}