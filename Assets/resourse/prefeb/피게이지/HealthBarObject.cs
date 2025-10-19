using UnityEngine;
using System.Collections;

public class HealthBarObject : MonoBehaviour
{
    public Transform redBar;  // ���� ü�� ������ ��
    public GameObject[] uiElements;  // �׵θ�, ��� �� ��Ÿ UI ��ҵ�

    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float visibleDuration = 60f;

    private Vector3 originalRedBarScale;
    private bool hasTakenFirstDamage = false;
    private bool isHealthBarLockedVisible = false; // �� �� �߸� ��� ���̰� �ϴ� �÷���

    void Start()
    {
        if (redBar != null)
            originalRedBarScale = redBar.localScale;

        // ���� �� ��ü �����
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
            // ù ������ ���Ŀ��� ������ ��� ���̰�
            if (!isHealthBarLockedVisible)
            {
                isHealthBarLockedVisible = true;
                SetHealthUIVisible(true);
                StopAllCoroutines(); // �� �̻� ������ �ʰ� �ڷ�ƾ ����
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
