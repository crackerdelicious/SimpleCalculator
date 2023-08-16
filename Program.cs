using System;


namespace SimpleCalculator
{
    public class Calculator
    {
        public static decimal PerformOperation(decimal x, decimal y, Func<decimal, decimal, decimal> operation)
        {
            return operation(x, y);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            do
            {
                PrintMenu();

                if (int.TryParse(Console.ReadLine(), out choice))
                    HandleChoice(choice);
                else
                    Console.WriteLine("Invalid input. Please enter a numeric value.\n");

            } while (true);
        }

        static void PrintMenu()
        {
            Console.WriteLine("Simple Calculator\n");
            Console.WriteLine("1. Add two numbers");
            Console.WriteLine("2. Subtract two numbers");
            Console.WriteLine("3. Multiple two numbers");
            Console.WriteLine("4. Divide two numbers");
            Console.WriteLine("5. Quit\n");
            Console.Write("Enter your choice: ");
        }

        static void HandleChoice(int choice)
        {
            if (choice >= 1 && choice <= 4)
            {
                ProcessArithmeticOperation(choice);
            }
            else if (choice == 5)
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please select 1 - 5.\n");
            }
        }

        static void ProcessArithmeticOperation(int operationChoice)
        {
            string[] operationSymbols = { "+", "-", "x", "/" };

            try
            {
                Console.Write("Enter the first number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal firstNumber)) return;

                Console.Write("Enter the second number: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal secondNumber)) return;

                if (operationChoice == 4 && secondNumber == 0)
                {
                    Console.WriteLine("Error: Division by zero is not allowed.\n");
                    return;
                }

                Func<decimal, decimal, decimal> operation = GetOperation(operationChoice);
                decimal result = Calculator.PerformOperation(firstNumber, secondNumber, operation);
                Console.WriteLine($"Result: {firstNumber} {operationSymbols[operationChoice - 1]} {secondNumber} = {result:N2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        static Func<decimal, decimal, decimal> GetOperation(int choice)
        {
            switch (choice)
            {
                case 1: return (x, y) => x + y;
                case 2: return (x, y) => x - y;
                case 3: return (x, y) => x * y;
                case 4: return (x, y) => x / y;
                default: throw new ArgumentException("Invalid operation choice.");
            }
        }
    }
}