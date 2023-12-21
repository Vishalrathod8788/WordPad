using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjRTB_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string myPath = "";
        bool isEdit = false;

        Font DefFont = new Font("arial", 14, FontStyle.Regular);
        Color DefForeColor = Color.Black;
        Color DefBackColor = Color.White;

        void LoadDefault()
        {
            RTB1.Font = DefFont;
            RTB1.ForeColor = DefForeColor;
            RTB1.BackColor = DefBackColor;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                if (DialogResult.Yes == MessageBox.Show("File Data Is Changed..\nDo You Want To Save File", "Edited File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }
            if(DialogResult.Yes == MessageBox.Show("Are You Sure Want To Exit","Aaavaa joo..TaaTaa..Byy..Byy..Khataam",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
                this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }

        private void FD_Apply(object sender, EventArgs e)
        {
            if (RTB1.SelectedText.Length > 0)
            {
                RTB1.SelectionFont = FD.Font;
                RTB1.SelectionColor = FD.Color;
            }
            else
            {
                RTB1.Font = FD.Font;
                RTB1.ForeColor = FD.Color;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            FD.Font = DefFont;
            FD.Color = DefForeColor;
            if (DialogResult.OK == FD.ShowDialog())
            {
                isEdit = true;
                if (RTB1.SelectedText.Length > 0)
                {
                    RTB1.SelectionFont = FD.Font;
                    RTB1.SelectionColor = FD.Color;
                }
                else
                {
                    RTB1.Font = FD.Font;
                    RTB1.ForeColor = FD.Color;
                }
            }
            else
            {
                LoadDefault();
            }
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            CD.AllowFullOpen = true;           

            if (DialogResult.OK == CD.ShowDialog())
            {

                isEdit = true;
                if (RTB1.SelectedText.Length > 0)
                {
                    RTB1.SelectionBackColor = CD.Color;
                    
                }
                else
                {
                    RTB1.BackColor = CD.Color;                    
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                if (DialogResult.Yes == MessageBox.Show("File Data Is Changed..\nDo You Want To Save File", "Edited File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }

            isEdit = true;
            RTB1.Clear();
            LoadDefault();
            this.Text = "myWordPad";
            myPath = "";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myPath != "")
            {
                RTB1.SaveFile(myPath);
            }
            else
            {

                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Title = "Save File From myWordPad";
                SFD.Filter = "RTF File|*.rtf|Word File|*.doc|Text File|*.txt| AllFile | *.*";

                if (DialogResult.OK == SFD.ShowDialog())
                {
                    if (SFD.FileName.ToString().Contains(".txt"))
                        RTB1.SaveFile(SFD.FileName.ToString(), RichTextBoxStreamType.PlainText);
                    else
                        RTB1.SaveFile(SFD.FileName.ToString(), RichTextBoxStreamType.RichText);                    
                    
                    myPath = SFD.FileName.ToString();
                    this.Text = "myWordPad : " + SFD.FileName.ToString();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = "Save File From myWordPad";
            SFD.Filter = "RTF File|*.rtf|Word File|*.doc|Text File|*.txt";

            if (DialogResult.OK == SFD.ShowDialog())
            {
                if (SFD.FileName.ToString().Contains(".txt"))
                    RTB1.SaveFile(SFD.FileName.ToString(), RichTextBoxStreamType.PlainText);
                else
                    RTB1.SaveFile(SFD.FileName.ToString(), RichTextBoxStreamType.RichText);
                myPath = SFD.FileName.ToString();
                this.Text = "myWordPad : " + SFD.FileName.ToString();
            }
        }

        int ReturnWordCount()
        {
            int i = 0;
            string[] spltArr = RTB1.Text.Trim().Split(' ');
            foreach (string s in spltArr)
            {
                if (s.Trim() != "")
                {
                    i = i + 1;
                }
            }
            return i;
        }

        private void RTB1_TextChanged(object sender, EventArgs e)
        {
            isEdit = true;
            lblCount.Text = RTB1.Text.Count().ToString();            
            lblWordCount.Text = ReturnWordCount().ToString();
        }

        private void foreColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            CD.AllowFullOpen = true;
            CD.AnyColor = true;
            if (DialogResult.OK == CD.ShowDialog())
            {
                isEdit = true;
                if (RTB1.SelectedText.Length > 0)
                {
                    RTB1.SelectionColor = CD.Color;
                }
                else
                {
                    RTB1.ForeColor = CD.Color;
                }
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isEdit = true;
            RTB1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isEdit = true;
            RTB1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isEdit = true;
            RTB1.Paste();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                if (DialogResult.Yes == MessageBox.Show("File Data Is Changed..\nDo You Want To Save File", "Edited File", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }
            
            
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "Open File To Load In myWordPad";
            OFD.Filter = "RTF Files|*.rtf|Word Files|*.doc|Text Files|*.txt";
            if (DialogResult.OK == OFD.ShowDialog())
            {                
                if (OFD.FileName.ToString().Contains(".txt"))                
                    RTB1.LoadFile(OFD.FileName.ToString(), RichTextBoxStreamType.PlainText);
                else                 
                    RTB1.LoadFile(OFD.FileName.ToString(), RichTextBoxStreamType.RichText);

                myPath = OFD.FileName.ToString();
                this.Text = "myWordPad : " + OFD.FileName.ToString();
            }
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RTB1.ZoomFactor < 5)
            {
                RTB1.ZoomFactor += 1;
            }
            else
            {
                RTB1.ZoomFactor = 5;
            }
            zo.Text = RTB1.ZoomFactor.ToString();
        }

        private void zoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (RTB1.ZoomFactor == 1)
            {
                RTB1.ZoomFactor = 1;
            }
            else
            {
                RTB1.ZoomFactor -= 1;
            }
            zo.Text = RTB1.ZoomFactor.ToString();
        }       
    }
}
