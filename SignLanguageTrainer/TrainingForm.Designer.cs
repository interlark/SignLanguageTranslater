namespace SignLanguageTrainer
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
            this.listBoxDactile = new System.Windows.Forms.ListBox();
            this.groupBoxDactil = new System.Windows.Forms.GroupBox();
            this.listViewGestures = new System.Windows.Forms.ListView();
            this.groupBoxGestures = new System.Windows.Forms.GroupBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxGesture = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.groupBoxDactil.SuspendLayout();
            this.groupBoxGestures.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGesture)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxDactile
            // 
            this.listBoxDactile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDactile.FormattingEnabled = true;
            this.listBoxDactile.Location = new System.Drawing.Point(3, 16);
            this.listBoxDactile.Name = "listBoxDactile";
            this.listBoxDactile.Size = new System.Drawing.Size(272, 403);
            this.listBoxDactile.TabIndex = 1;
            this.listBoxDactile.SelectedValueChanged += new System.EventHandler(this.listBoxDactile_SelectedValueChanged);
            // 
            // groupBoxDactil
            // 
            this.groupBoxDactil.Controls.Add(this.listBoxDactile);
            this.groupBoxDactil.Location = new System.Drawing.Point(15, 12);
            this.groupBoxDactil.Name = "groupBoxDactil";
            this.groupBoxDactil.Size = new System.Drawing.Size(278, 422);
            this.groupBoxDactil.TabIndex = 3;
            this.groupBoxDactil.TabStop = false;
            this.groupBoxDactil.Text = "Дактиль";
            // 
            // listViewGestures
            // 
            this.listViewGestures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewGestures.Location = new System.Drawing.Point(3, 16);
            this.listViewGestures.MultiSelect = false;
            this.listViewGestures.Name = "listViewGestures";
            this.listViewGestures.Size = new System.Drawing.Size(760, 403);
            this.listViewGestures.TabIndex = 4;
            this.listViewGestures.UseCompatibleStateImageBehavior = false;
            this.listViewGestures.View = System.Windows.Forms.View.List;
            this.listViewGestures.SelectedIndexChanged += new System.EventHandler(this.listViewGestures_SelectedIndexChanged);
            // 
            // groupBoxGestures
            // 
            this.groupBoxGestures.Controls.Add(this.listViewGestures);
            this.groupBoxGestures.Location = new System.Drawing.Point(312, 12);
            this.groupBoxGestures.Name = "groupBoxGestures";
            this.groupBoxGestures.Size = new System.Drawing.Size(766, 422);
            this.groupBoxGestures.TabIndex = 5;
            this.groupBoxGestures.TabStop = false;
            this.groupBoxGestures.Text = "Жесты";
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(1406, 392);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(105, 39);
            this.btnTrain.TabIndex = 6;
            this.btnTrain.Text = "Обучить";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxGesture);
            this.groupBox1.Location = new System.Drawing.Point(1101, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 358);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Предпросмотр";
            // 
            // pictureBoxGesture
            // 
            this.pictureBoxGesture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGesture.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxGesture.Name = "pictureBoxGesture";
            this.pictureBoxGesture.Size = new System.Drawing.Size(407, 339);
            this.pictureBoxGesture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxGesture.TabIndex = 0;
            this.pictureBoxGesture.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1104, 392);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(128, 39);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(1238, 392);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(162, 39);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "Добавить снимков";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // TrainingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 451);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.groupBoxGestures);
            this.Controls.Add(this.groupBoxDactil);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TrainingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Training Tool";
            this.Load += new System.EventHandler(this.TrainingForm_Load);
            this.groupBoxDactil.ResumeLayout(false);
            this.groupBoxGestures.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGesture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxDactile;
        private System.Windows.Forms.GroupBox groupBoxDactil;
        private System.Windows.Forms.ListView listViewGestures;
        private System.Windows.Forms.GroupBox groupBoxGestures;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxGesture;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button buttonAdd;
    }
}

