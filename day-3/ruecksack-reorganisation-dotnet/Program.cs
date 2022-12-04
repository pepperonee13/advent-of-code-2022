
var input = File.ReadAllLines("../puzzle-input.txt");

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
                    .Select((c, i) => new { index = i, letter = c })
                    .ToDictionary(key => key.letter, value => value.index + 1);

var total = 0;

foreach (var line in input)
{
    //split in half
    var half = line.Length / 2;
    var first = line.Take(half).ToHashSet();
    var second = line.Skip(half);

    //find common letter
    var commonLetter = first.Intersect(second).FirstOrDefault();

    //get priorities
    var priority = priorities[commonLetter];

    //sum
    total += priority;
}

WriteLine(total);