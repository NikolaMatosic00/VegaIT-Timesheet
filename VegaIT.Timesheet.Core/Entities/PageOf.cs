using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VegaIT.Timesheet.Core.Entities;

public class PageOf<T>
{
    public int TotalPagesCount { get; set; }
    public IEnumerable<T> Items { get; set; }

    public PageOf() { }

    public PageOf(int totalPagesCount, IEnumerable<T> items)
    {
        TotalPagesCount = totalPagesCount;
        Items = items;
    }
}
