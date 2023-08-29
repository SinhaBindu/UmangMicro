using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using UmangMicro.Models;
using UmangMicro.Manager;
using static UmangMicro.Manager.Enums;
using Antlr.Runtime.Misc;
using KGBV_JH.Controllers;

namespace UmangMicro.Controllers
{
    public class CandidateController : BaseController
    {
        UM_DBEntities db = new UM_DBEntities();
        int results = 0; int results2nd = 0;
        // GET: Candidate
        #region 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetIndex(string FD="", string TD = "")
        {
            DataTable tbllist = new DataTable();
            var html = "";
            try
            {
                tbllist = SP_Model.SPCandRegList(FD, TD);
                bool IsCheck = false;
                if (tbllist.Rows.Count > 0)
                {
                    IsCheck = true;
                    html = ConvertViewToString("_Index", tbllist);
                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
                else
                {
                    var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
                    res.MaxJsonLength = int.MaxValue;
                    return res;
                }
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
            }
        }
        public ActionResult Reg(int LangType = 1, int Id = 0)
        {
            RegModel model = new RegModel();
            model.LanguangeType = LangType;
            if (Id > 0)
            {
                var tbl = db.tbl_Registration.Find(Id);
                if (tbl != null)
                {
                    model.ID = tbl.ID;
                    model.CaseID = tbl.CaseID;
                    model.RegDate = tbl.RegDate;
                    model.Name = tbl.Name;
                    model.MotherName = tbl.MotherName;
                    model.FatherName = tbl.FatherName;
                    model.StateId = tbl.StateId;
                    model.DistrictId = tbl.DistrictId;
                    model.BlockId = tbl.BlockId;
                    model.ClusterId = tbl.ClusterId;
                    model.Village = tbl.Village;
                    model.Visited = tbl.Visited;
                    model.DOB = tbl.DOB;
                    model.Age = tbl.Age;
                    model.MobileNo = tbl.MobileNo;
                    model.IsSkillTraining = tbl.IsSkillTraining.Value;
                    model.IsMarriage = tbl.IsMarriage.Value;
                    model.IsStudy =tbl.IsStudy.Value;
                    model.SocialClass = tbl.SocialClass;
                    model.TillStudied = tbl.TillStudied;
                    model.IsWork = tbl.IsWork.Value;
                    model.Reason = tbl.Reason;
                    model.IsPsychometric = tbl.IsPsychometric.Value;
                    model.PsyYes_Result = tbl.PsyYes_Result;
                    model.Advice = tbl.Advice;
                    model.IsFollowUp = tbl.IsFollowUp;
                    model.FollowUp = tbl.FollowUp;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Reg(RegModel model)
        {
            JsonResponseData response = new JsonResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    var tbl = model.ID != 0 ? db.tbl_Registration.Find(model.ID) : new tbl_Registration();
                    if (tbl != null)
                    {
                        tbl.RegDate = model.RegDate;
                        tbl.Name = !(string.IsNullOrWhiteSpace(model.Name)) ? model.Name.Trim() : model.Name;
                        tbl.MotherName = !(string.IsNullOrWhiteSpace(model.MotherName)) ? model.MotherName.Trim() : model.MotherName;
                        tbl.FatherName = !(string.IsNullOrWhiteSpace(model.FatherName)) ? model.FatherName.Trim() : model.FatherName;
                        var sid = Convert.ToInt32(eState.Jharkhand);
                        tbl.StateId = db.State_Mast.Where(x => x.ID == sid).FirstOrDefault().ID;
                        tbl.DistrictId = model.DistrictId;
                        tbl.BlockId = model.BlockId;
                        tbl.ClusterId = model.ClusterId;
                        tbl.Village = !(string.IsNullOrWhiteSpace(model.Village)) ? model.Village.Trim() : model.Village;
                        tbl.Visited = model.Visited;
                        tbl.DOB = model.DOB;
                        tbl.Age = model.Age;
                        tbl.MobileNo = !(string.IsNullOrWhiteSpace(model.MobileNo)) ? model.MobileNo.Trim() : model.MobileNo;
                        tbl.IsSkillTraining = model.IsSkillTraining;
                        tbl.IsMarriage = model.IsMarriage;
                        tbl.IsStudy = model.IsStudy;
                        tbl.SocialClass = !(string.IsNullOrWhiteSpace(model.SocialClass)) ? model.SocialClass.Trim() : model.SocialClass;
                        tbl.TillStudied = model.TillStudied;
                        tbl.IsWork = model.IsWork;
                        tbl.Reason = model.Reason;
                        tbl.IsPsychometric = model.IsPsychometric;
                        tbl.PsyYes_Result = !(string.IsNullOrWhiteSpace(model.PsyYes_Result)) ? model.PsyYes_Result.Trim() : model.PsyYes_Result;
                        tbl.Advice = model.Advice;
                        tbl.IsFollowUp = model.IsFollowUp;
                        tbl.FollowUp = !(string.IsNullOrWhiteSpace(model.FollowUp)) ? model.FollowUp.Trim() : model.FollowUp;
                        tbl.IsActive = true; tbl.IsDeleted = false;
                        if (model.ID == 0)
                        {
                            tbl.CreatedBy = User.Identity.Name;
                            tbl.CreatedDt = DateTime.Now;
                            db.tbl_Registration.Add(tbl);
                        }
                        else
                        {
                            tbl.UpdatedBY = User.Identity.Name;
                            tbl.UpdatedDt = DateTime.Now;
                        }
                        results = db.SaveChanges();
                    }
                    if (results > 0)
                    {
                        //var resResponse = Json(new { IsSuccess = true, htmlData = html, msg = action }, JsonRequestBehavior.AllowGet);
                        //resResponse.MaxJsonLength = int.MaxValue;
                        //return resResponse;

                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Registration" + " Successfully.", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "All Record Required !!", Data = null };
                    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse1.MaxJsonLength = int.MaxValue;
                    return resResponse1;
                };
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
        #endregion
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