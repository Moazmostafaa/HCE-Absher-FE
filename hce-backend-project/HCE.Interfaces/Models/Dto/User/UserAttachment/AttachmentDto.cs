using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Dto.User.UserAttachment
{
    public class AttachmentDto
    {
        public Guid AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Extention { get; set; }
        public int ModuleId { get; set; }
        public long SizeByByte { get; set; }
        public byte[] FileData { get; set; }
    }
}
