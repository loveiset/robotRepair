using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameScript : MonoBehaviour {
    public GUISkin customSkin;

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
    bool playerHasWon = false;

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
                int someNum = Random.Range(0, aCards.Count);
                aGrids[i,j] = aCards[someNum];
                aCards.RemoveAt(someNum);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin = customSkin;
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        BuildGrid();
        //if (playerHasWon)
        {
            BuildWonPromot();
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    void BuildWonPromot()
    {
        int winPromotW = 200;
        int winPromotH = 120;

        float halfScreenW = Screen.width / 2;
        float halfScreenH = Screen.height / 2;

        int halfPromotW = winPromotW / 2;
        int halfPromotH = winPromotH / 2;

        GUI.BeginGroup(new Rect(halfScreenW - halfPromotW, halfScreenH - halfPromotH, winPromotW, winPromotH));
        GUI.Box(new Rect(0, 0, winPromotW, winPromotH), "YOU WIN!!!");
        if (GUI.Button(new Rect(10, 40, 100, 30), "PLAY AGAIN"))
        {
            Debug.Log("press");
            Application.LoadLevel("title");
        }
        GUI.EndGroup();
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
                string img;
                if (card.isMatched)
                {
                    img = "blank";
                }
                else
                {
                    if (card.isFaceUp)
                    {
                        img = card.img;
                    }
                    else
                    {
                        img = "wrench";
                    }
                }

                GUI.enabled = !card.isMatched;
                if (GUILayout.Button((Texture2D)Resources.Load(img),
                    GUILayout.Width(cardW)))
                {
                    if (playerCanClick)
                    {
                        StartCoroutine(FlipFaceUp(card));
                        Debug.Log(card.img);
                    }
                }
                GUI.enabled = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    void BuildDeck()
    {
        int id = 0;
        int totalRobots = 4;
        //Card card;
        for (int i = 0; i < totalRobots; i++)
        {
            List<string> aRobotsParts = new List<string> {"Head","Arm","Leg"};
            for (int j = 0; j < 2; j++)
            {
                int someNum = Random.Range(0, aRobotsParts.Count);
                string theMissingPart = aRobotsParts[someNum];

                aRobotsParts.RemoveAt(someNum);

                Card card = new Card("robot" + (i + 1) + "Missing" + theMissingPart, id);
                aCards.Add(card);

                Card card1 = new Card("robot" + (i + 1) + theMissingPart, id);
                aCards.Add(card1);

                id++;
            }
        }
    }

    IEnumerator FlipFaceUp(Card card)
    {
        card.isFaceUp = true;
        Debug.Log(aCardsFlipped.IndexOf(card));
        if (aCardsFlipped.IndexOf(card) < 0)
        {
            aCardsFlipped.Add(card);

            if (aCardsFlipped.Count == 2)
            {
                playerCanClick = false;
                yield return new WaitForSeconds(1);

                if (aCardsFlipped[0].id == aCardsFlipped[1].id)
                {
                    aCardsFlipped[0].isMatched = true;
                    aCardsFlipped[1].isMatched = true;

                    matchesMade++;

                    if (matchesMade >= matchesNeededToWin)
                    {
                        playerHasWon = true;
                    }
                }
                else
                {
                    aCardsFlipped[0].isFaceUp = false;
                    aCardsFlipped[1].isFaceUp = false;
                }

                aCardsFlipped.Clear();

                playerCanClick = true;
            }
        }
    }

    class Card : System.Object
    {
        public bool isFaceUp = false;
        public bool isMatched = false;
        public string img;
        public int id;



        public Card(string img, int id)
        {
            this.img = img;
            this.id = id;
        }
    }
}
