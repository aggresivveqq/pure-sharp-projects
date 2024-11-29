using Complex_Number_Calculator;
using Complex_Number_Calculator.service;

var calculatorService = new ComplexCalcService();
var consoleUI = new MenuHandler(calculatorService);
consoleUI.Run();