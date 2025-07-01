using System.Data;
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
        #endregion
    }
}