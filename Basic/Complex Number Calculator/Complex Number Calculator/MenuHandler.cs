using System;
using Complex_Number_Calculator.models;
using Complex_Number_Calculator.service;

namespace Complex_Number_Calculator
{
    public class MenuHandler
    {
        private readonly ComplexCalcService _complexCalcService;

        public MenuHandler(ComplexCalcService complexCalcService)
        {
            _complexCalcService = complexCalcService;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the Complex Number Calculator!");

            while (true)
            {
                Console.WriteLine("\nChoose an operation:");
                Console.WriteLine("1. Add (+)");
                Console.WriteLine("2. Subtract (-)");
                Console.WriteLine("3. Multiply (*)");
                Console.WriteLine("4. Divide (/)");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();
                if (choice == "5")
                {
                    Console.WriteLine("Exiting the calculator");
                    break;
                }

                Complex firstNumber = GetNumber("first");
                Complex secondNumber = GetNumber("second");

                try
                {
                    Complex result = choice switch
                    {
                        "1" => _complexCalcService.OperationPerform(firstNumber, secondNumber, "+"),
                        "2" => _complexCalcService.OperationPerform(firstNumber, secondNumber, "-"),
                        "3" => _complexCalcService.OperationPerform(firstNumber, secondNumber, "*"),
                        "4" => _complexCalcService.OperationPerform(firstNumber, secondNumber, "/"),
                        _ => throw new InvalidOperationException("Invalid operation selected."),
                    };

                    Console.WriteLine($"Result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private Complex GetNumber(string order)
        {
            double real = 0, imaginary = 0;

            while (true)
            {
                Console.Write($"Enter the real part of the {order} complex number: ");
                if (double.TryParse(Console.ReadLine(), out real)) break;
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            while (true)
            {
                Console.Write($"Enter the imaginary part of the {order} complex number: ");
                if (double.TryParse(Console.ReadLine(), out imaginary)) break;
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            return new Complex(real, imaginary);
        }
    }
}
