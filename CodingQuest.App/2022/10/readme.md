# Check the heat shields
![header](https://codingquest.io/may2022/heatshields-web.png)
You get a call from one of the safety engineers from the ship. They are having trouble with the computer system that verifies the integrity of the [heat shields](https://en.wikipedia.org/wiki/Heat_shield) and ask for your help.

The use of heat shields is a legacy that still persists since the 20th century, and just like then, they are still prone to being easily damaged or falling off. So frustrating! Each Earth Ship carries a stock of spare heat shields to replace damaged or missing shields, and a check is performed before any flight departs.

The ship computer generates a report for you (your input data) that comprises space occupied by each tile, but the system that is supposed to process this data and determine if it is safe is not functioning. Your task is to cross reference the location and dimensions of each tile with the surface area on the bottom of the ship (where the heat shield resides). Multiple heat shields on the same location are ok, but if there are any gaps in the heat shield coverage, they need to be identified for repair.

Each heat shield is given to you as four values: rectangles indicating their location and size. The values represent their X location, Y location, width and height.

Process the input data looking for surface area not covered by a heat shield. Calculate the total surface area in square millimetres that is currently not covered by a heat shield. That is your answer value.
## An example
Suppose the space to fill was 10 x 10 units, which could be represented as this...
```
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
```
... and the following is the heat shield data given ...
```
0 0 5 7
7 0 3 9
0 6 10 4
```
The first tile has x,y location of 0,0 and a width of 5, height of 7. It could be represented as
```
X X X X X . . . . .
X X X X X . . . . .
X X X X X . . . . .
X X X X X . . . . .
X X X X X . . . . .
X X X X X . . . . .
X X X X X . . . . .
. . . . . . . . . .
. . . . . . . . . .
. . . . . . . . . .
```
The second tile of `7 0 3 9` would then look like this
```
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
. . . . . . . Y Y Y
. . . . . . . Y Y Y
. . . . . . . . . .
```
Add the third tile, `0 6 10 4`. Note this tile provides an example showing double-ups may occur.
```
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
X X X X X . . Y Y Y
Z Z Z Z Z Z Z Z Z Z
Z Z Z Z Z Z Z Z Z Z
Z Z Z Z Z Z Z Z Z Z
Z Z Z Z Z Z Z Z Z Z
```
The end of this example is that there are 12 positions left uncovered, so that would be the answer.
## Example 2
A more detailed example. Given a 100 x 100 grid, the following heat shield tile data should result in `2061` spaces remaining.
```
66 87 34 13
64 67 36 33
40 54 58 46
51 17 49 45
83 15 17 51
46 46 51 54
20 34 52 52
65 21 35 46
32 68 49 32
13 79 43 21
87 81 13 19
65 26 35 55
46 79 51 21
17 53 46 45
77 17 23 41
4 17 54 47
7 28 42 53
9 47 45 41
40 14 45 44
77 61 23 39
```
## Your task
The ships bottom has width 20000mm and height 100000mm.

How many square millimetres remain uncovered?
