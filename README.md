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
    
### Submit.cs
- S: functions removed, middlegrounds found: add comments to longer operations, stating the intention of comparisons, so i dont have to do mental gymnastics when i revisit the code next time
     on hindsight i agree, making functions for specific operations does increase bloat

- should Blitzdata and Submit be the same class? 
    A: i originally split it out cuz submit class does alot of things already, to make it more modular i split it.
    can be moved back if you want, my idea for blitzData is that it stores and deals with the *confirmed* list of words, whereas submit gatekeeps that by checking validity etc.
    R: Is there a reason why blitzData is in a separate namespace/file from Submit cos I feel they are related, everything else yea get it
    A2: blitz uses submitletter and submitWord functions , analysis will use remove word function in blitzdata, although they act on the same list, their functions usage does not overlap,
    when counting scores, i also do not need the variables tile letters stack and tile positions, out of sight out of mind, keeping only what is relevant makes it easier for counting scores.

->summary: initialise screen(blitzscreen) ->intialise board(boardinitialiser) -> button clicked/panned (blitzevent handler)[currently broken for swipest] -> (cont \n)
    ->check legal button press(submit.cs)[check word validity not yet implemented]-> stores words submitted list  (blitzdata)
  prepared demo with comments in this version for tap mode, swipe mode is broken still[eventhandler needs foxing for swipe mode and swipelogic]

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

