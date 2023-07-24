using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBackgroundLetters : MonoBehaviour
{
    [SerializeField] private GameObject[] letters;

    //[SerializeField] private float maxX = 10f;
    //[SerializeField] private float maxY = 10f;
    //[SerializeField] private float minX = -10f;
    //[SerializeField] private float minY = -10f;

    private void Start()
    {
        RandomPos();
        RandomRotation();
        RandomColor();
    }

    private void RandomPos()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            float randomX = Random.Range(-468, 462);
            float randomY = Random.Range(-894, 886);

            letters[i].transform.localPosition = new Vector3(randomX, randomY, 0f);
        }
    }

    private void RandomRotation()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            float randomRot = Random.Range(0, 360);
            letters[i].transform.Rotate(0f, 0f, randomRot);
        }
    }

    Color[] colors = { Color.green, Color.yellow, Color.red };
    private void RandomColor()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            int randomColor = Random.Range(0, 3);
            letters[i].GetComponent<Image>().color = colors[randomColor];
        }
    }
}
