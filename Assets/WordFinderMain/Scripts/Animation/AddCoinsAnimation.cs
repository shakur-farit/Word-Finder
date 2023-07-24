using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinsAnimation : MonoBehaviour
{
    public static AddCoinsAnimation instance;

    [SerializeField] private GameObject pileOfCoinsParent;
    [SerializeField] private GameObject coinSpritePref;
    [SerializeField] private int prefsCount;
    [SerializeField] private Vector3 finishPos;

    private Vector3[] initialPos;
    // Use it if we need animate rotate
    //private Quaternion[] initialRot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        coinsPrefsInit();

        initialPos = new Vector3[pileOfCoinsParent.transform.childCount];
        //initialRot = new Quaternion[pileOfCoinsParent.transform.childCount];

        for (int i = 0; i < pileOfCoinsParent.transform.childCount; i++)
        {
            initialPos[i] = pileOfCoinsParent.transform.GetChild(i).position;
            //initialRot[i] = pileOfCoinsParent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < pileOfCoinsParent.transform.childCount; i++)
        {
            pileOfCoinsParent.transform.GetChild(i).position = initialPos[i];
            //pileOfCoinsParent.transform.GetChild(i).rotation = initialRot[i];
        }
    }

    public void RewardPileOfCoin()
    {
        Reset();

        //if (initCoinsCount > prefsCount)
        //    initCoinsCount = prefsCount;

        var delay = 0f;

        pileOfCoinsParent.SetActive(true);

        for (int i = 0; i < prefsCount; i++)
        {
            pileOfCoinsParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoinsParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(finishPos, 0.3f)
                .SetDelay(delay).SetEase(Ease.InBack);

            pileOfCoinsParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 0.3f).SetEase(Ease.OutBack);

            delay += 0.1f;
        }
    }

    private void coinsPrefsInit()
    {
        for (int i = 0; i < prefsCount; i++)
        {
            GameObject temp = Instantiate(coinSpritePref, pileOfCoinsParent.transform.position, Quaternion.identity);
            temp.transform.SetParent(pileOfCoinsParent.transform);
            temp.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}