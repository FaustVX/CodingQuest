# Message from home
![header](https://codingquest.io/may2022/buy-a-postcard-web.png)
You receive a message from your sister back on Earth. Because you are both computer science specialists, you communicate with each other using a private cipher, for a little bit of nerdy fun. The cipher you use is derived from the [Vigenère cipher](https://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher).

The Vigenère cipher requires three pieces of information to encode and decode messages:

- Secret key - The "password" or "secret information" that is used to lock and unlock the message.
- Agreed character set - The letters and symbols you will change when they appear in the message.
- Text to encode or decode - The content of your message.
## Example 1
Suppose you have the following information:

- Secret key: `SECRET`
- Character set: `ABCDEFGHIJKLMNOPQRSTUVWXYZ`
- Message: `WE COME IN PEACE`

To encode the message:

- Look at the first letter of the key, `S`. This is the 19th character of the character set.
- Look at the first letter of the message, `W`. Take the `W` and increase it 19 places through the character set. in this case, X, Y, Z, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P.
- The first letter of the message, `W` is therefore converted to `P`.
- For the next letter of the message, use the next letter of the secret key as well. In this example, the `E` of the secret key indicates we will move forward 5 places. This will mean the `E` of the message will become `J`.
- The third character of the message is a space. This is not in the character set, so use it unmodified.

Some clarifications:

- If you run out of letters in the character set, return to the start and keep counting.
- If you run out of letters in the secret key, return to the start of that as well.
- If part of the message does not appear in the character set, use it unmodified. In this example, the space character is not in the character set, so we just add spaces to the message without change.

The full converted text for the above example is: `PJ UTGX LF JXFFW`.
## Decoding
To decode, the process is exactly the same except you move backwards through the character set instead of forwards.

Consider the first example again. The encoded message starts with `P` and the secret key starts with `S`. So, take the letter `P` and move backwards 19 places to get the original `W`... O, N, M, L, K, J, I, H, G, F, E, D, C, B, A, Z, Y, X, W.
## Example 2
To make the encryption more useful, it helps to have uppercase, lowercase, numbers and punctuation in the character set.

- Secret key: `With great power comes great responsibility`
- Character set: `abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,;:?! '()` (the 4th last item is a space)
- Message: `Are you enjoying coding quest?`

The same rules apply just the character set is larger. The first letter of the secret key is `W` which is the 40th item in the characeter set. So the first letter of the message, `A` is incremented 40 places like so: `B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9 . , ; : ? ! space ' ( ) a b c d`.

This example should convert to: `dAyevvMbfHgENFsy:fDqnGddIzfMqm`
## Example 3
- Secret key: `With great power comes great responsibility`
- Character set: `abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,;:?! '()`
- Encoded message: `lfwwrsvbvMbmIEnK:wDjutpzoxfwowypDDHxB(rzfwKXBMd`

Should decode to: `I could use this to pass secret notes in class!`.
## Your task
Decode the message from your sister.

- The character set is `abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.,;:?! '()`
- Your shared secret key is `Roads? Where We're Going, We Don't Need Roads.`
- The message you received is the input data.
