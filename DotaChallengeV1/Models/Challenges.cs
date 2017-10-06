using System;
using System.Collections.Generic;
using System.Data;
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
            public int? HeroId { get; set; }
            public int? HeroLevel { get; set; }
            public int Score { get; set; }
            public int? ScoreTypeId { get; set; }
            public int? Item0 { get; set; }
            public int? Item1 { get; set; }
            public int? Item2 { get; set; }
            public int? Item3 { get; set; }
            public int? Item4 { get; set; }
            public int? Item5 { get; set; }
        }

        public static List<ChallengeDetail> GetChallengeDetailsById(int id)
        {
            Challenges ch = new Challenges();
            ch.ChallengeDetails = new List<ChallengeDetail>();
            if (MvcApplication.SqlConn.State == ConnectionState.Closed)
                MvcApplication.SqlConn.Open();
            SqlCommand comm = new SqlCommand("select * from challengedetail where challengeid = " + id, MvcApplication.SqlConn);
            SqlDataReader reader = null;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ch.ChallengeDetails.Add(new ChallengeDetail()
                {
                    DetailId = reader.GetInt32(0),
                    ChallengeId = reader.GetInt32(1),
                    UserId = reader.GetInt32(2),
                    HeroId = SafeGetInt32(reader, 3),
                    HeroLevel = reader.GetInt32(4),
                    Score = reader.GetInt32(5),
                    ScoreTypeId = SafeGetInt32(reader, 6),
                    Item0 = SafeGetInt32(reader, 7),
                    Item1 = SafeGetInt32(reader, 8),
                    Item2 = SafeGetInt32(reader, 9),
                    Item3 = SafeGetInt32(reader, 10),
                    Item4 = SafeGetInt32(reader, 11),
                    Item5 = SafeGetInt32(reader, 12)
                });
            }
            reader.Close();
            MvcApplication.SqlConn.Close();
            return ch.ChallengeDetails;
        }

        public static string AddChallengeDetail(
            int challengeId,
            int userId,
            int heroId,
            int? heroLvl,
            float score,
            int scoreTypeId,
            int? item0,
            int? item1,
            int? item2,
            int? item3,
            int? item4,
            int? item5
            )
        {
            MvcApplication.SqlConn.Close();
            MvcApplication.SqlConn.Open();
            ChallengeDetail chd = new ChallengeDetail();
            using (SqlCommand command = new SqlCommand(
                "insert into challengedetail values (@chId, @userId, @heroId, @heroLvl, @score, @scoreTypeId, @i0, @i1, @i2, @i3, @i4, @i5)",
                MvcApplication.SqlConn))
            {
                command.Parameters.AddWithValue("@chId", challengeId);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@heroId", heroId);
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

        public static int? SafeGetInt32(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            return null;
        }
    }
}