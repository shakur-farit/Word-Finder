using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    private void Start()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        StartCoroutine(Interaction());
    }

    IEnumerator Interaction()
    {
        while (true)
        {
            int randomBox = Random.Range(0, animators.Length);
            animators[randomBox].Play("TitleAnimation");

            animators[randomBox].Play("Idle");

            yield return new WaitForSeconds(0.5f);
        }
    }
}
