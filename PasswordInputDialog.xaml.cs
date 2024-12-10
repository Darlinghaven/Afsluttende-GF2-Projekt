using System.Windows;
using System.Text.RegularExpressions;

namespace Afsluttende_GF2_Projekt
{
    public partial class PasswordInputDialog : Window
    {
        public string NewPassword { get; set; }
        public bool IsConfirmed { get; private set; } = false;

        public PasswordInputDialog()
        {
            InitializeComponent();
        }

        private void PasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Password;
            if (IsValidPassword(password))
            {
                NewPassword = password;
                IsConfirmed = true;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Kodeordet opfylder ikke kravene. Prøv igen.");
            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            DialogResult = false;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            DialogResult = false;
            Close();
        }

        private void ShowHideButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Visibility == System.Windows.Visibility.Visible)
            {
                PasswordTextBox.Visibility = System.Windows.Visibility.Collapsed;
                PasswordTextBoxVisible.Visibility = System.Windows.Visibility.Visible;
                PasswordTextBoxVisible.Text = PasswordTextBox.Password;
            }
            else
            {
                PasswordTextBox.Visibility = System.Windows.Visibility.Visible;
                PasswordTextBoxVisible.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 10)
            {
                MessageBox.Show("Kodeordet skal være mindst 10 tegn langt.");
                return false;
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                MessageBox.Show("Kodeordet skal indeholde mindst ét lille bogstav.");
                return false;
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBox.Show("Kodeordet skal indeholde mindst ét stort bogstav.");
                return false;
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                MessageBox.Show("Kodeordet skal indeholde mindst ét tal.");
                return false;
            }

            return true;
        }

    }
}
