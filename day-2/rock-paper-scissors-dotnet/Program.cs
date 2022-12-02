using static Choice;
using static Result;

var input = File.ReadAllLines("../puzzle-input.txt");
var totalScore = input.Select(ParseGuideline).Select(PlayGame).Select(scores => scores.P2).Sum();
WriteLine(totalScore);

Guideline ParseGuideline(string line)
{
    Choice ParseChoice(string input)
    {
        return input switch
        {
            "A" => Rock,
            "B" => Paper,
            "C" => Scissors,
            _ => throw new Exception("Unknown choice")
        };
    }

    Result ParseResult(string input)
    {
        return input switch
        {
            "X" => Lose,
            "Y" => Draw,
            "Z" => Win,
            _ => throw new Exception("Unknown result")
        };
    }

    var splitResult = line.Split(" ");
    Choice opponent = ParseChoice(splitResult[0]);
    Result desiredResult = ParseResult(splitResult[1]);

    return new Guideline(opponent, desiredResult);
}

Score PlayGame(Guideline guideline)
{
    int GetPointsForChoice(Choice choice) => choice switch
    {
        Rock => 1,
        Paper => 2,
        Scissors => 3,
        _ => throw new Exception("unexpected choice")
    };

    var yourChoice = GetChoiceBasedOnGuideline(guideline);
    var result = GetResult(guideline.Opponent, yourChoice);

    var opponentScore = GetPointsForChoice(guideline.Opponent);
    var yourScore = GetPointsForChoice(yourChoice);
    var (p1Score, p2Score) = GetScores(result);

    return new Score(opponentScore + p1Score, yourScore + p2Score);
}

static Choice GetChoiceBasedOnGuideline(Guideline guideline)
{
    if (guideline.DesiredResult == Draw) return guideline.Opponent;

    return (guideline.Opponent, guideline.DesiredResult) switch
    {
        (Rock, Lose) => Scissors,
        (Rock, Win) => Paper,
        (Paper, Lose) => Rock,
        (Paper, Win) => Scissors,
        (Scissors, Lose) => Paper,
        (Scissors, Win) => Rock,
        _ => throw new Exception("unhandled case")
    };
}

static Result GetResult(Choice P1, Choice P2)
{
    if (P1 == P2) return Draw;

    return (P1, P2) switch
    {
        (Scissors, Paper) => Lose,
        (Paper, Rock) => Lose,
        (Rock, Scissors) => Lose,
        (Scissors, Rock) => Win,
        (Paper, Scissors) => Win,
        (Rock, Paper) => Win,
        _ => throw new Exception("unhandled")
    };
}

static (int, int) GetScores(Result result)
{
    return result switch
    {
        Lose => (6, 0),
        Win => (0, 6),
        Draw => (3, 3),
        _ => (0, 0)
    };
}

record Game(Choice P1, Choice P2);
record Score(int P1, int P2);
enum Result { Win, Lose, Draw };
enum Choice { Rock, Paper, Scissors }
record Guideline(Choice Opponent, Result DesiredResult);