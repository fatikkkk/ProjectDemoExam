using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectDemoExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        static string connectionString = @"Data Source=DESKTOP-91TJDSI;Initial Catalog=ExamPreparationDemo;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string message = "Invalid Credentials";
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("Select * from Users where UserLogin=@CustomerEmail", con);
                cmd.Parameters.AddWithValue("@CustomerEmail", tbLogin.Text.ToString());
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["UserPassword"].ToString().Equals(tbPass.Text.ToString(), StringComparison.InvariantCulture))
                    {
                        message = "1";
                        UserInfo.CustomerEmail = tbLogin.Text.ToString();
                        UserInfo.CustomerName = reader["UserSurname"].ToString() + reader["UserName"].ToString() + reader["UserPatronymic"].ToString();
                    }
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                message = ex.Message.ToString();
            }
            if (message == "1")
            {
                OrderWindow orderWindow = new OrderWindow();
                orderWindow.Show();
                this.Close();
            }
            else
                MessageBox.Show(message, "Info");

        }
    }
}
