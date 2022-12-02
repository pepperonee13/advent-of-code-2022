var input = File.ReadAllLines("../puzzle-input.txt");

var caloriesByElf = CalculateCalories(input).ToList();

WriteLine(caloriesByElf.Max(e => e));
WriteLine(caloriesByElf.OrderByDescending(e => e).Take(3).Sum());

IEnumerable<int> CalculateCalories(string[] input)
{
    var caloriesCarriedByElf = 0;

    foreach (var line in input)
    {
        if (int.TryParse(line, out var calories)) caloriesCarriedByElf += calories;

        if (string.IsNullOrEmpty(line))
        {
            yield return caloriesCarriedByElf;
            caloriesCarriedByElf = 0;
        }
    }

    yield return caloriesCarriedByElf;
}