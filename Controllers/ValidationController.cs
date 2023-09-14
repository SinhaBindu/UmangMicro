using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmangMicro.Models;

namespace UmangMicro.Controllers
{
    public class ValidationController : Controller
    {
        public JsonResult ValidateUploadedFileType(HttpPostedFileBase[] Doc, string FileType)
        {
            try
            {
                var file = Doc[0];
                if (file != null && file.ContentLength > 0)
                {
                    byte[] tempFileBytes = null;
                    var fileName = file.FileName.Trim();
                    using (BinaryReader reader = new BinaryReader(file.InputStream))
                    {
                        tempFileBytes = reader.ReadBytes((int)file.ContentLength);
                    }
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                    var filetype = Path.GetExtension(fileName).Replace(".", "").ToLower();
                    var fileExtension = Path.GetExtension(fileName);
                    //Enum.TryParse(FileType, out FileUploadCheck.FileType type);
                    var result = false;// FileUploadCheck.IsValidFile(tempFileBytes, type, filetype);

                    if (result == false)
                    {
                        return Json("Only valid " + FileType.ToLower() + " file is allowed", JsonRequestBehavior.AllowGet);
                    }
                    
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return Json("Unable to validate", JsonRequestBehavior.AllowGet);
            }
        }        
    }
}