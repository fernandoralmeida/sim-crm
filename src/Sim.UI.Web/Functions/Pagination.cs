using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Sim.UI.Web.Functions
{
    public class Pagination<T> : List<T>
    {
        public int PageIndex{get;set;}
        public int ItemsViews { get; set; }
        public int TotalPages{get;set;}

        public Pagination()
        {        }

        public Pagination(List<T> items, int count, int pageindex, int pagesize)
        {
            PageIndex = pageindex;
            ItemsViews = pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            this.AddRange(items);
        }

        public bool HasPreviusPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static Pagination<T> Create(IQueryable<T> source, int pageindex, int pagesize)
        {
            var count = source.Count();
            var items = source.Skip((pageindex - 1) * pagesize)
                        .Take(pagesize).ToList();

            return new Pagination<T>(items, count, pageindex, pagesize);
        }

    }
}