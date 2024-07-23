using Newtonsoft.Json;
using SubSonic.Extensions;
using System;
using System.Collections.Generic;
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
    public class TrainingController : Controller
    {
        UM_DBEntities db = new UM_DBEntities();
        int results = 0;
        // GET: Training
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetIndex(string Sdt, int Trainingtype, string Edt, string DistrictId, string SchoolId)
        {
            try
            {
                DistrictId = DistrictId == "0" ? string.Empty : DistrictId;
                SchoolId = SchoolId == "0" ? string.Empty : SchoolId;
                DistrictId = (string.IsNullOrWhiteSpace(DistrictId)) ? "ALL" : DistrictId;
                SchoolId = (string.IsNullOrWhiteSpace(SchoolId)) ? "ALL" : SchoolId;
                var items = SP_Model.GetSP_TrainingListData(Sdt, Trainingtype, Edt, DistrictId.ToUpper(), SchoolId.ToUpper());
                ViewBag.task = Trainingtype;
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_TrainingData", items);
                    return Json(new { IsSuccess = true, reshtml = html, res = data }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddTraining()
        {
            TrainingCentreModel model = new TrainingCentreModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddTraining(TrainingCentreModel model)
        {
            UM_DBEntities _db = new UM_DBEntities();
            JsonResponseData response = new JsonResponseData();
            int res = 0;
            var getschool = _db.AspNetUsers.ToList();
            try
            {
                if (model.Round==0)
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please Select Cohort", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                if (string.IsNullOrWhiteSpace(model.sessionIds))
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Please Select Session", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
                var tbl = (model.Id > 0) ? db.Tbl_Training.Find(model.Id) : new Tbl_Training();
                if (model.Trainingtype == 1)
                {
                    model.Nameofteacher = model.TeacherIds;
                    model.DistrictMlt = model.DistrictIds;
                    model.SchoolMlt = model.SchoolIds;
                    model.sessionMlt = model.sessionIds;
                    model.Cohortmlt = model.CohortIds;
                    
                    //tbl = tbl == null ? new tbl_Mapped() : tbl;
                    Tbl_Trainingteacher tbltcmapp;
                    List<Tbl_Trainingteacher> tbllist = new List<Tbl_Trainingteacher>();

                    if (tbl != null && !string.IsNullOrWhiteSpace(model.Nameofteacher))
                    {
                        tbl.Trainingtype = model.Trainingtype;
                        tbl.Round = model.Round;
                        tbl.Cohortmlt = model.Cohortmlt;
                        tbl.DistrictMlt = model.DistrictMlt;
                       
                        tbl.SchoolMlt = model.SchoolMlt;
                        tbl.sessionMlt = model.sessionMlt;
                        tbl.Comment = model.Comment;
                        tbl.Nameofteacher = model.Nameofteacher;
                        tbl.Date = model.Date;
                        tbl.IsActive = true;
                        var Nameofteacherplt = model.Nameofteacher.Split(','); ;
                        if (Nameofteacherplt.Length != 0)
                        {
                            if (model.Id == 0)
                            {
                                tbl.CreatedBy = MvcApplication.CUser.Id;
                                tbl.CreatedOn = DateTime.Now;
                                _db.Tbl_Training.Add(tbl);
                                res = _db.SaveChanges();
                            }
                            foreach (var item in Nameofteacherplt)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    var schid = getschool.Where(x => x.Id.ToString().ToLower() == item.ToLower())?.FirstOrDefault().SchoolId;
                                    tbltcmapp = new Tbl_Trainingteacher();
                                    tbltcmapp.TrainingtypeId_fk = tbl.Id;
                                    tbltcmapp.NameofTeacher = Guid.Parse(item);
                                    tbltcmapp.SchoolId = schid;
                                    tbltcmapp.IsActive = true;
                                    tbltcmapp.CreatedBy = MvcApplication.CUser.Id;
                                    tbltcmapp.CreatedOn = DateTime.Now;
                                    tbllist.Add(tbltcmapp);
                                }
                            }
                            if (tbllist.Count > 0 && res > 0)
                            {
                                _db.Tbl_Trainingteacher.AddRange(tbllist);
                                res += _db.SaveChanges();
                            }
                        }

                        if (res > 0)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                    }

                }
                else if (model.Trainingtype == 2)
                {
                    if (tbl != null)
                    {
                        tbl.Trainingtype = model.Trainingtype;
                        tbl.Noofteachertrained = model.Noofteachertrained;
                        tbl.SchoolId = model.SchoolId;
                        tbl.Round = model.Round;
                        //tbl.sessionMlt = model.sessionMlt != null ? string.Join(",", model.sessionMlt) : "";
                        tbl.sessionMlt = model.sessionIds;
                        tbl.Comment = model.Comment;
                        tbl.Date = model.Date;
                        tbl.IsActive = true;
                        if (model.Id == 0)
                        {
                            tbl.CreatedBy = MvcApplication.CUser.Id;
                            tbl.CreatedOn = DateTime.Now;
                            _db.Tbl_Training.Add(tbl);
                            res = _db.SaveChanges();
                        }
                        if (res > 0)
                        {
                            response = new JsonResponseData { StatusType = eAlertType.success.ToString(), Message = "Record Submitted Successfully!!!", Data = null };
                            var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                            resResponse3.MaxJsonLength = int.MaxValue;
                            return resResponse3;
                        }
                    }

                }
                else
                {
                    response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "Data Not Submitted !!", Data = null };
                    var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                    resResponse3.MaxJsonLength = int.MaxValue;
                    return resResponse3;
                }
            }
            catch (Exception)
            {
                response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error.", Data = null };
                var resResponse3 = Json(response, JsonRequestBehavior.AllowGet);
                resResponse3.MaxJsonLength = int.MaxValue;
                return resResponse3;
            }
            response = new JsonResponseData { StatusType = eAlertType.error.ToString(), Message = "There was a communication error..", Data = null };
            var resResponse4 = Json(response, JsonRequestBehavior.AllowGet);
            resResponse4.MaxJsonLength = int.MaxValue;
            return resResponse4;
        }
        public ActionResult GetTrainingSession()
        {
            TrainingCentreModel TranmodularS = new TrainingCentreModel();
            return View(TranmodularS);
        }
        [HttpGet]
      
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