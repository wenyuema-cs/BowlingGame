namespace BowlingGame;

public class Bowling
{
    private List<Frame> _frames = new List<Frame>();
    private const int MaxFrames = 10;
    private bool ShouldAddNewFrame(Frame frame) => frame.IsFrameComplete && _frames.Count < MaxFrames; // Determines if a new frame should be added
    private Frame GetCurrentFrame() => _frames.Last();
    
    
    public Bowling()
    {
       ResetGame(); // initialize the game
    }
    
    
    public void Roll(int pins)
    {
        if (pins < 0 || pins > MaxFrames)
        {
            throw new ArgumentException("Pins must be between 0 and MaxFrames.");
        }

        Frame currentFrame = GetCurrentFrame();

        if (currentFrame.IsLastFrame && currentFrame.Rolls.Count >= 3)
        {
            throw new InvalidOperationException("Cannot roll after the game has ended.");
        }

        currentFrame.AddRoll(pins); 
        
       
        string frameResultMessage = "";
        if (currentFrame.IsFrameComplete)
        {
            if (currentFrame.IsStrike)
            {
                frameResultMessage = "Strike! 🎳 ";
            }
            else if (currentFrame.IsSpare)
            {
                frameResultMessage = "Spare! 🎳 ";
            }
        }
        
        int currentRollInFrame = currentFrame.Rolls.Count;
        Console.WriteLine($"Frame {_frames.Count}/{MaxFrames}, Roll {currentRollInFrame} in this Frame. {frameResultMessage} Current Score: {Score()}");


        if (ShouldAddNewFrame(currentFrame))
        {
            _frames.Add(new Frame(_frames.Count == MaxFrames - 1));
        }
        
        if (IsGameComplete())
        {
            EndGame();
        }
    }

    // Resets the game to its initial state, or starting the new game.
    public void ResetGame()
    {
        _frames = new List<Frame>();
        _frames.Add(new Frame(false)); // Start with the first frame
    }
    
    private bool IsGameComplete()
    {
        if (_frames.Count < MaxFrames) return false; 

        Frame lastFrame = _frames.Last();
        return lastFrame.IsFrameComplete;
    }
    
    // Processes multiple rolls at once, useful for initializing specific scenarios.
    public void RollMultiple(List<int> rolls)
    { 
        try
        {
            foreach (var pins in rolls)
            {
                Roll(pins);
            }
            EndGame(); 
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid roll detected: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid game operation: {ex.Message}");
        }
    }
    
    
    private int CalculateFrameScore(int frameIndex)
    {
        Frame frame = _frames[frameIndex];
        int score = frame.Rolls.Sum(); // Base score for the frame

        // Add bonus for strikes and spares, also handling for the 10th frame
        if (frame.IsStrike && frameIndex < 9) { 
            score += StrikeBonus(frameIndex);
        } else if (frame.IsSpare && frameIndex < 9) {
            score += SpareBonus(frameIndex);
        }

        return score;
    }
    
    public int Score()
    {
        int totalScore = 0;
        for (int i = 0; i < _frames.Count; i++) {
            totalScore += CalculateFrameScore(i);
        }
        return totalScore;
    }

    private int StrikeBonus(int frameIndex)
    {
        if (frameIndex >= _frames.Count - 1) return 0;
        if (_frames[frameIndex + 1].IsStrike && frameIndex < _frames.Count - 2)
        {
            return 10 + (_frames[frameIndex + 2].Rolls.Count > 0 ? _frames[frameIndex + 2].Rolls[0] : 0);
        }
        return _frames[frameIndex + 1].Rolls.Take(2).Sum();
    }

    private int SpareBonus(int frameIndex)
    {
        if (frameIndex >= _frames.Count - 1) return 0;
        return _frames[frameIndex + 1].Rolls.FirstOrDefault();
    }

    public void EndGame()
    {
        Console.WriteLine("================================================================");
        if (IsPerfectGame())
        {
            Console.WriteLine("Congratulations! You bowled a Perfect Game! \u266a（\uff3e\u2200\uff3e\u25cf）ﾉｼ （\u25cf\u00b4\u2200\uff40）\u266a ");
        }
        else if (IsGutterGame())
        {
            Console.WriteLine("Don't worry \u256e(๑•\u0301 \u2083•\u0300๑)\u256d , everyone starts somewhere. Cheer up!");
        }
        else
        {
            Console.WriteLine($"Great game \u256d(\u25cf\uff40\u2200\u00b4\u25cf)\u256f ! Your final score is {Score()}.");
        }
        Console.WriteLine("================================================================");
    }

    private bool IsGutterGame()
    {
        return _frames.All(frame => frame.Rolls.All(roll => roll == 0));
    }

    private bool IsPerfectGame()
    {
        return Score() == 300;
    }
}
