using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Manager;
using UmangMicro.Models;

namespace UmangMicro.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        UM_DBEntities db = new UM_DBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetIndexList(string Para="", string CaseIDNameDOB = "",string Sdt = "", string Edt = "", string SchoolId = "")
        {
            try
            {
                var items = SP_Model.GetSP_RCTestRes_List(Para, CaseIDNameDOB, Sdt, Edt, SchoolId);
                if (items != null)
                {
                    var data = JsonConvert.SerializeObject(items);
                    var html = ConvertViewToString("_RCTestList", items);
                    return Json(new { IsSuccess = true, res = html }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { IsSuccess = false, res = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { IsSuccess = false, res = "There was a communication error." }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Add(int? Id)
        {
            var m = GetAdd(Id);
            return View(m);
        }
        private QesRes GetAdd(int? Id)
        {
            FormModel model;
            List<FormModel> modellist = new List<FormModel>();

            var tblmain = db.tbl_RIASEC_Main.Find(Id);
            var tbltest = db.tbl_RIASECTest.Where(x => x.RIASEC_ID_fk == tblmain.ID);
            var tbl = db.QuestionRIASECs.Where(x => x.IsActive == true).OrderBy(x => x.QuestionCode).ToList();
            if (tbl != null)
            {
                foreach (var item in tbl.ToList())
                {
                    model = new FormModel();
                    model.OptionList = new List<QuestOption>();
                    //model.OptionList.Clear();
                    model.QuestionId_pk = item.ID;
                    model.QuestionCode = item.QuestionCode;
                    model.HindiQuestion = item.QuestionHindi;
                    model.ControlType = item.ControlType;
                    model.SectionType = item.Section;
                    model.OrderBy = Convert.ToInt32(item.Ques_Order);

                    modellist.Add(model);
                }
            }
            var reslist = BuildQuestion(modellist);
            reslist = reslist.OrderBy(x => x.OrderBy).ToList();
            var qList = new List<FormModel>();
            foreach (var item in reslist.ToList())
            {
                qList.Add(item);
                if (item.ChildQuestionList != null && item.ChildQuestionList.Any())
                {
                    qList.AddRange(item.ChildQuestionList.ToList());
                }
            }
            if (tblmain != null)
            {
                if (tblmain.ID > 0)
                {
                    return new QesRes { Id = tblmain.ID, SchoolId = Convert.ToInt32(tblmain.SchoolId), Qlist = qList };
                }
            }
            return new QesRes { SchoolId = 0, Qlist = qList };//Convert.ToInt32(MvcApplication.CUser.SchoolId)
        }

        [HttpPost]
        public ActionResult Add(QesRes model)
        {
            UM_DBEntities db_ = new UM_DBEntities();
            var result = 0;
            try
            {
                //var resdata = this.Request.Unvalidated.Form.AllKeys;
                if (model != null)
                {
                    if (model.SchoolId == 0)
                    {
                        return Json(new { IsSuccess = false, res = "", msg = "Select School !" }, JsonRequestBehavior.AllowGet);
                    }
                    var maintbl = model.Id != 0 ? db.tbl_RIASEC_Main.Find(model.Id) : new tbl_RIASEC_Main();

                    if (maintbl != null)
                    {
                        var mid = maintbl.ID;
                        var childtbl = db.tbl_RIASECTest.Where(x => x.RIASEC_ID_fk == maintbl.ID).ToList();
                        if (childtbl != null)
                        {
                            db.tbl_RIASECTest.RemoveRange(childtbl);
                            db.SaveChanges();
                        }
                        tbl_RIASECTest tbl_childTest = new tbl_RIASECTest();
                        if (model.Qlist != null)
                        {
                            if (model.Id != 0)
                            {
                                if (db_.tbl_RIASEC_Main.Any(x => x.SchoolId == (model.SchoolId.ToString()) && x.CaseID == model.CaseID && x.TestDate == model.Date && model.Id == 0))
                                {
                                    return Json(new { IsSuccess = false, res = "", msg = "This Record Is Already Exists !" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            if (model.Id == 0)
                            {
                                var disblck = db.SchoolMaster_N.Where(x => x.ID == model.SchoolId)?.FirstOrDefault();
                                maintbl.GudId = Guid.NewGuid();
                                maintbl.DistrictId = disblck.DistrictCode;
                                maintbl.BlockId = disblck.BlockCode;
                                maintbl.SchoolId = model.SchoolId.ToString();
                                maintbl.CaseID = model.CaseID.ToString();
                                maintbl.TestDate = model.Date;
                                maintbl.CreatedBy = MvcApplication.CUser.Id.ToString();
                                maintbl.CreatedOn = DateTime.Now;
                                maintbl.IsActive = true;
                                db.tbl_RIASEC_Main.Add(maintbl);
                            }
                            else
                            {
                                maintbl.UpdatedBy = MvcApplication.CUser.Id.ToString();
                                maintbl.UpdatedOn = DateTime.Now;
                                maintbl.IsActive = true;
                            }
                            result += db.SaveChanges();
                            var regid = db.tbl_Registration.Where(x=>x.CaseID==model.CaseID)?.FirstOrDefault();
                            foreach (var item in model.Qlist.ToList())
                            {
                                if (!string.IsNullOrWhiteSpace(item.Answer) && item.ControlType.ToLower() == "radiobutton")
                                {
                                    if (maintbl.ID != 0)
                                    {
                                        if (item.Answer == item.SectionType)
                                        {
                                            tbl_childTest.GudId = Guid.NewGuid();
                                            tbl_childTest.Reg_ID_fk =regid.ID;
                                            tbl_childTest.CaseID = model.CaseID.ToString();
                                            tbl_childTest.RIASEC_ID_fk = maintbl.ID;
                                            tbl_childTest.QuestionID = item.QuestionId_pk;
                                            tbl_childTest.QuestionCode = item.SectionType;
                                            tbl_childTest.CreatedBy = MvcApplication.CUser.Id.ToString();
                                            tbl_childTest.CreatedOn = DateTime.Now;
                                            tbl_childTest.IsActive = true;
                                        }
                                        db.tbl_RIASECTest.Add(tbl_childTest);
                                    }
                                    result += db.SaveChanges();
                                }
                                //result += db.SaveChanges();
                            }
                        }
                    }
                    if (result > 0)
                    {
                        //Success($"{action} successfully!", true);
                        // return RedirectToAction("Add", "Infra");
                        //Success("HR save and modified successfully !", true);
                        // return RedirectToAction("Index", "HR");
                        ModelState.Clear();
                        var res = GetAdd(maintbl.ID);
                        var html = ConvertViewToString("add", res);
                        var action = maintbl.ID == 0 ? "Saved" : "RIASEC Test Successfully Completed.....";
                        var resResponse = Json(new { IsSuccess = true, htmlData = html, msg = action }, JsonRequestBehavior.AllowGet);
                        resResponse.MaxJsonLength = int.MaxValue;
                        return resResponse;
                        //Success("HR save and modified successfully !", true);
                        // return Json(new { IsSuccess = true, res = html, MSG = "HR save and modified successfully !" }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { IsSuccess = false, htmlData = "Something went wrong.", msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);

                //if (result > 0)
                //{
                //    Success("HR save and modified successfully !", true);
                //    return RedirectToAction("Add", "HR");
                //}
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                //Danger("Something went wrong..", true);
                return Json(new { IsSuccess = false, htmlData = "Something went wrong.", msg = "Something went wrong." }, JsonRequestBehavior.AllowGet);
            }
            // return View(model);
        }
        //public ActionResult GetViewData(int? Id)
        //{
        //    var html = "";
        //    try
        //    {
        //        var tblhr = db.tbl_HR.Find(Id);
        //        bool IsCheck = false;
        //        if (tblhr != null && tblhr.Id > 0)
        //        {
        //            FormModel model;
        //            List<FormModel> modellist = new List<FormModel>();

        //            var tblhrAns = db.tbl_HRAnswer.Where(x => x.HRId_fk == Id).ToList();
        //            var fid = Convert.ToInt32(Enums.EnumFormName.HR);
        //            var tbl = db.QuestionBooks.Where(x => x.IsActive == true && x.FormId_fk == fid).OrderBy(x => x.QuestionCode).ToList();
        //            if (tbl != null)
        //            {
        //                foreach (var item in tbl.ToList())
        //                {
        //                    model = new FormModel();
        //                    model.OptionList = new List<QuestOption>();
        //                    //model.OptionList.Clear();
        //                    model.QuestionId_pk = item.Id;
        //                    model.QuestionCode = item.QuestionCode;
        //                    model.Question = item.Question;
        //                    model.HindiQuestion = item.HindiQuestion;
        //                    model.ControlType = item.ControlType;
        //                    model.ParentQuestionCode = item.ParentQuestionCode;
        //                    model.DependedAnswer = item.DependedAnswer;
        //                    model.SectionType = item.SectionType;
        //                    var tbllist = db.QuestionOptions.Where(x => x.QuestionCode == item.QuestionCode).OrderBy(x => x.QuestionCode).ToList();
        //                    if (tbllist != null)
        //                    {
        //                        for (int i = 0; i < tbllist.Count; i++)
        //                        {
        //                            if (tblhrAns != null)
        //                            {
        //                                var ans = tblhrAns.FirstOrDefault(x => x.QuestionCode == tbllist[i].QuestionCode && x.QuestionOption_fk == tbllist[i].Id);
        //                                if (ans != null && ans.Id > 0)
        //                                {
        //                                    model.OptionList.Add(new QuestOption
        //                                    {
        //                                        Id = ans == null ? 0 : ans.Id,
        //                                        OptionId_Pk = tbllist[i].Id,
        //                                        QuestionId_fk = tbllist[i].QuestionId_fk.Value,
        //                                        QuestionCode = tbllist[i].QuestionCode,
        //                                        ControlInputType = tbllist[i].ControlInputType,
        //                                        OptionTypeValidation = tbllist[i].OptionTypeValidation,
        //                                        Limit = tbllist[i].Limit,
        //                                        Text = tbllist[i].OptionText,
        //                                        HindiOptionText = tbllist[i].HindiOptionText,
        //                                        Value = tbllist[i].OptionCode,
        //                                        InputText = ans?.InputValue,
        //                                        InputText1 = ans?.InputValue1,

        //                                        LabelName1 = tbllist[i].LabelName1,
        //                                        LabelName2 = tbllist[i].LabelName2,
        //                                        SelectedItem = ans != null
        //                                    });
        //                                    if (ans != null)
        //                                    {
        //                                        model.Answer = tbllist[i].OptionCode;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    modellist.Add(model);
        //                }
        //            }
        //            var reslist = BuildQuestion(modellist);
        //            if (tblhr != null)
        //            {
        //                if (tblhr.Id > 0)
        //                {
        //                    var m1 = new QesRes { Id = tblhr.Id, FrequencyId = tblhr.FrequencyId, YearId = tblhr.YearId, Qlist = reslist };
        //                    // var m = new QesRes { Qlist = reslist };
        //                    IsCheck = true;
        //                    html = ConvertViewToString("_ViewData", m1);
        //                    var res = Json(new { IsSuccess = IsCheck, Data = html }, JsonRequestBehavior.AllowGet);
        //                    res.MaxJsonLength = int.MaxValue;
        //                    return res;
        //                }
        //            }
        //            var res1 = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
        //            res1.MaxJsonLength = int.MaxValue;
        //            return res1;
        //        }
        //        else
        //        {
        //            var res = Json(new { IsSuccess = IsCheck, Data = "Record Not Found !!" }, JsonRequestBehavior.AllowGet);
        //            res.MaxJsonLength = int.MaxValue;
        //            return res;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string er = ex.Message;
        //        return Json(new { IsSuccess = false, Data = "" }, JsonRequestBehavior.AllowGet); throw;
        //    }
        //}

        #region Recursion
        public static List<FormModel> BuildQuestion(List<FormModel> source)
        {
            var groups = source.GroupBy(i => i.ParentQuestionCode);

            var roots = groups.FirstOrDefault(g => string.IsNullOrWhiteSpace(g.Key)).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => !string.IsNullOrWhiteSpace(g.Key)).ToDictionary(g => g.Key, g => g.ToList());
                for (int i = 0; i < roots.Count; i++)
                    AddChild(roots[i], dict);
            }

            return roots;
        }
        private static void AddChild(FormModel node, Dictionary<string, List<FormModel>> source)
        {
            if (source.ContainsKey(node.QuestionCode))
            {
                node.ChildQuestionList = source[node.QuestionCode];
                for (int i = 0; i < node.ChildQuestionList.Count; i++)
                    AddChild(node.ChildQuestionList[i], source);
            }
            else
            {
                node.ChildQuestionList = new List<FormModel>();
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
        #endregion
        public ActionResult RCResultDetail(string RowId = "", string CaseID = "")
        {
            DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            try
            {
                if (!string.IsNullOrWhiteSpace(CaseID))
                {
                    ds = SP_Model.GetSP_RCTestRes_Detail(RowId, CaseID);
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

    }
}