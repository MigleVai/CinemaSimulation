using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class Manager : Worker, InfoGetter
    {
        private string _profession;
        private int _salary;  // LAZY
        public Manager(string name) : base(name)
        {
            _profession = "Manager";
        }

        public string GetProfession
        {
            get
            {
                return _profession;
            }    
        }
        public int Salary
        {
            get
            {
                if(_salary == 0)
                {
                    _salary = 1000; 
                }
                return _salary;
            }
            set
            {
                _salary = value;
            }
        }

        public string InfoToString()
        {
            string info = "Name: " + Name + " || Id: " + Id
                        + " || Managers salary: " + Salary;
            return info;
        }
    }
}
