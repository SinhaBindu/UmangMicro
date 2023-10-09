using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmangMicro.Models
{
        public class ValidateFileAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                int MaxContentLength = 1024 * 1024 * 50; //100 MB
                string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };

                var file = value as HttpPostedFileBase;

                if (file == null)
                    return false;
                else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ErrorMessage = "Please upload Your image and pdf of type: " + string.Join(", ", AllowedFileExtensions);
                    return false;
                }
                else if (file.ContentLength > MaxContentLength)
                {
                    ErrorMessage = "Your image and pdf is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
                    return false;
                }
                else
                    return true;
            }
        }
}