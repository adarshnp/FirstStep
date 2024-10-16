using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles setting up the card grid, flipping cards, and managing card matching logic.
/// </summary>
public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance;
    public GameObject cardPrefab;
    public Transform board;
    public int totalPairs;
    public Camera cam;

    private List<int> cardValues = new List<int>();

    public int row;
    public int col;
    public float spacing;
    public float cardSize;
    public Transform samplecube;


    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private bool isCheckingMatch = false;

    private Vector3 centerRightAnchorPoint;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameManager.instance.SetTotalPairsCount(totalPairs);
        InitializeBoard(row, col);
    }
    public void InitializeBoard(int rows, int columns)
    {
        totalPairs = (rows * columns) / 2;

        // Initialize card Values for each pair
        for (int i = 0; i < totalPairs; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        //shuffle card values here


        //find cener right point of camera viewport in world space
        Vector3 centerRightAnchorPoint = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));

        Vector3 gridPosition = new Vector3(centerRightAnchorPoint.x - col * spacing, centerRightAnchorPoint.y - row * 0.5f * spacing, 0);
        transform.position = gridPosition;
        samplecube.position = centerRightAnchorPoint;

        // Card Distribution
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int cardIndex = row * columns + col;
                if (cardIndex >= cardValues.Count) break;

                GameObject newCard = Instantiate(cardPrefab, board);
                newCard.transform.localPosition = new Vector3(col * spacing, row * spacing, 0);

                Card cardComponent = newCard.GetComponent<Card>();
                cardComponent.SetCardValue(cardValues[cardIndex]);  // Assigning card value
            }
        }
    }

    public void CardSelected(Card selectedCard)
    {
        if (isCheckingMatch || selectedCard.isMatched || selectedCard == firstSelectedCard) return;

        GameManager.instance.IncrementTurns();

        if (firstSelectedCard == null)
        {
            firstSelectedCard = selectedCard;
            selectedCard.StartCardOpenAnimation();
        }
        else
        {
            secondSelectedCard = selectedCard;
            selectedCard.StartCardOpenAnimation();
            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        isCheckingMatch = true;

        //increment move count

        yield return new WaitForSeconds(1f);  // Delay for the user to see the flipped cards

        if (firstSelectedCard.value == secondSelectedCard.value)
        {
            firstSelectedCard.MarkAsMatched();
            secondSelectedCard.MarkAsMatched();
            GameManager.instance.IncrementMatches();
            //play match sound

        }
        else
        {
            firstSelectedCard.StartCardReturnAnimation();
            secondSelectedCard.StartCardReturnAnimation();
            //play mismatch sound
        }

        firstSelectedCard = null;
        secondSelectedCard = null;
        isCheckingMatch = false;
    }


}
