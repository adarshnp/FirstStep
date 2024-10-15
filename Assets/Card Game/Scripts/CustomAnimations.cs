using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// extended class for animating transforms. 
/// usefule for cards  etc.
/// </summary>
public static class CustomAnimations
{
    public static IEnumerator PlayCardOpen(this Transform transform, Vector3 originalScale,float popScale = 1.2f,float animationDuration = 0.5f)
    {
        // Pop forward
        yield return transform.AnimateScale(originalScale * popScale, animationDuration / 2f);

        // Flip 180 degrees
        yield return transform.AnimateRotation(0f, 180f, animationDuration / 2f);

    }
    public static IEnumerator PlayCardReturn(this Transform transform, Vector3 originalScale, float animationDuration = 0.5f)
    {
        // Flip back to original rotation
        yield return transform.AnimateRotation(180f, 360f, animationDuration / 2f);

        // Return to original scale
        yield return transform.AnimateScale(originalScale, animationDuration / 2f);
    }

    // Animate the scale of the Transform
    private static IEnumerator AnimateScale(this Transform transform,Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }

        transform.localScale = targetScale;
    }

    // Animate the Y-axis rotation of the Transform
    private static IEnumerator AnimateRotation(this Transform rectTransform,float startAngle, float endAngle, float duration)
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


