using System.Collections.Generic;
using UnityEngine;

public class InGameManager2 : MonoBehaviour
{
    public static InGameManager2 Instance;

    public List<string> selectedCharacterNames = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
