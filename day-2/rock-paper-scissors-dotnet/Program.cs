using static Choice;

var input = File.ReadAllLines("../puzzle-input.txt");
var lines = input.Select(ParseGuideline).ToList();
var scores = lines.Select(PlayGame).ToList();

var totalScore = scores.Select(x => x.P2).Sum();
WriteLine(totalScore);

Guideline ParseGuideline(string line)
{
    var splitResult = line.Split(" ");
    Choice opponent;
    switch (splitResult[0])
    {
        case "A": opponent = Rock; break;
        case "B": opponent = Paper; break;
        case "C": opponent = Scissors; break;
        default: throw new ArgumentException($"Could not parse {splitResult[0]}");
    }

    Choice you;
    switch (splitResult[1])
    {
        case "X": you = Rock; break;
        case "Y": you = Paper; break;
        case "Z": you = Scissors; break;
        default: throw new ArgumentException($"Could not parse {splitResult[1]}");
    }

    return new Guideline(opponent, you);
}

Score PlayGame(Guideline guideline)
{
    int GetPointsForChoice(Choice choice) => choice switch
    {
        Rock => 1,
        Paper => 2,
        Scissors => 3,
        _ => 0
    };
    var yourScore = GetPointsForChoice(guideline.You);
    var opponentScore = GetPointsForChoice(guideline.Opponent);

    var game = new Game(guideline.Opponent, guideline.You);

    var result = game switch
    {
        { P1: Rock, P2: Rock } => "draw",
        { P1: Paper, P2: Paper } => "draw",
        { P1: Scissors, P2: Scissors } => "draw",

        { P1: Scissors, P2: Paper } => "p1",
        { P1: Paper, P2: Rock } => "p1",
        { P1: Rock, P2: Scissors } => "p1",

        { P1: Scissors, P2: Rock } => "p2",
        { P1: Paper, P2: Scissors } => "p2",
        { P1: Rock, P2: Paper } => "p2",
        _ => "undefined"
    };

    var (p1Score, p2Score) = result switch
    {
        "p1" => (6, 0),
        "p2" => (0, 6),
        "draw" => (3, 3),
        _ => (0, 0)
    };

    return new Score(opponentScore + p1Score, yourScore + p2Score);
}

record Game(Choice P1, Choice P2);
record Score(int P1, int P2);
enum Choice { Rock, Paper, Scissors }
record Guideline(Choice Opponent, Choice You);