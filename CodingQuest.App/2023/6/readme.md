# Inventory check
The year is 2223 and you are completing your apprenticeship on the Starship Turing. You have had fun exploring the stars and strange new worlds but your mission is over and you are now headed home to Earth. Will your technical expertise in computer science be needed to save the day, or will you be able to relax and enjoy the ride?
##
The ship is departing from a recent stop at a space station where you took on-board new supplies for the trip. The captain has asked you to process all the goods in the loading bay and ensure it has been properly logged by the ship's master inventory system. Fortunately all the goods have barcodes on them that allow you to determine what they contain. You quickly scan all the codes into your computer (your input data). The scans provide three items of information: A unique item number, a quantity, and the category of the goods.

In order to confirm the inventory has been processed correctly, your task is:

1. To sum the total quantity brought on board for each category of supplies.
2. Once you have the totals, find the modulus of 100 for each category total.
3. Calculate the product of all those values.

That should match the verification value from the inventory computer.
## Example
For this worked example, let's start with an extract of the input data:
```
ed669507-8122-4f78-bc59-e1c6518aa3f1 62686 Fuel
707ff3b1-9b0c-4c43-a495-47af7970486f 45574 Food
1d8101bb-ebf1-4802-9da2-27bb4e238313 61915 Mechanical
91034b54-9170-4f7e-97d5-687893171cb5 04859 Water
bf06f8a5-b894-4a06-9b1d-b67236f982da 08414 Water
a056a0fd-0b61-4f38-a874-a464646447af 28211 Frozen
788cc982-73b2-4c73-ae78-c19aef96cfbf 09164 Food
55df7ab8-29fd-463a-a986-98d400437db2 30952 Food
f768ae8a-51f0-48a3-b1db-c7d2ff7b51e5 21834 Mechanical
a3f64ee8-eb55-41bf-9d66-0a4091e7cc2f 46272 Water
```
The first step to verifying the inventory computer has properly recorded the stock items, is to determine the total quantity for each individual category. Using the above data, you would get the following totals:
```
Food: 85690 units
Frozen: 28211 units
Water: 59545 units
Fuel: 62686 units
Mechanical: 83749 units
```
Having the totals for each category (note there might be additional categories in the input data), take the modulus of 100 for each category.
```
85690 % 100 = 90
28211 % 100 = 11
59545 % 100 = 45
62686 % 100 = 86
83749 % 100 = 49
```
Now find the product of those values.
```
90 * 11 * 45 * 86 * 49 = 187733700
```
In this example 187733700 would be the answer.
## Your task
Perform your verification calculations and if you get the same answer as the inventory computer, then you know the stock has been properly recorded!
