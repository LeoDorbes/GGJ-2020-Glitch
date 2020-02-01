using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Utils
{
    public class Animations : MonoBehaviour
    {
        
        private IEnumerator FadeInCoroutine(float fadeDuration, Image image)
        {
            float progress = 0f;
            var color = image.color;
            color = new Color(color.r, color.g, color.b, 0);
            image.color = color;
            while (progress < 1)
            {
                var color1 = image.color;
                image.color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(0, 1, progress));
                progress += Time.deltaTime / fadeDuration;

                yield return null;
            }
            var color2 = image.color;
            color2 = new Color(color2.r, color2.g, color2.b, 1);
            image.color = color2;
        }

        private IEnumerator FadeOutFadeInCoroutine(float fadeDuration, Image image)
        {
            float progress = 0f;
            var color = image.color;
            color = new Color(color.r, color.g, color.b, 1);
            image.color = color;
            while (progress < 1)
            {
                var color1 = image.color;
                image.color = new Color(color1.r, color1.g, color1.b, Mathf.Lerp(1, 0, progress));
                progress += Time.deltaTime / fadeDuration;

                yield return null;
            }
            var color2 = image.color;
            color2 = new Color(color2.r, color2.g, color2.b, 0);
            image.color = color2;
        }
    }
}