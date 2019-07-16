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

namespace ShortcutLauncher
{

    [Serializable]
    struct Data
    {
        public string[] iconPath;
        public string[] filePath;
        public string[] link;
    }

    public partial class MainWindow : Window
    {
        Data data;

        NotifyIcon tray = new NotifyIcon();

        bool isMovable = false;


        public MainWindow()
        {
            InitializeComponent();

            LoadData();

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

                    MovePosition(isMovable);
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
            icon16.Source = new BitmapImage(new Uri(@"C:\Users\ADMIN\Desktop\874964-tropical\png\F3.png", UriKind.RelativeOrAbsolute));
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            if(isMovable) DragMove();
        }

        private void LoadData()
        {

        }

        private void MovePosition(bool movability)
        {
            if (movability == false)
            {
                Border.Visibility = Visibility.Visible;
                isMovable = true;
            }
            else
            {
                Border.Visibility = Visibility.Hidden;
                isMovable = false;
            }
        }
    }
}
