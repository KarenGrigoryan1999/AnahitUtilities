using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using IWshRuntimeLibrary;

namespace startup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        List<string> startup_paths = new List<string>();//Global collection, which contains all paths of elements
        List<string> startup_names = new List<string>();//Global collection, which contains all element key names
        List<string> values = new List<string>();//Global collection, which contains all full names of autoload files
        List<string> active_apps = new List<string>();//Global collection, which contains all full names of active elements
        string condition_on = "Switched on";//Text for condition "Switched on"
        string condition_off = "Switched off";//Text for condition "Switched off"
        //Function, if program is in the deactivated list and is the activated list, at the same time
        bool isdisabled_func(string programfile)
        {
            bool isdisabled = true;
            foreach (string appfile in active_apps)
            {
                if (programfile == appfile)
                    isdisabled = false;//if app is in the activated list, then it must be removed from deactivated list
            }
            return isdisabled;
        }
        //Function, which get elements of autoload
        void get_autorun()
        {
            DirectoryInfo directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun");
            DirectoryInfo directory2 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Run");
            DirectoryInfo directory3 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\RunOnce");
            DirectoryInfo directory4 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Start_menu");
            DirectoryInfo directory5 = new DirectoryInfo("C:\\ProgramData" + "\\WinTools_autorun");
            DirectoryInfo directory6 = new DirectoryInfo("C:\\ProgramData" + "\\WinTools_autorun\\Run");
            DirectoryInfo directory7 = new DirectoryInfo("C:\\ProgramData" + "\\WinTools_autorun\\RunOnce");
            DirectoryInfo directory8 = new DirectoryInfo("C:\\ProgramData" + "\\WinTools_autorun\\Start_menu");
            if (!directory.Exists) { directory.Create(); }
            if (!directory2.Exists) { directory2.Create(); }
            if (!directory3.Exists) { directory3.Create(); }
            if (!directory4.Exists) { directory4.Create(); }
            if (!directory5.Exists) { directory5.Create(); }
            if (!directory6.Exists) { directory6.Create(); }
            if (!directory7.Exists) { directory7.Create(); }
            if (!directory8.Exists) { directory8.Create(); }
            string value = "";
            string[] paths = new string[2];
            string[] regkeys = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Run").GetValueNames();
            string[] regkeys2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run").GetValueNames();
            string[] regkeys3 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run").GetValueNames();
            string[] regkeys4 = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValueNames();
            string[] regkeys5 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValueNames();
            string[] regkeys6 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValueNames();
            DirectoryInfo dir = new DirectoryInfo("C:\\programdata\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp");
            FileInfo[] files = dir.GetFiles();
            DirectoryInfo dir_user_startup = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup");
            FileInfo[] files_user_startup = dir_user_startup.GetFiles();
            files = files.Concat(files_user_startup).ToArray();            
            label2.Text = "Active items:";
            label3.Text = "All items:";
            //reset lists
            active_apps.Clear();
            values.Clear();
            startup_paths.Clear();
            startup_names.Clear();
            int i = 0;//reset counter
            listView1.Items.Clear();//reset listView1
            string remove_params(string app_path)
            {
                app_path = app_path.Replace('/', '"');
                if (app_path.Split('"').Length > 1)
                {
                    app_path = app_path.Split('"')[1];
                }
                else
                {
                    app_path = app_path.Split('"')[0];
                }
                return app_path;
            }
            //described below condition is performed only for x64 system
            if (Environment.Is64BitOperatingSystem){
            //load active apps for x64
            foreach (var reg_value in regkeys)
            {
                    try
                    {
                        value = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Run").GetValue(reg_value).ToString();
                        values.Add(value);
                        value = remove_params(value);
                        active_apps.Add(value);
                        FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                        imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                        ListViewItem item = new ListViewItem(file.ProductName, i);
                        item.SubItems.Add(file.CompanyName);
                        item.SubItems.Add(condition_on);
                        item.SubItems.Add(file.FileDescription);
                        listView1.Items.Add(item);
                        startup_paths.Add("HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Run");
                        startup_names.Add(reg_value);
                        i++;
                    }catch { continue; }
            }
                foreach (var reg_value in regkeys6)
                {
                    try {
                    value = Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValue(reg_value).ToString();
                    values.Add(value);
                    value = remove_params(value);
                    active_apps.Add(value);
                    FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                    imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                    ListViewItem item = new ListViewItem(file.ProductName, i);
                    item.SubItems.Add(file.CompanyName);
                    item.SubItems.Add(condition_on);
                    item.SubItems.Add(file.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add("HKLM\\SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce");
                    startup_names.Add(reg_value);
                    i++;
                }catch { continue; }
            }
            }
            //load active apps
            foreach (var reg_value in regkeys2)
            {
                try {
                value = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run").GetValue(reg_value).ToString();
                values.Add(value);
                value = remove_params(value);
                active_apps.Add(value);
                FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());                
                ListViewItem item = new ListViewItem(file.ProductName, i);
                item.SubItems.Add(file.CompanyName);
                item.SubItems.Add(condition_on);
                item.SubItems.Add(file.FileDescription);
                listView1.Items.Add(item);
                startup_paths.Add("HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                startup_names.Add(reg_value);
                i++;
                }
                catch { continue; }
            }
            foreach (var reg_value in regkeys3)
            {
                try {
                value = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run").GetValue(reg_value).ToString();
                values.Add(value);
                value = remove_params(value);
                active_apps.Add(value);
                FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                ListViewItem item = new ListViewItem(file.ProductName, i);
                item.SubItems.Add(file.CompanyName);
                item.SubItems.Add(condition_on);
                item.SubItems.Add(file.FileDescription);
                listView1.Items.Add(item);
                startup_paths.Add("HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                startup_names.Add(reg_value);
                i++;
                }
                catch { continue; }
            }
            foreach (var reg_value in regkeys4)
            {
                try {
                value = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValue(reg_value).ToString();
                values.Add(value);
                value = remove_params(value);
                active_apps.Add(value);
                FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                ListViewItem item = new ListViewItem(file.ProductName, i);
                item.SubItems.Add(file.CompanyName);
                item.SubItems.Add(condition_on);
                item.SubItems.Add(file.FileDescription);
                listView1.Items.Add(item);
                startup_paths.Add("HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce");
                startup_names.Add(reg_value);
                i++;
                }
                catch { continue; }
            }
            foreach (var reg_value in regkeys5)
            {
                try {
                value = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce").GetValue(reg_value).ToString();
                values.Add(value);
                value = remove_params(value);
                active_apps.Add(value);
                FileVersionInfo file = FileVersionInfo.GetVersionInfo(value);
                imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                ListViewItem item = new ListViewItem(file.ProductName, i);
                item.SubItems.Add(file.CompanyName);
                item.SubItems.Add(condition_on);
                item.SubItems.Add(file.FileDescription);
                listView1.Items.Add(item);
                startup_paths.Add("HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce");
                startup_names.Add(reg_value);
                i++;
                }
                catch { continue; }
            }
            foreach (var file in files)
            {
                try {
                string ex;
                string programfile;
                if (file.Extension == ".lnk")
                {
                    WshShell shell = new WshShell();
                    IWshShortcut link = (IWshShortcut)shell.CreateShortcut(file.FullName);
                    ex = link.TargetPath.Split('.')[1];
                    programfile = link.TargetPath;
                }
                else
                {
                    ex = file.Extension;
                    programfile = file.FullName;
                }
                if (ex == "exe" || ex == "bat")
                {
                    active_apps.Add(programfile);
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(programfile);
                    values.Add(programfile);
                    string programname = fileinfo.ProductName;//load program name
                    if (fileinfo.ProductName == null)//if no name, then must load file name
                    programname = file.Name;
                    ListViewItem item = new ListViewItem(programname, i);
                    item.SubItems.Add(fileinfo.CompanyName);
                    item.SubItems.Add(condition_on);
                    item.SubItems.Add(fileinfo.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add(file.DirectoryName);
                    startup_names.Add(file.Name);
                    i++;
                }
            }catch { continue; }
        }
            label2.Text += " " + i.ToString();
            //load not active apps
            foreach (var file in directory4.GetFiles().Concat(directory8.GetFiles()))
            {
                try {
                string ex;
                string programfile;
                if (file.Extension == ".lnk")
                {
                    WshShell shell = new WshShell();
                    IWshShortcut link = (IWshShortcut)shell.CreateShortcut(file.FullName);
                    ex = link.TargetPath.Split('.')[1];
                    programfile = link.TargetPath;
                }
                else
                {
                    ex = file.Extension;
                    programfile = file.FullName;
                }
                if (ex == "exe" || ex == "bat")
                {
                    bool isdisabled = isdisabled_func(programfile);
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(programfile);
                    string programname = fileinfo.ProductName;//load program name
                    if (fileinfo.ProductName == null)//if no name, then must load file name
                    programname = file.Name;
                    if (isdisabled) {
                    imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(programfile).ToBitmap());
                    ListViewItem item = new ListViewItem(programname, i);
                    item.SubItems.Add(fileinfo.CompanyName);
                    item.SubItems.Add(condition_off);
                    item.SubItems.Add(fileinfo.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add(file.DirectoryName);
                    startup_names.Add(file.Name);
                    i++;
                }
                }
            }catch { continue; }
        }
            foreach (var file in directory2.GetFiles())
            {
                try {
                StreamReader read = new StreamReader(file.FullName);
                value = read.ReadLine();
                value = value.Replace('"', ' ').Replace(".exe", ".exe@").Replace(".bat", ".bat@").Trim();
                value = value.Split('@')[0];
                FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(value);
                string programname = fileinfo.ProductName;//load program name
                if (fileinfo.ProductName == null)//if no name, then must load file name
                programname = file.Name;
                bool isdisabled = isdisabled_func(value);
                if (isdisabled)
                {
                    imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                    ListViewItem item = new ListViewItem(programname, i);
                    item.SubItems.Add(fileinfo.CompanyName);
                    item.SubItems.Add(condition_off);
                    item.SubItems.Add(fileinfo.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add(file.DirectoryName);
                    startup_names.Add(file.Name);
                    read.Close();
                    i++;
                }
            }catch { continue; }
        }
            foreach (var file in directory3.GetFiles())
            {
                try {
                StreamReader read = new StreamReader(file.FullName);
                value = read.ReadLine();
                value = remove_params(value);
                FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(value);
                string programname = fileinfo.ProductName;//load program name
                if (fileinfo.ProductName == null)//if no name, then must load file name
                programname = file.Name;
                bool isdisabled = isdisabled_func(value);
                if (isdisabled)
                {
                    imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                    ListViewItem item = new ListViewItem(programname, i);
                    item.SubItems.Add(fileinfo.CompanyName);
                    item.SubItems.Add(condition_off);
                    item.SubItems.Add(fileinfo.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add(file.DirectoryName);
                    startup_names.Add(file.Name);
                    read.Close();
                    i++;
                }
            }catch { continue; }
        }            
            foreach (var file in directory6.GetFiles().Concat(directory7.GetFiles()))
            {
                try {
                StreamReader read = new StreamReader(file.FullName);
                value = read.ReadLine();
                value = remove_params(value);
                FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(value);                
                string programname = fileinfo.ProductName;//load program name
                if (fileinfo.ProductName == null)//if no name, then must load file name
                programname = file.Name;
                bool isdisabled = isdisabled_func(value);
                if (isdisabled)
                {
                    imageList1.Images.Add("image", Icon.ExtractAssociatedIcon(value).ToBitmap());
                    ListViewItem item = new ListViewItem(programname, i);
                    item.SubItems.Add(fileinfo.CompanyName);
                    item.SubItems.Add(condition_off);
                    item.SubItems.Add(fileinfo.FileDescription);
                    listView1.Items.Add(item);
                    startup_paths.Add(file.DirectoryName);
                    startup_names.Add(file.Name);
                    read.Close();
                    i++;
                }
            }catch { continue; }
        }
            label3.Text += " " + i.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            get_autorun();//call the function for getting autoload elements
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);//set autosize for columns
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                button1ToolStripMenuItem.Enabled = true;
                if (listView1.SelectedItems[0].SubItems[2].Text == condition_on)
                    button1ToolStripMenuItem.Text = "Deactivate";
                else
                    button1ToolStripMenuItem.Text = "Activate";
            }
            catch { }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                string text = listView1.SelectedItems[0].Text;
            }
            catch {
                button1ToolStripMenuItem.Enabled = false;
                button2ToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2РасположениеФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            get_autorun();//call the function for getting autoload elements
        }

        private void button1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] temp = startup_paths[listView1.SelectedItems[0].Index].Split('\\');
            if (listView1.SelectedItems[0].SubItems[2].Text == condition_on)
            {                
                //if section HKCU
                if (temp[0] == "HKCU")
                {
                    //if section Run
                    if (temp[temp.Length-1] == "Run")
                {
                        try { Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Run\\"+ startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Run";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                }
                }
                if (temp[0] == "HKCU")
                {
                    //if section Run
                    if (temp[temp.Length - 1] == "RunOnce")
                    {
                        try { Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\RunOnce";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                    }
                }
                if (temp[0] == "HKLM")
                {
                    //if section Run
                    if (temp[temp.Length - 1] == "Run" && temp[2] != "WOW6432Node")
                    {
                        try { Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter("C:\\ProgramData" + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = "C:\\ProgramData" + "\\WinTools_autorun\\Run";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                    }
                    if (temp[temp.Length - 1] == "Run" && temp[2] == "WOW6432Node")
                    {
                        try { Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter("C:\\ProgramData" + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = "C:\\ProgramData" + "\\WinTools_autorun\\Run";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                    }
                    //if section RunOnce
                    if (temp[temp.Length - 1] == "RunOnce" && temp[2] != "WOW6432Node")
                    {
                        try { Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter("C:\\ProgramData" + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = "C:\\ProgramData" + "\\WinTools_autorun\\RunOnce";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                    }
                    if (temp[temp.Length - 1] == "RunOnce" && temp[2] == "WOW6432Node")
                    {
                        try { Registry.LocalMachine.OpenSubKey("SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true).DeleteValue(startup_names[listView1.SelectedItems[0].Index]); } catch { }
                        StreamWriter write = new StreamWriter("C:\\ProgramData" + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = "C:\\ProgramData" + "\\WinTools_autorun\\RunOnce";
                        write.WriteLine(values[listView1.SelectedItems[0].Index]);
                        write.Close();
                    }
                }
                //if section StartUp
                if (temp[temp.Length - 1] == "Startup" && temp[1] == "Users")
                {
                    FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + startup_names[listView1.SelectedItems[0].Index]);
                    file.MoveTo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Start_menu\\" + startup_names[listView1.SelectedItems[0].Index]);
                    startup_paths[listView1.SelectedItems[0].Index] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +"\\WinTools_autorun\\Start_menu";
                }
                if (temp[temp.Length - 1] == "StartUp" && temp[1] == "programdata")
                {
                    FileInfo file = new FileInfo("C:\\programdata\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\" + startup_names[listView1.SelectedItems[0].Index]);
                    file.MoveTo("C:\\ProgramData" + "\\WinTools_autorun\\Start_menu\\" + startup_names[listView1.SelectedItems[0].Index]);
                    startup_paths[listView1.SelectedItems[0].Index] = "C:\\ProgramData" + "\\WinTools_autorun\\Start_menu";
                }
                listView1.SelectedItems[0].SubItems[2].Text = condition_off;
            }
            else
            {
                //if deactivated
                if (temp[1] == "Users")
                {
                    //if section Run
                    if (temp[temp.Length - 1] == "Run")
                    {
                        string value;
                        using (StreamReader read = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]))
                        {
                            value = read.ReadLine();
                            read.Close();
                        }
                            Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run",true).SetValue(startup_names[listView1.SelectedItems[0].Index], value);
                        startup_paths[listView1.SelectedItems[0].Index] = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
                        FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.Delete();
                    }
                    //if section RunOnce
                    if (temp[temp.Length - 1] == "RunOnce")
                    {
                        string value;
                        using (StreamReader read = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]))
                        {
                            value = read.ReadLine();
                            read.Close();
                        }
                        Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true).SetValue(startup_names[listView1.SelectedItems[0].Index], value);
                        startup_paths[listView1.SelectedItems[0].Index] = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
                        FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.Delete();
                    }
                    //if section StartUp
                    if (temp[temp.Length - 1] == "Start_menu" && temp[1] == "Users")
                    {
                        FileInfo file = new FileInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\WinTools_autorun\\Start_menu\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.MoveTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
                    }                    
                }
                if (temp[1] == "ProgramData")
                {
                    //if section Run
                    if (temp[temp.Length - 1] == "Run")
                    {
                        string value;
                        using (StreamReader read = new StreamReader("C:\\ProgramData" + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]))
                        {
                            value = read.ReadLine();
                            read.Close();
                        }
                        Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue(startup_names[listView1.SelectedItems[0].Index], value);
                        startup_paths[listView1.SelectedItems[0].Index] = "HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
                        FileInfo file = new FileInfo("C:\\ProgramData" + "\\WinTools_autorun\\Run\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.Delete();
                    }
                    //if section RunOnce
                    if (temp[temp.Length - 1] == "RunOnce")
                    {
                        string value;
                        using (StreamReader read = new StreamReader("C:\\ProgramData" + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]))
                        {
                            value = read.ReadLine();
                            read.Close();
                        }
                        Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true).SetValue(startup_names[listView1.SelectedItems[0].Index], value);
                        startup_paths[listView1.SelectedItems[0].Index] = "HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
                        FileInfo file = new FileInfo("C:\\ProgramData" + "\\WinTools_autorun\\RunOnce\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.Delete();
                    }
                    if (temp[temp.Length - 1] == "Start_menu")
                    {
                        FileInfo file = new FileInfo("C:\\ProgramData" + "\\WinTools_autorun\\Start_menu\\" + startup_names[listView1.SelectedItems[0].Index]);
                        file.MoveTo("C:\\programdata\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp\\" + startup_names[listView1.SelectedItems[0].Index]);
                        startup_paths[listView1.SelectedItems[0].Index] = "C:\\programdata\\Microsoft\\Windows\\Start Menu\\Programs\\StartUp";
                    }
                }
                listView1.SelectedItems[0].SubItems[2].Text = condition_on;
            }
            }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string text = listView1.SelectedItems[0].Text;
            }
            catch
            {
                button1ToolStripMenuItem.Enabled = false;
            }
        }
    }
    }

