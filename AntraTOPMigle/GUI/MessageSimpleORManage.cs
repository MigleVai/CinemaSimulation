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
    public partial class MessageSimpleORManage : Form
    {
        public int Choise { get; set; }
        public int? SimpleProf { get; private set; }
        public double? Number { get; private set; }
        public MessageSimpleORManage()
        {
            InitializeComponent();

            button1.Click +=  //Anonymous method
                delegate
                {
                    Choise = 1;
                    MessageBox.Show("Information submited!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Close();
                };
            button2.Click +=  //Anonymous method
                delegate
                {
                    Choise = 0;
                    WorkerMessage message = new WorkerMessage();
                    message.ShowDialog();
                    if (message.IsClosed)
                    {
                        SimpleProf = message.EnumNumber;
                        Number = message.WorkerHours;
                        MessageBox.Show("Information submited!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Close();
                    }
                    else
                    {
                        SimpleProf = null;
                        Number = null;
                        Close();
                    }
                };
        }

        //private void button2_Click(object sender, EventArgs e)
        //{

        //}
    }
}
