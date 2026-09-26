
namespace CP48
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbINT = new System.Windows.Forms.TextBox();
            this.tbMODREF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbREF = new System.Windows.Forms.TextBox();
            this.tbTECH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCOOL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBODY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbLUCK = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbATTR = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbLift = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbLeap = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbRun = new System.Windows.Forms.TextBox();
            this.tbEMP = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbMODEMP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dgvSkills = new System.Windows.Forms.DataGridView();
            this.Skill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbAllSkills = new System.Windows.Forms.ComboBox();
            this.btnAddSkill = new System.Windows.Forms.Button();
            this.btnRemoveSkill = new System.Windows.Forms.Button();
            this.rtbSkillPoints = new System.Windows.Forms.RichTextBox();
            this.btnRoll = new System.Windows.Forms.Button();
            this.tbAdditionalInfo = new System.Windows.Forms.TextBox();
            this.btnSetAdditionalInfo = new System.Windows.Forms.Button();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qtty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWeapons = new System.Windows.Forms.DataGridView();
            this.WeaponName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeaponType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tbWorked = new System.Windows.Forms.TextBox();
            this.tbFunds = new System.Windows.Forms.TextBox();
            this.btnShowItems = new System.Windows.Forms.Button();
            this.btnShowWeapons = new System.Windows.Forms.Button();
            this.btnItRemove = new System.Windows.Forms.Button();
            this.btnWRemove = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.label18 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeapons)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.expotToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadXMLToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadXMLToolStripMenuItem
            // 
            this.loadXMLToolStripMenuItem.Name = "loadXMLToolStripMenuItem";
            this.loadXMLToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadXMLToolStripMenuItem.Text = "&Load XML...";
            this.loadXMLToolStripMenuItem.Click += new System.EventHandler(this.loadXMLToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // expotToolStripMenuItem
            // 
            this.expotToolStripMenuItem.Name = "expotToolStripMenuItem";
            this.expotToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.expotToolStripMenuItem.Text = "&Export";
            this.expotToolStripMenuItem.Click += new System.EventHandler(this.expotToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Handle";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(53, 36);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 2;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(159, 39);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(48, 17);
            this.rbMale.TabIndex = 3;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(213, 39);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(59, 17);
            this.rbFemale.TabIndex = 4;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(278, 39);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(51, 17);
            this.rbOther.TabIndex = 5;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Role";
            // 
            // cbRole
            // 
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Location = new System.Drawing.Point(53, 66);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(121, 21);
            this.cbRole.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Age";
            // 
            // tbAge
            // 
            this.tbAge.Location = new System.Drawing.Point(229, 66);
            this.tbAge.Name = "tbAge";
            this.tbAge.Size = new System.Drawing.Size(30, 20);
            this.tbAge.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "INT";
            // 
            // tbINT
            // 
            this.tbINT.Location = new System.Drawing.Point(49, 104);
            this.tbINT.Name = "tbINT";
            this.tbINT.Size = new System.Drawing.Size(35, 20);
            this.tbINT.TabIndex = 11;
            // 
            // tbMODREF
            // 
            this.tbMODREF.Location = new System.Drawing.Point(121, 104);
            this.tbMODREF.Name = "tbMODREF";
            this.tbMODREF.ReadOnly = true;
            this.tbMODREF.Size = new System.Drawing.Size(35, 20);
            this.tbMODREF.TabIndex = 13;
            this.tbMODREF.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "REF";
            // 
            // tbREF
            // 
            this.tbREF.Location = new System.Drawing.Point(162, 104);
            this.tbREF.Name = "tbREF";
            this.tbREF.Size = new System.Drawing.Size(35, 20);
            this.tbREF.TabIndex = 14;
            // 
            // tbTECH
            // 
            this.tbTECH.Location = new System.Drawing.Point(240, 104);
            this.tbTECH.Name = "tbTECH";
            this.tbTECH.Size = new System.Drawing.Size(35, 20);
            this.tbTECH.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(203, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "TECH";
            // 
            // tbCOOL
            // 
            this.tbCOOL.Location = new System.Drawing.Point(315, 104);
            this.tbCOOL.Name = "tbCOOL";
            this.tbCOOL.Size = new System.Drawing.Size(35, 20);
            this.tbCOOL.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(278, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "COOL";
            // 
            // tbBODY
            // 
            this.tbBODY.Location = new System.Drawing.Point(279, 130);
            this.tbBODY.Name = "tbBODY";
            this.tbBODY.Size = new System.Drawing.Size(35, 20);
            this.tbBODY.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "BODY";
            // 
            // tbMA
            // 
            this.tbMA.Location = new System.Drawing.Point(199, 130);
            this.tbMA.Name = "tbMA";
            this.tbMA.Size = new System.Drawing.Size(35, 20);
            this.tbMA.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(168, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "MA";
            // 
            // tbLUCK
            // 
            this.tbLUCK.Location = new System.Drawing.Point(121, 130);
            this.tbLUCK.Name = "tbLUCK";
            this.tbLUCK.Size = new System.Drawing.Size(35, 20);
            this.tbLUCK.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(87, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "LUCK";
            // 
            // tbATTR
            // 
            this.tbATTR.Location = new System.Drawing.Point(49, 130);
            this.tbATTR.Name = "tbATTR";
            this.tbATTR.Size = new System.Drawing.Size(35, 20);
            this.tbATTR.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "ATTR";
            // 
            // tbLift
            // 
            this.tbLift.Location = new System.Drawing.Point(312, 156);
            this.tbLift.Name = "tbLift";
            this.tbLift.ReadOnly = true;
            this.tbLift.Size = new System.Drawing.Size(35, 20);
            this.tbLift.TabIndex = 36;
            this.tbLift.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(281, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Lift";
            // 
            // tbLeap
            // 
            this.tbLeap.Location = new System.Drawing.Point(237, 156);
            this.tbLeap.Name = "tbLeap";
            this.tbLeap.ReadOnly = true;
            this.tbLeap.Size = new System.Drawing.Size(35, 20);
            this.tbLeap.TabIndex = 34;
            this.tbLeap.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(206, 159);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Leap";
            // 
            // tbRun
            // 
            this.tbRun.Location = new System.Drawing.Point(159, 156);
            this.tbRun.Name = "tbRun";
            this.tbRun.ReadOnly = true;
            this.tbRun.Size = new System.Drawing.Size(35, 20);
            this.tbRun.TabIndex = 32;
            this.tbRun.TabStop = false;
            // 
            // tbEMP
            // 
            this.tbEMP.Location = new System.Drawing.Point(90, 156);
            this.tbEMP.Name = "tbEMP";
            this.tbEMP.Size = new System.Drawing.Size(35, 20);
            this.tbEMP.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(128, 159);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Run";
            // 
            // tbMODEMP
            // 
            this.tbMODEMP.Location = new System.Drawing.Point(49, 156);
            this.tbMODEMP.Name = "tbMODEMP";
            this.tbMODEMP.ReadOnly = true;
            this.tbMODEMP.Size = new System.Drawing.Size(35, 20);
            this.tbMODEMP.TabIndex = 29;
            this.tbMODEMP.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "EMP";
            // 
            // dgvSkills
            // 
            this.dgvSkills.AllowUserToAddRows = false;
            this.dgvSkills.AllowUserToDeleteRows = false;
            this.dgvSkills.AllowUserToResizeColumns = false;
            this.dgvSkills.AllowUserToResizeRows = false;
            this.dgvSkills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Skill,
            this.Value});
            this.dgvSkills.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSkills.Location = new System.Drawing.Point(15, 202);
            this.dgvSkills.MultiSelect = false;
            this.dgvSkills.Name = "dgvSkills";
            this.dgvSkills.RowHeadersVisible = false;
            this.dgvSkills.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvSkills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSkills.Size = new System.Drawing.Size(249, 156);
            this.dgvSkills.TabIndex = 37;
            // 
            // Skill
            // 
            this.Skill.HeaderText = "Skill";
            this.Skill.Name = "Skill";
            this.Skill.ReadOnly = true;
            this.Skill.Width = 175;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 50;
            // 
            // cbAllSkills
            // 
            this.cbAllSkills.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAllSkills.FormattingEnabled = true;
            this.cbAllSkills.Location = new System.Drawing.Point(271, 308);
            this.cbAllSkills.Name = "cbAllSkills";
            this.cbAllSkills.Size = new System.Drawing.Size(132, 21);
            this.cbAllSkills.TabIndex = 39;
            // 
            // btnAddSkill
            // 
            this.btnAddSkill.Enabled = false;
            this.btnAddSkill.Location = new System.Drawing.Point(338, 335);
            this.btnAddSkill.Name = "btnAddSkill";
            this.btnAddSkill.Size = new System.Drawing.Size(65, 23);
            this.btnAddSkill.TabIndex = 40;
            this.btnAddSkill.Text = "&Add";
            this.btnAddSkill.UseVisualStyleBackColor = true;
            this.btnAddSkill.Click += new System.EventHandler(this.btnAddSkill_Click);
            // 
            // btnRemoveSkill
            // 
            this.btnRemoveSkill.Enabled = false;
            this.btnRemoveSkill.Location = new System.Drawing.Point(270, 335);
            this.btnRemoveSkill.Name = "btnRemoveSkill";
            this.btnRemoveSkill.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveSkill.TabIndex = 41;
            this.btnRemoveSkill.Text = "&Remove";
            this.btnRemoveSkill.UseVisualStyleBackColor = true;
            this.btnRemoveSkill.Click += new System.EventHandler(this.btnRemoveSkill_Click);
            // 
            // rtbSkillPoints
            // 
            this.rtbSkillPoints.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbSkillPoints.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbSkillPoints.DetectUrls = false;
            this.rtbSkillPoints.Location = new System.Drawing.Point(15, 364);
            this.rtbSkillPoints.Name = "rtbSkillPoints";
            this.rtbSkillPoints.ReadOnly = true;
            this.rtbSkillPoints.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbSkillPoints.Size = new System.Drawing.Size(249, 35);
            this.rtbSkillPoints.TabIndex = 42;
            this.rtbSkillPoints.Text = "";
            // 
            // btnRoll
            // 
            this.btnRoll.Location = new System.Drawing.Point(270, 63);
            this.btnRoll.Name = "btnRoll";
            this.btnRoll.Size = new System.Drawing.Size(75, 23);
            this.btnRoll.TabIndex = 43;
            this.btnRoll.Text = "R&oll Stats";
            this.btnRoll.UseVisualStyleBackColor = true;
            this.btnRoll.Click += new System.EventHandler(this.btnRoll_Click);
            // 
            // tbAdditionalInfo
            // 
            this.tbAdditionalInfo.Enabled = false;
            this.tbAdditionalInfo.Location = new System.Drawing.Point(271, 202);
            this.tbAdditionalInfo.Name = "tbAdditionalInfo";
            this.tbAdditionalInfo.Size = new System.Drawing.Size(132, 20);
            this.tbAdditionalInfo.TabIndex = 44;
            // 
            // btnSetAdditionalInfo
            // 
            this.btnSetAdditionalInfo.Location = new System.Drawing.Point(338, 228);
            this.btnSetAdditionalInfo.Name = "btnSetAdditionalInfo";
            this.btnSetAdditionalInfo.Size = new System.Drawing.Size(65, 23);
            this.btnSetAdditionalInfo.TabIndex = 45;
            this.btnSetAdditionalInfo.Text = "S&et";
            this.btnSetAdditionalInfo.UseVisualStyleBackColor = true;
            this.btnSetAdditionalInfo.Click += new System.EventHandler(this.btnSetAdditionalInfo_Click);
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Qtty,
            this.Price});
            this.dgvItems.Location = new System.Drawing.Point(415, 63);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.Size = new System.Drawing.Size(437, 84);
            this.dgvItems.TabIndex = 46;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 200;
            // 
            // Qtty
            // 
            this.Qtty.HeaderText = "Qtty";
            this.Qtty.Name = "Qtty";
            this.Qtty.ReadOnly = true;
            this.Qtty.Width = 50;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 75;
            // 
            // dgvWeapons
            // 
            this.dgvWeapons.AllowUserToAddRows = false;
            this.dgvWeapons.AllowUserToDeleteRows = false;
            this.dgvWeapons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeapons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WeaponName,
            this.WeaponType,
            this.WPrice});
            this.dgvWeapons.Location = new System.Drawing.Point(415, 186);
            this.dgvWeapons.Name = "dgvWeapons";
            this.dgvWeapons.ReadOnly = true;
            this.dgvWeapons.RowHeadersVisible = false;
            this.dgvWeapons.Size = new System.Drawing.Size(437, 84);
            this.dgvWeapons.TabIndex = 47;
            // 
            // WeaponName
            // 
            this.WeaponName.HeaderText = "Weapon Name";
            this.WeaponName.Name = "WeaponName";
            this.WeaponName.ReadOnly = true;
            this.WeaponName.Width = 200;
            // 
            // WeaponType
            // 
            this.WeaponType.HeaderText = "Type";
            this.WeaponType.Name = "WeaponType";
            this.WeaponType.ReadOnly = true;
            // 
            // WPrice
            // 
            this.WPrice.HeaderText = "Price";
            this.WPrice.Name = "WPrice";
            this.WPrice.ReadOnly = true;
            this.WPrice.Width = 75;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(685, 39);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 13);
            this.label16.TabIndex = 48;
            this.label16.Text = "Available Funds";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(415, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 49;
            this.label17.Text = "Worked for";
            // 
            // tbWorked
            // 
            this.tbWorked.Location = new System.Drawing.Point(481, 36);
            this.tbWorked.Name = "tbWorked";
            this.tbWorked.ReadOnly = true;
            this.tbWorked.Size = new System.Drawing.Size(27, 20);
            this.tbWorked.TabIndex = 50;
            // 
            // tbFunds
            // 
            this.tbFunds.Location = new System.Drawing.Point(773, 36);
            this.tbFunds.Name = "tbFunds";
            this.tbFunds.ReadOnly = true;
            this.tbFunds.Size = new System.Drawing.Size(79, 20);
            this.tbFunds.TabIndex = 51;
            // 
            // btnShowItems
            // 
            this.btnShowItems.Enabled = false;
            this.btnShowItems.Location = new System.Drawing.Point(776, 156);
            this.btnShowItems.Name = "btnShowItems";
            this.btnShowItems.Size = new System.Drawing.Size(75, 23);
            this.btnShowItems.TabIndex = 52;
            this.btnShowItems.Text = "Items...";
            this.btnShowItems.UseVisualStyleBackColor = true;
            this.btnShowItems.Click += new System.EventHandler(this.btnShowItems_Click);
            // 
            // btnShowWeapons
            // 
            this.btnShowWeapons.Enabled = false;
            this.btnShowWeapons.Location = new System.Drawing.Point(775, 277);
            this.btnShowWeapons.Name = "btnShowWeapons";
            this.btnShowWeapons.Size = new System.Drawing.Size(75, 23);
            this.btnShowWeapons.TabIndex = 53;
            this.btnShowWeapons.Text = "Weapons...";
            this.btnShowWeapons.UseVisualStyleBackColor = true;
            this.btnShowWeapons.Click += new System.EventHandler(this.btnShowWeapons_Click);
            // 
            // btnItRemove
            // 
            this.btnItRemove.Enabled = false;
            this.btnItRemove.Location = new System.Drawing.Point(695, 157);
            this.btnItRemove.Name = "btnItRemove";
            this.btnItRemove.Size = new System.Drawing.Size(75, 23);
            this.btnItRemove.TabIndex = 54;
            this.btnItRemove.Text = "Remove";
            this.btnItRemove.UseVisualStyleBackColor = true;
            this.btnItRemove.Click += new System.EventHandler(this.btnItRemove_Click);
            // 
            // btnWRemove
            // 
            this.btnWRemove.Enabled = false;
            this.btnWRemove.Location = new System.Drawing.Point(692, 276);
            this.btnWRemove.Name = "btnWRemove";
            this.btnWRemove.Size = new System.Drawing.Size(75, 23);
            this.btnWRemove.TabIndex = 55;
            this.btnWRemove.Text = "Remove";
            this.btnWRemove.UseVisualStyleBackColor = true;
            this.btnWRemove.Click += new System.EventHandler(this.btnWRemove_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 418);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 56;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(34, 17);
            this.statusText.Text = "         ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(514, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 57;
            this.label18.Text = "months";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 440);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnWRemove);
            this.Controls.Add(this.btnItRemove);
            this.Controls.Add(this.btnShowWeapons);
            this.Controls.Add(this.btnShowItems);
            this.Controls.Add(this.tbFunds);
            this.Controls.Add(this.tbWorked);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dgvWeapons);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.btnSetAdditionalInfo);
            this.Controls.Add(this.tbAdditionalInfo);
            this.Controls.Add(this.btnRoll);
            this.Controls.Add(this.rtbSkillPoints);
            this.Controls.Add(this.btnRemoveSkill);
            this.Controls.Add(this.btnAddSkill);
            this.Controls.Add(this.cbAllSkills);
            this.Controls.Add(this.dgvSkills);
            this.Controls.Add(this.tbLift);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbLeap);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbRun);
            this.Controls.Add(this.tbEMP);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbMODEMP);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbBODY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbMA);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbLUCK);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbATTR);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbCOOL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbTECH);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbREF);
            this.Controls.Add(this.tbMODREF);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbINT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbAge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbOther);
            this.Controls.Add(this.rbFemale);
            this.Controls.Add(this.rbMale);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CP 2020 Character Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeapons)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbINT;
        private System.Windows.Forms.TextBox tbMODREF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbREF;
        private System.Windows.Forms.TextBox tbTECH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCOOL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbBODY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbMA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbLUCK;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbATTR;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbLift;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbLeap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbRun;
        private System.Windows.Forms.TextBox tbEMP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbMODEMP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dgvSkills;
        private System.Windows.Forms.DataGridViewTextBoxColumn Skill;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ComboBox cbAllSkills;
        private System.Windows.Forms.Button btnAddSkill;
        private System.Windows.Forms.Button btnRemoveSkill;
        private System.Windows.Forms.RichTextBox rtbSkillPoints;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.ToolStripMenuItem loadXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expotToolStripMenuItem;
        private System.Windows.Forms.TextBox tbAdditionalInfo;
        private System.Windows.Forms.Button btnSetAdditionalInfo;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qtty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridView dgvWeapons;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbWorked;
        private System.Windows.Forms.TextBox tbFunds;
        private System.Windows.Forms.Button btnShowItems;
        private System.Windows.Forms.Button btnShowWeapons;
        private System.Windows.Forms.Button btnItRemove;
        private System.Windows.Forms.Button btnWRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeaponName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeaponType;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPrice;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusText;
        private System.Windows.Forms.Label label18;
    }
}

