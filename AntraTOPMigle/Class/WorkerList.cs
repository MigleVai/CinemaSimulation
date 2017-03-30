using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class WorkerList<T>
    {
        List<T> listWorker = new List<T>();

        public void AddObject(T thing)
        {
            listWorker.Add(thing);
        }

        public void RemoveObject(int index)
        {
            listWorker.RemoveAt(index);
        }

        public List<T> ReturnList()
        {
            return listWorker;
        }

        public void SetList(List<T> another)
        {
            listWorker = another;
        }
    }
}
