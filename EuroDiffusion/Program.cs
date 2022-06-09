using EuroDiffusion.Entities;

try
{
    var lines = File.ReadAllLines("Resources/input.txt");

    var set = TestCaseSet.Parse(lines);

    set.Execute();

    File.WriteAllText("Resources/output.txt", set.ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
