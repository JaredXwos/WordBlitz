# WordBlitz
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

### Bugs
- font size issue for some phones <- change all "large" to fontsize 25
- Q/Qu bug
- game mode only changes on restart
