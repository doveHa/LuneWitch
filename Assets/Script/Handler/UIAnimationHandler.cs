using System.Collections;
using Script.DataDefinitions.Enum;
using UnityEngine;
using UnityEngine.Serialization;

public class UIAnimationHandler : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private UIAnimationNo animationNo;

    void Start()
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