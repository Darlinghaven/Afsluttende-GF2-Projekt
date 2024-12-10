using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Afsluttende_GF2_Projekt
{
    public partial class Password : UserControl
    {
        public Password()
        {
            InitializeComponent();
        }

        private void StartReset_Click(object sender, RoutedEventArgs e)
        {
            string employeeID = EmployeeIDTextBox.Text;
            if (string.IsNullOrEmpty(employeeID))
            {
                MessageBox.Show("Indtast venligst dit medarbejder ID.");
                return;
            }

            var result = MessageBox.Show($"Hej! {employeeID}\nTryk Yes, hvis du vil nulstille dit kodeord.\nTryk No / Cancel hvis du fortryder.", "Bekræft", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                string newPassword = PromptForNewPassword();
                if (newPassword != null)
                {
                    if (IsValidPassword(newPassword))  // Validerer kodeordet før det gemmes
                    {
                        MessageBox.Show($"Her er det nye kodeord til brugeren {employeeID}\n\nKodeord: {newPassword}\n\nTryk OK for at afslutte.", "Kodeord nulstillet", MessageBoxButton.OK);
                    }
                }
            }
        }

        private string PromptForNewPassword()
        {
            PasswordInputDialog inputDialog = new PasswordInputDialog();

            if (inputDialog.ShowDialog() == true && inputDialog.IsConfirmed)
            {
                return inputDialog.NewPassword;
            }
            return null;
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
                MessageBox.Show("Kodeordet skal indeholde mindst ét stor bogstav.");
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
