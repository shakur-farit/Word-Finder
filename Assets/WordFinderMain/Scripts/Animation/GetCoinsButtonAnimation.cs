using System.Collections;
using UnityEngine;

public class GetCoinsButtonAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float waitingTime = 30f;

    private void Start()
    {
        InvokeRepeating("StartAnimation", waitingTime, 20);
    }

    public void StartAnimation()
    {
        animator.SetBool("isVibrate", true);

        Debug.Log("Start Anim");

        animator.SetBool("isVibrate", false);

    }

    //IEnumerator Interaction()
    //{
    //    StartAnimation();
    //    new WaitForSeconds(waitingTime);
    //    StartCoroutine(Interaction());
    //    yield return null;
    //}
}
