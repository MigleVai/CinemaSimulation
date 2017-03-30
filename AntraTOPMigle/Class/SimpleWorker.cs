using AntraTOPMigle.Enums;
using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class SimpleWorker : Worker, InfoGetter
    {
        public double Share { get; set; }
        public int Salary { get; private set; } = 666;
        public Profession Prof { get; set; }

        public SimpleWorker(string name, int number, double share): base(name)
        {
            Prof = (Profession)number;
            Share = share;
            SetSalary();
        }

        public void SetSalary()
        {
            if (Share >= 1)
            {
                Salary = 400;
            }
            else if (Share < 1 && Share >= 0.5) //Minimum work time is 0.5
            {
                Salary = 270;
            }
        }

        public string InfoToString()
        {
            string info = "Name: " + Name + " || ID: " + Id
                        + " || Profession: " + Prof + " || Salary: " + Salary;
            return info;
        }
    }
}
