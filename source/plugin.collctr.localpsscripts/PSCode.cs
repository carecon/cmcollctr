﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Reflection;
using System.Configuration;

namespace plugin.collctr.localpsscripts
{
    public partial class PSCode : Form
    {
        public PSCode()
        {
            InitializeComponent();
        }

        public string PSHost = Properties.Settings.Default.PSHost;
        public string PSHostUser = Properties.Settings.Default.PSHostUser;
        public string PSHostPW = Properties.Settings.Default.PSHostPW;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PSCode_Load(object sender, EventArgs e)
        {
            string sCurrentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                ToolStripMenuItem tsm = new ToolStripMenuItem();
                LoadPSFolders(new DirectoryInfo(Path.Combine(sCurrentDir, "PSScripts_Host")), tsm);

                while (tsm.DropDownItems.Count > 0)
                {
                    tsCustomScripts.DropDownItems.Add(tsm.DropDownItems[0]);
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }


            tstbCM12Server.Text = Properties.Settings.Default.PSHost;
        }


        private void LoadPSFolders(DirectoryInfo Dir, ToolStripMenuItem Menu)
        {
            foreach (string sFile in Directory.GetFiles(Dir.FullName, "*.ps1", SearchOption.TopDirectoryOnly))
            {
                ToolStripMenuItem PSItem = new ToolStripMenuItem();
                PSItem.Text = Path.GetFileNameWithoutExtension(new FileInfo(sFile).FullName);
                PSItem.Tag = sFile;
                PSItem.Image = plugin.collctr.localpsscripts.Properties.Resources.Windows_PowerShell_icon; //CMHealthMon.Properties.Resources.Windows_PowerShell_icon;
                PSItem.Name = sFile;
                PSItem.Click += PSItem_Click;
                Menu.DropDownItems.Add(PSItem);

            }

            foreach (string sDir in Directory.GetDirectories(Dir.FullName, "*", SearchOption.TopDirectoryOnly))
            {
                ToolStripMenuItem PSFolder = new ToolStripMenuItem();
                PSFolder.Text = new DirectoryInfo(sDir).Name;
                PSFolder.Image = plugin.collctr.localpsscripts.Properties.Resources.Folder_16; //CMHealthMon.Properties.Resources.Folder_16;
                PSFolder.Name = sDir;
                
                LoadPSFolders(new DirectoryInfo(sDir), PSFolder);

                Menu.DropDownItems.Add(PSFolder);
            }

        }

        void PSItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sFile = ((ToolStripMenuItem)sender).Tag.ToString();
                sFile.ToString();

                StreamReader streamReader = new StreamReader(sFile, Encoding.UTF8);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                tbPSCode.Text = text;
            }
            catch { }
        }

        private void PSCode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }



        private void tstbCM12Server_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.PSHost = tstbCM12Server.Text;
            PSHost = Properties.Settings.Default.PSHost;
        }


    }
    


}
