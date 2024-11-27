using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Console Calculator!");

            while (true)
            {
                Console.Write("Enter the first number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal num1))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                Console.Write("Enter the second number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal num2))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                Console.WriteLine("Select an operation:");
                Console.WriteLine("1. Addition  ");
                Console.WriteLine("2. Subtraction");
                Console.WriteLine("3. Multiplication");
                Console.WriteLine("4. Division");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    continue;
                }

                decimal result;

                switch (choice)
                {
                    case 1:
                        result = num1 + num2;
                        Console.WriteLine($"Result: {num1} + {num2} = {result}");
                        break;
                    case 2:
                        result = num1 - num2;
                        Console.WriteLine($"Result: {num1} - {num2} = {result}");
                        break;
                    case 3:
                        result = num1 * num2;
                        Console.WriteLine($"Result: {num1} * {num2} = {result}");
                        break;
                    case 4:
                        if (num2 == 0)
                        {
                            Console.WriteLine("Error: Division by zero is not allowed.");
                        }
                        else
                        {
                            result = num1 / num2;
                            Console.WriteLine($"Result: {num1} / {num2} = {result}");
                        }
                        break;
                }

                Console.WriteLine("Do you want to perform another calculation? (y/n)");
                var answer = Console.ReadLine().ToLower();
                if (answer != "y")
                {
                    break;
                }
            }

            Console.WriteLine("Thank you for using the calculator!");
        }
    }
}