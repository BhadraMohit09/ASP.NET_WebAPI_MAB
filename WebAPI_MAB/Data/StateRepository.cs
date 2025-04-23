using System.Data;
using System.Data.SqlClient;
using WebAPI_MAB.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_MAB.Data
{ 
    public class StateRepository
    {
        private readonly string _connectionString;

        public StateRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<StateModel> SelectAll()
        {
            var states = new List<StateModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_State_SelectAll", conn)
                {

                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    states.Add(new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"].ToString(),
                    });
                }
            }
            return states;
        }
        #endregion

        #region SelectByID
        public StateModel SelectByPK(int stateID)
        {
            StateModel state = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_State_SelectById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StateID", stateID);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    state = new StateModel
                    {
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        StateName = reader["StateName"].ToString(),
                        StateCode = reader["StateCode"]?.ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["ModifiedDate"])
                    };
                }
            }
            return state;
        }
        #endregion

        #region Delete Data
        public bool Delete(int stateID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_State_Delete", conn)

                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StateID", stateID);

                conn.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region Insert State Data
        public bool Insert(StateModel state)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_State_Insert", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StateID", state.StateID);
                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);
                cmd.Parameters.AddWithValue("@CityName", state.StateName);
                cmd.Parameters.AddWithValue("@StateCode", state.StateCode);

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update State Data
        public bool Update(StateModel state)

        {

            using (SqlConnection conn = new SqlConnection(_connectionString))

            {

                SqlCommand cmd = new SqlCommand("PR_State_Update", conn)

                {

                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StateID", state.StateID);

                cmd.Parameters.AddWithValue("@CountryID", state.CountryID);

                cmd.Parameters.AddWithValue("@StateName", state.StateName);

                cmd.Parameters.AddWithValue("@StateCode", state.StateCode); // New parameter


                conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;

            }

        }

        #endregion
    }
}
