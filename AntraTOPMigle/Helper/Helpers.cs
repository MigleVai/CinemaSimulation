using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntraTOPMigle.Helper
{
    public static class Helpers
    {
        public static string ErrorAnswer { get; set; } = "Yes";
        public static void ShowError(string text, bool isCritical)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (isCritical == true)
            {
                Application.Exit();
            }
        }

        public static void ShowWarningFile(string text)
        {
            var temp = MessageBox.Show(text + " Do you want to continue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (temp == DialogResult.Yes)
            {
                ErrorAnswer = "Yes";
            }
            else
            {
                ErrorAnswer = "No";
            }
        }

        public static string GetString(TextBox txtbox)
        {
            if (!string.IsNullOrWhiteSpace(txtbox.Text))
            {
                return txtbox.Text;
            }
            else
            {
                return null;
            }
        }

        public static double? GetLength(TextBox textbox)
        {
            double length;
            try
            {
                return length = Convert.ToDouble(textbox.Text);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }
        }

        public static void GetListInfo<T>(T thing) where T : InfoGetter 
        {
                string info = thing.InfoToString();
                MessageBox.Show(info, "Information", MessageBoxButtons.OK);
        }
    }
}
