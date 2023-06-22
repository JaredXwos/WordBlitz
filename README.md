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
- Add options to select Fun Mode/ Practice Mode in settings <- CY working on this
- Bind Score to BlitzScreen to update live <- JX awaiting the fun/practice config
- Allow for scoring system to be configured, Make a picker to allow users to choose the number of pts for words of specific lengths

### Less immediate To-do features
- Create windows specific UI using pointerGesture
- Create a function that can solve a boggle 4x4 given a dictionary
- Make background and tile styles in settings
- Create a loading bar XAML

### Minor Bugs
- SwipeTap mode insta submits another letter at final location upon submission, is not intended <- CY working on this

# How to get started
1. Send me your Github username, then accept the request to collaborate
2. Download visual studio installer if you haven't and install Visual Studio
3. Under Workloads, ensure that the .NET Multiplatform UI Development (under Desktop & Mobile) is seletected amongst others
4. Open Visual Studio, select clone a repository, dump the url of this repository in
5. Ensure you pull before you begin (under Git tab)

