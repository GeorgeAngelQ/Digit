using System.Data;
using System.Dynamic;
using Digitalizacion.EN;
using Libreria;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Digitalizacion.DA
{
    public class ProcesoDA : ConfigDataAccess
    {
        #region Constantes
        private const string UpProcesoInsert = "UpProcesoInsert";
        private const string UpProcesoSelectById = "UpProcesoSelectById";
        private const string UpProcesoUpdate = "UpProcesoUpdate";
        private const string UpProcesoDelete = "UpProcesoDelete";
        private const string UpProcesoPagination = "UpProcesoPagination";
        #endregion

        #region Propiedades
        private string CadenaDeConexion { get; set; }
        #endregion

        #region Constructores
        public ProcesoDA()
        {
            CadenaDeConexion = Configuration.GetConnectionString("PROCDIGITAL")!;
        }
        #endregion

        #region Métodos

        public void Insert(Proceso enProceso)
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
                            sqlCmd.CommandText = UpProcesoInsert;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = enProceso.IdResponsable;
                            sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = enProceso.IdDepartamento;
                            sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = enProceso.IdEquipo;
                            if (enProceso.FechaFin.HasValue)
                            {
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = enProceso.FechaFin;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DBNull.Value;
                            }
                            if (string.IsNullOrWhiteSpace(enProceso.Estado))
                            {
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = enProceso.Estado;
                            }
                            if (string.IsNullOrWhiteSpace(enProceso.Prioridad))
                            {
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = enProceso.Prioridad;
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

        public Proceso? SelectById(int idProceso)
        {
            Proceso beProceso = null;
            SqlDataReader dr = null;

            try
            {
                using (var sqlCon = new SqlConnection(CadenaDeConexion))
                {
                    sqlCon.Open();
                    using (var sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = sqlCon;
                        sqlCmd.CommandText = UpProcesoSelectById;
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandTimeout = 0;

                        sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = idProceso;
                        dr = sqlCmd.ExecuteReader();

                        while (dr.Read())
                        {
                            beProceso = new Proceso();
                            beProceso.IdProceso = Convert.ToInt32(dr["IdProceso"]);
                            beProceso.IdResponsable = Convert.ToInt32(dr["IdResponsable"]);
                            beProceso.IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]);
                            beProceso.IdEquipo = Convert.ToInt32(dr["IdEquipo"]);
                            if (dr["FechaInicio"] != DBNull.Value)
                            {
                                beProceso.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);
                            }
                            if (dr["FechaFin"] != DBNull.Value)
                            {
                                beProceso.FechaFin = Convert.ToDateTime(dr["FechaFin"]);
                            }
                            if (dr["Estado"] != DBNull.Value)
                            {
                                beProceso.Estado = Convert.ToString(dr["Estado"]);
                            }
                            if (dr["Prioridad"] != DBNull.Value)
                            {
                                beProceso.Prioridad = Convert.ToString(dr["Prioridad"]);
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

            return beProceso;
        }

        public void Update(int idProceso, Proceso enProceso)
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
                            sqlCmd.CommandText = UpProcesoUpdate;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = idProceso;
                            sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = enProceso.IdResponsable;
                            sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = enProceso.IdDepartamento;
                            sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = enProceso.IdEquipo;
                            if (enProceso.FechaFin.HasValue)
                            {
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = enProceso.FechaFin;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DBNull.Value;
                            }
                            if (string.IsNullOrWhiteSpace(enProceso.Estado))
                            {
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = enProceso.Estado;
                            }
                            if (string.IsNullOrWhiteSpace(enProceso.Prioridad))
                            {
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = DBNull.Value;
                            }
                            else
                            {
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = enProceso.Prioridad;
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

        public void Delete(int idProceso)
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
                            sqlCmd.CommandText = UpProcesoDelete;
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.Transaction = tran;
                            sqlCmd.CommandTimeout = 0;

                            sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = idProceso;
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
        public List<ProcesoDTO> List()
        {
            var lista = new List<ProcesoDTO>();

            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("SELECT P.IdProceso,R.IdResponsable,D.IdDepartamento,E.IdEquipo,P.FechaInicio,P.FechaFin,P.Estado,P.Prioridad,R.NombreResponsable,D.NombreDepartamento,E.MarcaEquipo,E.ModeloEquipo FROM Proceso P INNER JOIN Responsable R ON P.IdResponsable=R.IdResponsable INNER JOIN Departamento D ON P.IdDepartamento=D.IdDepartamento INNER JOIN EquipoDigitalizacion E ON P.IdEquipo=E.IdEquipo", sqlCon))
                {
                    using (var dr = sqlCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var procesoDTO = new ProcesoDTO
                            {
                                IdProceso = Convert.ToInt32(dr["IdProceso"]),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                Estado = Convert.ToString(dr["Estado"]),
                                Prioridad = Convert.ToString(dr["Prioridad"]),
                                IdResponsable = Convert.ToInt32(dr["IdResponsable"]),
                                NombreResponsable = Convert.ToString(dr["NombreResponsable"]),

                                IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                NombreDepartamento = Convert.ToString(dr["NombreDepartamento"]),

                                IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                                MarcaEquipo = Convert.ToString(dr["MarcaEquipo"]),
                                ModeloEquipo = Convert.ToString(dr["ModeloEquipo"]),
                            };
                            lista.Add(procesoDTO);
                        }
                    }
                }
            }

            return lista;
        }
        public List<ProcesoDTO> Pagination(string texto, int pageSize, int currentPage, string orderBy, bool? sortOrder)
        {
            var lista = new List<ProcesoDTO>();
            using (var sqlCon = new SqlConnection(CadenaDeConexion))
            {
                sqlCon.Open();
                using (var sqlCmd = new SqlCommand("UpProcesoPagination", sqlCon))
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
                            var item = new ProcesoDTO
                            {
                                IdProceso = Convert.ToInt32(dr["IdProceso"]),
                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = dr["FechaFin"] != DBNull.Value ? Convert.ToDateTime(dr["FechaFin"]) : (DateTime?)null,
                                Estado = Convert.ToString(dr["Estado"]),
                                Prioridad = Convert.ToString(dr["Prioridad"]),

                                IdResponsable = Convert.ToInt32(dr["IdResponsable"]),
                                NombreResponsable = Convert.ToString(dr["NombreResponsable"]),

                                IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]),
                                NombreDepartamento = Convert.ToString(dr["NombreDepartamento"]),

                                IdEquipo = Convert.ToInt32(dr["IdEquipo"]),
                                MarcaEquipo = Convert.ToString(dr["MarcaEquipo"]),
                                ModeloEquipo = Convert.ToString(dr["ModeloEquipo"]),

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