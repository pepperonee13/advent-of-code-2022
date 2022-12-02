// See https://aka.ms/new-console-template for more information
if (args.Length == 0)
{
    WriteLine("Please specify the input file path as an argument!");
    return;
}

var input = File.ReadAllLines(args[0]);

var caloriesByElf = ParseCaloriesByElf(input);

WriteLine(caloriesByElf.Max(e => e));

IEnumerable<int> ParseCaloriesByElf(string[] input)
{
    var caloriesCarriedByElf = 0;

    foreach (var line in input)
    {
        if (int.TryParse(line, out var calories))
        {
            caloriesCarriedByElf += calories;
        }

        if (string.IsNullOrEmpty(line))
        {
            yield return caloriesCarriedByElf;
            caloriesCarriedByElf = 0;
        }
    }
}