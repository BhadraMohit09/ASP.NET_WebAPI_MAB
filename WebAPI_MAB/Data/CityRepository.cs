﻿using System.Data;
using System.Data.SqlClient;
using WebAPI_MAB.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_MAB.Data
{
    public class CityRepository
    {
        private readonly string _connectionString;

        public CityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        #region SelectAll
        public IEnumerable<CityModel> SelectAll()
        {
            var cities = new List<CityModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_SelectAll", conn)
                {

                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString()
                    });
                }
            }
            return cities;
        }
        #endregion

        #region SelectByID
        public CityModel SelectByPK(int cityID)

        {
            CityModel city = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_SelectByPK", conn)

                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CityID", cityID);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    city = new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        StateID = Convert.ToInt32(reader["StateID"]),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString()
                    };
                }
            }
            return city;
        }
        #endregion

        #region DeleteData
        public bool Delete(int cityID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_Delete", conn)

                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CityID", cityID);

                conn.Open();
                int rowAffected = cmd.ExecuteNonQuery();
                return rowAffected > 0;
            }
        }
        #endregion

        #region InsertCity
        public bool Insert(CityModel city)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_Insert", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@StateID", city.StateID);
                cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);

                con.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateCity
        public bool Update(CityModel city)

        {

            using (SqlConnection conn = new SqlConnection(_connectionString))

            {

                SqlCommand cmd = new SqlCommand("PR_LOC_City_Update", conn)

                {

                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CityID", city.CityID);

                cmd.Parameters.AddWithValue("@StateID", city.StateID);

                cmd.Parameters.AddWithValue("@CountryID", city.CountryID); // New parameter

                cmd.Parameters.AddWithValue("@CityName", city.CityName);

                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);


                conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;

            }

        }

        #endregion
    }
}
