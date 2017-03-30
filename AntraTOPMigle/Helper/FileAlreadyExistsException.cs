using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Helper
{
    [Serializable]
    class FileAlreadyExistsException : Exception
    {
        public FileAlreadyExistsException(string message) : base(message)
        {
            Helpers.ShowWarningFile(message);
        }
    }
}
