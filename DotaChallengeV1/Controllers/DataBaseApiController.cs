using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotaChallengeV1.Models;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace DotaChallengeV1.Controllers
{
    public class DataBaseApiController : Controller
    {
        // GET: DataBaseApi
        public async Task<ActionResult> Index(int? id)
        {
            //ViewBag.SyncOrAsync = "Asynchronous";
            List<ChallengeGlobal.ChallengeDetail> list = await ChallengeGlobal.GetChallengeDetailsByIdAsync((int)id);
            if (id == 0)
                return Json(null, JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddChallengeDetailAsync (int challengeId, int userId, string heroName, int heroLvl, decimal score, int scoreTypeId, string item0, string item1, string item2, string item3, string item4, string item5)
        {
            ChallengeGlobal.ChallengeDetail ch = new ChallengeGlobal.ChallengeDetail()
            {
                ChallengeId = challengeId,
                UserId = userId,
                HeroName = heroName,
                HeroLevel = heroLvl,
                Score = score,
                ScoreTypeId = scoreTypeId,
                Item0 = item0,
                Item1 = item1,
                Item2 = item2,
                Item3 = item3,
                Item4 = item4,
                Item5 = item5
            };
            string result = await ChallengeGlobal.AddChallengeDetailAsync(ch);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}