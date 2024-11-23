# Navigation sensor
Your hopes for a restful trip home have been quickly broken. The main navigation sensor onboard is producing weird data and the captain has asked for your assistance in figuring it out so they can plot a course home to Earth! It seems the network connection between the sensors outside the ship and the navigation computer are experiencing heavy interference resulting in a lot of bit flips occurring. You look at the software on the navigation computer and realise it is not checking the parity bits before accepting the data. Whoever the original programmer was must have assumed fluctuations were so rare that they didn't bother implementing such a simple check! Fortunately you understand the importance of parity bits and how they work.

You record a few values from the sensors to analyse (your input data). This data consists of 16 bit unsigned integers where the least significant 15 bits represent a positive value (ie: 0 to 32676) and the 16th bit is reserved for parity. According to the manuals, the navigation sensors are designed to use even parity.

To determine the correct value coming from the navigation sensors:

1- Discard values from the sensor that fail even parity.
2- With the values that remain, remove the parity bit so you just have the value portion of the unsigned integer.
3- What is the average (rounded to nearest integer) of these remaining values?
## Example
Consider the following input data:
```
30635
34132
46818
15895
37924
52364
31114
4040
6676
53800
```
Most of these values do not adhere to even parity. For instance, 30635 in binary is 111011110101011 which has 11 bits turned on. In fact, of the above values, only 3 adhere to even parity. These are the only 3 you would keep in this scenario.
```
34132
31114
53800
```
Turning off the 16th bit for each (to remove the effect of the parity bit on the value of the number) changes them to...
```
1364
31114
21032
```
The average of those 3 numbers is 17837 (rounded to nearest integer). That would be the value the navigation sensor is trying to provide.
## Your task
Using the input data, find the value the navigation sensor is trying to provide the navigation computer.
