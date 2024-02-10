using HCE.Interfaces.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.ResponseModel
{
    public class PagedResponseResult<TEntity> : IPagedResponseResult<TEntity>
    {
        public PagedResponseResult() : this(new List<TEntity>(), 0)
        { }

        public PagedResponseResult(List<TEntity> entities) : this(entities, entities.Count)
        { }

        public PagedResponseResult(List<TEntity> entities, int totalRecords)
        {
            Entities = entities;
            TotalRecords = totalRecords;
        }

        public int TotalRecords { get; set; }
        public List<TEntity> Entities { get; set; }
    }
}
