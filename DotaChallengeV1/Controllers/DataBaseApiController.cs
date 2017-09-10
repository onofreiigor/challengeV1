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
    }
}