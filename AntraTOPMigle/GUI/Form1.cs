using AntraTOPMigle.Class;
using AntraTOPMigle.Enums;
using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntraTOPMigle.GUI
{
    public partial class Form1 : Form
    {
        SaveAllFilms filmList = new SaveAllFilms();
        WorkerList<Manager> managerList = new WorkerList<Manager>();
        WorkerList<SimpleWorker> simpleWorkerList = new WorkerList<SimpleWorker>();
        private static int _numberId;
        private bool _oneTime = false;
        public string Decision { get; private set; }
        //WorkerList<Worker> workerList = new WorkerList<Worker>();
        //private int length;

        public Form1()
        {
            var task = GetFromFileAsync(filmList);
            InitializeComponent();
            tabPage1.Text = "Film";
            tabPage2.Text = "Worker";
            MakeBowNow();
            task.Wait();
            AddToListbox();
            //GetFromFile(filmList, listBox2);
           
            filmList.Exists += SendMessage;
        }

        private void MakeBowNow()
        {
            Array genreArr = typeof(Genre).GetEnumValues();
            foreach (Genre gen in genreArr)
            {
                listBox1.Items.Add(gen);
            }
        }

        private void AddToListbox()
        {
            foreach (Film f in filmList.GetList())
            {
                listBox2.Items.Add(f.Id + " - " + f.Name);
            }
        }

        private void SettingIdOneTime()
        {
            if (!_oneTime)
            {
                filmList.GetList()[filmList.SizeList()-1].SetId(_numberId);
                _oneTime = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) //ADD
        {
            SettingIdOneTime();
            string name = Helpers.GetString(textBox1);
            string length = Helpers.GetString(textBox2);
            GetRadio();
            if (name == null || length == null || Decision == null || IntEnum(listBox1) == 0)
            {
                MessageBox.Show("Not all choises are made!");
            }
            else
            {
                int? number = (int?)Helpers.GetLength(textBox2);
                if (number != null && number > 0)
                {
                    Genre gen = (Genre)IntEnum(listBox1);
                    var film = filmList.CreateFilm(name, (int)number, gen, Decision);
                    if (filmList.CompareFilms(film) != 1)
                    {
                        filmList.AddFilm(film);
                        listBox2.Items.Add(film.Id + " - " + film.Name);
                    }
                    Decision = null;
                }
                else
                {   
                    MessageBox.Show("Not valid number!");
                }
            }
        }

        private void SendMessage(object sender, FilmExistsEventArgs args)
        {
            DialogResult result = MessageBox.Show("You want to create an existing film. Do you still want to add it?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                filmList.AddFilm(args.ExistingFilm);
                listBox2.Items.Add(args.ExistingFilm.Id + " - " + args.ExistingFilm.Name);
            }
            else if (result == DialogResult.No)
            {
                args.ExistingFilm.SmallerId();
            }
        }

        private void GetRadio()
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                bool isChecked = radioButton1.Checked;
                if (isChecked)
                {
                    Decision = radioButton1.Text;
                }
                else
                {
                    Decision = radioButton2.Text;
                }
            }
            else
            {
                Decision = null;
            }
        }

        private int IntEnum(ListBox listbox)
        {
            if (listbox.SelectedIndex != -1)
            {
                return (int)(Genre)listbox.SelectedItem;
            }
            else
            {
                return 0; // there is no such value in enum "Genre"
            }
        }

        private void button2_Click(object sender, EventArgs e) //SHOW
        {
            if (listBox2.SelectedIndex != -1)
            {
                int index = GetIndex(listBox2);
                List<Film> film = filmList.GetList();
                string info = film[index].InfoToString();
                Helpers.GetListInfo(film[index]);
                //MessageBox.Show("Name: "+ $"{film[index].Name}" + Environment.NewLine
                //              + "Length: " + $"{film[index].Length}" + Environment.NewLine
                //              + "Type: " + $"{film[index].Type}" + Environment.NewLine
                //              + "Genre: " + $"{film[index].FilmGenre}");
            }
            else
            {
                MessageBox.Show("No choise is made!");
            }
        }

        private int GetIndex(ListBox listBox)
        {
            string number = listBox.GetItemText(listBox.SelectedItem);
            int cha = number.IndexOf("-") - 1;
            int id = Int32.Parse(number.Substring(0, cha));  // filmo id
            List<Film> films = filmList.GetList();
            return films.FindIndex(f => f.Id == id); //lambda
        }

        private void button3_Click(object sender, EventArgs e) //REMOVE
        {
            if (listBox2.SelectedIndex != -1)
            {
                int index = GetIndex(listBox2);
                Film film = filmList.RemoveObject(index);
                listBox2.Items.Remove(listBox2.SelectedItem);
                MessageBox.Show("Film with " + film.Id  + " ID was removed!");
            }
            else
            {
                MessageBox.Show("No choise is made!");
            }
        }

        private void SaveToFile()
        {
            SaveToFile save = new SaveToFile();
            save.SerializeObject("Films.txt", filmList);
        }

        private static void GetFromFile(SaveAllFilms tempFilmList)
        {
            lock(tempFilmList)
            {
                if (MessageBox.Show("Application is starting. Do you want to recover film list?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveToFile save = new SaveToFile();
                    var films = new SaveAllFilms();
                    films.FilmsModels = save.DeSerializeObject("Films.txt").FilmsModels;
                    foreach (Film f in films.GetList())
                    {
                        tempFilmList.AddFilm(f);
                    }
                    var temp = tempFilmList.GetList();
                    _numberId = tempFilmList.GetList().Max(f => f.Id) + 1; //LAMBDA
                }
            }
            return;
        }

        private async static Task<bool> GetFromFileAsync(SaveAllFilms tempFilms)
        {
            await Task.Run(() => GetFromFile(tempFilms)).ConfigureAwait(false);
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("You are closing application. Do you want to save film list?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SaveToFile();
            }
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e) //SORT a - z
        {
            if (listBox2.Items.Count != 0)
            {
                ListsSorting a = new ListsSorting();
                ListsSorting.SortInspector<Film> b = a.SortingListsName<Film>;
                filmList.SetList(b(filmList.GetList()));
                ClearBoxForSorting();
            }
            else
            {
                MessageBox.Show("No items to sort!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count != 0)
            {
                ListsSorting a = new ListsSorting();
                filmList.SetList(a.SortingListId(filmList.GetList()));
                ClearBoxForSorting();
            }
            else
            {
                MessageBox.Show("No items to sort!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void ClearBoxForSorting()
        {
            listBox2.Items.Clear();
            foreach (Film f in filmList.GetList())
            {
                listBox2.Items.Add(f.Id + " - " + f.Name);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = Helpers.GetString(textBox4);
            if (name != null)
            {
                MessageSimpleORManage creation = new MessageSimpleORManage();
                creation.ShowDialog();
                if (creation.Choise == 1)
                {
                    Manager manager = new Manager(name);
                    managerList.AddObject(manager);
                    listBox4.Items.Add(manager.Id + " - " + manager.Name + " (" + manager.GetProfession +")");
                }
                else
                {
                    if (creation.SimpleProf != null && creation.Number != null)
                    {
                        SimpleWorker simple = new SimpleWorker(name, (int)creation.SimpleProf, (int)creation.Number);
                        simpleWorkerList.AddObject(simple);
                        listBox3.Items.Add(simple.Id + " - " + simple.Name + " (" + simple.Prof + ")");
                    }
                }
            }
            else
            {
                MessageBox.Show("Not all choises are made!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button9_Click(object sender, EventArgs e) //SORT
        {
            if (listBox2.Items.Count != 0)
            {
                ListsSorting a = new ListsSorting();
                ListsSorting.SortInspector<SimpleWorker> b = a.SortingListsName<SimpleWorker>;
                simpleWorkerList.SetList(b(simpleWorkerList.ReturnList()));
                ClearWorkerList();
            }
            else
            {
                MessageBox.Show("No items to sort!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void ClearWorkerList()
        {
            listBox3.Items.Clear();
            foreach (SimpleWorker sim in simpleWorkerList.ReturnList())
            {
                listBox3.Items.Add(sim.Id + " - " + sim.Name + " (" + sim.Prof + ")");
            }
        }

        private void button8_Click(object sender, EventArgs e) //Remove
        {
            if (listBox3.SelectedIndex != -1)
            {
                int numb = DoingIndexSimp();
                simpleWorkerList.RemoveObject(numb);
                listBox3.Items.Remove(listBox3.SelectedItem);
            }
            else if (listBox4.SelectedIndex != -1)
            {
                int numb = DoingIndexMan();
                managerList.RemoveObject(numb);
                listBox4.Items.Remove(listBox4.SelectedItem);
            }
            else
            {
                MessageBox.Show("No choise is made!");
            }
        }

        public int DoingIndexSimp()
        {
            string number = listBox3.GetItemText(listBox3.SelectedItem);
            int cha = number.IndexOf("-") - 1;
            int id = Int32.Parse(number.Substring(0, cha));
            List<SimpleWorker> worker = simpleWorkerList.ReturnList();
            int numb = worker.FindIndex(w => w.Id == id); //lambda
            return numb;
        }

        public int DoingIndexMan()
        {
            string number = listBox4.GetItemText(listBox4.SelectedItem);
            int cha = number.IndexOf("-") - 1;
            int id = Int32.Parse(number.Substring(0, cha));
            List<Manager> manager = managerList.ReturnList();
            int numb = manager.FindIndex(w => w.Id == id); //lambda
            return numb;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                int numb = DoingIndexSimp();
                string info = simpleWorkerList.ReturnList()[numb].InfoToString();
                MessageBox.Show(info, "Information", MessageBoxButtons.OK);
            }
            else if (listBox4.SelectedIndex != -1)
            {
                int numb = DoingIndexMan();
                string info = managerList.ReturnList()[numb].InfoToString();
                MessageBox.Show(info, "Information", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("No choise is made!", "Warning", MessageBoxButtons.OK);
            }
        }

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    foreach (Worker w in simpleWorkerList.ReturnList())
        //    {
        //        workerList.AddObject(w);
        //    }
        //    ListsSorting a = new ListsSorting();
        //    a.SortingListIdW(workerList.ReturnList());
        //    ClearWorkerList();
        //}
        //private static void MakeListBox(ListBox listBox)
        //{

        //    return;
        //}

        //private async static Task<bool> MakeListBoxAsync(ListBox listBox)
        //{
        //    await Task.Run(() => MakeListBox(listBox));
        //    return true;
        //}

        //private async static Task<bool> BeginingApp(SaveAllFilms tempFilms) //ListBox list
        //{
        //    await GetFromFileAsync(tempFilms).ConfigureAwait(false);
        //    //Ties cia nebeveikia....

        //    return true;
        //}
    }
}
