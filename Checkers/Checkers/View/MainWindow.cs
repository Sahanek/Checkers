using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers.View
{
    public partial class MainWindow : Form
    {
        PictureBox SelectedField = null;

        public void MakeSelection(object ob)
        {

            if (SelectedField != null)
                SelectedField.BackColor = Color.Black;
               
            PictureBox Field = (PictureBox)ob;
            SelectedField = Field;
            SelectedField.BackColor = Color.Lime;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void MouseClick(object sender, MouseEventArgs e)
        {
            MakeSelection(sender);
        }
    }
}
