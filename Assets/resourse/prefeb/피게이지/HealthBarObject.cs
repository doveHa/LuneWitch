using UnityEngine;
using System.Collections;

public class HealthBarObject : MonoBehaviour
{
    public Transform redBar;  // 빨간 체력 게이지 바
    public GameObject[] uiElements;  // 테두리, 배경 등 기타 UI 요소들

    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float visibleDuration = 60f;

    private Vector3 originalRedBarScale;
    private bool hasTakenFirstDamage = false;
    private bool isHealthBarLockedVisible = false; // 한 번 뜨면 계속 보이게 하는 플래그

    void Start()
    {
        if (redBar != null)
            originalRedBarScale = redBar.localScale;

        // 시작 시 전체 숨기기
        SetHealthUIVisible(false);
    }

    void Update()
    {
        float ratio = currentHealth / maxHealth;

        if (redBar != null)
        {
            Vector3 newScale = originalRedBarScale;
            newScale.x = originalRedBarScale.x * ratio;
            redBar.localScale = newScale;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (!hasTakenFirstDamage)
        {
            hasTakenFirstDamage = true;
            SetHealthUIVisible(true);
            StartCoroutine(HideAfterDelay(visibleDuration));
        }
        else
        {
            // 첫 데미지 이후에는 무조건 계속 보이게
            if (!isHealthBarLockedVisible)
            {
                isHealthBarLockedVisible = true;
                SetHealthUIVisible(true);
                StopAllCoroutines(); // 더 이상 숨기지 않게 코루틴 정지
            }
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isHealthBarLockedVisible)
        {
            SetHealthUIVisible(false);
        }
    }

    private void SetHealthUIVisible(bool visible)
    {
        if (redBar != null)
            redBar.gameObject.SetActive(visible);

        foreach (GameObject ui in uiElements)
        {
            if (ui != null)
                ui.SetActive(visible);
        }
    }
}
