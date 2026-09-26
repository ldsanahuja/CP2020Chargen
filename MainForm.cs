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
    public partial class MainForm : Form
    {
        public RollForm rollForm;
        public ItemListForm itemsForm;
        public ItemListForm weaponsForm;

        public static bool UseMoney = true;
        public static Sheet CurrentSheet = new Sheet();
        private bool hasChangesPending = false;
        private Random rng = new Random(System.DateTime.Now.Millisecond);
        private bool hasChoosenRole = false;
        private int profSkillPoints = 0;
        private int freeSkillPoints = 0;
        private bool careerSkillLocked = false;
        private string lastFilePath;
        private bool isLoading = false;
      //  private int itemCellSelected = -1;
     //   private int weaponCellSelected = -1;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init & events for stats, name... 
            tbName.TextChanged += Stat_TextChanged;
            tbINT.TextChanged += Stat_TextChanged;
            tbINT.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbREF.TextChanged += Stat_TextChanged;
            tbREF.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbTECH.TextChanged += Stat_TextChanged;
            tbTECH.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbCOOL.TextChanged += Stat_TextChanged;
            tbCOOL.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbATTR.TextChanged += Stat_TextChanged;
            tbATTR.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbLUCK.TextChanged += Stat_TextChanged;
            tbLUCK.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbMA.TextChanged += Stat_TextChanged;
            tbMA.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbBODY.TextChanged += Stat_TextChanged;
            tbBODY.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            tbEMP.TextChanged += Stat_TextChanged;
            tbEMP.KeyPress += new KeyPressEventHandler(Event_IgnoreLetters);
            
            rbMale.CheckedChanged += Gender_CheckedChanged;
            rbFemale.CheckedChanged += Gender_CheckedChanged;
            rbOther.CheckedChanged += Gender_CheckedChanged;
            
            //roles
            string[] roles = System.Enum.GetNames(typeof(eRole));
            cbRole.Items.AddRange(roles);
            cbRole.SelectedValueChanged += CbRole_SelectedValueChanged;

            // skill combobox
            eSkill[] skills = System.Enum.GetValues(typeof(eSkill)).Cast<eSkill>().ToArray();

            for (int x = 10; x < skills.Length; x++) //shouldnt select a role skill, start at 9
            {
                cbAllSkills.Items.Add(Sheet.eSkillToString(skills[x]));
            }
            //skill datagrid
            dgvSkills.CellValueChanged += DgvSkills_CellValueChanged;
            dgvSkills.CellClick += DgvSkills_CellClick;

            //items & weapons datagrid
   //         dgvItems.CellClick += DgvItems_CellClick;
   //         dgvWeapons.CellClick += DgvWeapons_CellClick;

            statusText.Text = "Ready...";
        }


        #region Events for Controls
        /*
        private void DgvWeapons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        */
        private void Gender_CheckedChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            if (rbMale.Checked)
                CurrentSheet.Gender = eGender.Male;
            else if (rbFemale.Checked)
                CurrentSheet.Gender = eGender.Female;
            else if (rbOther.Checked)
                CurrentSheet.Gender = eGender.Other;
        }
        
        private void DgvSkills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            string selected = dgvSkills.Rows[rowindex].Cells[0].Value.ToString();

            eSkill sk = Sheet.StringToeSkill(selected);
            int index = CurrentSheet.Skills.FindIndex(x => x.ID == sk);
            if (index != -1)
            {
                for (int x = 0; x < Sheet.AdditionalTextSkills.Length; x++)
                {
                    if ((int)Sheet.AdditionalTextSkills[x] == (int)sk)
                    {
                        tbAdditionalInfo.Enabled = true;
                        tbAdditionalInfo.Text = CurrentSheet.Skills[index].AdditionalData;
                        break;
                    }
                    tbAdditionalInfo.Enabled = false;
                    tbAdditionalInfo.Text = "";
                }
            }
            else
            {
                tbAdditionalInfo.Enabled = false;
                tbAdditionalInfo.Text = "";
            }

        }

        private void DgvSkills_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading)
                return;

            List<Skill> initial = Sheet.GetSkillPack(CurrentSheet.Role);
            freeSkillPoints = 0;
            profSkillPoints = 0;
            
            for (int x = 0; x < dgvSkills.Rows.Count; x++)
            {
                eSkill sk = Sheet.StringToeSkill(dgvSkills.Rows[x].Cells[0].Value.ToString());
                
                if((int)sk <= 9 && careerSkillLocked)
                {
                    dgvSkills.Rows[x].Cells[1].Value = CurrentSheet.Skills.Find(f => f.ID == sk).Value;
                    statusText.Text = "Career skill modification is locked. Remove any items";
                    InformSkillPoints();
                    return;
                }
                else
                { 
                    CurrentSheet.Skills.Find(f => f.ID == sk).Value = Int32.Parse(dgvSkills.Rows[x].Cells[1].Value.ToString());
                }

                if (initial.FindIndex(i => i.ID == sk) != -1)
                {
                    profSkillPoints += CurrentSheet.Skills.Find(f => f.ID == sk).Value;
                }
                else
                {
                    freeSkillPoints += CurrentSheet.Skills.Find(f => f.ID == sk).Value;
                }
                if((int)sk <= 9 && !careerSkillLocked)
                {
                    
                    CurrentSheet.InitialFunds = (Sheet.InitialFundsTable[CurrentSheet.Role][CurrentSheet.Skills.Find(f => f.ID == sk).Value] * 100)*CurrentSheet.MonthsWorked;
                    tbFunds.Text = CurrentSheet.InitialFunds.ToString();                    
                }
            }            
            InformSkillPoints();
        }

        private void CbRole_SelectedValueChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            if (hasChoosenRole)
            {
                DialogResult dlg = MessageBox.Show("Warning", "Choosing a new role will remove any skill or skill points!\nAre you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dlg == DialogResult.No)
                    return;
            }

            int roleindex = ((ComboBox)sender).SelectedIndex;
            dgvSkills.Rows.Clear();
            CurrentSheet.Skills.Clear();
            CurrentSheet.Skills = Sheet.GetSkillPack((eRole)roleindex);
            CurrentSheet.Role = (eRole)roleindex;
            CurrentSheet.MonthsWorked = rng.NextDouble() > 0.49999f ? 2 : 1;
            tbWorked.Text = CurrentSheet.MonthsWorked.ToString();
            foreach (Skill skill in CurrentSheet.Skills)
            {
                int last = dgvSkills.Rows.Add(Sheet.eSkillToString(skill.ID), skill.Value);
                dgvSkills.Rows[last].Cells[0].Style.Font = new Font(dgvSkills.Font.FontFamily, dgvSkills.Font.Size, FontStyle.Bold);
            }
            hasChoosenRole = true;
            btnAddSkill.Enabled = true;
            btnRemoveSkill.Enabled = true;
            btnShowItems.Enabled = true;
            btnShowWeapons.Enabled = true;
            btnItRemove.Enabled = true;
            btnWRemove.Enabled = true;
            statusText.Text = "Role selected";
        }

        private void Event_IgnoreLetters(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }
        private void Stat_TextChanged(object sender, EventArgs e)
        {
            SetBaseInformation();
        }

        #endregion
        #region Controls
        private void btnItRemove_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedCells == null || dgvItems.SelectedCells.Count < 1)
                return;
            int rowIndex = dgvItems.SelectedCells[0].RowIndex;

            if (dgvItems.Rows.Count <= rowIndex || dgvItems.Rows[rowIndex] == null)
                return;
            Item itemincell = EquipmentManager.DB.Items.Find(x => x.Name == dgvItems.Rows[rowIndex].Cells[0].Value.ToString());
            if (itemincell == null)
                return;
            if ((int)dgvItems.Rows[rowIndex].Cells[1].Value > 1)
            {
                int val = (int)dgvItems.Rows[rowIndex].Cells[1].Value;
                val--;
                dgvItems.Rows[rowIndex].Cells[1].Value = val;
            }
            else
            {
                dgvItems.Rows.RemoveAt(rowIndex);
            }
            CurrentSheet.InitialFunds += itemincell.Price;
            tbFunds.Text = CurrentSheet.InitialFunds.ToString();
            hasChangesPending = true;
        }

        private void btnShowItems_Click(object sender, EventArgs e)
        {
            if (itemsForm == null || itemsForm.IsDisposed)
            {
                itemsForm = new ItemListForm();
                itemsForm.InitializeItemWindow(this);
            }
            itemsForm.Show();
            
        }

        private void btnWRemove_Click(object sender, EventArgs e)
        {
            if (dgvWeapons.SelectedCells == null || dgvWeapons.SelectedCells.Count < 1)
                return;
            int rowIndex = dgvWeapons.SelectedCells[0].RowIndex;

            if (dgvWeapons.Rows.Count <= rowIndex || dgvWeapons.Rows[rowIndex] == null)
                return;
            Weapon itemincell = EquipmentManager.DB.Weapons.Find(x => x.Name == dgvWeapons.Rows[rowIndex].Cells[0].Value.ToString());
            if (itemincell == null)
                return;

            dgvWeapons.Rows.RemoveAt(rowIndex);
            CurrentSheet.InitialFunds += itemincell.Price;
            tbFunds.Text = CurrentSheet.InitialFunds.ToString();
            hasChangesPending = true;
        }

        private void btnShowWeapons_Click(object sender, EventArgs e)
        {
            if (weaponsForm == null || weaponsForm.IsDisposed)
            {
                weaponsForm = new ItemListForm();
                weaponsForm.InitializeWeaponWindow(this);
            }
            weaponsForm.Show();
        }
        private void btnAddSkill_Click(object sender, EventArgs e)
        {
            foreach (Skill s in CurrentSheet.Skills)
            {
                if ((int)s.ID == cbAllSkills.SelectedIndex + 10)
                {
                    statusText.Text = "Skill already added!";
                    return;
                }
            }
            Skill ns = new Skill() { ID = (eSkill)cbAllSkills.SelectedIndex + 10, Value = 0 };
            CurrentSheet.Skills.Add(ns);
            int last = dgvSkills.Rows.Add(Sheet.eSkillToString(ns.ID), ns.Value.ToString());

            if (Sheet.AdditionalTextSkills.Contains((int)ns.ID))
            {
                dgvSkills.Rows[last].Cells[0].Style.Font = new Font(dgvSkills.Font.FontFamily, dgvSkills.Font.Size, FontStyle.Italic);
                statusText.Text = "Added skill. This skill requires additional information";
            }
            else
            {
                statusText.Text = "Added skill";
            }
            InformSkillPoints();
            hasChangesPending = true;
        }
        private void btnRemoveSkill_Click(object sender, EventArgs e)
        {
            DataGridViewCell selectedcell = dgvSkills.SelectedCells[0];
            int rowindex = selectedcell.RowIndex;
            string selected = dgvSkills.Rows[rowindex].Cells[0].Value.ToString();

            eSkill sk = Sheet.StringToeSkill(selected);
            int index = CurrentSheet.Skills.FindIndex(x => x.ID == sk);
            List<Skill> initial = Sheet.GetSkillPack(CurrentSheet.Role);
            if (index != -1)
            {
                int isinitial = initial.FindIndex(i => i.ID == sk);
                if (isinitial < 0)
                {
                    CurrentSheet.Skills.RemoveAt(index);
                    dgvSkills.Rows.RemoveAt(rowindex);
                }
            }

            InformSkillPoints();
            hasChangesPending = true;
        }
        private void btnRoll_Click(object sender, EventArgs e)
        {
            if (rollForm == null || rollForm.Disposing || rollForm.IsDisposed)
                rollForm = new RollForm();
            rollForm.Show();
        }

        private void btnSetAdditionalInfo_Click(object sender, EventArgs e)
        {
            if (!tbAdditionalInfo.Enabled)
                return;
            DataGridViewCell selectedcell = dgvSkills.SelectedCells[0];
            int rowindex = selectedcell.RowIndex;
            string selected = dgvSkills.Rows[rowindex].Cells[0].Value.ToString();

            eSkill sk = Sheet.StringToeSkill(selected);
            int index = CurrentSheet.Skills.FindIndex(x => x.ID == sk);
            if (index != -1)
            {
                CurrentSheet.Skills[index].AdditionalData = tbAdditionalInfo.Text;
                tbAdditionalInfo.Text = "";
                statusText.Text = "Skill updated: " + selected + " - " + tbAdditionalInfo.Text;
            }
            InformSkillPoints();
            hasChangesPending = true;
        }

        #endregion

        #region MenuStrip
        private void loadXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasChangesPending)
            {
                DialogResult dlg = MessageBox.Show("There are unsaved changes. Do you want to save?", "Pending changes", MessageBoxButtons.YesNoCancel);
                if (dlg == DialogResult.Yes)
                    saveToolStripMenuItem_Click(null, null);
                else if (dlg == DialogResult.No)
                {
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "xml files (*.xml)|*.xml";
                        ofd.RestoreDirectory = true;
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            lastFilePath = ofd.FileName;
                            LoadExistingSheet(Sheet.LoadXML(ofd.FileName));
                        }
                    }
                }
                else if (dlg == DialogResult.Cancel)
                    return;
            }
            else
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "xml files (*.xml)|*.xml";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        lastFilePath = ofd.FileName;
                        LoadExistingSheet(Sheet.LoadXML(ofd.FileName));
                    }
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hasChangesPending)
            {
                DialogResult dlg = MessageBox.Show("There are unsaved changes. Do you want to save before closing?", "Pending changes", MessageBoxButtons.YesNoCancel);
                if (dlg == DialogResult.Yes)
                    saveToolStripMenuItem_Click(null, null);
                else if (dlg == DialogResult.No)
                    Application.Exit();
                else
                    return;
            }
            else
                Application.Exit();
        }

        private void expotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "pdf files(*.pdf) | *.pdf";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                PDFExport.ExportToPDF(CurrentSheet, sfd.FileName);
            }

        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbox = new AboutBox1();
            aboutbox.Show();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastFilePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xml files(*.xml) | *.xml";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Sheet.SaveXML(sfd.FileName, CurrentSheet);
                    hasChangesPending = false;
                    lastFilePath = sfd.FileName;
                }
            }
            else
            {
                Sheet.SaveXML(lastFilePath, CurrentSheet);
                hasChangesPending = false;
            }
            statusText.Text = "File saved correctly";
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files(*.xml) | *.xml";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Sheet.SaveXML(sfd.FileName, CurrentSheet);
                hasChangesPending = false;
                lastFilePath = sfd.FileName;
            }
            statusText.Text = "File saved correctly";
        }
        #endregion
        private void SetBaseInformation()
        {
            if (isLoading)
                return;

            if (!string.IsNullOrEmpty(tbName.Text))
            {
                CurrentSheet.Name = tbName.Text;
            }
            if (Int32.TryParse(tbINT.Text, out int intel))
            {
                intel = Clamp(intel, 1, 10);
                CurrentSheet.Stats.Int.Value = intel;
                tbINT.Text = intel.ToString();
            }
            if (Int32.TryParse(tbREF.Text, out int refl))
            {
                refl = Clamp(refl, 1, 10);
                CurrentSheet.Stats.Ref.Value = refl;
                tbREF.Text = refl.ToString();
                tbMODREF.Text = refl.ToString();
            }
            if (Int32.TryParse(tbTECH.Text, out int tech))
            {
                tech = Clamp(tech, 1, 10);
                CurrentSheet.Stats.Tech.Value = tech;
                tbTECH.Text = tech.ToString();
            }
            if (Int32.TryParse(tbCOOL.Text, out int cool))
            {
                cool = Clamp(cool, 1, 10);
                CurrentSheet.Stats.Cool.Value = cool;
                tbCOOL.Text = cool.ToString();
            }
            if (Int32.TryParse(tbATTR.Text, out int attr))
            {
                attr = Clamp(attr, 1, 10);
                CurrentSheet.Stats.Attr.Value = attr;
                tbATTR.Text = attr.ToString();
            }
            if (Int32.TryParse(tbLUCK.Text, out int luck))
            {
                luck = Clamp(luck, 1, 10);
                CurrentSheet.Stats.Luck.Value = luck;
                tbLUCK.Text = luck.ToString();
            }
            if (Int32.TryParse(tbMA.Text, out int ma))
            {
                ma = Clamp(ma, 1, 10);
                CurrentSheet.Stats.MA.Value = ma;
                tbMA.Text = ma.ToString();
                tbRun.Text = CurrentSheet.Stats.Run.ToString();
                tbLeap.Text = CurrentSheet.Stats.Leap.ToString();
            }
            if (Int32.TryParse(tbBODY.Text, out int body))
            {
                body = Clamp(body, 1, 16);
                CurrentSheet.Stats.Body.Value = body;
                tbBODY.Text = body.ToString();
                tbLift.Text = CurrentSheet.Stats.Lift.ToString();
            }
            if (Int32.TryParse(tbEMP.Text, out int emp))
            {
                emp = Clamp(emp, 1, 10);
                CurrentSheet.Stats.Emp.Value = emp;
                tbEMP.Text = emp.ToString();
                tbMODEMP.Text = emp.ToString();
            }
            if (rbMale.Checked)
            {
                CurrentSheet.Gender = eGender.Male;
            }
            else if (rbFemale.Checked)
            {
                CurrentSheet.Gender = eGender.Female;
            }
            else if(rbOther.Checked)
            {
                CurrentSheet.Gender = eGender.Other;
            }
            if (string.IsNullOrEmpty(tbAge.Text))
            {
                int age = 16 + rng.Next(2, 12);
                tbAge.Text = age.ToString();
            }
            else
            {
                if (Int32.TryParse(tbAge.Text, out int age))
                    CurrentSheet.Age = age;
            }
            hasChangesPending = true;
        }
        public void AddItem(Item i)
        {
            if (CurrentSheet.InitialFunds < i.Price && UseMoney)
            {
                statusText.Text = "Not enough founds!";
                return;
            }
            //Armor check, sourcebook p 101-102
            if (i is Armor armor)
            {
                if (!CanAddArmor(armor))
                    return;
            }
            //end armor check

            if (CurrentSheet.Items.Count > 0 || CurrentSheet.Weapons.Count > 0)
            {
                if (!careerSkillLocked)
                    careerSkillLocked = true;

            }
            else
            {
                if (careerSkillLocked)
                    careerSkillLocked = false;
            }
            int foundindex = -1;
            for (int x = 0; x < dgvItems.RowCount; x++)
            {
                if (dgvItems.Rows[x].Cells[0].Value.ToString().Equals(i.Name))
                {
                    foundindex = x;
                    break;
                }
            }
            if (foundindex >= 0) //exist an item in list
            {
                int val = (int)dgvItems.Rows[foundindex].Cells[1].Value;
                dgvItems.Rows[foundindex].Cells[1].Value = val + 1;
            }
            else
            {
                dgvItems.Rows.Add(i.Name, 1, i.Price);
            }
            if (UseMoney)
                CurrentSheet.InitialFunds -= i.Price;
            tbFunds.Text = CurrentSheet.InitialFunds.ToString();
            CurrentSheet.AddItem(i, 1);
            hasChangesPending = true;
            statusText.Text = "Added item " + i.Name;
        }
        public void AddWeapon(Weapon w)
        {
            if (CurrentSheet.InitialFunds < w.Price && UseMoney)
            {
                statusText.Text = "Not enough funds!";
                return;
            }

            if (CurrentSheet.Items.Count > 0 || CurrentSheet.Weapons.Count > 0)
            {
                if (!careerSkillLocked)
                    careerSkillLocked = true;
               
            }
            else
            {
                if (careerSkillLocked)
                    careerSkillLocked = false;
            }

            int lastAdded = dgvWeapons.Rows.Add(w.Name, w.CategoryString, w.Price);
            for (int x = 0; x < dgvWeapons.ColumnCount; x++)
            {
                dgvWeapons.Rows[lastAdded].Cells[x].ToolTipText = "WA: " + w.WA + " Co: " + w.Concealability + " Dam: " + w.DamageAndAmmo + " Shots: " + w.Shots + "/" + w.RoF;
            }
            if (UseMoney)
                CurrentSheet.InitialFunds -= w.Price;

            CurrentSheet.AddWeapon(w, 1);
            tbFunds.Text = CurrentSheet.InitialFunds.ToString();
            hasChangesPending = true;
            statusText.Text = "Added Weapon " + w.Name;
        }

        private void InformSkillPoints()
        {
            string res = "Available role points: ";
            res += profSkillPoints.ToString() + "/" + CurrentSheet.MaxProfessionalSkillPoints.ToString(); ;
            res += "\nAvailable free points:";
            res += freeSkillPoints.ToString() + "/" + CurrentSheet.MaxFreeSkillPoints.ToString();
            rtbSkillPoints.Text = res;

            if (!AreSkillsValid())
            {
                rtbSkillPoints.SelectionLength = rtbSkillPoints.TextLength;
                rtbSkillPoints.SelectionStart = 0;
                rtbSkillPoints.SelectionColor = Color.Red;
            }
        }
        public int Clamp(int value, int min, int max)
        {
            if (value > max)
                value = max;
            if (value < min)
                value = min;
            return value;
        }
        public bool AreSkillsValid()
        {

            if (freeSkillPoints != CurrentSheet.MaxFreeSkillPoints || freeSkillPoints == 0 || CurrentSheet.MaxFreeSkillPoints == 0)
                return false;
            if (profSkillPoints != CurrentSheet.MaxProfessionalSkillPoints || profSkillPoints == 0)
                return false;
            return true;
        }

        public bool CanAddArmor(Armor armor) //TODO: Check by location
        {
            int currentcount = 0;
            int currentsoft = 0;
            int currenthard = 0;
            for (int x = 0; x < CurrentSheet.Items.Count; x++)
            {
                if (CurrentSheet.Items[x].item is Armor inbag)
                {
                    if (inbag.Name == armor.Name)
                    {
                        statusText.Text = "Armor already purchased. Can't have two of the same.";
                        return false;
                    }
                    currentsoft += inbag.IsHard == false ? CurrentSheet.Items[x].quantity : 0;
                    currenthard += inbag.IsHard == true ? CurrentSheet.Items[x].quantity : 0;
                    currentcount++;
                }
            }
            if (currentcount >= 3)
            {
                statusText.Text = "You can't have more than three layers of armor";
                return false;
            }
            if (currentsoft >= 2 && !armor.IsHard)
            {
                statusText.Text = "No more than two layers of soft armor are allowed";
                return false;
            }
            if (currenthard >= 1 && armor.IsHard)
            {
                statusText.Text = "Only a single layer of hard armor is allowed";
                return false;
            }
            return true;
        }

        private void LoadExistingSheet(Sheet characterSheet)
        {
            isLoading = true;
            CurrentSheet = characterSheet;
            CurrentSheet.Gender = characterSheet.Gender;
            dgvSkills.Rows.Clear();
            dgvItems.Rows.Clear();
            dgvWeapons.Rows.Clear();
            tbName.Text = CurrentSheet.Name;
            cbRole.SelectedIndex = (int)CurrentSheet.Role;
            switch (CurrentSheet.Gender)
            {
                case eGender.Male:
                    rbFemale.Checked = false;
                    rbOther.Checked = false;
                    rbMale.Checked = true;
                    break;
                case eGender.Female:
                    rbMale.Checked = false;
                    rbOther.Checked = false;
                    rbFemale.Checked = true;
                    break;
                case eGender.Other:
                    rbFemale.Checked = false;
                    rbMale.Checked = false;
                    rbOther.Checked = true;
                    break;
            }
            tbAge.Text = CurrentSheet.Age.ToString();
            tbINT.Text = CurrentSheet.Stats.Int.Value.ToString();
            tbREF.Text = CurrentSheet.Stats.Ref.Value.ToString();
            tbMODREF.Text = CurrentSheet.Stats.Ref.Remaining.ToString();
            tbTECH.Text = CurrentSheet.Stats.Tech.Value.ToString();
            tbCOOL.Text = CurrentSheet.Stats.Cool.Value.ToString();
            tbATTR.Text = CurrentSheet.Stats.Attr.Value.ToString();
            tbLUCK.Text = CurrentSheet.Stats.Luck.Value.ToString();
            tbMA.Text = CurrentSheet.Stats.MA.Value.ToString();
            tbBODY.Text = CurrentSheet.Stats.Body.Value.ToString();
            tbMODEMP.Text = CurrentSheet.Stats.Emp.Remaining.ToString();
            tbEMP.Text = CurrentSheet.Stats.Emp.Value.ToString();
            tbRun.Text = CurrentSheet.Stats.Run.ToString();
            tbLeap.Text = CurrentSheet.Stats.Leap.ToString();
            tbLift.Text = CurrentSheet.Stats.Lift.ToString();

            List<Skill> initial = Sheet.GetSkillPack(CurrentSheet.Role);
            foreach(Skill t in CurrentSheet.Skills)
            {
                int last = dgvSkills.Rows.Add(Sheet.eSkillToString(t.ID), t.Value.ToString());

                int idx = initial.FindIndex(x => x.ID == t.ID);
                if(idx >= 0)
                {
                    profSkillPoints += t.Value;
                    dgvSkills.Rows[last].Cells[0].Style.Font = new Font(dgvSkills.Font.FontFamily, dgvSkills.Font.Size, FontStyle.Bold);
                }
                else
                {
                    freeSkillPoints += t.Value;
                }

                if (Sheet.AdditionalTextSkills.Contains((int)t.ID))
                {
                    dgvSkills.Rows[last].Cells[0].Style.Font = new Font(dgvSkills.Font.FontFamily, dgvSkills.Font.Size, FontStyle.Italic);
                }
            }

            foreach(Sheet.ItemTuple i in CurrentSheet.Items)
            {
                dgvItems.Rows.Add(i.item.Name, i.quantity, i.item.Price);
            }
            foreach (Sheet.WeaponTuple i in CurrentSheet.Weapons)
            {
                int lastAdded = dgvWeapons.Rows.Add(i.weapon.Name, i.weapon.CategoryString, i.quantity, i.weapon.Price);
                for (int x = 0; x < dgvWeapons.ColumnCount; x++)
                {
                    dgvWeapons.Rows[lastAdded].Cells[x].ToolTipText = "WA: " + i.weapon.WA + " Co: " + i.weapon.Concealability + " Dam: " + i.weapon.DamageAndAmmo + " Shots: " + i.weapon.Shots + "/" + i.weapon.RoF;
                }

            }
            tbWorked.Text = CurrentSheet.MonthsWorked.ToString();
            tbFunds.Text = CurrentSheet.InitialFunds.ToString();
            hasChoosenRole = true;
            btnAddSkill.Enabled = true;
            btnRemoveSkill.Enabled = true;
            btnShowItems.Enabled = true;
            btnShowWeapons.Enabled = true;
            btnItRemove.Enabled = true;
            btnWRemove.Enabled = true;
            isLoading = false;
            statusText.Text = "Sheet loaded! Happy trails!";
        }


    }
}
