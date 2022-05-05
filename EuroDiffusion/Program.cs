using EuroDiffusion.Entities;

var lines = File.ReadAllLines("Resources/input.txt");

var set = TestCaseSet.InputFromConsole();

set.Execute();

set.OutputToConsole();
