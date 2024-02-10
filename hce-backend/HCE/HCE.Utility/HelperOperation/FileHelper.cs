using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.HelperOperation
{
    public static class FileHelper
    {
        public static bool IsImage(string extension)
        {
            List<string> imageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
            if (string.IsNullOrEmpty(extension))
                return false;
            if (!imageExtensions.Contains(extension.ToUpper()))
                return false;
            return true;

        }
    }
}
