# Tic tac toe
Hooray! Your actions yesterday saved the day with the navigation computer! The captain has rewarded you with a day off today. You sit down with a friend in the break room to play tic-tac-toe. You and your friend are known for taking things to the extreme, however, and before you realise it you've played 1000 games of tic-tac-toe!

You and your friend have a heated debate about which of you was the ultimate tic-tac-toe master so to settle it once and for all, you ask the Ship AI to play back all the games for you.

It seems the Ship AI has decided to play games with you itself. While it does provide the data of all the moves of the 1000 games, it has made up moves to continue each game until all 9 squares are filled! This means you are going to have to process the moves of each game to determine who the winner was.

Examining your input data, it is designed such that:

- Each line of the input data represents one game of tic-tac-toe.
- Player X starts first in every game. Moves then alternate between player O and player X as normal.
- For each line of game data, the numbers represent game squares filled in by the two people playing tic-tac-toe as follows

```
 1 | 2 | 3
---|---|---
 4 | 5 | 6
---|---|---
 7 | 8 | 9
```
- Process each game until a player has 3 in a row. At that point the game is over and you should ignore any remaining moves.
- If no player obtains 3 squares in the row, the game is classified as a draw.
- Your answer value is `number of games won by X` multiplied by `number of games won by O` multiplied by `number of drawn games`.
## Example
Given game data of `2 4 5 8 1 3 9 6 7`.
The first two moves see `player X` taking square 2 and `player O` taking square 4.
```
   | X |  
---|---|---
 O |   |  
---|---|---
   |   |  
```
The next two moves see `player X` taking square 5 and `player O` taking square 8.
```
   | X |  
---|---|---
 O | X |  
---|---|---
   | O |  
```
The next two moves see `player X` taking square 1 and `player O` taking square 3.
```
 X | X | O
---|---|---
 O | X |  
---|---|---
   | O |  
```
`Player X` wins with the next move of square 9. Increase `Player X`'s total of won games by 1.
```
X | X | O
---|---|---
 O | X |  
---|---|---
   | O | X
```
As indicated, the AI continued the game, filling in the remaining squares, giving player O square 6 and player X square 7. These moves are irrelevant to the outcome of the game. You will need to be able to correctly determine when the game is supposed to end so you can recognise the rightful winners.
```
X | X | O
---|---|---
 O | X | O
---|---|---
 X | O | X
```
## Your task
Your answer value is `number of games won by X` multiplied by nu`mber of games won by O` multiplied by `number of drawn games`.
