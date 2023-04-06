using System;
using ExpTreeLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using CustomExtensions;

namespace Bioware_Aurora_Engine_Utility
{
    public partial class AuroraEngine_Utility : Form
    {
        private XMLConfiguration xmlConfig;
        private static bool formLoading = true;
        private CShItem LastSelectedCSI;
        private readonly ManualResetEvent Event1 = new ManualResetEvent(true);
        private readonly DateTime testTime = new DateTime(1, 1, 1, 0, 0, 0);

        private ListBox _listBox;
        private TableLayoutRowStyleCollection rowStyleCollection;

        private List<TabPage> hiddenTabs = new List<TabPage>();
        public AuroraEngine_Utility()
        {
            InitializeComponent();
            MyInitialize();
            formLoading = false;
        }

        private void MyInitialize()
        {
            this.Text = Global.AssemblyTitle;
            rowStyleCollection = tableLayoutPanel1.RowStyles;
            rowStyleCollection[0].Height = 0;

            ERF_Routines.progressBar = progressBar1;

            hiddenTabs.Clear();
            hiddenTabs.Add(tpERF2);
            ERFTab.TabPages.Remove(tpERF2);
            hiddenTabs.Add(tpERF3);
            ERFTab.TabPages.Remove(tpERF3);

            LoadConfiguration();

            string str = Global._columnWidth.ToString();
            Uncheck_cmnuColumns();

            foreach (var item in cmnuColumns.DropDownItems)
            {
                ToolStripMenuItem tsmItem = (ToolStripMenuItem)item;

                if (tsmItem.Tag.ToString() == str)
                {
                    tsmItem.Checked = true;
                    break;
                }
            }
        }

        private void LoadConfiguration()
        {
            xmlConfig = XMLConfiguration.Load(XMLConfiguration.GetConfigFileName());

            Left = xmlConfig.window.main.Left;
            Top = xmlConfig.window.main.Top;
            Width = xmlConfig.window.main.Width;
            Height = xmlConfig.window.main.Height;

            switch (xmlConfig.window.main.State)
            {
                case "Minimized":
                    WindowState = FormWindowState.Minimized;
                    break;
                case "Maximized":
                    WindowState = FormWindowState.Maximized;
                    break;
                default:
                    WindowState = FormWindowState.Normal;
                    break;
            }
        }

        private void AuroraEngine_Utility_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (xmlConfig.IsDirty)
            {
                xmlConfig.Save(XMLConfiguration.GetConfigFileName());
            }
        }

        private void AuroraEngine_Utility_Resize(object sender, EventArgs e)
        {
            if (formLoading) return;

            if (this.WindowState == FormWindowState.Normal)
            {
                xmlConfig.window.main.Left = this.Left;
                xmlConfig.window.main.Top = this.Top;
                xmlConfig.window.main.Height = this.Height;
                xmlConfig.window.main.Width = this.Width;
            }
            xmlConfig.window.main.State = WindowState.ToString();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            char[] anyDelim = { '/', '\\' };

            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Aurora files (*.erf,*.mod,*.sav,*.hak)|*.erf;*.mod;*.sav;*.hak|All files (*.*)|*.*"
            };
            DialogResult dr = dlg.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                int i = dlg.FileName.LastIndexOfAny(anyDelim);
                string str = dlg.FileName;
                if (i < 0)
                { }
                else
                {
                    str = dlg.FileName.Substring(i + 1);

                }

                ERF_Routines.listBox = lboxERF;

                if (ERF_Routines.Open_File(dlg.FileName))
                {
                    rowStyleCollection[0].Height = 32;

                    if (!hiddenTabs.Contains(tpERF3))
                    {
                        hiddenTabs.Add(tpERF2);
                        hiddenTabs.Add(tpERF3);
                        ERFTab.TabPages.Remove(tpERF2);
                        ERFTab.TabPages.Remove(tpERF3);
                    }

                    lblERFStructure.Text = str;
                    lboxERF.Items.Clear();
                    txtERF1.Text = "";
                    tpERF1.Text = "...";

                    ERF_Routines.cancelLoad = false;
                    ERF_Routines.Process_Open_File();

                    rowStyleCollection[0].Height = 0;
                }
            }
        }

        private void lboxERF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _listBox = (ListBox)sender;

            Dump_SelectedItem(_listBox.SelectedItem);
        }

        private void Dump_SelectedItem(object selectedItem)
        {
            string strItem = _listBox.SelectedItem.ToString();
            string[] temp = strItem.Split(':');

            switch (temp[0])
            {
                case "ERFHeader":
                    if (!hiddenTabs.Contains(tpERF3))
                    {
                        hiddenTabs.Add(tpERF2);
                        hiddenTabs.Add(tpERF3);
                        ERFTab.TabPages.Remove(tpERF2);
                        ERFTab.TabPages.Remove(tpERF3);
                    }
                    ERF_Routines.Dump_ERFStructure((ERFHeader)selectedItem, tpERF1);
                    break;
                case "ERFLocalizedString":
                    if (!hiddenTabs.Contains(tpERF3))
                    {
                        hiddenTabs.Add(tpERF2);
                        hiddenTabs.Add(tpERF3);
                        ERFTab.TabPages.Remove(tpERF2);
                        ERFTab.TabPages.Remove(tpERF3);
                    }
                    ERF_Routines.Dump_ERFStructure((ERFLocalizedString)selectedItem, tpERF1);
                    break;
                case "ERFKeyList":
                    if (!ERFTab.TabPages.Contains(tpERF3))
                    {
                        ERFTab.TabPages.Insert(1, tpERF2);
                        ERFTab.TabPages.Insert(2, tpERF3);
                        hiddenTabs.Remove(tpERF2);
                        hiddenTabs.Remove(tpERF3);
                    }
                    TabPage[] tabPages = { tpERF1, tpERF2, tpERF3 }; // { tpERF1 };
                    ERF_Routines.Dump_ERFStructure((ERFKeyList)selectedItem, tabPages);
                    break;
                default:
                    break;
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            if (xmlConfig.window.IsDirty)
            {
                xmlConfig.Save(XMLConfiguration.GetConfigFileName());
            }

            Close();
        }

        private void cmnuColumns_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            Uncheck_cmnuColumns();

            item.Checked = true;

            Global._columnWidth = Convert.ToInt32(item.Tag);

            Dump_SelectedItem(_listBox.SelectedItem);
        }

        private void Uncheck_cmnuColumns(object cmnuColumn = null)
        {
            if (cmnuColumn == null)
            {
                cmnu32Columns.Checked = false;
                cmnu16Columns.Checked = false;
                cmnu8Columns.Checked = false;
                cmnu4Columns.Checked = false;
            }
            else
            {
                ((ToolStripMenuItem)cmnuColumn).Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ERF_Routines.cancelLoad = true;
        }
    }
}
