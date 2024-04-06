using BowlingGame;


Bowling game = new Bowling();
int score;
// Frame 1
game.Roll(1);
game.Roll(4);

// Frame 2
game.Roll(4);
game.Roll(5);

// Frame 3
game.Roll(6);
game.Roll(4);

// Frame 4
game.Roll(5);
game.Roll(5);

// Frame 5
game.Roll(10);

// Frame 6
game.Roll(0);
game.Roll(1);

// Frame 7
game.Roll(7);
game.Roll(3);

// Frame 8
game.Roll(6);
game.Roll(4);

// Frame 9
game.Roll(10);

// Frame 10
game.Roll(2); // Strike in the first roll of the 10th frame
game.Roll(8); // Bonus roll (also a strike)
game.Roll(6);  // Final bonus roll

game.ResetGame();


// Example Game
List<int> rolls = new List<int> { 1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6 };
game.RollMultiple(rolls);
score = game.Score();
Console.WriteLine($"Total Score: {score}");
game.ResetGame();


// // perfect game Game
rolls = new List<int> {10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10};
game.RollMultiple(rolls);
game.ResetGame();


// gutter game Game
game.RollMultiple(Enumerable.Repeat(0, 20).ToList());
game.ResetGame();

// Unfinished Game
List<int> unfinishedroll = new List<int> { 1, 4, 4, 5, 6, 4, 5, 5, 10,10,10,10,10,10,10, 0, 1, 7, 3 };
game.RollMultiple(unfinishedroll);
game.ResetGame();




