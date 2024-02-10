using HCE.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Entities.Audit
{
    public class AuditUserAction : UpdateEntity<long>
    {
        public long AuditUserActionId
        {
            get => Id;
            set => Id = value;
        }
        public string JsonData { get; set; }
        public string EventType { get; set; }
        //public string Area { get; set; }
        //public string ControllerName { get; set; }
        //public string ActionName { get; set; }
        //public string RoleId { get; set; }
        //public string LangId { get; set; }
        //public string IpAddress { get; set; }
        //public string IsFirstLogin { get; set; }
        //public string LoggedInAt { get; set; }
        //public string LoggedOutAt { get; set; }
        //public string LoginStatus { get; set; }
        //public string PageAccessed { get; set; }
        //public string SessionId { get; set; }
        //public string UrlReferrer { get; set; }
    }
}
