using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using SynapseZAPI;
using System.Threading;

namespace Syn_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Stream stream = File.OpenRead("./Bin/Syntax/lua.xshd");
            XmlReader reader = new XmlTextReader(stream);
            LuaEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
            LuaEditor.Options.ShowSpaces = false;
        }

        public SynapseZAPI.SynapseZAPI synapseZAPI = new SynapseZAPI.SynapseZAPI();

        private void MainPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        } 

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            EditorTab.Visibility = Visibility.Visible;
            ScriptHubTab.Visibility = Visibility.Hidden;
        }

        private void Script_Hub_Click(object sender, RoutedEventArgs e)
        {
            EditorTab.Visibility = Visibility.Hidden;
            ScriptHubTab.Visibility = Visibility.Visible;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool flag = this.ScriptList.SelectedIndex != -1;
            if (flag)
            {
                LuaEditor.Text = File.ReadAllText("Scripts\\" + this.ScriptList.SelectedItem.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ScriptList.Items.Clear();
            foreach (FileInfo fileInfo in new DirectoryInfo("./Scripts").GetFiles("*.txt"))
            {
                this.ScriptList.Items.Add(fileInfo.Name);
            }

            foreach (FileInfo fileInfo2 in new DirectoryInfo("./Scripts").GetFiles("*.lua"))
            {
                this.ScriptList.Items.Add(fileInfo2.Name);
            }
        }

        private void ExecuteMain_Click(object sender, RoutedEventArgs e)
        {
            string scriptContent = LuaEditor.Text;
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), scriptContent);
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string fileContent = File.ReadAllText(filePath);
                    LuaEditor.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Lua Files (*.lua)|*.lua|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.FileName = "Untitled.lua";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    File.WriteAllText(filePath, LuaEditor.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Inject_Click(object sender, RoutedEventArgs e)
        {
            synapseZAPI.Inject(Directory.GetCurrentDirectory());
        }

        private void TopmostButton_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            string scriptContent = LuaEditor.Text;
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), scriptContent);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            LuaEditor.Text = "-- Cleared";
        }

        private void ReloadList_Click(object sender, RoutedEventArgs e)
        {
            this.ScriptList.Items.Clear();
            foreach (FileInfo fileInfo in new DirectoryInfo("./Scripts").GetFiles("*.txt"))
            {
                this.ScriptList.Items.Add(fileInfo.Name);
            }

            foreach (FileInfo fileInfo2 in new DirectoryInfo("./Scripts").GetFiles("*.lua"))
            {
                this.ScriptList.Items.Add(fileInfo2.Name);
            }
        }

        private void InfiniteYield_Click(object sender, RoutedEventArgs e)
        {
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), "loadstring(game:HttpGet(\"https://raw.githubusercontent.com/EdgeIY/infiniteyield/master/source\"))()");
        }

        private void DarkDex_Click(object sender, RoutedEventArgs e)
        {
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), "loadstring(game:HttpGet(\"https://raw.githubusercontent.com/Babyhamsta/RBLX_Scripts/main/Universal/BypassedDarkDexV3.lua\", true))()");
        }

        private void SimpleSpy_Click(object sender, RoutedEventArgs e)
        {
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), "loadstring(game:HttpGet(\"https://github.com/exxtremestuffs/SimpleSpySource/raw/master/SimpleSpy.lua\"))()");
        }

        private void UNCTest_Click(object sender, RoutedEventArgs e)
        {
            synapseZAPI.Execute(Directory.GetCurrentDirectory(), "loadstring(game:HttpGet(\"https://raw.githubusercontent.com/unified-naming-convention/NamingStandard/main/UNCCheckEnv.lua\"))()\r\n");
        }
    }
}
