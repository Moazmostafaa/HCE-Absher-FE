using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Utility.CommonModels
{
    public class AppSetting
    {
        public int MaxFileSize { get; set; }
        public int DefaultPageSize { get; set; }
        public int MaxChatMembers { get; set; }
        public string GatewayPrefix { get; set; }
        public string BlobDirectory { get; set; }
    }
}
