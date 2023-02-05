using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAnimation : MonoBehaviour
{

    public float secsBeforeFlames = 1.0f;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string animationName;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(DelayedAnim());
    }

    IEnumerator DelayedAnim()
    {
        yield return new WaitForSeconds(secsBeforeFlames);
        animator.Play(animationName);
    }
}
