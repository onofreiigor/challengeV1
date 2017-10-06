using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Steam.Models.SteamCommunity;

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
            public string UserName { get; set; }
            public string AvatarPath { get; set; }
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

        public static async Task<List<ChallengeDetail>> GetChallengeDetailsByIdAsync(int id)
        {
            Challenges ch = new Challenges();
            ch.ChallengeDetails = new List<ChallengeDetail>();
            if (MvcApplication.SqlConn.State == System.Data.ConnectionState.Closed)
                MvcApplication.SqlConn.Open();
            SqlCommand comm = new SqlCommand("select * from challengedetail where challengeid = " + id + " order by score desc", MvcApplication.SqlConn);
            SqlDataReader reader = comm.ExecuteReader();

            //TO DO change to norm id
            ulong TmpSteamId = 76561198262139387;

            while (reader.Read())
            {
                SteamCommunityProfileModel rs = await MvcApplication.SteamUser.GetCommunityProfileAsync(TmpSteamId);
                string avatarPath = rs.AvatarFull.ToString();
                ch.ChallengeDetails.Add(new ChallengeDetail()
                {
                    DetailId = (int)reader["DetailId"],
                    ChallengeId = (int)reader["ChallengeId"],
                    UserId = (int)reader["UserId"],
                    AvatarPath = avatarPath,
                    HeroName = reader["HeroName"].ToString(),
                    HeroLevel = (int)reader["HeroLevel"],
                    Score = (decimal)reader["Score"],
                    Item0 = SafeGetString(reader, "Item0"),
                    ScoreTypeId = (int)reader["ScoreTypeId"],
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

        public static async Task<string> AddChallengeDetailAsync(ChallengeDetail ch)
        {
            List<ChallengeDetail> list = await GetChallengeDetailsByIdAsync(ch.ChallengeId);
            ChallengeDetail tmp = list.FirstOrDefault(el => el.UserId == ch.UserId && el.HeroName == ch.HeroName && el.HeroLevel == ch.HeroLevel);

            if (MvcApplication.SqlConn.State == System.Data.ConnectionState.Closed)
                MvcApplication.SqlConn.Open();
            if (tmp != null)
            {
                if (tmp.Score >= ch.Score)
                    return "low score";
                using (SqlCommand command = new SqlCommand(
                "update challengedetail set score = @score where detailid = @detailId and challengeid = @chId",
                MvcApplication.SqlConn))
                {
                    command.Parameters.AddWithValue("@detailId", tmp.DetailId);
                    command.Parameters.AddWithValue("@chId", tmp.ChallengeId);
                    command.Parameters.AddWithValue("@score", ch.Score);
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        return "error";
                }
            }
            else
            {
                using (SqlCommand command = new SqlCommand(
                    "insert into challengedetail values (@chId, @userId, @heroName, @heroLvl, @score, @scoreTypeId, @i0, @i1, @i2, @i3, @i4, @i5)",
                    MvcApplication.SqlConn))
                {
                    command.Parameters.AddWithValue("@chId", ch.ChallengeId);
                    command.Parameters.AddWithValue("@userId", ch.UserId);
                    command.Parameters.AddWithValue("@heroName", ch.HeroName);
                    command.Parameters.AddWithValue("@heroLvl", ch.HeroLevel);
                    command.Parameters.AddWithValue("@score", ch.Score);
                    command.Parameters.AddWithValue("@scoreTypeId", ch.ScoreTypeId);
                    command.Parameters.AddWithValue("@i0", (object)ch.Item0 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@i1", (object)ch.Item1 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@i2", (object)ch.Item2 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@i3", (object)ch.Item3 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@i4", (object)ch.Item4 ?? DBNull.Value);
                    command.Parameters.AddWithValue("@i5", (object)ch.Item5 ?? DBNull.Value);

                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        return "error";
                }
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