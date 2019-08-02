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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

namespace ShortcutLauncher
{
    enum icon
    {
        Null,
        File,
        Link
    }

    [Serializable]
    class ValueObject
    {
        public Enum[] state = Enumerable.Repeat<Enum>(icon.Null, 16).ToArray<Enum>();
        public string[] name = new string[16];
        public string[] iconPath = new string[16];
        public string[] filePath = new string[16];
        public string[] url = new string[16];

        public int x = 500;
        public int y = 500;
    }

    public partial class MainWindow : Window
    {
        ValueObject VO;

        NotifyIcon tray = new NotifyIcon();

        bool isMovable = false;

        EditWindow e = new EditWindow();


        public MainWindow()
        {
            InitializeComponent();

            //Make_UrlShortcut("naver", "www.naver.com");

            //Load_Value();

            // Set tray function
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item3 = new System.Windows.Forms.MenuItem();

                item1.Index = 0;
                item1.Text = "Move Position";
                item1.Click += (click, eClick) =>
                {
                    if (isMovable == false) item1.Checked = true;
                    else item1.Checked = false;

                    Move_Position(isMovable);
                };

                item2.Index = 1;
                item2.Text = "Add / Delete Icon";

                item3.Index = 2;
                item3.Text = "Exit";
                item3.Click += (click, eClick) => System.Windows.Application.Current.Shutdown();

                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);

                tray.Icon = Properties.Resources.cs;
                tray.Visible = true;
                tray.ContextMenu = menu;
                tray.Text = "ShortcutLauncher";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r Report Error", "Tray Register ERROR");
            }


            icon00.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon01.Source = new BitmapImage(new Uri(@"\Resources\plus.png", UriKind.RelativeOrAbsolute));
            icon02.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon03.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon04.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon05.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon06.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon07.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon08.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon09.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon10.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon11.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon12.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon13.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon14.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
            icon15.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));

            e.Show();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            if(isMovable) DragMove();
        }

        private void Load_Value()
        {
            IFormatter readFormatter = new BinaryFormatter();
            Stream readStream = new FileStream("data.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
            VO = readFormatter.Deserialize(readStream) as ValueObject;
            readStream.Close();
        }

        private void Save_Value()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("data.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, VO);
            stream.Close();
        }

        private void Move_Position(bool movability)
        {
            if (movability == false)
            {
                Border.Visibility = Visibility.Visible;
                BorderRect1.Visibility = Visibility.Visible;
                BorderRect2.Visibility = Visibility.Visible;
                BorderRect3.Visibility = Visibility.Visible;
                BorderRect4.Visibility = Visibility.Visible;
                isMovable = true;
            }
            else
            {
                Border.Visibility = Visibility.Hidden;
                BorderRect1.Visibility = Visibility.Hidden;
                BorderRect2.Visibility = Visibility.Hidden;
                BorderRect3.Visibility = Visibility.Hidden;
                BorderRect4.Visibility = Visibility.Hidden;
                isMovable = false;
            }
        }

        private void Icon_OnClick(int index)
        {
            if ((icon)VO.state[index] == icon.Null)
            {
                // open edit window

            }
            else if ((icon)VO.state[index] == icon.File)
            {
                Open_Shortcut(index);
            }
            else if ((icon)VO.state[index] == icon.Link)
            {
                Process.Start(VO.url[index]);
            }
        }

        private void Open_Shortcut(int index)
        {
            // Code to run shortcut
            Process.Start(VO.filePath[index]);
        }

        private void Delete_Icon(int index)
        {
            // Delete data of VO
            VO.state[index] = icon.Null;
            VO.name[index] = null;
            VO.iconPath[index] = null;
            VO.filePath[index] = null;
            VO.url[index] = null;

            // Save date 
            Save_Value();
        }

        private void Add_NewIcon()
        {
            // Add new data to VO

            // Save data
        }


        /// 
        /// COMPENENT EVENT
        /// 


        private void Icon01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon09_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Icon16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }





        private void Delete01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete09_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delete16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
