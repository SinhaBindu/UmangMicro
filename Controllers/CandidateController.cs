using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;
using UmangMicro.Models;
using static UmangMicro.Manager.Enums;

namespace UmangMicro.Controllers
{
    public class CandidateController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        int results = 0; int results2nd = 0;
        // GET: Candidate
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reg(int LangType = 1)
        {
            RegModel model = new RegModel();
            model.LanguangeType = LangType;
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
                        response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Registration" + " Successfully.", Data = null };
                        var resResponse1 = Json(response, JsonRequestBehavior.AllowGet);
                        resResponse1.MaxJsonLength = int.MaxValue;
                        return resResponse1;
                    }
                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Registration" + " Successfully.", Data = null };
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