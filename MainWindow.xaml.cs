using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using Microsoft.Win32;


namespace prack6_TRPO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainClass _mainClass = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainClass();

        
        }

        private void Registration_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.RegistrDoctor();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
          

        }

        private void Login_Click (object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.LoginDoctor();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


  

        private void SaveNewPatient(object sender, RoutedEventArgs e)
        {

            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.SaveNewPatinet();
                }
            }
            catch(ArgumentException ex) 
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Savechange(object sender, RoutedEventArgs e)
        {

            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.SaveChangeInfoPatient();
                    MessageBox.Show("Данные сохранены", "Успех");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackInfo(object sender, RoutedEventArgs e)
        {

            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.ResetInfoPatient();
                    MessageBox.Show("Данные сброшены", "Успех");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchPatientInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is MainClass mc)
                {
                    mc.ShowInfoPatient();
                    MessageBox.Show(" Пациент найден", "Успех");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchPetientForChange_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                if (DataContext is MainClass mc)
                {
                    mc.SeatchPatinetForChang();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}