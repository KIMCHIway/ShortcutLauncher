using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ShortcutLauncher
{
    /// <summary>
    /// EditWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditWindow : Window
    {
        private bool isFile = false;
        private int index;

        public EditWindow(int _index)
        {
            InitializeComponent();

            index = _index;
        }

        private void ApplyButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameBox.Text) || string.IsNullOrWhiteSpace(iconPathBlock.Text) || (string.IsNullOrWhiteSpace(filePathBlock.Text) && string.IsNullOrWhiteSpace(linkBox.Text)) )
            {
                MessageBox.Show("아이콘 정보를 모두 입력하세요", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (isFile)
                {
                    ((MainWindow)Application.Current.MainWindow).Add_NewIcon(index, isFile, nameBox.Text, iconPathBlock.Text, filePathBlock.Text);
                }
                else
                {
                    ((MainWindow)Application.Current.MainWindow).Add_NewIcon(index, isFile, nameBox.Text, iconPathBlock.Text, linkBox.Text);
                }

                Close();
            }
        }

        private void CancelButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void IconPathBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string path = GetName_FileExplorer();

            iconPathBlock.Text = path;
        }

        private void PathCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isFile = true;

            filePathBlock.Visibility = Visibility.Visible;
            filePathBlock.Text = "C:\\";
            filePathLine.Visibility = Visibility.Visible;

            linkCheckBox.IsChecked = false;
            linkBox.Text = string.Empty;
            linkBox.Visibility = Visibility.Hidden;
            linkLine.Visibility = Visibility.Hidden;
        }

        private void LinkCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isFile = false;

            linkBox.Visibility = Visibility.Visible;
            linkBox.Text = "www.";
            linkLine.Visibility = Visibility.Visible;

            pathCheckBox.IsChecked = false;
            filePathBlock.Text = string.Empty;
            filePathBlock.Visibility = Visibility.Hidden;
            filePathLine.Visibility = Visibility.Hidden;
        }

        private void FilePathBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string path = GetName_FileExplorer();

            filePathBlock.Text = path;
        }

        private string GetName_FileExplorer()
        {
            OpenFileDialog dig = new OpenFileDialog();

            bool? result = dig.ShowDialog();

            if (result == true) return dig.FileName;
            else return string.Empty;
        }
    }
}
