using System.Data;
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

                            sqlCmd.Parameters.Add("@IdProceso", SqlDbType.Int).Value = enProceso.IdProceso;

                            if (enProceso.IdResponsable.HasValue)
                                sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = enProceso.IdResponsable;
                            else
                                sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.IdDepartamento.HasValue)
                                sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = enProceso.IdDepartamento;
                            else
                                sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.IdEquipo.HasValue)
                                sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = enProceso.IdEquipo;
                            else
                                sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.FechaInicio.HasValue)
                                sqlCmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = enProceso.FechaInicio;
                            else
                                sqlCmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = DBNull.Value;

                            if (enProceso.FechaFin.HasValue)
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = enProceso.FechaFin;
                            else
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enProceso.Estado))
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = enProceso.Estado;

                            if (string.IsNullOrWhiteSpace(enProceso.Prioridad))
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = enProceso.Prioridad;

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

                            if (dr["IdResponsable"] != DBNull.Value)
                                beProceso.IdResponsable = Convert.ToInt32(dr["IdResponsable"]);

                            if (dr["IdDepartamento"] != DBNull.Value)
                                beProceso.IdDepartamento = Convert.ToInt32(dr["IdDepartamento"]);

                            if (dr["IdEquipo"] != DBNull.Value)
                                beProceso.IdEquipo = Convert.ToInt32(dr["IdEquipo"]);

                            if (dr["FechaInicio"] != DBNull.Value)
                                beProceso.FechaInicio = Convert.ToDateTime(dr["FechaInicio"]);

                            if (dr["FechaFin"] != DBNull.Value)
                                beProceso.FechaFin = Convert.ToDateTime(dr["FechaFin"]);

                            if (dr["Estado"] != DBNull.Value)
                                beProceso.Estado = Convert.ToString(dr["Estado"]);

                            if (dr["Prioridad"] != DBNull.Value)
                                beProceso.Prioridad = Convert.ToString(dr["Prioridad"]);
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

                            if (enProceso.IdResponsable.HasValue)
                                sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = enProceso.IdResponsable;
                            else
                                sqlCmd.Parameters.Add("@IdResponsable", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.IdDepartamento.HasValue)
                                sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = enProceso.IdDepartamento;
                            else
                                sqlCmd.Parameters.Add("@IdDepartamento", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.IdEquipo.HasValue)
                                sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = enProceso.IdEquipo;
                            else
                                sqlCmd.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = DBNull.Value;

                            if (enProceso.FechaInicio.HasValue)
                                sqlCmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = enProceso.FechaInicio;
                            else
                                sqlCmd.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = DBNull.Value;

                            if (enProceso.FechaFin.HasValue)
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = enProceso.FechaFin;
                            else
                                sqlCmd.Parameters.Add("@FechaFin", SqlDbType.Date).Value = DBNull.Value;

                            if (string.IsNullOrWhiteSpace(enProceso.Estado))
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = enProceso.Estado;

                            if (string.IsNullOrWhiteSpace(enProceso.Prioridad))
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = DBNull.Value;
                            else
                                sqlCmd.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = enProceso.Prioridad;

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

        #endregion
    }
}