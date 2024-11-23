# Mayday!
After your day off, you report to duty just as some drama starts to occur on the ship bridge. The communications officer is reporting an incoming message on the emergency frequency! It sounds like a ship might be in trouble!

The emergency frequency uses its own customised networking protocol, designed specifically for emergencies. Unfortunately the last update to your communications computer broke the receiving software for it, so you are going to need to quickly write your own. The emergency network protocol is a simplified protocol compared to other networking protocols like TCP/IP.

The protocol used divides transmission into 256-bit packets using the following structure:

- Bits 0 to 15 (16 bits): A header to denote a new packet. Always set to 0101010101010101.
- Bits 16 to 47 (32 bits): Sender number. Unsigned 32 bit integer indicating the ship number sending the message.
- Bits 48 to 55 (8 bits): Sequence number. Used to reassemble the message into the correct order. Messages on the emergency frequency are often repeated many times to help ensure the message gets through interference. The first packet will have sequence number 0.
- Bits 56 to 63 (8 bits): Checksum. Used to ensure the packet has been correctly received. Each byte of the message payload is summed together. The modulus 256 of that summation is stored in the checksum byte.
- Bits 64 to 255 (192 bits or 24 bytes): Message content in 8-bit ASCII.

Decode the incoming message (your input data) and submit the reassembled message as your answer.
## Example
Consider the following two packets, shown in their hexadecimal representation.
```
55550000005800f754686973206973206120746573742e205468697320697320
55550000005801f06120746573742e205468616e6b796f752e20202020202020
```
The process to decode these packets follows:

1. The start of both packets is 5555. This converts to 0101 0101 0101 0101. Any packet not starting with this should be discarded.
2. The next part of both packets is 00000058. This converts to decimal 88, the ship number. Because these match, we know that both signals came from the same ship.
3. The next byte of both packets is 00 in the first, and 01 in the second. This represents the sequence number for when we reassemble the message portion of the packets into their correct order.
4. The next byte are the checksums: F7 in the first packet and F0 in the second.
5. The next 24 bytes represent the payload. Calculate the checksum for the data in the payload given to ensure it matches the checksum provided. Calculating the checksum involves summing the bytes of the payload together and finding the modulus of 256 of that summation. Looking at the first packet... The payload bytes are `54-68-69-73-20-69-73-20-61-20-74-65-73-74-2e-20-54-68-69-73-20-69-73-20`. Take this data one byte at a time, adding them together.
```
Hex 54 = Decimal 84
Hex 68 = Decimal 104
Hex 69 = Decimal 105
Hex 73 = Decimal 115
Hex 20 = Decimal 32
```
... continued to the end. When you sum these values together, they total to 2039. To calculate the correct checksum then, 2039 % 256 = 247, which is represented in hexadecimal as F7. This matches the content of the checksum byte so we know the packet is valid. Discard any packets with an invalid checksum.

Both packets are valid according to their checksum.

6. For the packets that have been kept, combine the message payloads together in correct order based on the sequence number.
```
Payload for sequence=0 is 54686973206973206120746573742e205468697320697320
Payload for sequence=1 is 6120746573742e205468616e6b796f752e20202020202020
Combined message payload is 54686973206973206120746573742e2054686973206973206120746573742e205468616e6b796f752e20202020202020
```
7. Use an ASCII table to convert this combined message payload into a human readable message (discarding any trailing spaces at the end).
```
54686973206973206120746573742e2054686973206973206120746573742e205468616e6b796f752e20202020202020
T h i s   i s   a   t e s t .   T h i s   i s   a   t e s t .   T h a n k y o u .
```
8. Therefore, the final decoded message for this example was `This is a test. This is a test. Thankyou.`.
## Your task
Decode the incoming message (your input data) and submit the reassembled message as your answer.
