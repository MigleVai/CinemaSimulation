using AntraTOPMigle.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    [Serializable()]
    class SaveAllFilms : ISerializable
    {
        public delegate void FilmExists(object sender, FilmExistsEventArgs args);
        public event FilmExists Exists;

        List<Film> saveList = new List<Film>();
        public Film CreateFilm(string name, int length, Genre gen, string type)
        {
            Film film = new Film(name, length, gen, type);
            return film;
        }

        public void AddFilm(Film film)
        {
            lock(this)
            {
                saveList.Add(film);
            }
        }

        public int CompareFilms(Film film)
        {
            if (Exists != null) // if no one listens
            {
                if (saveList.Any(f => f.Equals(film))) //LAMBDA
                {
                    FilmExistsEventArgs args = new FilmExistsEventArgs();
                    args.ExistingFilm = film;
                    Exists(this, args);
                    return 1;
                }
                else { return 0; }
            }
            else { return 0; }
        }

        public int SizeList()
        {
            return saveList.Count;
        }
        public Film RemoveObject(int index)
        {
            Film film = saveList[index];
            saveList.RemoveAt(index);
            return film;
            
        }
        public List<Film> GetList()
        {
            return saveList;
        }

        public void SetList(List<Film> another)
        {
            saveList = another;
        }

        public SaveAllFilms (SerializationInfo info, StreamingContext ctxt)
        {
            saveList = (List<Film>)info.GetValue("Films", typeof(List<Film>)); // to get
        }

        public SaveAllFilms()
        {
        }

        public List<Film> FilmsModels
        {
            get { return saveList; }
            set { saveList = value; }  // to change and get info
        }

        public void GetObjectData (SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Films", saveList);  // to put info
        }
    }
}
