using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complex_Number_Calculator.models;
namespace Complex_Number_Calculator.service
{
    public class ComplexCalcService
    {
        public Complex OperationPerform(Complex a,Complex b,string operation) {
            return operation switch
            {
                "+" => Complex.Add(a, b),
                "-" => Complex.Subtract(a, b),
                "*" => Complex.Multiply(a, b),
                "/" => Complex.Divide(a, b),
                _ => throw new InvalidOperationException("Invalid operation.Can be + - * /")
            };
        }
    }
}
