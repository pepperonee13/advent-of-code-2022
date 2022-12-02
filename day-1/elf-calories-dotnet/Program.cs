// See https://aka.ms/new-console-template for more information

//TODO: get from command line or file
var input = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000";


var caloriesByElf = ParseCaloriesByElf(input);

WriteLine(caloriesByElf.Max(e => e));


IEnumerable<int> ParseCaloriesByElf(string input)
{
    var lines = input.Split(Environment.NewLine);

    var caloriesCarriedByElf = 0;

    foreach (var line in lines)
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