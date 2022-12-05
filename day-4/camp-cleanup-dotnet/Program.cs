var input = File.ReadAllLines("../puzzle-input.txt");

var assignmentsToReconsider = 0;

foreach (var line in input)
{
    var assignments = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

    var sectionsA = ParseSections(assignments[0]);
    var sectionsB = ParseSections(assignments[1]);

    if (sectionsA.ToHashSet().IsSubsetOf(sectionsB) || sectionsB.ToHashSet().IsSubsetOf(sectionsA))
    {
        assignmentsToReconsider++;
    }
}

WriteLine(assignmentsToReconsider);

static IEnumerable<int> ParseSections(string source)
{
    var assignment = source.Split("-", StringSplitOptions.RemoveEmptyEntries);
    var start = int.Parse(assignment[0]);
    var end = int.Parse(assignment[1]);
    return Enumerable.Range(start, end - start + 1);
}
