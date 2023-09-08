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
using UmangMicro.Controllers;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class MasterController : BaseController
    {
        UM_DBEntities db = new UM_DBEntities();
        JsonResponseData response = new JsonResponseData();
        // int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;

        #region Course Details Create,Update
        public ActionResult CourseD(int Id = 0)
        {
            CoursesDModel model=new CoursesDModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CourseD(CoursesDModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var tbl = model.ID != 0 ? db.tbl_CoursesDetail.Find(model.ID) : new tbl_CoursesDetail();
            if (tbl != null)
            {
                tbl.NameCourseEng = model.NameCourseEng;
                tbl.NameCourseHindi = model.NameCourseHindi;
                tbl.CourseTypeEng = model.CourseTypeEng;
                tbl.CourseTypeHindi = model.CourseTypeHindi;
                tbl.JobOpportunityEng = model.JobOpportunityEng;
                tbl.JobOpportunityHindi = model.JobOpportunityHindi;
                tbl.CourseDurationEng = model.CourseDurationEng;
                tbl.CourseDurationHindi = model.CourseDurationHindi;
                tbl.ShortDescriptionCourseEng = model.ShortDescriptionCourseEng;
                tbl.ShortDescriptionCourseHindi = model.ShortDescriptionCourseHindi;
                tbl.EligibilityEng = model.EligibilityEng;
                tbl.EligibilityHindi = model.EligibilityHindi;
                tbl.MarksCriteriaEng = model.MarksCriteriaEng;
                tbl.MarksCriteriaHindi = model.MarksCriteriaHindi;
                tbl.AdmissionProcessEng = model.AdmissionProcessEng;
                tbl.AdmissionProcessHindi = model.AdmissionProcessHindi;
                tbl.MediumInstructionEng = model.MediumInstructionEng;
                tbl.MediumInstructionHindi = model.MediumInstructionHindi;
                tbl.HostelAvailabilityEng = model.HostelAvailabilityEng;
                tbl.HostelAvailabilityHindi = model.HostelAvailabilityHindi;
                tbl.AvailableScholarshipEng = model.AvailableScholarshipEng;
                tbl.AvailableScholarshipHindi = model.AvailableScholarshipHindi;
                tbl.CategoryEng = model.CategoryEng;
                tbl.CategoryHindi = model.CategoryHindi;
                tbl.CategoryOtherEng = model.CategoryOtherEng;
                tbl.CategoryOtherHindi = model.CategoryOtherHindi;
                tbl.College_Unvty_InstEng = model.College_Unvty_InstEng;
                tbl.College_Unvty_InstHindi = model.College_Unvty_InstHindi;
                tbl.FeeStructureEng = model.FeeStructureEng;
                tbl.FeeStructureHindi = model.FeeStructureHindi;
                tbl.StatusInstitutionEng = model.StatusInstitutionEng;
                tbl.StatusInstitutionHindi = model.StatusInstitutionHindi;
                tbl.DistrictEng = model.DistrictEng;
                tbl.DistrictHindi = model.DistrictHindi;
                tbl.IsActive = true;
                if (model.ID == 0)
                {
                    tbl.CreatedBy = User.Identity.Name;
                    tbl.CreatedDt = DateTime.Now;
                    db.tbl_CoursesDetail.Add(tbl);
                }
                else
                {
                    tbl.UpdatedBy = User.Identity.Name;
                    tbl.UpdatedDt = DateTime.Now;
                }
                var res = db.SaveChanges();
                if (res > 0)
                {
                    Success("Employee added successfully !", true);
                    return RedirectToAction("Add", new { id = 0 });
                }
            }

            return View(model);
        }
        #endregion

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
        public ActionResult GetDistrictList(int StateId = 20)
        {
            try
            {
                var items = CommonModel.GetDistrict(false, StateId);
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
                var items = CommonModel.GetBlock(false, DistrictId);
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
                var items = CommonModel.GetCluster(false, BlockId);
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