using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// extended class for animating recttransforms. 
/// usefule for cards, UI buttons etc.
/// </summary>
public static class CustomAnimations
{
    public static IEnumerator PlayCardOpen(this RectTransform rectTransform, Vector3 originalScale,float popScale = 1.2f,float animationDuration = 0.5f)
    {
        // Pop forward
        yield return rectTransform.AnimateScale(originalScale * popScale, animationDuration / 2f);

        // Flip 180 degrees
        yield return rectTransform.AnimateRotation(0f, 180f, animationDuration / 2f);

    }
    public static IEnumerator PlayCardReturn(this RectTransform rectTransform, Vector3 originalScale, float animationDuration = 0.5f)
    {
        // Flip back to original rotation
        yield return rectTransform.AnimateRotation(180f, 360f, animationDuration / 2f);

        // Return to original scale
        yield return rectTransform.AnimateScale(originalScale, animationDuration / 2f);
    }

    // Animate the scale of the RectTransform
    private static IEnumerator AnimateScale(this RectTransform rectTransform,Vector3 targetScale, float duration)
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
    private static IEnumerator AnimateRotation(this RectTransform rectTransform,float startAngle, float endAngle, float duration)
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


