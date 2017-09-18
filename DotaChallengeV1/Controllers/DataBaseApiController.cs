using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotaChallengeV1.Models;
using System.Web.Script.Serialization;

namespace DotaChallengeV1.Controllers
{
    public class DataBaseApiController : Controller
    {
        // GET: DataBaseApi
        public ActionResult Index(int? id)
        {
            List<ChallengeGlobal.ChallengeDetail> list = ChallengeGlobal.GetChallengeDetailsById((int)id);
            if (id == 0)
                return Json(null, JsonRequestBehavior.AllowGet);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddChallengeDetail (int challengeId, int userId, int heroId, int heroLvl, float score, int scoreTypeId, int? item0, int? item1, int? item2, int? item3, int? item4, int? item5)
        {
            string result = ChallengeGlobal.AddChallengeDetail(
            challengeId, 
            userId,
            heroId,
            heroLvl,
            score,
            scoreTypeId,
            item0,
            item1,
            item2,
            item3,
            item4,
            item5);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}