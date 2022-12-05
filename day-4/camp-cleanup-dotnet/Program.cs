var input = File.ReadAllLines("../puzzle-input.txt");


var assignmentsToReconsider = 0;

foreach (var line in input)
{
    var assignments = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

    var (startA, endA) = ParseRange(assignments[0]);
    var (startB, endB) = ParseRange(assignments[1]);

    if (startA >= startB && endA <= endB) assignmentsToReconsider++;
    else if (startB >= startA && endB <= endA) assignmentsToReconsider++;
}

WriteLine(assignmentsToReconsider);

static (int start, int end) ParseRange(string source)
{
    var assignment = source.Split("-", StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(assignment[0]), int.Parse(assignment[1]));
}
