using UnityEngine;

public class BossTriggerDetector : MonoBehaviour
{
    public BossPattern bossPattern;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossPattern.SetPlayerInRange(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossPattern.SetPlayerInRange(false);
        }
    }
}
