# WordBlitz

## Project Ideals

### Purpose
Provide a digital alternative for boggle players to train while on the go.

### Function
This mobile application will have a two part functionality, word finding practice and anagram finding.

In the practice mode, the user will be provided with a simulation of a boggle board, and invited to play a 3 min boggle style word search.
After the game, the app will mark the words and provide feedback.

In the anagram finding, the user will be given a minature section of the boggle board with best-value triangle of letters.
These best value triangles will be calculated based off the user's dictionary of choice.
Each triangle+letter(s) may or may not contain acceptable words, and the user is to imput all the words and be assessed for completeness and acurracy.

In addition, there will be a configuration page where the user can choose 
- a desired dictionary
- best value ratio (between commonness of triangle pair and number of valid hooks)
- grading settings used to assess performance
- graphical settings

## Structure
This is a MAUI project, and thus consists of 
- XAML files, representing a DOM for each page
- C# files representing the program bidnings for each XAML DOM
- A global (public) C# file containing global variables like config and user data.

Each page consists of the XAML DOM as well as their corresponding C# file.
Component pages are nested under the AppShell, which is in turn nested under the App.

Thus far component pages include
- MainPage (Home page which navigates to blitz and config)
- Blitz (Navigates back to MainPage)
- Config (Navigates back to Config)

## Immediate To-do
- Fix clunkly swipe gestures on Windows and Android (clunky in different ways)
- Optimise the button clicks and swipes somehow (was built rushed)
- Make a picker to allow users to choose timing for simulated boggle game
- Create a field in config to save a user created time and use that in the timer object
- Allow users to delete words before they are checked and score counted

### Less immediate To-do
- Figure out how to detect swiping and fix that as a binding
- Create a function that can solve a boggle 4x4 given a dictionary
- Formalise a way to store graphical settings like colours of buttons
- Amend the menu buttons such that they show they are being pressed
- Create a loading bar XAML
- Figure out how to allow code to load a grid object
- Make loading of dictionaries truly async


# How to get started
1. Send me your Github username, then accept the request to collaborate
2. Download visual studio installer if you haven't and install Visual Studio
3. Under Workloads, ensure that the .NET Multiplatform UI Development (under Desktop & Mobile) is seletected amongst others
4. Open Visual Studio, select clone a repository, dump the url of this repository in
5. Ensure you pull before you begin (under Git tab)

