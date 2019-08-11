using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;
using Label = System.Windows.Controls.Label;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace ShortcutLauncher
{

    [Serializable]
    public class ValueObject
    {
        public bool[] Allocation = new bool[16];
        public bool[] isFile = new bool[16];
        public string[] name = new string[16];
        public string[] iconPath = new string[16];
        public string[] infor = new string[16];

        public double left;
        public double top;
    }

    public partial class MainWindow : Window
    {
        private RegistryKey runRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        private ValueObject VO = new ValueObject();

        private NotifyIcon tray = new NotifyIcon();

        private List<Image> iconObject = new List<Image>();
        private List<Label> NameObject = new List<Label>();
        private List<Image> DeleteObject = new List<Image>();
        private void Init_iconObjects()
        {
            iconObject.Add(icon00);
            iconObject.Add(icon01);
            iconObject.Add(icon02);
            iconObject.Add(icon03);
            iconObject.Add(icon04);
            iconObject.Add(icon05);
            iconObject.Add(icon06);
            iconObject.Add(icon07);
            iconObject.Add(icon08);
            iconObject.Add(icon09);
            iconObject.Add(icon10);
            iconObject.Add(icon11);
            iconObject.Add(icon12);
            iconObject.Add(icon13);
            iconObject.Add(icon14);
            iconObject.Add(icon15);

            NameObject.Add(name00);
            NameObject.Add(name01);
            NameObject.Add(name02);
            NameObject.Add(name03);
            NameObject.Add(name04);
            NameObject.Add(name05);
            NameObject.Add(name06);
            NameObject.Add(name07);
            NameObject.Add(name08);
            NameObject.Add(name09);
            NameObject.Add(name10);
            NameObject.Add(name11);
            NameObject.Add(name12);
            NameObject.Add(name13);
            NameObject.Add(name14);
            NameObject.Add(name15);

            DeleteObject.Add(delete00);
            DeleteObject.Add(delete01);
            DeleteObject.Add(delete02);
            DeleteObject.Add(delete03);
            DeleteObject.Add(delete04);
            DeleteObject.Add(delete05);
            DeleteObject.Add(delete06);
            DeleteObject.Add(delete07);
            DeleteObject.Add(delete08);
            DeleteObject.Add(delete09);
            DeleteObject.Add(delete10);
            DeleteObject.Add(delete11);
            DeleteObject.Add(delete12);
            DeleteObject.Add(delete13);
            DeleteObject.Add(delete14);
            DeleteObject.Add(delete15);
        }
        private bool movement = false;
        private bool edit = false;


        public MainWindow()
        {
            InitializeComponent();
            Init_iconObjects();

            Register_StartProcess();

            //Save_Data();
            Load_Data();

            // Sync position
            Left = VO.left;
            Top = VO.top;

            Refresh_Icon();

            // Set tray function
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item3 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item4 = new System.Windows.Forms.MenuItem();

                item1.Index = 0;
                item1.Text = "위치 이동";
                item1.Click += (click, eClick) =>
                {
                    if (movement == false)
                    {
                        movement = true;
                        item1.Checked = true;
                    }
                    else
                    {
                        movement = false;
                        item1.Checked = false;
                    }

                    Move_Position();
                };

                item2.Index = 1;
                item2.Text = "아이콘 추가 / 삭제";
                item2.Click += (click, eClick) =>
                {
                    if (edit == false)
                    {
                        edit = true;
                        item2.Checked = true;

                        Show_EditIcons();
                    }
                    else
                    {
                        edit = false;
                        item2.Checked = false;

                        Refresh_Icon();
                    }
                };

                item3.Index = 2;
                item3.Text = "시작 프로그램 등록";
                if (runRegKey.GetValue("Shortcut_Launcher") == null)
                {
                    item3.Checked = false;
                }
                else
                {
                    item3.Checked = true;
                }

                item4.Index = 3;
                item4.Text = "종료";
                item4.Click += (click, eClick) => System.Windows.Application.Current.Shutdown();

                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);
                menu.MenuItems.Add(item4);

                tray.Icon = Properties.Resources.logo;
                tray.Visible = true;
                tray.ContextMenu = menu;
                tray.Text = "Shortcut_Launcher";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r Report Error", "Tray Register ERROR");
            }

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            if(movement) DragMove();
        }

        private void Register_StartProcess()
        {
            if (runRegKey.GetValue("Shortcut_Launcher") == null)
            {
                runRegKey.SetValue("Shortcut_Launcher", Environment.CurrentDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName);
            }
        }

        private void Load_Data()
        {
            BinaryFormatter readFormatter = new BinaryFormatter();
            FileStream readStream = new FileStream("data.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
             VO = (ValueObject)readFormatter.Deserialize(readStream);
            readStream.Close();
        }

        private void Save_Data()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("data.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, VO);
            stream.Close();
        }

        private void Refresh_Icon()
        {
            for (int i = 0; i < 16; i++)
            {
                // Hide unnecessary icon
                // Delete icon
                DeleteObject[i].Visibility = Visibility.Hidden;
                if (VO.Allocation[i] == false)
                {
                    // icon
                    iconObject[i].Visibility = Visibility.Hidden;
                    // name
                    NameObject[i].Visibility = Visibility.Hidden;
                }
                // Load and Apply icon files
                else
                {
                    // icon file
                    if (VO.isFile[i] == false || (VO.isFile[i] == true && File.Exists(VO.infor[i])))
                    {
                        try
                        {
                            iconObject[i].Visibility = Visibility.Visible;
                            iconObject[i].Source = new BitmapImage(new Uri(VO.iconPath[i], UriKind.RelativeOrAbsolute));

                            NameObject[i].Visibility = Visibility.Visible;
                            NameObject[i].Content = VO.name[i];
                        }
                        catch (FileNotFoundException) // Icon file path missing
                        {
                            iconObject[i].Visibility = Visibility.Visible;
                            iconObject[i].Source = new BitmapImage(new Uri(@"\Resources\Icon_Error.png", UriKind.RelativeOrAbsolute));

                            NameObject[i].Visibility = Visibility.Visible;
                            NameObject[i].Content = "IconFilePathMissing";
                        }
                    }
                    else // File path Missing 
                    {
                        iconObject[i].Visibility = Visibility.Visible;
                        iconObject[i].Source = new BitmapImage(new Uri(@"\Resources\Icon_Error.png", UriKind.RelativeOrAbsolute));

                        NameObject[i].Visibility = Visibility.Visible;
                        NameObject[i].Content = "FilePathMissing";
                    }
                }

            }
        }

        private void Show_EditIcons()
        {
            // Show Edit icons
            for (int i = 0; i < 16; i++)
            {
                if (VO.Allocation[i] == true)
                {
                    // Delete icons
                    DeleteObject[i].Visibility = Visibility.Visible;
                }
                else
                {
                    // Apply Addition icon
                    iconObject[i].Visibility = Visibility.Visible;
                    iconObject[i].Source = new BitmapImage(new Uri(@"\Resources\Icon_Plus.png", UriKind.RelativeOrAbsolute));
                }
            }

        }

        private void Move_Position()
        {
            if (movement == true)
            {
                Border.Visibility = Visibility.Visible;
                BorderRect1.Visibility = Visibility.Visible;
                BorderRect2.Visibility = Visibility.Visible;
                BorderRect3.Visibility = Visibility.Visible;
                BorderRect4.Visibility = Visibility.Visible;
            }
            else
            {
                Border.Visibility = Visibility.Hidden;
                BorderRect1.Visibility = Visibility.Hidden;
                BorderRect2.Visibility = Visibility.Hidden;
                BorderRect3.Visibility = Visibility.Hidden;
                BorderRect4.Visibility = Visibility.Hidden;

                VO.left = System.Windows.Application.Current.MainWindow.Left;
                VO.top = System.Windows.Application.Current.MainWindow.Top;
                Debug.WriteLine(VO.left);
                Debug.WriteLine(VO.top);

                Save_Data();
            }
        }

        private void Icon_OnClick(int index)
        {
            if (VO.Allocation[index] == true)
            {
                try
                {
                    Process.Start(VO.infor[index]);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    return;
                }
            }
            else
            {
                // open edit window
                EditWindow edit = new EditWindow(index);
                edit.Show();
            }
        }

        private void Delete_Icon(int index)
        {
            // Delete data of VO
            VO.Allocation[index] = false;
            VO.name[index] = null;
            VO.iconPath[index] = null;
            VO.infor[index] = null;


            Save_Data();

            Refresh_Icon();

            Show_EditIcons();
        }

        public void Add_NewIcon(int index, bool _isFile, string _name, string _iconPath, string _infor)
        {
            // Add new data to VO
            VO.Allocation[index] = true;
            VO.isFile[index] = _isFile;
            VO.name[index] = _name;
            VO.iconPath[index] = _iconPath;
            VO.infor[index] = _infor;

            Save_Data();

            Refresh_Icon();

            Show_EditIcons();
        }


        /// 
        /// COMPENENT EVENT
        /// 


        private void Icon00_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(0);
        }

        private void Icon01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(1);
        }

        private void Icon02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(2);
        }

        private void Icon03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(3);
        }

        private void Icon04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(4);
        }

        private void Icon05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(5);
        }

        private void Icon06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(6);
        }

        private void Icon07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(7);
        }

        private void Icon08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(8);
        }

        private void Icon09_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(9);
        }

        private void Icon10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(10);
        }

        private void Icon11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(11);
        }

        private void Icon12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(12);
        }

        private void Icon13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(13);
        }

        private void Icon14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(14);
        }

        private void Icon15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Icon_OnClick(15);
        }



        private void Delete00_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(0);
        }

        private void Delete01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(1);
        }

        private void Delete02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(2);
        }

        private void Delete03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(3);
        }

        private void Delete04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(4);
        }

        private void Delete05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(5);
        }

        private void Delete06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(6);
        }

        private void Delete07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(7);
        }

        private void Delete08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(8);
        }

        private void Delete09_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(9);
        }

        private void Delete10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(10);
        }

        private void Delete11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(11);
        }

        private void Delete12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(12);
        }

        private void Delete13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(13);
        }

        private void Delete14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(14);
        }

        private void Delete15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Delete_Icon(15);
        }
    }
}
