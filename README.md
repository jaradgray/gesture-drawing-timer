# Gesture Drawing Timer

Gesture Drawing Timer is a timed study tool for artists that shows each image in a selected folder in a random order for a selected length of time.

![Demo](/demo.png)

## Building

To build the project locally:

1. Clone or download the project's source
2. Open the solution in Visual Studio
3. Build the solution

## Usage

Usage of the app can be divided into two main categories:

1. Setting up a drawing session, and
2. Using the app during a drawing session

### Setting up a drawing session

To configure the app to show images:

- Click the "Select folder" button to choose a folder that contains the images you want to draw
  - Note: due to WPF's lack of a good folder selection dialog, a file selection dialog is used instead, so users should select any file from the desired folder instead of the folder itself
- Click a time interval button to select the duration each image will be shown
- When you're ready to start drawing, click the "Practice!" button

### Using the app during a drawing session

The app automatically counts down the time until the selected interval has expired, then switches to the next image. You can override this behaviour with the onscreen UI or with the keyboard:

- Click the Play/Pause button (or press the spacebar) to pause or resume the interval timer
- Click the left/right arrow buttons (or press the left/right arrow keys) to show the previous or next image

## Technology used

This app is built using:

- C#
- WPF (.NET Framework 4.6.1)

## What I learned

- Familiarity with the MVVM design pattern
- Sharing data between ViewModel instances
- Styling resources using merged ResourceDictionaries

## Credits

- This app is a personal project inspired by [GestureDrawing!](https://cubebrush.co/advanches/products/d9q6yq/gesturedrawing) by Daniel Vanches