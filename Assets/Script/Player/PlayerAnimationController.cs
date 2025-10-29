using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public bool IsSkillEnd { get; private set; }
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SkillOnAnimation()
    {
        animator.SetTrigger("SkillOn");
    }

    public void ActiveSkillAnimation()
    {
        animator.SetTrigger("ActiveSkill");
    }

    public void SkillEnd()
    {
        IsSkillEnd = true;
    }

    public void ReturnIdle()
    {
        animator.SetTrigger("Idle");
    }

    public void DeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}