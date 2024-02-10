using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Models.Import
{
    //public interface ICustomerDatabaseAndReports
    //{
    //    public bool IsSuccess { get; set; }
    //    public string ErrorMessage { get; set; }
    //    public List<IRawDataFromCRMComplains> RawDataFromCRMComplains { get; set; }
    //}

    public interface IImportResult<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T FileData { get; set; }
    }
}
