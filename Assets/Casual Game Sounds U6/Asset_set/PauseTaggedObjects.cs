using UnityEngine;

public class PauseTaggedObjects : MonoBehaviour
{
    public void Pause()
    {
        // Player 태그가 붙은 모든 오브젝트
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = false;
            }
        }

        // Enemy 태그가 붙은 모든 오브젝트
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = false;
            }
        }
    }

    public void Resume()
    {
        // Player 태그
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = true;
            }
        }

        // Enemy 태그
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            MonoBehaviour[] scripts = enemy.GetComponents<MonoBehaviour>();
            foreach (var script in scripts)
            {
                script.enabled = true;
            }
        }
    }
}
