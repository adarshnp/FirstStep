using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int value;
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        StartCoroutine(StartCardOpenAnimation());
    }

    private IEnumerator StartCardOpenAnimation()
    {
        yield return transform.PlayCardOpen(originalScale);
        yield return new WaitForSeconds(1);
        yield return transform.PlayCardReturn(originalScale);
    }
}
