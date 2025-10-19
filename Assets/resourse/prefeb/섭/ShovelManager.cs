using UnityEngine;

public class ShovelManager : MonoBehaviour
{
    public static ShovelManager Instance;

    public bool isShovelMode = false;

    private void Awake()
    {
        Instance = this;
    }

    public void EnterShovelMode()
    {
        isShovelMode = true;
    }

    public void ExitShovelMode()
    {
        isShovelMode = false;
    }
}
