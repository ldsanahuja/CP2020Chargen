using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP48
{
    public partial class RollForm : Form
    {
        private Random rng = new Random(System.DateTime.Now.Millisecond);
        public RollForm()
        {
            InitializeComponent();
        }

        private void RollForm_Load(object sender, EventArgs e)
        {
            Roll();
        }

        private void Roll()
        {
            int[] res = new int[9];
            int total = 0;
            for(int x = 0; x < 9; x++)
            {
                res[x] = rng.Next(2, 11);
                total += res[x];
            }
            tbRoll1.Text = res[0].ToString();
            tbRoll2.Text = res[1].ToString();
            tbRoll3.Text = res[2].ToString();
            tbRoll4.Text = res[3].ToString();
            tbRoll5.Text = res[4].ToString();
            tbRoll6.Text = res[5].ToString();
            tbRoll7.Text = res[6].ToString();
            tbRoll8.Text = res[7].ToString();
            tbRoll9.Text = res[8].ToString();
            lblRollResults.Text = GetTotalStatsValueInString(total) + "\nTotal points: " + total.ToString() + " pts.";
        }
        private string GetTotalStatsValueInString(int value)
        {
            if (value <= 45)
                return "Consider reroll, please";
            if (value <= 50)
                return "Average";
            if (value <= 60)
                return "Minor Supp. Character";
            if (value <= 70)
                return "Major Supp. Character";
            if (value <= 75)
                return "Minor Hero";
            if (value <= 80)
                return "Major Hero";
            return "What the..! man! That's a roll!";

        }

        private void bntReRoll_Click(object sender, EventArgs e)
        {
            DialogResult dlg = MessageBox.Show("Are you sure?\nYou will lose those results", "Confirmation", MessageBoxButtons.OKCancel);
            if(dlg == DialogResult.OK)                
                Roll();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
