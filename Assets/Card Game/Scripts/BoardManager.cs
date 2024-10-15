using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles setting up the card grid, flipping cards, and managing card matching logic.
/// </summary>
public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance;


    private Card firstSelectedCard;
    private Card secondSelectedCard;
    private bool isCheckingMatch = false;

    private void Awake()
    {
        Instance = this;
    }

    public void CardSelected(Card selectedCard)
    {
        if (isCheckingMatch || selectedCard.isMatched || selectedCard == firstSelectedCard) return;

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
            //increment match count
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
