namespace SignLanguageTrainer
{
    partial class CaptureGesturesForm
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
            this.groupBoxCameraColor = new System.Windows.Forms.GroupBox();
            this.pictureBoxCameraColor = new System.Windows.Forms.PictureBox();
            this.groupBoxDepthCamera = new System.Windows.Forms.GroupBox();
            this.pictureBoxDeepCamera = new System.Windows.Forms.PictureBox();
            this.groupBoxInfraredCamera = new System.Windows.Forms.GroupBox();
            this.pictureBoxInfraredCamera = new System.Windows.Forms.PictureBox();
            this.groupBoxGesture = new System.Windows.Forms.GroupBox();
            this.pictureBoxGesture = new System.Windows.Forms.PictureBox();
            this.btnAddGesture = new System.Windows.Forms.Button();
            this.groupBoxCameraColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCameraColor)).BeginInit();
            this.groupBoxDepthCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeepCamera)).BeginInit();
            this.groupBoxInfraredCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfraredCamera)).BeginInit();
            this.groupBoxGesture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGesture)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxCameraColor
            // 
            this.groupBoxCameraColor.Controls.Add(this.pictureBoxCameraColor);
            this.groupBoxCameraColor.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCameraColor.Name = "groupBoxCameraColor";
            this.groupBoxCameraColor.Size = new System.Drawing.Size(966, 559);
            this.groupBoxCameraColor.TabIndex = 0;
            this.groupBoxCameraColor.TabStop = false;
            this.groupBoxCameraColor.Text = "Камера";
            // 
            // pictureBoxCameraColor
            // 
            this.pictureBoxCameraColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCameraColor.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxCameraColor.Name = "pictureBoxCameraColor";
            this.pictureBoxCameraColor.Size = new System.Drawing.Size(960, 540);
            this.pictureBoxCameraColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCameraColor.TabIndex = 0;
            this.pictureBoxCameraColor.TabStop = false;
            // 
            // groupBoxDepthCamera
            // 
            this.groupBoxDepthCamera.Controls.Add(this.pictureBoxDeepCamera);
            this.groupBoxDepthCamera.Location = new System.Drawing.Point(999, 12);
            this.groupBoxDepthCamera.Name = "groupBoxDepthCamera";
            this.groupBoxDepthCamera.Size = new System.Drawing.Size(262, 231);
            this.groupBoxDepthCamera.TabIndex = 1;
            this.groupBoxDepthCamera.TabStop = false;
            this.groupBoxDepthCamera.Text = "Глубинная камера";
            // 
            // pictureBoxDeepCamera
            // 
            this.pictureBoxDeepCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxDeepCamera.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxDeepCamera.Name = "pictureBoxDeepCamera";
            this.pictureBoxDeepCamera.Size = new System.Drawing.Size(256, 212);
            this.pictureBoxDeepCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDeepCamera.TabIndex = 0;
            this.pictureBoxDeepCamera.TabStop = false;
            // 
            // groupBoxInfraredCamera
            // 
            this.groupBoxInfraredCamera.Controls.Add(this.pictureBoxInfraredCamera);
            this.groupBoxInfraredCamera.Location = new System.Drawing.Point(1286, 12);
            this.groupBoxInfraredCamera.Name = "groupBoxInfraredCamera";
            this.groupBoxInfraredCamera.Size = new System.Drawing.Size(262, 231);
            this.groupBoxInfraredCamera.TabIndex = 1;
            this.groupBoxInfraredCamera.TabStop = false;
            this.groupBoxInfraredCamera.Text = "Инфракрасная камера";
            // 
            // pictureBoxInfraredCamera
            // 
            this.pictureBoxInfraredCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxInfraredCamera.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxInfraredCamera.Name = "pictureBoxInfraredCamera";
            this.pictureBoxInfraredCamera.Size = new System.Drawing.Size(256, 212);
            this.pictureBoxInfraredCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInfraredCamera.TabIndex = 0;
            this.pictureBoxInfraredCamera.TabStop = false;
            // 
            // groupBoxGesture
            // 
            this.groupBoxGesture.Controls.Add(this.pictureBoxGesture);
            this.groupBoxGesture.Location = new System.Drawing.Point(999, 270);
            this.groupBoxGesture.Name = "groupBoxGesture";
            this.groupBoxGesture.Size = new System.Drawing.Size(546, 298);
            this.groupBoxGesture.TabIndex = 2;
            this.groupBoxGesture.TabStop = false;
            this.groupBoxGesture.Text = "Жест";
            // 
            // pictureBoxGesture
            // 
            this.pictureBoxGesture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGesture.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxGesture.Name = "pictureBoxGesture";
            this.pictureBoxGesture.Size = new System.Drawing.Size(540, 279);
            this.pictureBoxGesture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxGesture.TabIndex = 0;
            this.pictureBoxGesture.TabStop = false;
            // 
            // btnAddGesture
            // 
            this.btnAddGesture.Location = new System.Drawing.Point(661, 614);
            this.btnAddGesture.Name = "btnAddGesture";
            this.btnAddGesture.Size = new System.Drawing.Size(209, 50);
            this.btnAddGesture.TabIndex = 3;
            this.btnAddGesture.Text = "Добавить жест";
            this.btnAddGesture.UseVisualStyleBackColor = true;
            this.btnAddGesture.Click += new System.EventHandler(this.btnAddGesture_Click);
            // 
            // CaptureGesturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 711);
            this.Controls.Add(this.btnAddGesture);
            this.Controls.Add(this.groupBoxGesture);
            this.Controls.Add(this.groupBoxInfraredCamera);
            this.Controls.Add(this.groupBoxDepthCamera);
            this.Controls.Add(this.groupBoxCameraColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CaptureGesturesForm";
            this.Text = "Захват жестов";
            this.groupBoxCameraColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCameraColor)).EndInit();
            this.groupBoxDepthCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeepCamera)).EndInit();
            this.groupBoxInfraredCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfraredCamera)).EndInit();
            this.groupBoxGesture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGesture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCameraColor;
        private System.Windows.Forms.PictureBox pictureBoxCameraColor;
        private System.Windows.Forms.GroupBox groupBoxDepthCamera;
        private System.Windows.Forms.PictureBox pictureBoxDeepCamera;
        private System.Windows.Forms.GroupBox groupBoxInfraredCamera;
        private System.Windows.Forms.PictureBox pictureBoxInfraredCamera;
        private System.Windows.Forms.GroupBox groupBoxGesture;
        private System.Windows.Forms.PictureBox pictureBoxGesture;
        private System.Windows.Forms.Button btnAddGesture;
    }
}