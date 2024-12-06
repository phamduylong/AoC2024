public class Day6Solution {
    private const string UP = "^";
    private const string DOWN = "v";
    private const string LEFT = "<";
    private const string RIGHT = ">";
    private const char OBSTACLE = '#';

    private char[][] Grid { get; set; }
    private string Direction { get; set; } = "^";
    private Coordinate Coordinate { get; set; } = new Coordinate();

    private int XMax { get; set; } = 0;
    private int YMax { get; set; } = 0;
    public string input = "";

    public Day6Solution() {
        Setup();
    }

    public void Setup()
    {
        input = File.ReadAllText("Day6/input.txt");
        if(input.Contains(UP)) {
            Direction = UP;
        } else if(input.Contains(DOWN)) {
            Direction = DOWN;
        } else if(input.Contains(LEFT)) {
            Direction = LEFT;
        } else {
            Direction = RIGHT;
        }
        var lines = input.Split("\n");
        Grid = new char[lines.Length][];
        for(int i = 0; i < lines.Length; ++i)
        {
            Grid[i] = lines[i].ToCharArray();
        }
        XMax = lines.Length - 1;
        YMax = lines.Length - 1;
        for(int i = 0; i < lines.Length; ++i) {
            var line = lines[i];
            if(line.Contains(Direction)) {
                Coordinate.Y = i;
                Coordinate.X = line.IndexOf(Direction);
                break;
            }
        }
    }

    // The task is to find the amount of spaces covered before the guard goes out of zone
    public int Task1() {
        var stepsCovered = new List<Coordinate>();
        while(Coordinate.X < XMax && Coordinate.X > 0 && Coordinate.Y < YMax && Coordinate.Y > 0) {
            if(Direction == UP) {
                if(Grid[Coordinate.Y -1][Coordinate.X] == OBSTACLE) {
                    Direction = RIGHT;
                    continue;
                }
                Coordinate.Y--;
            } else if(Direction == DOWN) {
                if(Grid[Coordinate.Y + 1][Coordinate.X] == OBSTACLE) {
                    Direction = LEFT;
                    continue;
                }
                Coordinate.Y++;
            } else if(Direction == LEFT) {
                if(Grid[Coordinate.Y][Coordinate.X - 1] == OBSTACLE) {
                    Direction = UP;
                    continue;
                }
                Coordinate.X--;
            } else {
                if(Grid[Coordinate.Y][Coordinate.X + 1] == OBSTACLE) {
                    Direction = DOWN;
                    continue;
                }
                Coordinate.X++;
            }
            if(!stepsCovered.Any(x => x.X == Coordinate.X && x.Y == Coordinate.Y)) {    
                stepsCovered.Add(new Coordinate { X = Coordinate.X, Y = Coordinate.Y });
            }
        }
        return stepsCovered.Count;
    }
}
public class Coordinate {
    public int X { get; set; }
    public int Y { get; set; }
    public override string ToString() {
        return $"({X + 1}, {Y + 1})";
    }
}