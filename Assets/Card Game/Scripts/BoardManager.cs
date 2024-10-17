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

        ScaleBoard(rows, columns);
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
        if (isCheckingMatch || selectedCard.IsMatched || selectedCard == firstSelectedCard) return;


        if (firstSelectedCard == null)
        {
            firstSelectedCard = selectedCard;
            selectedCard.StartCardOpenAnimation();
        }
        else
        {
            secondSelectedCard = selectedCard;
            selectedCard.StartCardOpenAnimation();
            GameManager.instance.IncrementTurns();
            StartCoroutine(CheckForMatch());
        }
    }

    private IEnumerator CheckForMatch()
    {
        isCheckingMatch = true;

        //increment move count

        yield return new WaitForSeconds(1f);  // Delay for the user to see the flipped cards

        if (firstSelectedCard.Value == secondSelectedCard.Value)
        {
            firstSelectedCard.MarkAsMatched();
            secondSelectedCard.MarkAsMatched();
            GameManager.instance.IncrementMatches();
            SoundManager.Instance.PlayMatchSound();

        }
        else
        {
            firstSelectedCard.StartCardReturnAnimation();
            secondSelectedCard.StartCardReturnAnimation();
            SoundManager.Instance.PlayMismatchSound();
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

    private void ScaleBoard(int rows, int columns)
    {
        Vector2 gridSize = new Vector2(columns * spacing, rows * spacing);

        Vector3 centerTopPoint = cam.ViewportToWorldPoint(new Vector3(0.5f,1,cam.nearClipPlane));
        Vector3 centerBottomPoint = cam.ViewportToWorldPoint(new Vector3(0.5f,0,cam.nearClipPlane));

        Vector3 centerLeftPoint = cam.ViewportToWorldPoint(new Vector3(0.2f, 0.5f, cam.nearClipPlane));//left most point corrected to avoid UI overlaping over board
        Vector3 centerRightPoint = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, cam.nearClipPlane));

        float viewPortHeightInWorldSpace = Vector3.Distance(centerTopPoint, centerBottomPoint);
        float viewPortWidthInWorldSpace = Vector3.Distance(centerLeftPoint, centerRightPoint);

        float aspectRatio = gridSize.x / gridSize.y;

        float scaleFactor;

        if (aspectRatio > 1) 
        {
            scaleFactor = viewPortWidthInWorldSpace / gridSize.x;
        }
        else
        {
            scaleFactor = viewPortHeightInWorldSpace / gridSize.y;
        }
        scaleFactor = Mathf.Clamp(scaleFactor,0.2f,2);
        board.localScale = Vector3.one * scaleFactor;
    }
}
