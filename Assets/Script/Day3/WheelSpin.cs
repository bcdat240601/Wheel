using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : SetupBehaviour
{
    [System.Serializable]
    public class GiftPrize
    {
        public int Heart = 1;
        public int Coin = 1;
        public int NumberOfGift;
        public bool AlreadyGet = false;
        public Gift gift;
    }
    private const float CIRCLE = 360f;
    private int _numberOfGift = 9;
    [SerializeField] protected GiftPrize[] giftPrizes = new GiftPrize[9];
    [SerializeField] protected float currentTime;
    [SerializeField] protected float timeRotate = 5f;
    public AnimationCurve animationCurve;
    [SerializeField] protected bool canBeSpined = true;
    
    protected virtual void Update()
    {
        GetInputNumber();
    }
    protected override void Awake()
    {
        base.Awake();
        SetupGift();

    }
    protected override void ResetValue()
    {
        base.ResetValue();
        SetupGift();
    }
    protected virtual void SetupGift()
    {
        for (int i = 0; i < giftPrizes.Length; i++)
        {
            giftPrizes[i].NumberOfGift = i + 1;
        }
        Gift[] gift = GetComponentsInChildren<Gift>();
        for (int i = 0; i < giftPrizes.Length; i++)
        {
            giftPrizes[i].gift = gift[i];
        }
    }
    IEnumerator SpinWheel(int index)
    {       
        currentTime = 0;
        canBeSpined = false;
        int NumberOfSpin = Random.Range(2,6);
        float startAngle = transform.localEulerAngles.z;
        Debug.Log(startAngle);
        float angleDesire = (NumberOfSpin * CIRCLE) + (index - 1) * (CIRCLE / _numberOfGift) - startAngle;
        while (currentTime < timeRotate)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
            float currentAngle = angleDesire * animationCurve.Evaluate(currentTime / timeRotate);
            transform.localEulerAngles = new Vector3(0, 0, currentAngle + startAngle);
        }
        GetGift(index);
        canBeSpined = true;
    }
    [ContextMenu("Rotate")]
    protected virtual void Rotate(int index)
    {
        if (!canBeSpined) return;
        StartCoroutine(SpinWheel(index));
    }
    protected virtual void GetGift(int index)
    {
        index--;
        if (giftPrizes[index].AlreadyGet)
            Debug.Log("This gift is already got");
        else
        {
            giftPrizes[index].AlreadyGet = true;
            giftPrizes[index].gift.SpriteGiftBlack();
            Debug.Log("You got " + giftPrizes[index].NumberOfGift + " gift with" + giftPrizes[index].Heart + " Heart and " + giftPrizes[index].Coin + " Coin");
        }
        GameManager.Instance.ChangeScene(++index);
        ResetGift();
    }
    protected virtual void GetInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Rotate(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Rotate(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Rotate(3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Rotate(4);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Rotate(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Rotate(6);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Rotate(7);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Rotate(8);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Rotate(9);
    }
    protected virtual void ResetGift()
    {
        bool getAll = true;
        foreach (GiftPrize prize in giftPrizes)
        {
            if (!prize.AlreadyGet)
            {
                getAll = false;
                break;
            }
        }
        if(getAll)
            foreach (GiftPrize prize in giftPrizes)
            {
                prize.gift.ResetGift();
            }
    }
}
