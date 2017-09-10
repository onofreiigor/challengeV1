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
            public int MatchId { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string HerroName { get; set; }
            public int HeroLevel { get; set; }
            public int Score { get; set; }
            public int GameDuration { get; set; }
            public int InventoryId { get; set; }
        }

        public static List<ChallengeDetail> GetChallengeDetailsById(int id)
        {
            Challenges ch = new Challenges();
            ch.ChallengeDetails = new List<ChallengeDetail>();
            SqlCommand comm = new SqlCommand("select * from challengedetail where challengeid = " + id, MvcApplication.SqlConn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                ch.ChallengeDetails.Add(new ChallengeDetail()
                {
                    DetailId = reader.GetInt32(0),
                    ChallengeId = reader.GetInt32(1),
                    MatchId = reader.GetInt32(2),
                    UserId = reader.GetInt32(3),
                    UserName = reader.GetString(4),
                    HerroName = reader.GetString(5),
                    HeroLevel = reader.GetInt32(6),
                    Score = reader.GetInt32(7),
                    GameDuration = reader.GetInt32(8),
                    InventoryId = reader.GetInt32(9)
                });
            }
            reader.Close();
            return ch.ChallengeDetails;
        }
    }
}