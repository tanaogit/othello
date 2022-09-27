using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace オセロ.Forms
{
    public partial class StartMenuForm : Form
    {
        public StartMenuForm()
        {
            InitializeComponent();
        }

        private void Button_Start_Click(object sender, EventArgs e)
        {
            try
            {
                OthelloBoardForm OthelloBoard = new OthelloBoardForm();
                OthelloBoard.ShowDialog();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                MessageBox.Show("予期せぬエラーが発生しました");
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
