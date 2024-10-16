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
    private int totalPairs;
    public Camera cam;

    private List<int> cardValues = new List<int>();

    public float spacing;

    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private bool isCheckingMatch = false;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        GameManager.instance.onGridGeneration += InitializeBoard;
    }

    private void InitializeBoard(int rows, int columns)
    {
        ClearCards();
        cardValues.Clear();

        totalPairs = (rows * columns) / 2;

        GameManager.instance.SetTotalPairsCount(totalPairs);

        // Initialize card Values for each pair
        for (int i = 0; i < totalPairs; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        ShuffleCardValues();

        PopulateCardsToGrid(rows, columns);
    }
    
    private void PopulateCardsToGrid(int rows, int columns)
    {
        float halfLength = (columns - 1) * spacing * 0.5f;
        float halfHeight = (rows - 1) * spacing * 0.5f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                int cardIndex = row * columns + col;
                if (cardIndex >= cardValues.Count) break;

                GameObject newCard = Instantiate(cardPrefab, board);
                newCard.transform.localPosition = new Vector3(col * spacing - halfLength, row * spacing - halfHeight, 0);

                Card cardComponent = newCard.GetComponent<Card>();
                cardComponent.SetCardValue(cardValues[cardIndex]);
            }
        }
    }
    
    private void ShuffleCardValues()
    {
        for (int i = 0; i < cardValues.Count; i++)
        {
            int randomIndex = Random.Range(0, cardValues.Count);
            int temp = cardValues[i];
            cardValues[i] = cardValues[randomIndex];
            cardValues[randomIndex] = temp;
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
    
    private void ClearCards()
    {
        foreach (Transform child in board)
        {
            Destroy(child.gameObject);
        }
    }
}
