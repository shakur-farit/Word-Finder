using System.Collections;
using UnityEngine;

public class ButtonsAnimation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float waitingTime = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();       
    }

    public void StartAnimation()
    {
        StartCoroutine(Interaction());
    }

    public void StopAnimation()
    {
        StopAllCoroutines();
    }

    IEnumerator Interaction()
    {
        while (true)
        {
            animator.Play("Vibration");

            Debug.Log("Start Anim");

            animator.Play("Idle");

            yield return new WaitForSeconds(waitingTime);
        }       
    }
}
