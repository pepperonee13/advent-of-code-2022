using static Choice;

var input = File.ReadAllLines("../test-input.txt");

var lines = input.Select(ParseGuideline).ToList();

var scores = lines.Select(PlayGame).ToList();

scores.ForEach(scores => WriteLine($"{scores.Item1}:{scores.Item2}"));

WriteLine(scores.Select(x => x.Item2).Sum());

Guideline ParseGuideline(string line)
{
    var splitResult = line.Split(" ");
    Decision opponent;
    switch (splitResult[0])
    {
        case "A": opponent = new Decision(Rock); break;
        case "B": opponent = new Decision(Paper); break;
        case "C": opponent = new Decision(Scissors); break;
        default: throw new ArgumentException($"Could not parse {splitResult[0]}");
    }

    Decision you;

    switch (splitResult[1])
    {
        case "X": you = new Decision(Rock); break;
        case "Y": you = new Decision(Paper); break;
        case "Z": you = new Decision(Scissors); break;
        default: throw new ArgumentException($"Could not parse {splitResult[1]}");
    }

    return new Guideline(opponent, you);
}

(int, int) PlayGame(Guideline guideline)
{
    var opponentScore = 0;
    var yourScore = 0;

    opponentScore += guideline.Opponent.Points;
    yourScore += guideline.You.Points;

    var game = new Game(guideline.Opponent.Choice, guideline.You.Choice);

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

    return (opponentScore + p1Score, yourScore + p2Score);
}

record Game(Choice P1, Choice P2);

enum Choice
{
    Rock, Paper, Scissors
}
record Decision(Choice Choice)
{
    public int Points
    {
        get
        {
            switch (this.Choice)
            {
                case Rock: return 1;
                case Paper: return 2;
                case Scissors: return 3;
                default: return 0;
            }
        }
    }
};
record Guideline(Decision Opponent, Decision You);