# Lottery tickets
![header](https://codingquest.io/may2022/lottery-web.jpg)
You have completed your assigned tasks and are free to explore the market near the port where the ship is docked. You grab your belongings and head out for your adventure.

One universal truth about markets is that hawkers outside their stalls can be very pushy, especially with tourists, and the grand markets of Ral'Malgor are no exception.

As you are approaching the main entrance of the markets, one hawker in particular convinces you to purchase some lottery tickets for a draw that is just about to start. You stay to watch the draw on his TV with interest as the numbers are called. You decide you want to calculate your winnings immediately so you can spend the proceeds while souvenir shopping!

The winnings work like this:

- A ticket with 3 of the winning numbers wins $1,
- A ticket with 4 of the winning numbers wins $10,
- A ticket with 5 of the winning numbers wins $100, and
- A ticket with 6 of the winning numbers wins $1000.

## An example
If the winning numbers are `12 48 30 95 15 55 97`, and your lottery tickets are...

```
46 46 5 87 92 73
95 73 30 12 97 27
34 49 42 41 58 16
33 5 91 60 40 88
74 52 63 74 19 31
13 31 13 6 68 4
57 36 41 17 40 15
29 59 20 85 73 42
31 67 82 51 44 80
48 41 55 12 15 30
```
...then you will have won $110.
This is because ticket `95 73 30 12 97 27` contains 4 of the winning numbers (winning $10) and ticket `48 41 55 12 15 30` contains 5 of the winning numbers (winning $100).
## Your task
The winning lottery numbers were `12 48 30 95 15 55 97`. Your input data contains the various lottery tickets you purchased. Each line represents a seperate ticket.

Process the input data and calculate the total of your winnings! That is your answer value (without the $ sign).

_To clarify: The same winning number may appear more than once on the same ticket, which would count as 'two' winning numbers._
