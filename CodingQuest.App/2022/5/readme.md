# Obsessing over Connect 4
![header](https://codingquest.io/may2022/connect-4-web.png)
It seems some games exist no matter where in the universe you travel. You stumble across some people who are playing a slightly modified version of the classic childhood game, Connect 4. The main difference is, this is a three player version! Also, this version of connect 4 has 7 rows and 7 columns.

You are invited to join in and quickly become addicted, playing 1000 games in quick succession! (your input data).

You record the game moves on your computer so you can re-live them later. When recording the moves, you simply write down the column that each player drops tokens into, since that is enough information to work out where the token ends up and whose turn it is.
## Example
This represents the game board before any player has made a move.
```
  1   2   3   4   5   6   7
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
 --- --- --- --- --- --- ---
```
This is an example of a game you have recorded:
```
5615177132314422633773357162745162431525676654244
```
The above information can be interpreted as:

- Player 1 drops a token in column 5
- Player 2 drops a token in column 6
- Player 3 drops a token in column 1
- Player 1 drops a token in column 5
- Player 2 drops a token in column 1
- Player 3 drops a token in column 7
- Player 1 drops a token in column 7, and so forth.

After the 7 moves described above the board would look like:
```
  1   2   3   4   5   6   7
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
|   |   |   |   |   |   |   |
| 2 |   |   |   | 1 |   | 1 |
| 3 |   |   |   | 1 | 2 | 3 |
 --- --- --- --- --- --- ---
```
The game continues until the 30th move when player 3 wins
```
|   |   |   |   |   |   |   |
|   |   | 2 |   |   |   | 2 |
| 2 |   | 1 |   |   |   | 1 |
| 3 | 1 | 1 |   |   |   | 3 |
| 2 | 1 | 3 | 3 | 3 | 3 | 2 |
| 2 | 3 | 2 | 2 | 1 | 2 | 1 |
| 3 | 1 | 3 | 1 | 1 | 2 | 3 |
 --- --- --- --- --- --- ---
```
At this point the game is terminated. Note that your input data contains moves beyond the end of the game. These should be ignored. Once the game has been won, end the game and move on. Each one line of the input data represents one game.

It is guaranteed that all moves are for valid columns (1 through 7) and that no player attempts to fill a column beyond its capacity (each column can hold up to 7 tokens).

Some games may end in a draw, with no player reaching 4 in a row.

Players can win a game by obtaining 4 tokens in a row horizontally, vertically or diagonally.
## Your task
Consider the memories of your 1000 games of Connect 4. How many games ended in a draw, or were won by each player?

Your answer is the number of games won by player 1 multiplied by the number of games won by player 2, multiplied again by the number of games won by player 3.

(ie: `player_1_wins * player_2_wins * player_3_wins`).
