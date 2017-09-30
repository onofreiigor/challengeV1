using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DotaChallengeV1.Models
{
    public class ChallengeGlobal
    {
        public class Challenges
        {
            public int ChallengeId { get; set; }
            public string ChallengeName { get; set; }
            public string ChallengeDesc { get; set; }
            public int NextCh { get; set; }
            public int PrevCh { get; set; }
            public List<ChallengeDetail> ChallengeDetails { get; set; }
        }

        public class ChallengeDetail
        {
            public int DetailId { get; set; }
            public int ChallengeId { get; set; }
            public int UserId { get; set; }
            public string HeroName { get; set; }
            public int? HeroLevel { get; set; }
            public decimal Score { get; set; }
            public int? ScoreTypeId { get; set; }
            public string Item0 { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public string Item3 { get; set; }
            public string Item4 { get; set; }
            public string Item5 { get; set; }
        }

        public static List<ChallengeDetail> GetChallengeDetailsById(int id)
        {
            Challenges ch = new Challenges();
            ch.ChallengeDetails = new List<ChallengeDetail>();
            if (MvcApplication.SqlConn.State == System.Data.ConnectionState.Closed)
                MvcApplication.SqlConn.Open();
            SqlCommand comm = new SqlCommand("select * from challengedetail where challengeid = " + id, MvcApplication.SqlConn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ch.ChallengeDetails.Add(new ChallengeDetail()
                {
                    DetailId = (int)reader["DetailId"],
                    ChallengeId = (int)reader["ChallengeId"],
                    UserId = (int)reader["UserId"],
                    HeroName = reader["HeroName"].ToString(),
                    HeroLevel = (int)reader["HeroLevel"],
                    Score = (decimal)reader["Score"],
                    ScoreTypeId = (int)reader["ScoreTypeId"],
                    Item0 = SafeGetString(reader, "Item0"),
                    Item1 = SafeGetString(reader, "Item1"),
                    Item2 = SafeGetString(reader, "Item2"),
                    Item3 = SafeGetString(reader, "Item3"),
                    Item4 = SafeGetString(reader, "Item4"),
                    Item5 = SafeGetString(reader, "Item5")
                });
            }
            reader.Close();
            MvcApplication.SqlConn.Close();
            return ch.ChallengeDetails;
        }

        public static string AddChallengeDetail(
            int challengeId,
            int userId,
            string heroName,
            int? heroLvl,
            float score,
            int scoreTypeId,
            string item0,
            string item1,
            string item2,
            string item3,
            string item4,
            string item5
            )
        {
            if (MvcApplication.SqlConn.State == System.Data.ConnectionState.Closed)
                MvcApplication.SqlConn.Open();
            ChallengeDetail chd = new ChallengeDetail();
            using (SqlCommand command = new SqlCommand(
                "insert into challengedetail values (@chId, @userId, @heroName, @heroLvl, @score, @scoreTypeId, @i0, @i1, @i2, @i3, @i4, @i5)",
                MvcApplication.SqlConn))
            {
                command.Parameters.AddWithValue("@chId", challengeId);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@heroName", heroName);
                command.Parameters.AddWithValue("@heroLvl", heroLvl);
                command.Parameters.AddWithValue("@score", score);
                command.Parameters.AddWithValue("@scoreTypeId", scoreTypeId);
                command.Parameters.AddWithValue("@i0", (object)item0 ?? DBNull.Value);
                command.Parameters.AddWithValue("@i1", (object)item1 ?? DBNull.Value);
                command.Parameters.AddWithValue("@i2", (object)item2 ?? DBNull.Value);
                command.Parameters.AddWithValue("@i3", (object)item3 ?? DBNull.Value);
                command.Parameters.AddWithValue("@i4", (object)item4 ?? DBNull.Value);
                command.Parameters.AddWithValue("@i5", (object)item5 ?? DBNull.Value);

                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    return "error";
            }
            MvcApplication.SqlConn.Close();
            return "succes";
        }

        public static int? SafeGetInt(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return (int)reader[colIndex];
            return null;
        }

        public static string SafeGetString(SqlDataReader reader, string colName)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(colName)))
                return reader[colName].ToString();
            return null;
        }
    }
}