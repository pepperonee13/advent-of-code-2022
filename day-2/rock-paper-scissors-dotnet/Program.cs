using static Choice;
using static Result;

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

    Result desiredResult;
    switch (splitResult[1])
    {
        case "X": desiredResult = Lose; break;
        case "Y": desiredResult = Draw; break;
        case "Z": desiredResult = Win; break;
        default: throw new ArgumentException($"Could not parse {splitResult[1]}");
    }

    return new Guideline(opponent, desiredResult);
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

    var opponentScore = GetPointsForChoice(guideline.Opponent);
    var yourChoice = GetChoiceBasedOnGuideline(guideline);
    var yourScore = GetPointsForChoice(yourChoice);

    var game = new Game(guideline.Opponent, yourChoice);

    var result = game switch
    {
        { P1: Rock, P2: Rock } => Draw,
        { P1: Paper, P2: Paper } => Draw,
        { P1: Scissors, P2: Scissors } => Draw,

        { P1: Scissors, P2: Paper } => Lose,
        { P1: Paper, P2: Rock } => Lose,
        { P1: Rock, P2: Scissors } => Lose,

        { P1: Scissors, P2: Rock } => Win,
        { P1: Paper, P2: Scissors } => Win,
        { P1: Rock, P2: Paper } => Win,
        _ => throw new Exception("unhandled")
    };

    var (p1Score, p2Score) = result switch
    {
        Lose => (6, 0),
        Win => (0, 6),
        Draw => (3, 3),
        _ => (0, 0)
    };

    return new Score(opponentScore + p1Score, yourScore + p2Score);
}



static Choice GetChoiceBasedOnGuideline(Guideline guideline)
{
    Choice yourChoice;
    if (guideline.DesiredResult == Draw) yourChoice = guideline.Opponent;
    else yourChoice = guideline switch
    {
        { Opponent: Rock, DesiredResult: Lose } => Scissors,
        { Opponent: Rock, DesiredResult: Win } => Paper,

        { Opponent: Paper, DesiredResult: Lose } => Rock,
        { Opponent: Paper, DesiredResult: Win } => Scissors,

        { Opponent: Scissors, DesiredResult: Lose } => Paper,
        { Opponent: Scissors, DesiredResult: Win } => Rock,
        _ => throw new Exception("unhandled case")
    };
    return yourChoice;
}

record Game(Choice P1, Choice P2);
record Score(int P1, int P2);
enum Result { Win, Lose, Draw };
enum Choice { Rock, Paper, Scissors }
record Guideline(Choice Opponent, Result DesiredResult);