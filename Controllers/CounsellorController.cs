using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
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
        public ActionResult CaseHistory(int Id = 0)
        {
            CHModel model=new CHModel();
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
                if (ModelState.IsValid)
                {
                    //var getdt = db_.tbl_Registration.Where(x => x.MobileNo == model.MobileNo);
                    //if (getdt.Any(x => x.Name == model.Name.Trim() && x.FatherName == model.FatherName.Trim() && x.MotherName == model.MotherName.Trim() && x.DOB == model.DOB && model.ID == 0))
                    //{
                    //    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Already Exists Registration.<br /> <span> Case ID : <strong>" + getdt?.FirstOrDefault().CaseID + " </strong>  </span>", Data = null };
                    //    var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                    //    resResponse1.MaxJsonLength = int.MaxValue;
                    //    return resResponse1;
                    //}
                    var tbl = model.Id != 0 ? db.tbl_CaseHistory.Find(model.Id) : new tbl_CaseHistory();
                    if (tbl != null)
                    {
                        //tbl.RegDate = model.RegDate;
                        //tbl.Name = !(string.IsNullOrWhiteSpace(model.Name)) ? model.Name.Trim() : model.Name;
                        //tbl.MotherName = !(string.IsNullOrWhiteSpace(model.MotherName)) ? model.MotherName.Trim() : model.MotherName;
                        //tbl.FatherName = !(string.IsNullOrWhiteSpace(model.FatherName)) ? model.FatherName.Trim() : model.FatherName;
                        //var sid = Convert.ToInt32(eState.Jharkhand);
                        //tbl.StateId = db.State_Mast.Where(x => x.ID == sid).FirstOrDefault().ID.ToString();
                        //tbl.DistrictId = model.DistrictId;
                        //tbl.BlockId = model.BlockId;
                        //tbl.PanchayatId = model.PanchayatId;
                        //tbl.VillageId = model.VillageId;
                        //tbl.SchoolId = model.SchoolId;
                        //if ("990099" == model.VillageId)
                        //{
                        //    tbl.VillageOther = !(string.IsNullOrWhiteSpace(model.VillageOther)) ? model.VillageOther.Trim() : model.VillageOther;
                        //}
                        //tbl.DOB = model.DOB;
                        //tbl.Age = model.Age;
                        //tbl.MobileNo = !(string.IsNullOrWhiteSpace(model.MobileNo)) ? model.MobileNo.Trim() : model.MobileNo;
                        //tbl.Sex = "Female";//!(string.IsNullOrWhiteSpace(model.Sex)) ? model.Sex.Trim() : model.Sex;
                        //tbl.Cast = !(string.IsNullOrWhiteSpace(model.Cast)) ? model.Cast.Trim() : model.Cast;
                        //tbl.IsActive = true; tbl.IsDeleted = false;

                        //tbl.Visited = model.Visited;
                        //tbl.IsSkillTraining = model.IsSkillTraining;
                        //tbl.IsMarriage = model.IsMarriage;
                        //tbl.IsStudy = model.IsStudy;
                        //tbl.SocialClass = !(string.IsNullOrWhiteSpace(model.SocialClass)) ? model.SocialClass.Trim() : model.SocialClass;
                        //tbl.TillStudied = model.TillStudied;
                        //tbl.IsWork = model.IsWork;
                        //tbl.Reason = model.Reason;
                        //tbl.IsPsychometric = model.IsPsychometric;
                        //tbl.PsyYes_Result = !(string.IsNullOrWhiteSpace(model.PsyYes_Result)) ? model.PsyYes_Result.Trim() : model.PsyYes_Result;
                        //tbl.Advice = model.Advice;
                        //tbl.IsFollowUp = model.IsFollowUp;
                        //tbl.FollowUp = !(string.IsNullOrWhiteSpace(model.FollowUp)) ? model.FollowUp.Trim() : model.FollowUp;

                        if (model.Id == 0)
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                tbl.CreatedBy = User.Identity.Name;
                            }
                            tbl.CreatedOn = DateTime.Now;
                            db.tbl_CaseHistory.Add(tbl);
                        }
                        else
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                tbl.UpdatedBy = User.Identity.Name;
                            }
                            tbl.UpdatedOn = DateTime.Now;
                        }
                        results = db.SaveChanges();
                    }
                    if (results > 0)
                    {
                        // var taskres = CU_RegLogin(model, tbl.ID);
                        var case_id = db_.tbl_Registration.Where(x => x.CaseID == model.CaseID.Trim())?.FirstOrDefault().CaseID; //if (case_id != null) { }
                        if (model.Id > 0)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been Updated successfully ! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = " Congratulations, you have been successfully case history registered! \r\nPlease Note Your <br /> <span> Case ID : <strong>" + case_id + " </strong> </span>", Data = null };
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

    }
}