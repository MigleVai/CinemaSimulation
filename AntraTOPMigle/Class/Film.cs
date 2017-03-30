using AntraTOPMigle.Enums;
using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    [Serializable()]
    class Film : ISerializable, IEquatable<Film>, IComparable<Film>, InfoGetter
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public Genre FilmGenre { get; set; }
        public string Type { get; set; }
        private static int _idCounter = 1;
        public int Id { get; private set; }
        private bool _once = false;

        public Film(string name, int length, Genre gen, string type)
        {
            Name = name;
            Length = length;
            FilmGenre = (Genre)gen;
            Type = type;
            Id = NextId();
        }
        public int NextId()
        {
                return _idCounter++;
        }

        public void SetId(int number)
        {
            if (!_once)
            {
                _idCounter = number;
                _once = true;
            }
        }

        public void SmallerId()
        {
            _idCounter -= 1;   
        }

        public Film(SerializationInfo info, StreamingContext ctxt)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Length = (int)info.GetValue("Length", typeof(int));
            FilmGenre = (Genre)info.GetValue("Genre", typeof(Genre));
            Type = (string)info.GetValue("Type", typeof(string));
            Id = (int)info.GetValue("Id", typeof(int));
        } 

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Length", Length);
            info.AddValue("Genre", FilmGenre);
            info.AddValue("Type", Type);
            info.AddValue("Id", Id);
        }

        public bool Equals(Film other)
        {
            return (this.Name == other.Name && this.Type == other.Type && this.FilmGenre == other.FilmGenre);
        }

        public int CompareTo(Film other)
        {
            return Name.CompareTo(other.Name);
        }

        public string InfoToString()
        {
            var info = "Name: " + Name + " || Length: " + Length
                        + " || Genre: " + FilmGenre + " || Type: " + Type;
            return info;
        }
    }
}
