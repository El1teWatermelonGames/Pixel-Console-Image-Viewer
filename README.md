# Pixel Console Image Viewer

This is a very simple image viewer project for the console using a custom filetype, Pixel Console Image (.pci). This project was inspired by learning how image files are stored from my computer science lessons.

## How to create a file

To create a file just make a file with any name with the .pci file extension. These files use hexadecimal for determining each pixel color, you can create a new row of pixels just by making a new line in the .pci file.

## Color Code
- 0 | BLACK
- 1 | RED
- 2 | GREEN
- 3 | YELLOW
- 4 | BLUE
- 5 | MAGENTA
- 6 | CYAN
- 7 | WHITE
- 8 | BRIGHT BLACK
- 9 | BRIGHT RED
- A | BRIGHT GREEN
- B | BRIGHT YELLOW
- C | BRIGHT BLUE
- D | BRIGHT MAGENTA
- E | BRIGHT CYAN
- F | BRIGHT WHITE

## How to render an image

To render an image place the .pci file inside the same directory as the renderer (this applies to **all** versions of the program unless specified). Once you run the render just enter the image file's name. You can render the image with the color code printed over the image by writing "pat." infront of the file.

## All versions of the program

### Planned: C++, Rust
### In Development: C#
### Stable Version: Python