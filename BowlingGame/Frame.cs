namespace BowlingGame;

public class Frame
{
    private readonly List<int> _rolls = new List<int>();
    private readonly bool _isLastFrame;

    public Frame(bool isLastFrame)
    {
        this._isLastFrame = isLastFrame;
    }

    public void AddRoll(int pins)
    {
        _rolls.Add(pins);
    }

    public bool IsStrike => _rolls.Count > 0 && _rolls[0] == 10;
    public bool IsSpare => _rolls.Count == 2 && _rolls.Sum() == 10;
    public List<int> Rolls => _rolls;
    public bool IsLastFrame => _isLastFrame;
    public bool IsFrameComplete
    {
        get
        {
            if (!_isLastFrame)
            {
                return IsStrike || Rolls.Count == 2;
            }
            else
            {
                // For the tenth frame: complete if it has 3 rolls or if it has 2 rolls that aren't a spare or strike
                return Rolls.Count == 3 || (Rolls.Count == 2 && Rolls.Sum() < 10);
            }
        }
    }
}
