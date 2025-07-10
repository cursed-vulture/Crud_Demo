using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Crud_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> ids = new List<int>();
        public class Employee
        {
            public int ID { get; set; }
            public required string Name { get; set; }
            public required string Username { get; set; }
            public required string Address { get; set; }
            public int Age { get; set; }
        }

        public ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public MainWindow()
        {
            
            ids.Add(1);
            ids.Add(2);
            ids.Add(3);
            ids.Add(4);
            InitializeComponent();
            usersGrid.ItemsSource = Employees;          
            Employees.Add(new Employee { ID = ids[0], Name = "Gustavo", Username = "gut", Address = "Rua Arterial", Age = 23 });
            Employees.Add(new Employee { ID = ids[1], Name = "Miguel", Username = "mig", Address = "Rua Arterial", Age = 20 });
            Employees.Add(new Employee { ID = ids[2], Name = "Barros", Username = "bar", Address = "Rua Arterial", Age = 22 });
            Employees.Add(new Employee { ID = ids[3], Name = "Carranço", Username = "ran", Address = "Rua Arterial", Age = 21 });
        }
        public void Create(object sender, RoutedEventArgs e)
        {           
            try
            {
                if (int.TryParse(ID.Text, out int id) && int.TryParse(Age.Text, out int age))
                {
                    id = ids.LastOrDefault() + 1;
                    ids.Add(id);
                    Employees.Add(new Employee { ID = id, Name = Name.Text, Username = Username.Text, Address = Address.Text, Age = age });
                    ClearFields();
                    MessageBox.Show("Usuário criado com sucesso");
                }
                else
                {
                    if (int.TryParse(ID.Text, out id))
                    {
                        MessageBox.Show("Please enter a valid age");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid ID");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }                 
        }
        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Read(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID.Text != string.Empty)
                {
                    var filtered = Employees.Where(p => p.ID == int.Parse(ID.Text));
                    usersGrid.ItemsSource = filtered;
                }
                else if (Age.Text != string.Empty)
                {
                    var filtered = Employees.Where(p => p.Age == int.Parse(Age.Text));
                    usersGrid.ItemsSource = filtered;
                }
                else if (Name.Text != string.Empty)
                {
                    var filtered = Employees.Where(p => p.Name == Name.Text);
                    usersGrid.ItemsSource = filtered;
                }
                else if (Username.Text != string.Empty)
                {
                    var filtered = Employees.Where(p => p.Username == Username.Text);
                    usersGrid.ItemsSource = filtered;
                }
                else if (Address.Text != string.Empty)
                {
                    var filtered = Employees.Where(p => p.Address == Address.Text);
                    usersGrid.ItemsSource = filtered;
                }
                else
                {
                    usersGrid.ItemsSource = Employees;
                }
            }
            catch (Exception)
            {
                usersGrid.ItemsSource = Employees;
            }
        }
        private void ClearFields()
        {
            ID.Text = string.Empty;
            Age.Text = string.Empty;
            Address.Text = string.Empty;
            Name.Text = string.Empty;
            Username.Text = string.Empty;
        }
        private void ClearGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                usersGrid.ItemsSource = null;
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItem is Employee selected)
            {
                selected.ID = int.Parse(ID.Text);
                selected.Age = int.Parse(Age.Text);
                selected.Name = Name.Text;
                selected.Username = Username.Text;
                selected.Address = Address.Text;
                usersGrid.Items.Refresh();
                ClearFields();
            }
        }
        private void usersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersGrid.SelectedItem is Employee selected)
            {
                Name.Text = selected.Name;
                Username.Text= selected.Username;
                Address.Text= selected.Address;
                ID.Text = selected.ID.ToString();
                Age.Text = selected.Age.ToString();
            }          
        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItem is Employee selected)
            {
                Employees.Remove(selected);
                ClearFields();
            }
        }
    }
}