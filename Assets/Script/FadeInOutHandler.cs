using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Script
{
    public class FadeInOutHandler : MonoBehaviour
    {
        [SerializeField] private float fadeDuration = 1.5f;
        [SerializeField] private float waitBetweenInOut = 1f;
        [SerializeField] private float waitBeforeStart = 0f;

        private SpriteRenderer spriteRenderer;
        private Image image;
        private Color _color;

        private const int EMPTY = 0, FULL = 1;

        private enum FadeType
        {
            FadeIn,
            FadeOut,
            FadeInOut
        }

        [SerializeField] private FadeType type;

        void Awake()
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                this.spriteRenderer = spriteRenderer;
                _color = spriteRenderer.color;
            }
            else if (TryGetComponent(out Image image))
            {
                this.image = image;
                _color = image.color;
            }
        }

        void Start()
        {
            switch (type)
            {
                case FadeType.FadeIn:
                    StartCoroutine(WaitAndFadeIn());
                    break;
                case FadeType.FadeOut:
                    StartCoroutine(WaitAndFadeOut());
                    break;
                case FadeType.FadeInOut:
                    StartCoroutine(WaitAndFadeInOut());
                    break;
            }
        }

        private IEnumerator WaitAndFadeIn()
        {
            yield return new WaitForSeconds(waitBeforeStart);
            Debug.Log("Fade In");
            yield return Fade(EMPTY,FULL);
        }

        private IEnumerator WaitAndFadeOut()
        {
            yield return new WaitForSeconds(waitBeforeStart);
            yield return Fade(FULL,EMPTY);
            gameObject.SetActive(false);
        }

        private IEnumerator WaitAndFadeInOut()
        {
            yield return new WaitForSeconds(waitBeforeStart);
            yield return StartCoroutine(Fade(EMPTY, FULL));
            yield return new WaitForSeconds(waitBetweenInOut);
            yield return StartCoroutine(Fade(FULL, EMPTY));
            gameObject.SetActive(false);
        }
        
        private IEnumerator Fade(float from, float to)
        {
            float time = 0f;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                ChangeAlpha(Mathf.Lerp(from, to, time / fadeDuration));
                yield return null;
            }

            ChangeAlpha(to);
        }
        
        private void ChangeAlpha(float alpha)
        {
            _color.a = alpha;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = _color;
            }

            if (image != null)
            {
                image.color = _color;
            }
        }

    }
}