# Debugging
![header](https://codingquest.io/may2022/programming-web.png)
As you walk around the markets, you hear someone becoming quite animated and expressing alien curses at their computer. Ever curious, you walk inside realise what you heard was an alien sitting at a coffee shop with their computer working on a a program they are writing. The alien complains they can't get the code to work properly. Being the friendly and knowledgeable computer scientist you are, you offer to take a look.

You immediately recognise they are programming in a form of assembly language. Fortunately the alien has the reference guide handy so you can look up the various op-codes to see what they do.

This assembler system automatically creates 12 integer variables (64 bit size) which are initially set to 0, one for each letter, A through L. That makes things more convenient than having to manually specify memory addresses like the form of assembly you are used to!

The opcodes for this language are:

- `ADD target source` - Take the value of source and add it to target
- `MOD target source` - Calculate the modulus target % source, and save to target
- `DIV target source` - Perform integer division of target // source, and save to target
- `MOV target source` - Take the value of source and copy it into target (replacing whatever was there)
- `JMP source` - Jump source number of instructions within the code
- `JIF source` - Jump source number of instructions within the code IF the most recent CEQ or CGE operation was TRUE
- `CEQ source1 source2` - Compare the values in source1 and source2, are they equal?
- `CGE source1 source2` - Compare the values in source1 and source2. Is source1 greater than or equal to source2?
- `OUT source` - Output the value of source to the terminal
- `END` - Terminate the program

Furthermore, `target` must always be a variable name, while `source` can either be a variable name or an integer value.
## Example 1
Consider the following program:
```
ADD A 10
ADD B 50
CGE A B
JIF 3
OUT 111
JMP 2
OUT B
END
```
This program will behave as follows:
```
Line 1. Set A to 10
Line 2. Set B to 50
Line 3. Is A >= B ?
Line 4. If yes, JUMP forward 3 instructions to line 7, otherwise continue
Line 5. Output 111 on the terminal
Line 6. Unconditional JUMP forward 2 instructions to line 8
Line 7. Output the content of B on the terminal
Line 8. End the program
```
As the program is currently written, it will output `111` but if you were to change B to 5 instead of 50, it would output `5`.
## Example 2
The following is a second example that demonstrates looping behaviour. It should output the first 10 numbers of the Fibonacci sequence: 1, 1, 2, 3, 5, 8, 13, 21, 34, 55.
```
ADD A 0
ADD B 1
ADD C 0
ADD I 0
MOV C A
ADD C B
MOV A B
MOV B C
ADD I 1
CEQ I 10
OUT A
JIF 2
JMP -8
END
```
## Your task
Help your new friend at the coffee shop. What is the output of the program contained in the input data?
