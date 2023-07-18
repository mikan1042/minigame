using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryTable : MonoBehaviour
{
    enum CardPattern
    {
        Spade,
        Heart,
        Diamond,
        Club,
    }

    private const int MAXCARDCOUNT = 13;
    [SerializeField] private List<int> cardNum = new();
    public List<GameObject> cardList = new();

    private void Start()
    {
        ChooseCards();
    }
    private void ChooseCards()
    {
        SelectCardNumber();
        AddCardList();
        ShuffleList(cardList);
        SettingCard();

    }
    private void SelectCardNumber()
    {
        for (int i = 1; i <= MAXCARDCOUNT; i++)
            cardNum.Add(i);

        while (cardNum.Count > 10)
            cardNum.Remove(Random.Range(0, cardNum.Count - 1));


    }
    private void AddCardList()
    {
        var allPattern = CardPattern.GetValues(typeof(CardPattern));
        for (int i = 0; i < 10; i++)
        {
            var RandNum = RandomPattern();
            cardList.Add(AddCard(i, RandNum.Item1));
            cardList.Add(AddCard(i, RandNum.Item2));
        }
    }
    private GameObject AddCard(int Num, int pattern)
    {
        GameObject obj = null;
        switch (pattern)
        {
            case (int)CardPattern.Spade:
                obj = Instantiate(MemoryDataManager.Instance.spadeCards[cardNum[Num] - 1]);
                break;
            case (int)CardPattern.Heart:
                obj = Instantiate(MemoryDataManager.Instance.heartCards[cardNum[Num] - 1]);
                break;
            case (int)CardPattern.Diamond:
                obj = Instantiate(MemoryDataManager.Instance.diamondCards[cardNum[Num] - 1]);
                break;
            case (int)CardPattern.Club:
                obj = Instantiate(MemoryDataManager.Instance.clubCards[cardNum[Num] - 1]);
                break;
        }
        if(obj != null)
        {
            obj.name = cardNum[Num].ToString();
            return obj;
        }
        else
            throw new System.Exception("Invalid pattern");
    }
    private (int, int) RandomPattern()
    {
        int[] numbers = { 0, 1, 2, 3 };

        int randomNumber1 = 42;
        int randomNumber2 = 42;

        randomNumber1 = (Random.Range(0, numbers.Length));
        for (; ; )
        {
            randomNumber2 = (Random.Range(0, numbers.Length));
            if (randomNumber1 != randomNumber2)
                break;
        }
        return (randomNumber1, randomNumber2);
    }
    private void ShuffleList<T>(List<T> list)
    {
        int listCount = list.Count;
        while (listCount > 1)
        {
            listCount--;
            int k = Random.Range(0, listCount + 1);
            T value = list[k];
            list[k] = list[listCount];
            list[listCount] = value;
        }
    }
    private void SettingCard()
    {
        var cardPositions = MemoryDataManager.Instance.cardPositions;
        for (int i = 0; i < cardList.Count; i++)
        {
            cardList[i].transform.parent = cardPositions[i].transform;
            cardList[i].transform.localPosition = Vector3.zero;
            cardList[i].transform.localScale = Vector3.one;
            cardList[i].transform.localRotation = Quaternion.Euler(Vector3.forward * 180);
        }
    }
}
