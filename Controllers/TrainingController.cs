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
                var tbl = (model.Id > 0) ? db.Tbl_Training.Find(model.Id) : new Tbl_Training();
                if (model.Trainingtype == 1)
                {
                    model.Nameofteacher = model.TeacherIds;
                    //tbl = tbl == null ? new tbl_Mapped() : tbl;
                    Tbl_Trainingteacher tbltcmapp;
                    List<Tbl_Trainingteacher> tbllist = new List<Tbl_Trainingteacher>();

                    if (tbl != null && !string.IsNullOrWhiteSpace(model.Nameofteacher))
                    {
                        tbl.Trainingtype = model.Trainingtype;
                        tbl.Round = model.Round;
                        tbl.Nameofteacher = model.Nameofteacher;
                        tbl.Date = model.Date;
                        tbl.IsActive = true;
                        var Nameofteacherplt = model.Nameofteacher.Split(',');
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
    }



}