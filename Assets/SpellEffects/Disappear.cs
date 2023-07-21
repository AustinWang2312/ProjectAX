using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour,ISpellComponent
{
    public float duration;
    public Animator animator;
    private bool isDestroying = false;

    public void ApplyStats(SpellStats spellStats)
    {
        this.duration = spellStats.ObjectDuration;
        StartDestroySequence();
    }

    public void StartDestroySequence()
    {
        if (!isDestroying)
        {
            StartCoroutine(StartAnimation());
            isDestroying = true;
        }
    }

    private IEnumerator StartAnimation()
    {
        // Wait for duration before starting the animation.
        yield return new WaitForSeconds(duration);

        // Trigger the fade out animation.
        animator.SetTrigger("FadeOut");

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
