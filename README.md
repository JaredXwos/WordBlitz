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

## Errors specific to CYTest5
### BlitzScreen.xaml.cs
- OnGridButtonTap function not complete, add letter handler no arguments? No references to function, usage unclear.
- OnGridButtonPanned function not complete, calls nonexistent function with parameters as variable declarations (illegal), usage unclear
- Method names first letter should be caps as per C# guidelines to distinguish them from variables
### Submit.cs
- isPreviousTile does not check previous tile, it checks if 2 pairs of values are equal. Consider isPairEqual, or instead making PreviousTile check the previous tile
- isSameTile literally does the exact same thing as IsPreviousTile, compares 2 pairs of ints. Consider removing it altogether or making it actually useful
- Consider making 1 function for both of them
- in submitLetter(), question why you used .Item1 .Item2 instead of just (int i, int j) = position; do you find it more readable?
  this is more pythonic and imo more readable, if needed could declare int i,j,lasti,lastj; at the top then (i,j) = position;
- should Blitzdata and Submit be the same class? why is there a need to separate the submit word from submit letter
- once you are done settling that please fix the line in Analysis.xaml.cs to retrive the list

## Immediate To-do features
- Reimplement counting of score in Analysis <- CY working on this
- Add options to select Fun Mode/ Practice Mode in settings <- CY working on this
- Bind Score to BlitzScreen to update live <- CY working on this

### Less immediate To-do features
- Create windows specific UI using pointerGesture <- JX working on this
- Create a function that can solve a boggle 4x4 given a dictionary
- Make background and tile styles in settings
- Enable button backtracking <- JX working on this
- Create a loading bar XAML
- Allow for scoring system to be configured, Make a picker to allow users to choose the number of pts for words of specific lengths
- implement score counter and check for bugs <- CY is working on this

### Bugs
- Fix the swipe in android which broke by adding a label to be swiped over at the top and bottom
- Implement queues for Config.Submit to prevent crash <- JX is almost done working on this
- Fix analysis timing navigation loop 

# How to get started
1. Send me your Github username, then accept the request to collaborate
2. Download visual studio installer if you haven't and install Visual Studio
3. Under Workloads, ensure that the .NET Multiplatform UI Development (under Desktop & Mobile) is seletected amongst others
4. Open Visual Studio, select clone a repository, dump the url of this repository in
5. Ensure you pull before you begin (under Git tab)

