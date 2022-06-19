using MediatorConsoleTest.Multiply;
using MediatorConsoleTest.Sum;
using MediatorLib;
using MediatorLib.Interfaces;
using System.Reflection;
using static System.Console;

Write("Please enter a number 'a': ");
int a = int.Parse(ReadLine()!);
Write("Please enter a number 'b': ");
int b = int.Parse(ReadLine()!);

WriteLine();

IMediator mediator = new Mediator(Assembly.GetExecutingAssembly());

SumRequest sumRequest = new SumRequest 
{
    A = a,
    B = b
};

MultiplyRequest multiplyRequest = new MultiplyRequest 
{
    A = a,
    B = b
};

int sum = await mediator.Send(sumRequest);
WriteLine($"a + b = {sum}");

int multiply = await mediator.Send(multiplyRequest);
WriteLine($"a * b = {multiply}");

WriteLine();

WriteLine("The end.");
ReadKey();