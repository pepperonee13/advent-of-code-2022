
var input = File.ReadAllLines("../puzzle-input.txt");

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
                    .Select((c, i) => new { index = i, letter = c })
                    .ToDictionary(key => key.letter, value => value.index + 1);

var total = 0;

var group = new string[3];
var i = 1;

foreach (var line in input)
{
    group[i - 1] = line;

    if (i % 3 == 0)
    {
        var commonLetter = group[0].Intersect(group[1]).Intersect(group[2]).FirstOrDefault();
        total += priorities[commonLetter];
        i = 1;
        group = new string[3];
    }
    else
    {
        i++;
    }
}

WriteLine(total);