using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class ListsSorting
    {
        public delegate List<T> SortInspector<T>(List<T> list);

        public List<T> SortingListsName<T>(List<T> listToSort)
        {
            return listToSort.OrderBy(f => f).ToList(); 
        }

        public List<Film> SortingListId(List<Film> list)
        {
            list.Sort((x, y) => x.Id.CompareTo(y.Id));
            return list;
        }

        public List<Worker> SortingListIdW(List<SimpleWorker> list)
        {
            List<Worker> work = new List<Worker>();
            foreach (Worker w in list)
            {
                work.Add(w);
            }
            work.Sort((x, y) => x.Id.CompareTo(y.Id));
            return work;
        }
    }
}
