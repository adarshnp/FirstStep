using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    private Vector3 originalScale;
    public bool isMatched=false;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(VanishAndDisable());
    }

    private IEnumerator VanishAndDisable()
    {
        // Start the vanish animation and wait until it's done
        yield return StartCoroutine(spriteRenderer.PlayCardVanish(originalScale));

        // After the animation is complete, disable the GameObject
        gameObject.SetActive(false);
    }
}
