using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AntraTOPMigle.Class
{
    class SaveToFile
    {
        public void SerializeObject(string fileName, SaveAllFilms objectToSerialize)  // from program to file
        {
            try
            {
                try
                {
                    if (File.Exists(fileName)) throw new FileAlreadyExistsException(fileName + " file already exists.");
                }
                catch (FileAlreadyExistsException ex)
                {
                    Console.WriteLine(ex);
                }
                if (Helpers.ErrorAnswer == "Yes")
                {
                    Stream stream = File.Open(fileName, FileMode.Create);
                    var bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(stream, objectToSerialize);
                    stream.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Helpers.ShowError(fileName + " - file was not found", false);
            }
        }

        public SaveAllFilms DeSerializeObject(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            var bFormatter = new BinaryFormatter();
            var obejctToSerialize = (SaveAllFilms)bFormatter.Deserialize(stream);
            stream.Close();
            return obejctToSerialize;
        }
    }
}
