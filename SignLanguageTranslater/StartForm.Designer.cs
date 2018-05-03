namespace SignLanguageTranslater
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxPB = new System.Windows.Forms.GroupBox();
            this.btnGraphBrowse = new System.Windows.Forms.Button();
            this.textBoxGraph = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLabelsBrowse = new System.Windows.Forms.Button();
            this.textBoxLabels = new System.Windows.Forms.TextBox();
            this.groupBoxTranslateMode = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonDactile = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBoxSkipFrames = new System.Windows.Forms.GroupBox();
            this.numericUpDownSkipFrames = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxTranslateMode.SuspendLayout();
            this.groupBoxSkipFrames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPB
            // 
            this.groupBoxPB.Controls.Add(this.btnGraphBrowse);
            this.groupBoxPB.Controls.Add(this.textBoxGraph);
            this.groupBoxPB.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPB.Name = "groupBoxPB";
            this.groupBoxPB.Size = new System.Drawing.Size(651, 63);
            this.groupBoxPB.TabIndex = 0;
            this.groupBoxPB.TabStop = false;
            this.groupBoxPB.Text = "Tensorflow граф";
            // 
            // btnGraphBrowse
            // 
            this.btnGraphBrowse.Location = new System.Drawing.Point(558, 25);
            this.btnGraphBrowse.Name = "btnGraphBrowse";
            this.btnGraphBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnGraphBrowse.TabIndex = 1;
            this.btnGraphBrowse.Text = "Обзор";
            this.btnGraphBrowse.UseVisualStyleBackColor = true;
            this.btnGraphBrowse.Click += new System.EventHandler(this.btnGraphBrowse_Click);
            // 
            // textBoxGraph
            // 
            this.textBoxGraph.Location = new System.Drawing.Point(15, 28);
            this.textBoxGraph.Name = "textBoxGraph";
            this.textBoxGraph.Size = new System.Drawing.Size(517, 20);
            this.textBoxGraph.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLabelsBrowse);
            this.groupBox1.Controls.Add(this.textBoxLabels);
            this.groupBox1.Location = new System.Drawing.Point(12, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(651, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tensorflow символы";
            // 
            // btnLabelsBrowse
            // 
            this.btnLabelsBrowse.Location = new System.Drawing.Point(558, 29);
            this.btnLabelsBrowse.Name = "btnLabelsBrowse";
            this.btnLabelsBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnLabelsBrowse.TabIndex = 1;
            this.btnLabelsBrowse.Text = "Обзор";
            this.btnLabelsBrowse.UseVisualStyleBackColor = true;
            this.btnLabelsBrowse.Click += new System.EventHandler(this.btnLabelsBrowse_Click);
            // 
            // textBoxLabels
            // 
            this.textBoxLabels.Location = new System.Drawing.Point(15, 32);
            this.textBoxLabels.Name = "textBoxLabels";
            this.textBoxLabels.Size = new System.Drawing.Size(517, 20);
            this.textBoxLabels.TabIndex = 0;
            // 
            // groupBoxTranslateMode
            // 
            this.groupBoxTranslateMode.Controls.Add(this.radioButton1);
            this.groupBoxTranslateMode.Controls.Add(this.radioButtonDactile);
            this.groupBoxTranslateMode.Enabled = false;
            this.groupBoxTranslateMode.Location = new System.Drawing.Point(12, 154);
            this.groupBoxTranslateMode.Name = "groupBoxTranslateMode";
            this.groupBoxTranslateMode.Size = new System.Drawing.Size(651, 70);
            this.groupBoxTranslateMode.TabIndex = 2;
            this.groupBoxTranslateMode.TabStop = false;
            this.groupBoxTranslateMode.Text = "Режим перевода";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(367, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(76, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Джестуно";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButtonDactile
            // 
            this.radioButtonDactile.AutoSize = true;
            this.radioButtonDactile.Checked = true;
            this.radioButtonDactile.Location = new System.Drawing.Point(181, 28);
            this.radioButtonDactile.Name = "radioButtonDactile";
            this.radioButtonDactile.Size = new System.Drawing.Size(69, 17);
            this.radioButtonDactile.TabIndex = 0;
            this.radioButtonDactile.TabStop = true;
            this.radioButtonDactile.Text = "Дактиль";
            this.radioButtonDactile.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(551, 246);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 34);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Запуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBoxSkipFrames
            // 
            this.groupBoxSkipFrames.Controls.Add(this.numericUpDownSkipFrames);
            this.groupBoxSkipFrames.Location = new System.Drawing.Point(12, 230);
            this.groupBoxSkipFrames.Name = "groupBoxSkipFrames";
            this.groupBoxSkipFrames.Size = new System.Drawing.Size(192, 50);
            this.groupBoxSkipFrames.TabIndex = 4;
            this.groupBoxSkipFrames.TabStop = false;
            this.groupBoxSkipFrames.Text = "Пропуск кадров";
            // 
            // numericUpDownSkipFrames
            // 
            this.numericUpDownSkipFrames.Location = new System.Drawing.Point(15, 19);
            this.numericUpDownSkipFrames.Name = "numericUpDownSkipFrames";
            this.numericUpDownSkipFrames.Size = new System.Drawing.Size(160, 20);
            this.numericUpDownSkipFrames.TabIndex = 0;
            this.numericUpDownSkipFrames.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownSkipFrames.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDownSkipFrames_KeyPress);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 292);
            this.Controls.Add(this.groupBoxSkipFrames);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBoxTranslateMode);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxPB);
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Распознавание жестов";
            this.groupBoxPB.ResumeLayout(false);
            this.groupBoxPB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxTranslateMode.ResumeLayout(false);
            this.groupBoxTranslateMode.PerformLayout();
            this.groupBoxSkipFrames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkipFrames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxTranslateMode;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButtonDactile;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnGraphBrowse;
        private System.Windows.Forms.TextBox textBoxGraph;
        private System.Windows.Forms.Button btnLabelsBrowse;
        private System.Windows.Forms.TextBox textBoxLabels;
        private System.Windows.Forms.GroupBox groupBoxSkipFrames;
        private System.Windows.Forms.NumericUpDown numericUpDownSkipFrames;
    }
}

