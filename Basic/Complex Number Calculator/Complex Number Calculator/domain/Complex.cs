using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator.models
{
    public class Complex
    {
        public double RealNumber {  get; set; }
        public double ImaginaryNumber { get; set; }
        public Complex(double real,double imaginary ) {

            RealNumber = real;
            ImaginaryNumber = imaginary;

        }
        public static Complex Add(Complex a, Complex b) { 
        
            return new Complex(a.RealNumber + b.RealNumber,a.ImaginaryNumber + b.ImaginaryNumber);  
        }
        public static Complex Subtract(Complex a, Complex b)
        {
            return new Complex(a.RealNumber - b.RealNumber, a.ImaginaryNumber - b.ImaginaryNumber);

        }
        public static Complex Multiply(Complex a, Complex b)
        {
           double real =(a.RealNumber * b.RealNumber) - (a.ImaginaryNumber * b.ImaginaryNumber);
           double imaginary = (a.RealNumber * b.ImaginaryNumber) + (a.ImaginaryNumber*b.RealNumber);
            return new Complex(real,imaginary);


        }

        public static Complex Divide(Complex a, Complex b)
        {
            double denominator = (b.RealNumber * b.RealNumber) + (b.ImaginaryNumber * b.ImaginaryNumber);
            if (denominator == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            double real = ((a.RealNumber * b.RealNumber) + (a.ImaginaryNumber * b.ImaginaryNumber)) / denominator;
            double imaginary = ((a.ImaginaryNumber * b.RealNumber) - (a.RealNumber * b.ImaginaryNumber)) / denominator;

            return new Complex(real, imaginary);
        }

        public override string ToString()
        {
            return $"{RealNumber}+{ImaginaryNumber}i";
        }





    }
}
