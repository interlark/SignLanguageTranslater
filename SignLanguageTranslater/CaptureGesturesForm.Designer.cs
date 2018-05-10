namespace SignLanguageTranslater
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
            this.groupBoxTranslatedGesture = new System.Windows.Forms.GroupBox();
            this.pictureBoxTranslatedGesture = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBoxCameraColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCameraColor)).BeginInit();
            this.groupBoxTranslatedGesture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTranslatedGesture)).BeginInit();
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
            // groupBoxTranslatedGesture
            // 
            this.groupBoxTranslatedGesture.Controls.Add(this.pictureBoxTranslatedGesture);
            this.groupBoxTranslatedGesture.Location = new System.Drawing.Point(984, 12);
            this.groupBoxTranslatedGesture.Name = "groupBoxTranslatedGesture";
            this.groupBoxTranslatedGesture.Size = new System.Drawing.Size(488, 448);
            this.groupBoxTranslatedGesture.TabIndex = 2;
            this.groupBoxTranslatedGesture.TabStop = false;
            this.groupBoxTranslatedGesture.Text = "Жест";
            // 
            // pictureBoxTranslatedGesture
            // 
            this.pictureBoxTranslatedGesture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxTranslatedGesture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTranslatedGesture.Location = new System.Drawing.Point(3, 16);
            this.pictureBoxTranslatedGesture.Name = "pictureBoxTranslatedGesture";
            this.pictureBoxTranslatedGesture.Size = new System.Drawing.Size(482, 429);
            this.pictureBoxTranslatedGesture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTranslatedGesture.TabIndex = 0;
            this.pictureBoxTranslatedGesture.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1338, 534);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(134, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // CaptureGesturesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 578);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBoxTranslatedGesture);
            this.Controls.Add(this.groupBoxCameraColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CaptureGesturesForm";
            this.Text = "Захват жестов";
            this.groupBoxCameraColor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCameraColor)).EndInit();
            this.groupBoxTranslatedGesture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTranslatedGesture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCameraColor;
        private System.Windows.Forms.PictureBox pictureBoxCameraColor;
        private System.Windows.Forms.GroupBox groupBoxTranslatedGesture;
        private System.Windows.Forms.PictureBox pictureBoxTranslatedGesture;
        private System.Windows.Forms.Button btnClose;
    }
}