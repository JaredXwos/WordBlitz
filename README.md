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
- Blitz (Navigates back to MainPage, navigates to Analysis)
- Config (Navigates back to MainPage)
- Analysis (Navigates back to MainPage)

## Immediate To-do features
- Make a picker to allow users to choose the number of pts for words of specific lengths
- Create a field in config to save user configured scoring system and use that in score tabulation
- Implement tap to crossout, tap again to undo in analysis <- CY working on this
- Reimplement counting of score in Analysis
- Check if the new blitzgrid initialisation is satisfactory and delete redundant code <- CY check

### Less immediate To-do features
- Figure out how to detect swiping and fix that as a binding <- CY working on this
- Create a function that can solve a boggle 4x4 given a dictionary
- Make background and tile styles in settings
- Enable button backtracking
- Create a loading bar XAML 
- Figure out how to allow code to load a grid object

### Bugs
- Slider blitztime in settings minimum amount needs to be 0
- Fix the swipe in android which broke by adding a label to be swiped over at the top and bottom <- CY working on this
- Ensure the title.png can be resized to fit the height of the screen automatically <- CY working on this

# How to get started
1. Send me your Github username, then accept the request to collaborate
2. Download visual studio installer if you haven't and install Visual Studio
3. Under Workloads, ensure that the .NET Multiplatform UI Development (under Desktop & Mobile) is seletected amongst others
4. Open Visual Studio, select clone a repository, dump the url of this repository in
5. Ensure you pull before you begin (under Git tab)

