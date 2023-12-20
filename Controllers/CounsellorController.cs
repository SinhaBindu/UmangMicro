using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SubSonic.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class CounsellorController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        JsonResponseData response = new JsonResponseData();
        int result = 0; bool CheckStatus = false;
        string MSG = string.Empty;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CaseHDetail(string RowId = "", string CaseID = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrWhiteSpace(CaseID))
                {
                    ds = SP_Model.GetSP_CaseHistoryDetails(RowId, CaseID);
                    if (ds.Tables.Count > 0)
                    {
                        return View(ds);
                    }
                }
                return View(ds);
            }
            catch (Exception)
            {
                return View(ds);
            }
        }
        public ActionResult GetRIASEClList(string CaseID = "")
        {
            try
            {
                var items = SP_Model.GetSP_RIASECList(CaseID);
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
        //Result of Psychometric test
        [HttpPost]
        public ActionResult RIASECldata(string Para = "")
        {
            Para = "1";
            DataSet ds = SP_Model.GetSP_RIASECGuidData(Para);
            if (ds.Tables.Count > 0)
            {
                return PartialView("_RTOCData", ds);
            }
            return PartialView("_RTOCData", ds);
        }
        //Student Search
        public ActionResult GetStudentlList(string Para = "", string SearchBy = "", string DOB = "")
        {
            try
            {
                var items = SP_Model.GetSP_StudentList(Para, SearchBy, DOB);
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
        public ActionResult GetCaseHistoryCount(string RowId = "", string CaseID = "")
        {
            try
            {
                var items = SP_Model.GetSP_CaseHistoryCount(RowId, CaseID);
                if (items != null && items.Rows.Count > 0)
                {
                    var data = JsonConvert.SerializeObject(items.Rows[0]["NoofCaseHistory"]);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetSearchByCD()
        {
            try
            {
                DataSet ds = SP_Model.GetSP_SearchBYCD();
                if (ds.Tables.Count > 0)
                {
                    var data = JsonConvert.SerializeObject(ds);
                    return Json(new { IsSuccess = true, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetCoursesDetail(SearchModel DList)
        {
            try
            {

                var resdata = this.Request.Unvalidated.Form["DList"];
                bool IsCheck = false;
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataSet ds = new DataSet();
                if (resdata != null)
                {
                    var model = JsonConvert.DeserializeObject<SearchModel>(resdata);
                    ds = SP_Model.GetComdSearchlist(model);

                    if (ds.Tables.Count > 0)
                    {
                        IsCheck = true;
                        if (model.Paratbl == "1")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dt = ds.Tables[0];
                            }
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                dt1 = ds.Tables[1];
                            }
                        }
                        if (model.Paratbl == "3")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dt2 = ds.Tables[0];
                            }
                        }
                        if (model.Paratbl == "4")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dt3 = ds.Tables[0];
                            }
                        }
                    }
                }
                var htmlCD = ConvertViewToString("_CD", dt);
                var htmlST = ConvertViewToString("_SkillTrain", dt1);
                var htmlSSP = ConvertViewToString("_Scholarship", dt2);
                var htmlSM = ConvertViewToString("_Scheme", dt3);
                //var html3 = ConvertViewToString("_UserDetailData", tbllist);
                var res = Json(new { IsSuccess = IsCheck, html_CD = htmlCD, html_ST = htmlST, html_SM = htmlSM, html_SSP = htmlSSP }, JsonRequestBehavior.AllowGet);
                res.MaxJsonLength = int.MaxValue;
                return res;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "There was a communication error." }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult GetClassno(string CaseId)
        {
            try
            {
                var items = db.tbl_Registration.Where(x => x.CaseID == CaseId).FirstOrDefault().ClassId;
                if (items != null)
                {
                    //var data = JsonConvert.SerializeObject(items);
                    return Json(new { IsSuccess = true, res = items }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult CaseHistory(int Id = 0, int Para = 0, string CaseID = "", string DOB = "")
        {
            CHModel model = new CHModel();
            if (CommonModel.GetUserRoleLogin() == MvcApplication.CUser.Role)
            {
                model.TypeCounsellor = MvcApplication.CUser.RoleId;
            }
            model.StratTime = DateTime.Now;
            if (Para > 0)
            {
                model.TypeCase = "2";
                model.CaseID = CaseID;
                model.Searchtxt = CaseID;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CaseHistory(CHModel model)
        {
            var results = 0;
            UM_DBEntities db_ = new UM_DBEntities();
            JsonResponseData response = new JsonResponseData();
            try
            {
                var resPHYlist = this.Request.Unvalidated.Form["CH_PHY_model"];
                var resCDlist = this.Request.Unvalidated.Form["CH_CD_model"];
                var resSTlist = this.Request.Unvalidated.Form["CH_SkillT_model"];
                var resSSPlist = this.Request.Unvalidated.Form["CH_SSP_model"];
                var resSMlist = this.Request.Unvalidated.Form["CH_SM_model"];
                var cdate = string.Format("{0:M/dd/yyyy}",
                               DateTime.Now.Date);
                var getcaseid =
                          //(from dt in
                          (from ac in db_.tbl_CaseHistory.ToList()
                           where ac.CaseID == model.CaseID
                           select new
                           {
                               CreatedOndmy = string.Format("{0:M/dd/yyyy}",
                               ac.CreatedOn),
                               ac.CaseID
                           }).ToList();
                //select string.Format("{0:M/dd/yyyy}", dt.)).ToList();
                //DateTime ruleData = Convert.ToDateTime(DateTime.Now.Date).Date;
                //if (ModelState.IsValid)
                //{
                //if (db_.tbl_CaseHistory.Any(x => x.CaseID == model.CaseID && ((x.CaseDate.Value) == Nullable<System.DateTime>(cdate)) ))
                if (getcaseid.Any(x => x.CreatedOndmy == cdate))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Case History Detail.<br /> <span> Case ID : <strong>" + model.CaseID + " </strong>  </span>", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }
                if (db_.tbl_CaseHistory.Any(x => x.CaseID == model.CaseID && x.DOC.Value == model.DOC.Value))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Case History Detail.<br /> <span> Case ID : <strong>" + model.CaseID + " </strong>  </span>", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                }

                var tbl = model.Id != 0 ? db.tbl_CaseHistory.Find(model.Id) : new tbl_CaseHistory();
                if (tbl != null)
                {
                    var regid = db.tbl_Registration.Where(x => x.CaseID == model.CaseID)?.FirstOrDefault();
                    tbl.GudID = Guid.NewGuid();
                    tbl.Reg_Id_fk = regid.ID;
                    tbl.RIASECTest_Id_fk = model.RIASECTest_Id_fk;
                    tbl.DistrictId = Convert.ToInt32(regid.DistrictId);
                    tbl.BlockId = Convert.ToInt32(regid.BlockId);
                    tbl.SchoolId = Convert.ToInt32(regid.SchoolId);

                    //tbl.CountdownStart = model.CountdownStart;
                    //tbl.CountdownEnd = model.CountdownEnd;
                    tbl.DOC = model.DOC;
                    tbl.CaseID = !(string.IsNullOrWhiteSpace(model.CaseID)) ? model.CaseID.Trim() : model.CaseID;
                    tbl.ClassId = Convert.ToInt32(model.ClassId);
                    tbl.TypeCounsellor = !(string.IsNullOrWhiteSpace(model.TypeCounsellor)) ? model.TypeCounsellor.Trim() : model.TypeCounsellor;
                    tbl.TypeQuery = model.TypeQuery;
                    tbl.KeyWords = model.KeyWords;
                    if (!(string.IsNullOrWhiteSpace(model.ClassId)) && (model.ClassId == "11" || model.ClassId == "12"))
                    {
                        tbl.Study12th = model.Study12th;
                    }
                    else if (!(string.IsNullOrWhiteSpace(model.ClassId)) && (model.ClassId == "10"))
                    {
                        tbl.Study10th = model.Study10th;
                    }
                    tbl.Subject = model.Subject;
                    if (model.TypeQuery == "1" || model.TypeQuery == "2")
                    {
                        tbl.IsPsychometric = model.IsPsychometric;
                        tbl.Psychometric = model.Psychometric;
                    }
                    tbl.Counselling = model.Counselling;

                    tbl.Recommendation = model.Recommendation;
                    tbl.AreasImprovement = model.AreasImprovement;
                    tbl.IsFollow = model.IsFollow;
                    if (model.IsFollow == "1")
                    {
                        tbl.FM = model.FM;
                        tbl.FY = model.FY;
                    }
                    tbl.IsGoalClear = model.IsGoalClear;
                    tbl.IsActive = true;

                    if (model.Id == 0)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                        }
                        tbl.CaseDate = DateTime.Now.Date;
                        tbl.CreatedOn = DateTime.Now;
                        tbl.StratTime = model.StratTime;
                        tbl.EndTime = DateTime.Now;
                        tbl.FormSubmitTime = ((tbl.EndTime.Value - tbl.StratTime.Value).TotalSeconds.ToString());
                        db.tbl_CaseHistory.Add(tbl);
                    }
                    else
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            tbl.UpdatedBy = MvcApplication.CUser.Id;
                        }
                        tbl.UpdatedOn = DateTime.Now;
                    }
                    results = db.SaveChanges();
                }
                if (results > 0)
                {
                    if (resPHYlist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<CH_Psychometric_Model>>(resPHYlist);
                        if (mlist.Count() > 0)
                        {
                            tbl_CH_Psychometric tbl_Psy;
                            List<tbl_CH_Psychometric> tbl_list = new List<tbl_CH_Psychometric>();
                            foreach (var m in mlist)
                            {
                                tbl_Psy = new tbl_CH_Psychometric()
                                {
                                    RIASECTest_Id_fk = model.RIASECTest_Id_fk,
                                    CaseHistoryId = tbl.Id.ToString(),
                                    CaseId = tbl.CaseID,
                                    RIASEC_Guided_Id = m.RIASEC_Guided_Id,
                                    PsychometricTestId = m.PsychometricTestId,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                                };
                                tbl_list.Add(tbl_Psy);
                            }
                            db.tbl_CH_Psychometric.AddRange(tbl_list);
                            results = db.SaveChanges();

                        }

                    }
                    if (resCDlist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<CH_CourseD_Model>>(resCDlist);
                        if (mlist.Count() > 0)
                        {
                            tbl_CH_CourseD tbl_;
                            List<tbl_CH_CourseD> tbl_list = new List<tbl_CH_CourseD>();
                            foreach (var m in mlist)
                            {
                                tbl_ = new tbl_CH_CourseD()
                                {
                                    CaseHistoryId = tbl.Id.ToString(),
                                    CaseId = tbl.CaseID,
                                    CourseD_Id = m.CourseD_Id,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                                };
                                tbl_list.Add(tbl_);
                            }
                            db.tbl_CH_CourseD.AddRange(tbl_list);
                            results = db.SaveChanges();
                        }
                    }
                    if (resSTlist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<CH_SkillT_Model>>(resSTlist);
                        if (mlist.Count() > 0)
                        {
                            tbl_CH_SkillT tbl_;
                            List<tbl_CH_SkillT> tbl_list = new List<tbl_CH_SkillT>();
                            foreach (var m in mlist)
                            {
                                tbl_ = new tbl_CH_SkillT()
                                {
                                    CaseHistoryId = tbl.Id.ToString(),
                                    CaseId = tbl.CaseID,
                                    SkillT_Id = m.SkillT_Id,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                                };
                                tbl_list.Add(tbl_);
                            }
                            db.tbl_CH_SkillT.AddRange(tbl_list);
                            results = db.SaveChanges();
                        }
                    }
                    if (resSSPlist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<CH_Scholarship_Model>>(resSSPlist);
                        if (mlist.Count() > 0)
                        {
                            tbl_CH_Scholarship tbl_;
                            List<tbl_CH_Scholarship> tbl_list = new List<tbl_CH_Scholarship>();
                            foreach (var m in mlist)
                            {
                                tbl_ = new tbl_CH_Scholarship()
                                {
                                    CaseHistoryId = tbl.Id.ToString(),
                                    CaseId = tbl.CaseID,
                                    Scholarship_Id = m.Scholarship_Id,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                                };
                                tbl_list.Add(tbl_);
                            }
                            db.tbl_CH_Scholarship.AddRange(tbl_list);
                            results = db.SaveChanges();
                        }
                    }
                    if (resSMlist != null)
                    {
                        var mlist = JsonConvert.DeserializeObject<List<CH_Scheme_Model>>(resSMlist);
                        if (mlist.Count() > 0)
                        {
                            tbl_CH_Scheme tbl_;
                            List<tbl_CH_Scheme> tbl_list = new List<tbl_CH_Scheme>();
                            foreach (var m in mlist)
                            {
                                tbl_ = new tbl_CH_Scheme()
                                {
                                    CaseHistoryId = tbl.Id.ToString(),
                                    CaseId = tbl.CaseID,
                                    Scheme_Id = m.Scheme_Id,
                                    CreatedBy = MvcApplication.CUser.Id,
                                    CreatedOn = DateTime.Now,
                                    IsActive = true
                                };
                                tbl_list.Add(tbl_);
                            }
                            db.tbl_CH_Scheme.AddRange(tbl_list);
                            results = db.SaveChanges();
                        }
                    }

                    response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, Case History Details Submitted successfully ! \r\n Your <br /> <span> Case ID : <strong>" + tbl.CaseID + " </strong> </span>", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;

                }
                //response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully case history registered! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + tbl.CaseID + " </strong> </span>", Data = null };
                //var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                //resResponse1.MaxJsonLength = int.MaxValue;
                //return resResponse1;
                //}
                //else
                //{
                //    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                //    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                //    resResponse1.MaxJsonLength = int.MaxValue;
                //    return resResponse1;
                //};
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse1.MaxJsonLength = int.MaxValue;
                return resResponse1;
            }
            return View();
        }
        public ActionResult CaseHistoryList()
        {
            return View();
        }
        public ActionResult GetCaseHList(string Para = "", string SearchBy = "", string DOB = "", string Sdt = "", string Edt = "", string DistrictId = "", string BlockId = "")
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                BlockId = BlockId == "0" ? string.Empty : BlockId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                BlockId = (string.IsNullOrWhiteSpace(BlockId)) ? "ALL" : BlockId;
                var items = SP_Model.GetSP_CaseHList(Para, SearchBy, DOB, Sdt, Edt, DistrictId.ToUpper(), BlockId.ToUpper());
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_CaseHList", items);
                    return Json(new { IsSuccess = true, res = html }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
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
    }
}