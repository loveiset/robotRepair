using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameScript : MonoBehaviour {

    int cols = 4;
    int rows = 4;

    int totalCards;
    int matchesNeededToWin;

    int matchesMade = 0;
    int cardW = 100;
    int cardH = 100;

    List<Card> aCards;
    Card[,] aGrids;
    List<Card> aCardsFlipped;
    bool playerCanClick;
    bool palyerHasWon = false;

	// Use this for initialization
	void Start () 
    {
        playerCanClick = true;
        totalCards = cols * rows;
        matchesNeededToWin = totalCards/2;

        aCards = new List<Card>();
        aGrids = new Card[cols,rows];
        aCardsFlipped = new List<Card>();

        BuildDeck();

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                aGrids[i,j] = new Card();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        BuildGrid();
        GUILayout.EndArea();
    }

    void BuildGrid()
    {
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        for (int i = 0; i < rows; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int j = 0; j < cols; j++)
            {
                Card card = aGrids[i,j];
                if (GUILayout.Button((Texture2D)Resources.Load(card.img),
                    GUILayout.Width(cardW)))
                {
                    Debug.Log(card.img);
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    void BuildDeck()
    {
        int totalRobots = 4;
        Card card = new Card();
        for (int i = 0; i < totalRobots; i++)
        {
            List<string> aRobotsParts = new List<string> {"Head","Arm","Leg"};
            for (int j = 0; j < 2; j++)
            {
                int someNum = Random.Range(0, aRobotsParts.Count);
                string theMissingPart = aRobotsParts[someNum];

                aRobotsParts.RemoveAt(someNum);
            }
        }
    }

    class Card : System.Object
    {
        public bool isFaceUp = false;
        public bool isMatched = false;
        public string img;



        public Card()
        {
            img = "robot";
        }
    }
}
