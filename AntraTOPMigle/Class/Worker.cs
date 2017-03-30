using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class Worker : IComparable<Worker>
    {
        public string Name { get; set; }
        private static int _idCounter = 1;
        public int Id { get; }
        public Worker(string name)
        {
            Name = name;
            Id = NextId();
        }

        private int NextId()
        {
            return _idCounter++;
        }

        public int CompareTo(Worker worker)
        {
            return Name.CompareTo(worker.Name);
        }
    }
}
