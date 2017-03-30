using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class FilmExistsEventArgs : EventArgs
    {
        public Film ExistingFilm { get; set; }
    }
}
