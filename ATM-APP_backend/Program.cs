using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ATMApp
{
    class Program
    {


        private static int _pin = 1234;
        private static long _ballance = 5000000;
        private static long _Updatedballance;

        private static int _newPinForLog;
        private static int _depositIn;
        
        
        // Helper static functions
        public static string GetLatestPinRecord(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "file not found"; // File doesn't exist
            }

            // ყველაფრის წაკითხვა ფაილში
            string[] lines = File.ReadAllLines(filePath);

            string pinCode = "1234";
            var latestRecord = lines.LastOrDefault(line => line.StartsWith("NEW PIN CODE IS"));

            if (!string.IsNullOrEmpty(latestRecord))
            {
                string[] parts = latestRecord.Split(':');
                if (parts.Length >= 2)
                {
                    pinCode = parts[1].Trim();
                }
            }

            return pinCode;
        }
    
        public static long GetBalance()
        {
            return _ballance;
        }
        public static long GetUpdatedBalance()
        {
            return _Updatedballance;
        }

        public static int GetPin()
        {
            string latestPin;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string bankFolderPath = Path.Combine(desktopPath, "MY-Bank");
            if (Directory.Exists(bankFolderPath))
            {
                string subFolderName = $"Operations-on{DateTime.Now.ToString("yyyyMMdd_HH")}";
                string subFolderPath = Path.Combine(bankFolderPath, subFolderName);

                if (Directory.Exists(subFolderPath))
                {

                    string filePath = Path.Combine(subFolderPath, "log-operations.txt");

                    // Get the latest PIN record (GetLatestPinRecord) ფუნქციიდან
                    latestPin = GetLatestPinRecord(filePath);

                    try
                    {
                        int.TryParse(latestPin,out int pinFromLogs);
                        _pin = pinFromLogs;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" error : {ex.Message}");
                    }

                }


            }
            return _pin;
        }
        public static void UpdateBalance(long newBalance)
        {
            _Updatedballance = newBalance;
            _ballance = newBalance;
        }
        public static int GetNewPinForLog()
        {
            return _newPinForLog;
        }
        public static void DepositeIn(int deposit)
        {
            _depositIn = deposit;
        }
        public static int GetDepositeForLog()
        {
            return _depositIn;
        }
        public static void SetNewPinForLog(int newPin)
        {
            _newPinForLog = newPin;
        }



      
        // კლასი განსახორციელებელი ოპერაცვიებისთვის 
        public class MenuOption
        {
            public int OptionNumber { get; set; }
            public string Description { get; set; }

            public MenuOption(int optionNumber, string description)
            {
                OptionNumber = optionNumber;
                Description = description;
            }
        }
   

        static void Main(string[] args)
        {

            string bankFolder = "MY-Bank";

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string bankFolderPath = Path.Combine(desktopPath, bankFolder);

            // შემოწმება folfer არსებობს თუ არა
            if (!Directory.Exists(bankFolderPath))
            {
                Directory.CreateDirectory(bankFolderPath);
            }

            int attempts = 3;
            bool exitApp = true;

            MenuOption[] menuOptions = {
               new MenuOption(0,"Check Balance"),
               new MenuOption(1, "Withdraw Money"),
               new MenuOption(2, "Change PIN"),
               new MenuOption(3, "Deposit Cash"),
               new MenuOption(4, "Transfer Money"),
               new MenuOption(5, "Exit")
            };

            while (exitApp)
            {
                Console.Write(" (: -- MY - Bank -- :) >  Enter your PIN: ");
                int.TryParse(Console.ReadLine(), out int enteredPin);

                if (enteredPin == GetPin())
                {
                    attempts = 3;
                    Console.WriteLine("Welcome to the ATM!");

                    // კონსოლში გამოსატანად ოპერაციები foreach loop:
                    foreach (var item in menuOptions)
                    {
                        Console.WriteLine($"{item.OptionNumber} . {item.Description}");
                    }

                    int.TryParse(Console.ReadLine(), out int choice);

                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Your balance is: " + GetBalance() + " USD");
                            break;
                        case 1:
                            
                            Console.Write("Enter the amount to withdraw: ");
                           
                            long.TryParse(Console.ReadLine(), out long withdrawAmount);

                            if (withdrawAmount <= GetBalance())
                            {
                                UpdateBalance(GetBalance() - withdrawAmount);
                                Console.WriteLine("Withdrawal successful. New balance: " + GetUpdatedBalance() + " USD");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient balance.");
                            }
                            break;
                        case 2:
                            Console.Write("Please Enter your old PIN: ");

                            int.TryParse(Console.ReadLine(), out int oldPin);

                            if(oldPin == GetPin())
                            {
                                Console.Write("Enter your new PIN: ");
                                int.TryParse(Console.ReadLine(), out int newPin);
                                if(newPin != 0)
                                {
                                    SetNewPinForLog(newPin);
                                    Console.WriteLine("PIN changed successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Please Enter Valid 4 digit PIN.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please Enter Valid 4 digit PIN.");
                            }
                         

                            break;
                        case 3:

                            Console.Write("How Much Money do you want to Deposit ? :  ");
                            int.TryParse(Console.ReadLine(), out int deposit);
                            UpdateBalance(GetBalance() + deposit);
                            Console.WriteLine("Deposit was Added successful. New balance: " + GetUpdatedBalance() + " USD");
                            DepositeIn(deposit);
                            break;

                        case 4:
                            Console.Write("Enter the recipient's account number: ");
                            var recipientAccount = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(recipientAccount)) return;
                            Console.Write("Enter the amount to transfer: ");
                            long.TryParse(Console.ReadLine(), out long transferAmount);
                        
                            try
                            {
                                if (transferAmount <= GetBalance())
                                {
                                    UpdateBalance(GetBalance() - transferAmount);
                                    Console.WriteLine($"Transfer of {transferAmount} -USD-  to {recipientAccount}  successful. New balance: {GetUpdatedBalance()}");

                                    //  // ტრანსფერის ფოლდერის შექმნა (yyyyMMdd_HHmm) წუთებით
                                    string transferFolderName = $"Cash_Transfers_On-{DateTime.Now:yyyyMMdd_HH}";
                                    string transferFolderPath = Path.Combine(bankFolderPath, transferFolderName);

                                    if (!Directory.Exists(transferFolderPath))
                                    {
                                        Directory.CreateDirectory(transferFolderPath);
                                    }
                                    // ტრანსფერის დროს ლოგი
                                    string transferFilePath = Path.Combine(transferFolderPath, "transfers_log.txt");
                                    using (StreamWriter writer = new StreamWriter(transferFilePath, true))
                                    {
                                        writer.WriteLine("--------------");
                                        writer.WriteLine(DateTime.Now);
                                        writer.WriteLine("Transfer to: " + recipientAccount);
                                        writer.WriteLine("Amount: " + transferAmount);
                                        
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient balance for transfer.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error {ex.Message} ");
                            }
                            break;
                        case 5:
                            Console.WriteLine("Thank you for using the ATM.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    // ქვეფოლდერის შექმა თარიღის მიხედვით (წუთების სხვაობით იქმნება ახალი)
                    string subFolderName = $"Operations-on{DateTime.Now.ToString("yyyyMMdd_HH")}";
                    string subFolderPath = Path.Combine(bankFolderPath, subFolderName);
                  
                    if (!Directory.Exists(subFolderPath))
                    {
                        Directory.CreateDirectory(subFolderPath);
                    }

                    string filePath = Path.Combine(subFolderPath, "log-operations.txt");

                    //StreamWriter  Open in append mode დამატებისთვის ახალი ხაზიდან
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {

                        writer.WriteLine("-----------");
                        writer.WriteLine(DateTime.Now);
                        try
                        {
                                writer.WriteLine("Operation:" + menuOptions[choice].Description);
                                // აქ ხდება შემოწმება თუ პინკოდის ცვლილებაა ვინახავთ ახალ პინკოდს ბაზაში
                                if (menuOptions[choice].Description == "Change PIN")
                                {
                                    writer.WriteLine($"NEW PIN CODE IS: {GetNewPinForLog()}");
                                }
                                else if (menuOptions[choice].Description == "Deposit Cash")
                                {
                                    writer.WriteLine($"Deposit was added : {GetDepositeForLog()}");
                                }
                        }
                        catch (Exception ex)
                        {
                       
                            Console.WriteLine("Invalid choice. Please select a valid option}");
                            // ლოგ ფაილში ჩაწერა Error ის
                            writer.WriteLine($"Error Message  : {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect PIN. Please try again.");
                   // მცდელობების დათვლა და გამოსვლა აპპ - დან
                    attempts--;
                    if (attempts == 0)
                    {
                        Console.WriteLine($"You have No More attempts, Try Again after 10 minutes");
                        exitApp = false;
                        return;
                    }
                    Console.WriteLine($"You have Only {attempts} attempts Left");
                }


            }
            Console.WriteLine("Thank you for using the ATM.");
        }
    }



}



