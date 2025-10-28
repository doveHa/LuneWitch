using Script.Manager;
using UnityEngine;

public class RoundInputHandler : MonoBehaviour
{
    [SerializeField] private int nextRound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneLoadManager.SelectedRoundNo = nextRound;
    }

    // Update is called once per frame
    void Update()
    {
    }
}