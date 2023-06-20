using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    //custom interface defininitions
    public interface IViewText
    {
        string Text { get; set; }
    }

    public interface IScoreCounter //all the public functions we will (want to and able to access) aside from constructors is here
    {
        int getCurrentTotalScore { get; }
        void OnSubmission(string submittedWord);

        //OnRemovedWord(string submittedWord) method is available for practice mode score counter
    }   //FunScoreCounter(IViewText elementParam , Grid boardGridParam) , PracticeScoreCounter(Grid boardGridParam)





    //base scoreCounter class definintion---------------------
    public class ScoreCounter : IScoreCounter
    {
        //variables----------------------------
        protected HashSet<string> validwords = new();//will make a shorter list specific to board
        protected int currentWordScore = 0;
        protected Grid boardGrid;


        //getters-----------------------------
        protected int getCurrentWordScore => currentWordScore;
        public virtual int getCurrentTotalScore => currentWordScore;


        //constructor------------------------
        public ScoreCounter(Grid boardGridParam)//constructor, pass in the score display label as element if fun mode, constructed for every new board
        {
            boardGrid = boardGridParam;
            validwords = Dict.dict;
            /*Dict.dict.Intersect(Submit.Getlist()).Where(c => c.Length > 2)*/ //temporaily not using this, TODO implement check possible words on board

            //{insert whatever other constructors here}
        }



        //methods---------------------------
        protected bool checkValid(string submittedWord)
        {
            if (validwords.Contains(submittedWord)) { return true; } else { return false; }
        }

        protected int countWordScore(string submittedWord)
        {
            switch (submittedWord.Length)
            {
                case 3: return 1; //3 letter is most common so check that first
                case 4: return 1;   
                case 5: return 2;
                case 6: return 3;
                case 7: return 5;
                default: if (submittedWord.Length < 3) { throw new ArgumentException("[custom exception written by cy:word length was not checked before sending to count score!!!]"); }
                    else { return 7; }
            }
        }

        public virtual void OnSubmission(string submittedWord)
        {
            currentWordScore += checkValid(submittedWord) ? countWordScore(submittedWord) : 0;
        }
        //end of class defnition------
    }






    //class defnition for Fun Mode
    public class FunScoreCounter : ScoreCounter
    {
        protected IViewText element;
        FunScoreCounter(IViewText elementParam , Grid boardGridParam) : base(boardGridParam)
        { 
            element.Text = $"{currentWordScore}"; 
        }

        public override void OnSubmission(string submittedWord) 
        { 
            base.OnSubmission(submittedWord);//updates score
            updateWith(submittedWord); //updates element
        }

        public void updateWith(string submittedWord)
        {
            element.Text = $"{currentWordScore}";
        }
    }





    //class definition for Practice Mode
    public class PracticeScoreCounter : ScoreCounter
    {
        //variables----------------------------
        private int currentPenaltyScore = 0;

        //getters-----------------------------
        private int getCurrentPenaltyScore => currentPenaltyScore;
        public override int getCurrentTotalScore => (currentWordScore - currentPenaltyScore);

        //constructor------------------------
        PracticeScoreCounter(Grid boardGridParam) : base(boardGridParam){}

        //methods---------------------------
        private int countPenaltyScore(string submittedWord)
        {
            switch (submittedWord.Length)
            {
                case 3: return 1; //3 letter is most common so check that first
                case 4: return 1;
                case 5: return 1;
                case 6: return 2;
                case 7: return 3;
                default:
                    if (submittedWord.Length < 3) { throw new ArgumentException("[custom exception written by cy:word length was not checked before sending to count score!!!]"); }
                    else { return 4; }
            }
        }

        public override void OnSubmission(string submittedWord)
        {
            base.OnSubmission(submittedWord);//adds word score
            currentPenaltyScore += checkValid(submittedWord) ? countPenaltyScore(submittedWord) : 0;//penalises for wrong words
        }

        public void OnRemovedWord(string submittedWord)
        {
            if (checkValid(submittedWord)) { currentWordScore    -= countWordScore   (submittedWord); }
            else                           { currentPenaltyScore -= countPenaltyScore(submittedWord); }
        }
    }
}

