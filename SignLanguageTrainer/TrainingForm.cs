﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SignLanguageTrainer
{
    public partial class TrainingForm : Form
    {
        public TrainingForm()
        {
            InitializeComponent();
            CreateFolderStructure();
        }

        private void CreateFolderStructure()
        {
            var rootDir = Settings.GetTrainFolderName();
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            var dirs = Settings.GetTranslatedFolders();
            foreach (var dir in dirs)
            {
                var path = Path.Combine(rootDir, dir.Key);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        private void TrainingForm_Load(object sender, EventArgs e)
        {
            //Fill listBoxDactile
            listBoxDactile.DataSource = new BindingSource(Settings.GetTranslatedFolders(), null);
            listBoxDactile.DisplayMember = "Value";
            listBoxDactile.ValueMember = "Key";


        }

        private void listBoxDactile_SelectedValueChanged(object sender, EventArgs e)
        {
            //Смена дактиля - заполняем listview
            var listBoxDactile = sender as ListBox;
            if (listBoxDactile != null && listBoxDactile.SelectedValue is string)
            {
                var gestureFolder = (string)listBoxDactile.SelectedValue;
                var path = Path.Combine(Settings.GetTrainFolderName(), gestureFolder);

                listViewGestures.Items.Clear();

                var files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    var item = new ListViewItem(Path.GetFileName(file));
                    item.Tag = file;
                    listViewGestures.Items.Add(item);
                }
            }

            btnDelete.Enabled = false;
        }

        private void listViewGestures_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Смена дактиля - заполняем listview
            var listViewGesture = sender as ListView;
            if (listViewGesture != null)
            {
                if(listViewGesture.SelectedItems.Count > 0)
                {
                    var selectedGesture = listViewGesture.SelectedItems[0];
                    var path = (string)selectedGesture.Tag;
                    if (path != null)
                    {
                        pictureBoxGesture.Image = new Bitmap(path);
                        btnDelete.Enabled = true;
                    }
                }
                else
                {
                    pictureBoxGesture.Image = null;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //listViewGesture тут имеет выбранный элемент
            var gestureItem = listViewGestures.SelectedItems[0];
            if (gestureItem.Tag != null) {
                pictureBoxGesture.Image = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete((string)gestureItem.Tag);
                gestureItem.Remove();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBoxDactile != null && listBoxDactile.SelectedValue is string)
            {
                var gestureFolder = (string)listBoxDactile.SelectedValue;
                var path = Path.Combine(Settings.GetTrainFolderName(), gestureFolder);
                var form = new CaptureGesturesForm();
                form.Path = path;
                form.ShowDialog();
            }
        }
    }
}
