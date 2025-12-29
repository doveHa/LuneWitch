using System.Collections;
using Script.DataDefinitions.Enum;
using UnityEngine;

public class UIAnimationHandler : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private UIAnimationNo animationNo;
    [SerializeField] private bool isOnPlay = false;

    void Start()
    {
        if (isOnPlay)
        {
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        StartCoroutine(WaitTime());
        GetComponent<Animator>().SetBool("IsPlay", true);
        GetComponent<Animator>().SetInteger("UIAnimationNo", (int)animationNo);
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
    }
}