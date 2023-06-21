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
- isPreviousTile does not check previous tile, it checks if 2 pairs of values are equal. Consider isPairEqual, or instead making PreviousTile check the previous tile
    A: it seems to work as i intended doesnt seem to have a bug. [bug is with blitzeventhandler if ur refering to swipe mode]
- isSameTile literally does the exact same thing as IsPreviousTile, compares 2 pairs of ints. Consider removing it altogether or making it actually useful
    A: i made them unique to its more readable, it says what its doing.
    its function is to prevent submission when the user clicks on the same tile twice.
    R: Disagree. You aren't even comparing tiles in the first place, you're comparing coords.
       And you aren't even comparing coords else it would take two Tuple<int,int> instead of 2 pairs of ints
       Furthermore, the "IsPrevious" is meaningless cos you already insert the previous coordinates. I could call IsPrevious() on any pair of coordinates, not only previous
       Consider a function that takes a stack, and a tuple, then checks if the tuple is at the top of the stack/the second to top of the stack
       This will much better reflect the function (It checks if the given tuple is previous on the stack, or if you want to call the coordinates tiles, it checks if the given tile is previous)
       This is much better than checking if two pairs of ints are the same or at most stretching it, if two pairs of coordinates are the same, i.e. IsSame()
       Note the way you are using both functions in the code is as if both functions are IsSame()
       You get top and current tile (cur) then IsSame(cur,top), you get previous and cur, then IsSame(prev, cur)
- Consider making 1 function for both of them
    A: it can be done, though i feel it is a trivial change.
- in submitLetter(), question why you used .Item1 .Item2 instead of just (int i, int j) = position; do you find it more readable?
  this is more pythonic and imo more readable, if needed could declare int i,j,lasti,lastj; at the top then (i,j) = position;
    A: i dint know i can do that. changed to as you described.
- should Blitzdata and Submit be the same class? 
    A: i originally split it out cuz submit class does alot of things already, to make it more modular i split it.
    can be moved back if you want, my idea for blitzData is that it stores and deals with the *confirmed* list of words, whereas submit gatekeeps that by checking validity etc.
    R: Is there a reason why blitzData is in a separate namespace/file from Submit cos I feel they are related, everything else yea get it

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

