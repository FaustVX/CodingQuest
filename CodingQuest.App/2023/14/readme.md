# Snakes on a spaceship
Having rescued the crew and passengers of StarShip Titanic II, you sit down with a fellow apprentice from that ship to chat and play a few computer games. You end up challenging each other on the classic arcade game Snake.

Snake involves moving a character across a screen grid to eat the fruit that appears. Every time the snake eats a piece of fruit, the snake grows 1 piece longer. If the snake moves outside the boundaries of the game board, or crosses over itself, the game is over.

Your game of snake is based on the following rules. What was your score?

1. Every successful move of the snake character is awarded 1 point.
2. Every time the snake character eats a piece of fruit is awarded 100 points, and the length of the snake grows by 1.
3. Fruit is 'eaten' when the front of the snake (the 'head') is on the same grid coordinates as the current piece of fruit. Once a piece of fruit is 'eaten', it is removed from the board, and the next piece of fruit is placed onto the board, based on the coordinates provided through the input data.
4. The input data also contains the moves made by your snake character as follows:
    - L = the head of the snake moves 1 square to the left
    - R = the head of the snake moves 1 square to the right
    - U = the head of the snake moves 1 square up
    - D = the head of the snake moves 1 square down
5. Coordinates 0,0 is the top left corner of the game board
6. The game board is 20,20 in size. If the snake attempts to move outside these limits, the game is over.
7. At the start of the game, only the first piece of fruit is on the board, and the head of the snake will be at position 0,0 with length of 1 block (the 'head').
## Example
The following example operates on a reduced size game board of 8x8 and the input data of:
```
fruit
3,3 2,5 7,7 6,0
moves
DDDRRRDDLLLDRRRRRRRDD
```
At the start of the game, the snake is at 0,0 and the first piece of fruit is at 3,3 so the board would resemble
```
S.......
........
........
...F....
........
........
........
........
```
#1: Move down. Score updated to 1
```
........
S.......
........
...F....
........
........
........
........
```
#2: Move down. Score updated to 2
```
........
........
S.......
...F....
........
........
........
........
```
#3: Move down. Score updated to 3
```
........
........
........
S..F....
........
........
........
........
```
#4: Move right. Score updated to 4
```
........
........
........
.S.F....
........
........
........
........
```
#5: Move right. Score updated to 5
```
........
........
........
..SF....
........
........
........
........
```
#6: Move right. Score updated to 106 (1 point for the move, 100 for eating fruit). The next piece of fruit is placed onto the board.
```
........
........
........
..SS....
........
..F.....
........
........
```
#7: Move down. Score updated to 107
```
........
........
........
...S....
...S....
..F.....
........
........
```
#8: Move down. Score updated to 108
```
........
........
........
........
...S....
..FS....
........
........
```
#9: Move left. Score updated to 209. Next piece of fruit is placed onto the board.
```
........
........
........
........
...S....
..SS....
........
.......F
```
#10: Move left. Score updated to 210
```
........
........
........
........
........
.SSS....
........
.......F
```
#11: Move left. Score updated to 211
```
........
........
........
........
........
SSS.....
........
.......F
```
#12: Move down. Score updated to 212
```
........
........
........
........
........
SS......
S.......
.......F
```
#13: Move right. Score updated to 213
```
........
........
........
........
........
S.......
SS......
.......F
```
#14: Move right. Score updated to 214
```
........
........
........
........
........
........
SSS.....
.......F
```
#15: Move right. Score updated to 215
```
........
........
........
........
........
........
.SSS....
.......F
```
#16: Move right. Score updated to 216
```
........
........
........
........
........
........
..SSS...
.......F
```
#17: Move right. Score updated to 217
```
........
........
........
........
........
........
...SSS..
.......F
```
#18: Move right. Score updated to 218
```
........
........
........
........
........
........
....SSS.
.......F
```
#19: Move right. Score updated to 219
```
........
........
........
........
........
........
.....SSS
.......F
```
#20: Move down. Score updated to 320. Next piece of fruit is placed onto the board.
```
......F.
........
........
........
........
........
.....SSS
.......S
```
#21: Move down. OUT OF BOUNDS.
Therefore the final score in this example was 320.
## Your task
Process the snake game recorded in the input data. What was the score?