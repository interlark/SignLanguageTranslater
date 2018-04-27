﻿namespace SignLanguageTrainer
{
    partial class TrainingForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBoxDactil = new System.Windows.Forms.ListBox();
            this.groupBoxConsole = new System.Windows.Forms.GroupBox();
            this.groupBoxDactil = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBoxGestures = new System.Windows.Forms.GroupBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupBoxConsole.SuspendLayout();
            this.groupBoxDactil.SuspendLayout();
            this.groupBoxGestures.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 16);
            this.textBox1.MaxLength = 0;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(1406, 160);
            this.textBox1.TabIndex = 0;
            // 
            // listBoxDactil
            // 
            this.listBoxDactil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDactil.FormattingEnabled = true;
            this.listBoxDactil.Location = new System.Drawing.Point(3, 16);
            this.listBoxDactil.Name = "listBoxDactil";
            this.listBoxDactil.Size = new System.Drawing.Size(272, 403);
            this.listBoxDactil.TabIndex = 1;
            // 
            // groupBoxConsole
            // 
            this.groupBoxConsole.Controls.Add(this.textBox1);
            this.groupBoxConsole.Location = new System.Drawing.Point(12, 440);
            this.groupBoxConsole.Name = "groupBoxConsole";
            this.groupBoxConsole.Size = new System.Drawing.Size(1412, 179);
            this.groupBoxConsole.TabIndex = 2;
            this.groupBoxConsole.TabStop = false;
            this.groupBoxConsole.Text = "Консоль";
            // 
            // groupBoxDactil
            // 
            this.groupBoxDactil.Controls.Add(this.listBoxDactil);
            this.groupBoxDactil.Location = new System.Drawing.Point(15, 12);
            this.groupBoxDactil.Name = "groupBoxDactil";
            this.groupBoxDactil.Size = new System.Drawing.Size(278, 422);
            this.groupBoxDactil.TabIndex = 3;
            this.groupBoxDactil.TabStop = false;
            this.groupBoxDactil.Text = "Дактиль";
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 16);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(760, 403);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // groupBoxGestures
            // 
            this.groupBoxGestures.Controls.Add(this.listView1);
            this.groupBoxGestures.Location = new System.Drawing.Point(312, 12);
            this.groupBoxGestures.Name = "groupBoxGestures";
            this.groupBoxGestures.Size = new System.Drawing.Size(766, 422);
            this.groupBoxGestures.TabIndex = 5;
            this.groupBoxGestures.TabStop = false;
            this.groupBoxGestures.Text = "Жесты";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(1430, 456);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(84, 163);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Обучить";
            this.btnTrain.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Location = new System.Drawing.Point(1101, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 358);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Предпросмотр";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(3, 16);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(407, 339);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1104, 392);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(189, 39);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(1323, 392);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(188, 39);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "Добавить снимков";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // TrainingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 631);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.groupBoxGestures);
            this.Controls.Add(this.groupBoxDactil);
            this.Controls.Add(this.groupBoxConsole);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TrainingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Tool";
            this.Load += new System.EventHandler(this.TrainingForm_Load);
            this.groupBoxConsole.ResumeLayout(false);
            this.groupBoxConsole.PerformLayout();
            this.groupBoxDactil.ResumeLayout(false);
            this.groupBoxGestures.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBoxDactil;
        private System.Windows.Forms.GroupBox groupBoxConsole;
        private System.Windows.Forms.GroupBox groupBoxDactil;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBoxGestures;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button buttonAdd;
    }
}

