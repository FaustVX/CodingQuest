# A special painting
![header](https://codingquest.io/may2022/a-strange-photo-web.jpg)
While your time on Ral'Malgor is about to come to an end, you take comfort knowing there will be more adventures ahead.

You board the ship, return to your cabin, and take time to admire your favorite souvenir - a portrait of you exploring the markets. One of the stalls was an art studio and you commissioned them to paint it while you were on your adventures. The artist has also sent you a digital copy of the painting (see image at bottom of page).

You take a closer look at the image and realise something looks a little strange. You have extraordinary eyesight and can observe very fine detail in the painting that doesn't quite look right. You decide to analyse the image at a binary level and discover someone has applied [steganography](https://en.wikipedia.org/wiki/Steganography) to your image!

The exact form of steganography that has occurred is that the [least significant bit](https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit_in_digital_steganography) of every byte has been altered to collectively form a message.

Complete the analysis of your image to find the hidden message!!
## An example
Consider the following 15x15 pixel greyscale image of hexadecimal values. For each pixel, 00 indicates black, and FF indicates white, with all values in between representing a shade of grey.

![example image](https://codingquest.io/may2022/010-example-7426324724.png)

The pixel data for this image is...
```
3a 3d 3c 40 41 3a 3c 46 42 3d 85 98 66 4f 48 
3f 5a 6b 6d 68 8f b9 c6 c6 a8 91 7b 5a 49 47 
44 8a 98 a5 c9 cc c7 c7 cd d7 e2 9a 51 44 41 
4d 8a 9c ba bc b9 b6 b8 c0 c4 c8 d2 7b 47 45 
48 89 b3 af 94 a1 b3 b0 87 85 a7 c3 b0 4f 45 
4b 8c b4 95 86 88 af a7 6c 41 6d bc b6 62 47 
51 92 ae 9d 94 92 a2 a0 6d 34 60 b4 ae 6f 4a 
36 72 9c a0 a2 96 76 8e 6e 36 5c aa a4 66 50 
2a 36 4e 6a 94 90 60 98 6a 38 5a a2 9c 6a 6a 
32 34 38 3e 4a 5e 62 a0 6c 3c 5a 9c 98 8c 74 
2e 30 36 36 2e 2e 5e a6 6e 42 5a 7a 88 74 68 
26 24 26 2a 2e 30 42 74 66 3e 5c 68 82 5a 66 
2e 24 22 22 2a 36 36 2c 2e 28 48 66 7c 5e 66 
2a 28 24 22 26 2e 38 38 36 32 2e 26 44 58 6c 
26 2c 2e 2a 24 2c 2e 32 38 36 34 2c 24 26 38 
```
The least significant bit of every byte can be combined to create a secret message. Given the first 8 bytes of this data is...
```
3A = 00111010
3D = 00111101
3C = 00111100
40 = 01000000
41 = 01000001
3A = 00111010
3C = 00111100
46 = 01000110
```
...the least significant bit of each of those values, can form a byte, `01001000`. This byte is `H` on the ASCII table.

Extracting the least significant bit from all pixels, every group of 8 is used to form a byte. Continue the process until you encounter a zero byte `00000000` to represent end-of-message.

The above image data contains the secret message: `'H', 'e', 'l', 'l', 'o', ',', ' ', 'w', 'o', 'r', 'l', 'd', '!', '\x00'`... or, more simply: `Hello, world!`.
## Your task
Your input data is the the digital painting of you exploring the markets as shown below. You will need to use an image processing library to extract the pixels directly from the image.

In contrast to the example above, this is a full RGB color image. The secret message is on the RED color channel.

What is the last word in the secret message? (ignore any punctuation)

Hints:

- Python: Check out [Pillow](https://pillow.readthedocs.io/en/stable/)
- Java: Check out [ImageIO and BufferedImage](https://docs.oracle.com/javase/tutorial/2d/images/index.html)
- Javascript: Check out [Canvas API](https://developer.mozilla.org/en-US/docs/Web/API/Canvas_API)
- C++: Check out [Magick++](http://www.imagemagick.org/Magick++/)

![image with hidden message](https://codingquest.io/may2022/010-inputdata-327485957345.png)
## Closing remarks
As this is the last challenge of the event, I'd like to acknowledge and thank the support of:

- Chris Hall, for proof reading and testing the challenges! Thank you for catching the errors!
- Vivian, for the incredible digital painting above that captures the alien market perfectly!
- Clarice and Emily, for the awesome cartoon sketches that appeared in the daily challenges. You helped bring each part of the story to life!
- James Abela, for the last minute language proof read and edits, much appreciated!

After this question please complete the [event feedback survey](https://docs.google.com/forms/d/e/1FAIpQLScCOXG8jv_h0a24pFZ_XZ0QrI0UEfAwoFjHOHzV4nYvHWMgqQ/viewform). I'd really appreciate it.

I hope you found the Quest an enjoyable and rewarding experience to develop your computer science skills.

See you next time!

_Paul Baumgarten._
