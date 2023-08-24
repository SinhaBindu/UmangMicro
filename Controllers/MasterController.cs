using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Models;
using UmangMicro.Manager;
using Newtonsoft.Json;
using static UmangMicro.Manager.Enums;
using System.IO;
using System.Data;
using System;
using System.EnterpriseServices;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class MasterController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        JsonResponseData response = new JsonResponseData();
        // int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;

        #region
        public ActionResult GetStateList()
        {
            try
            {
                var items = CommonModel.GetState();
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetDistrictList(int StateId= 20)
        {
            try
            {
                var items = CommonModel.GetDistrict(false,StateId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBlockList(int DistrictId)
        {
            try
            {
                var items = CommonModel.GetBlock(false,DistrictId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetClusterList(int BlockId)
        {
            try
            {
                var items = CommonModel.GetCluster(false,BlockId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet); 
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Master
        public ActionResult UserDetaillist()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }
        public ActionResult GetUserDetailData()
        {
            try
            {
                bool IsCheck = false;
                var tbllist = CommonModel.GetSPCutUserlist();
                if (tbllist != null)
                {
                    IsCheck = true;
                }
                var html = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }

        //public JsonResult GetTypeParticipantForWeeklyCollation(string PerformID, string PlanID, string ActivityID, string TypeofActivityID)
        //{
        //    try
        //    {
        //        //var data = CommonModel.GetParticipant(TypeofActivityID);
        //        DataTable dt = CommonModel.SPGetParticipantTypeForWeeklyCollation(PerformID, PlanID, ActivityID, TypeofActivityID);
        //        if (dt.Rows.Count > 0)
        //        {
        //            var data = JsonConvert.SerializeObject(dt);
        //            return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);  
        //        }
        //        return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet); 
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        #endregion
    }
}