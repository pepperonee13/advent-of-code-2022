
var input = File.ReadAllLines("../puzzle-input.txt");

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
                    .Select((c, i) => new { index = i, letter = c })
                    .ToDictionary(key => key.letter, value => value.index + 1);

const int GROUP_COUNT = 3;
var total = 0;

var currentGroup = new HashSet<char>();
var i = 1;

foreach (var line in input)
{
    if (currentGroup.Count == 0) currentGroup.UnionWith(line);
    else currentGroup.IntersectWith(line);

    if (i != GROUP_COUNT) i++;
    else
    {
        total += priorities[currentGroup.Single()];
        currentGroup.Clear();
        i = 1;
    }
}

WriteLine(total);