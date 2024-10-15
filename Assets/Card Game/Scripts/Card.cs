using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    private Vector3 originalScale;
    public bool isMatched=false;
    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        BoardManager.Instance.CardSelected(this);
    }

    public void StartCardOpenAnimation()
    {
        StartCoroutine(transform.PlayCardOpen(originalScale));
    }
    public void StartCardReturnAnimation()
    {
        StartCoroutine(transform.PlayCardReturn(originalScale));
    }
    public void MarkAsMatched()
    {
        isMatched = true;
        gameObject.SetActive(false);  // Vanish the matched card
    }
}
