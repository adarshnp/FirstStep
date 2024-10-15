using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// class for producing animation of card
/// </summary>
public class CardAnimations : MonoBehaviour
{

    public float popScale = 1.2f;           // How much the image should pop out
    public float animationDuration = 0.5f;  // How long the pop and flip animation should take
    public float pauseDuration = 0.2f;      // Optional pause between pop and return

    private RectTransform rectTransform;
    private Vector3 originalScale;
    private bool isAnimating = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        StartCoroutine(TestAnimations());
    }

    public IEnumerator TestAnimations()
    {
        if (!isAnimating)
        {
            yield return PlayCardOpen();

            yield return new WaitForSeconds(pauseDuration);

            yield return PlayCardReturn();
        }
    }

    private IEnumerator PlayCardOpen()
    {
        isAnimating = true;

        // Pop forward
        yield return AnimateScale(originalScale * popScale, animationDuration / 2f);

        // Flip 180 degrees
        yield return AnimateRotation(0f, 180f, animationDuration / 2f);


        isAnimating = false;
    }
    private IEnumerator PlayCardReturn()
    {
        isAnimating = true;

        // Flip back to original rotation
        yield return AnimateRotation(180f, 360f, animationDuration / 2f);

        // Return to original scale
        yield return AnimateScale(originalScale, animationDuration / 2f);

        isAnimating = false;
    }

    // Animate the scale of the RectTransform
    private IEnumerator AnimateScale(Vector3 targetScale, float duration)
    {
        Vector3 startScale = rectTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }

        rectTransform.localScale = targetScale;
    }

    // Animate the Y-axis rotation of the RectTransform
    private IEnumerator AnimateRotation(float startAngle, float endAngle, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            float rotationAngle = Mathf.Lerp(startAngle, endAngle, progress);
            rectTransform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            yield return null;
        }

        rectTransform.localRotation = Quaternion.Euler(0f, endAngle, 0f); 
    }
}


