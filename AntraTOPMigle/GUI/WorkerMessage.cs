using AntraTOPMigle.Enums;
using AntraTOPMigle.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntraTOPMigle.GUI
{
    public partial class WorkerMessage : Form
    {
        public int EnumNumber { get; private set; }
        public double WorkerHours { get; private set; }
        public bool IsClosed { get; private set; }
        public WorkerMessage()
        {
            InitializeComponent();

            Array profArray = typeof(Profession).GetEnumValues();
            foreach (Profession prof in profArray)
            {
                listBox1.Items.Add(prof);
            }

            button1.Click +=  //Anonymous method
                delegate
                {
                    string share = Helpers.GetString(textBox1);
                    if (share != null && listBox1.SelectedIndex != -1)
                    {
                        Profession prof = (Profession)listBox1.SelectedItem;
                        EnumNumber = (int)prof;
                        var number = Helpers.GetLength(textBox1);
                        if (number <= 1 && number >= 0.5 && number != null)
                        {
                            WorkerHours = (double)number;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Not valid number!", "Warning", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not all choises are made!", "Warning", MessageBoxButtons.OK);
                    }
                    IsClosed = true;
                };
            if (IsClosed)
            {
                IsClosed = false;
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
