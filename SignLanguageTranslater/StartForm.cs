using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignLanguageTranslater
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();



        }

        private void btnGraphBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tensorflow Graph File(*.pb) | *.pb";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxGraph.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnLabelsBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Tensorflow Labels File(*.txt) | *.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxLabels.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBoxGraph.Text) && File.Exists(textBoxLabels.Text))
            {
                var gestureTranslator = new CaptureGesturesForm();
                gestureTranslator.GraphPath = textBoxGraph.Text;
                gestureTranslator.LabelsPath = textBoxLabels.Text;
                gestureTranslator.SkipFrames = (int)numericUpDownSkipFrames.Value;
                gestureTranslator.ShowDialog();
            }
            else
            {
                MessageBox.Show("Укажите корректно пути до графа и символов", 
                    "Файл не найден", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDownSkipFrames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 48 || e.KeyChar > 57)
            {
                e.Handled = true;
            }
        }
    }
}
