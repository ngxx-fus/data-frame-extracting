### Data frame:
    <1 bit of parity> <8 bits of data MSB -> LSB> <1 bit of start bit>
### Transfer: 
    LSB -> MSB (right to left of data-frame) 
### Recorded in OSC: 
    <1 bit of start bit> <8 bits of data LSB -> MSB> <1 bit of parity >
### This software:
    Extract from input data:
        - Data
        - Parity
        - Start bit
    Create 10bits data-frame. 
### How to run this software:
    - Clone folder which has the prefix "Release"
    - Run exe file.
### Demo:
You can add several spaces in input, the app will auto remove them and process: </br>
Auto create 10bits data-frame when you type 1 character!

![plot](https://github.com/ngxx-fus/data-frame-extracting/blob/main/WindowsFormsApp1_R5qarcpIj1.png?raw=true)
![plot](https://github.com/ngxx-fus/data-frame-extracting/blob/main/WindowsFormsApp1_43fPYiaJ01.png?raw=true)

