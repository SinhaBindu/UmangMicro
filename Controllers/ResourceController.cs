using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Controllers;
using UmangMicro.Manager;
using UmangMicro.Models;
using static UmangMicro.Manager.CommonModel;

namespace UmangMicro.Controllers
{
    [Authorize]
    public class ResourceController : BaseController
    {
        UM_DBEntities db = new UM_DBEntities();
        // GET: Resource
        public ActionResult Index()
        {
            var x = db.Tbl_FileResource.ToList();
            return View(x);
        }
        public ActionResult ResourceUpload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         [Authorize(Roles = UserRoles.Admin + ", " + UserRoles.Umang)]
        public ActionResult ResourceUpload(FileResourceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tbl_FileResource tbl = new Tbl_FileResource();
                    tbl.FileGuid = Guid.NewGuid().ToString();
                    tbl.DocumentType = model.DocumentType;
                    tbl.Subject = model.Subject;
                    tbl.Description = model.Description;
                    tbl.LetterNo = model.LetterNo;
                    tbl.DateofIssue = model.DateofIssue;
                    tbl.IsActive = true;
                    tbl.Upload_date = DateTime.Now;
                    tbl.FileName = model.file.FileName;
                    tbl.UplaodBy = User.Identity.Name;
                    string attachmentfile = saveFile(model.file, model.DocumentType.ToString()+ "DocumentType", "");
                    if (attachmentfile != "")
                    {
                        tbl.AttachmentFile = attachmentfile;
                    }

                    db.Tbl_FileResource.Add(tbl);
                    db.SaveChanges();
                    Success("File Saved successfully !", true);
                    return RedirectToAction("ResourceUpload");
                }
                else
                {
                    Warning("please fill all mandatory fields.", true);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Danger("Something went wrong..", true);
            }
            return View(model);
        }
        public string saveFile(HttpPostedFileBase item, string Requestby, string FileName = "")
        {
            string URL = "";
            string filepath = string.Empty;
            if (item != null && item.ContentLength > 0)
            {
                URL = "/Uploads/" + Requestby + "/" + CommonModel.GetRandomNumber() + "/";
                string folderPath = Server.MapPath("~" + URL);

                var supportedTypes = new[] { "pdf", "xls", "xlsx", "jpeg", "png", "jpg" };

                var fileName = Path.GetFileName(item.FileName);
                // var rondom = Guid.NewGuid() + fileName;

                // var fileExt = System.IO.Path.GetExtension(rondom).Substring(1).ToLower();

                //if (!supportedTypes.Contains(fileExt.ToLower()))
                //{
                //   // Danger("File Extension Is InValid - Upload Only PDF/EXCEL/JPEG/PNG/JPG File");
                //   // return RedirectToAction("VendorDetails", new { id = d.guid });
                //}

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // string Document = Path.Combine("~/Uploads/VendorDoc/" + rondom);

                item.SaveAs(folderPath + fileName);
                filepath = URL + fileName;
            }
            return filepath;

        }
    }
}