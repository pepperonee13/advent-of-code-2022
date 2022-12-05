var input = File.ReadAllLines("../puzzle-input.txt");


var assignmentsToReconsider = 0;
var overlappingAssignments = 0;

foreach (var line in input)
{
    var assignments = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

    var (startA, endA) = ParseRange(assignments[0]);
    var (startB, endB) = ParseRange(assignments[1]);

    var (maxStart, minEnd) = (Math.Max(startA, startB), Math.Min(endA, endB));
    
    if (startA == maxStart && endA == minEnd) assignmentsToReconsider++;
    else if (startB == maxStart && endB == minEnd) assignmentsToReconsider++;

    if (maxStart <= minEnd)
    {
        overlappingAssignments++;
    }
}

WriteLine(assignmentsToReconsider);
WriteLine(overlappingAssignments);

static (int start, int end) ParseRange(string source)
{
    var assignment = source.Split("-", StringSplitOptions.RemoveEmptyEntries);
    return (int.Parse(assignment[0]), int.Parse(assignment[1]));
}
