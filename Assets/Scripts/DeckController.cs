using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;
    void Awake()
    {
        instance = this;
    }
    public List<SOCard> deckToUse = new List<SOCard>();
    private List<SOCard> activeCards = new List<SOCard>();
    public Card cardToSpawn;
    public int drawCardCost = 2;
    public float waitBetweenDrawingCards = 0.25f;

    void Start()
    {
        SetupDeck();
    }


    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            DrawCardToHand();
        }
        */
    }
    public void SetupDeck()
    {
        activeCards.Clear();
        List<SOCard> tempDeck = new List<SOCard>();
        tempDeck.AddRange(deckToUse);
        int iterations = 0;

        while (tempDeck.Count > 0 && iterations < 500)
        {
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);
            iterations++;
        }
    }
    public void DrawCardToHand()
    {
        if (activeCards.Count == 0)
        {
            SetupDeck();
        }
        Card newCard = Instantiate(cardToSpawn, transform.position, transform.rotation);
        newCard.cardSO = activeCards[0];
        newCard.SetupCard();
        activeCards.RemoveAt(0);
        HandController.instance.AddCardToHand(newCard);
    }
    public void DrawCardForMana()
    {
        if (BattleController.instance.playerMana >= drawCardCost)
        {
            DrawCardToHand();
            BattleController.instance.SpendPlayerMana(drawCardCost);
        }
        else
        {
            UICOntroller.instance.ShowManaWarning();
            UICOntroller.instance.drawCardButton.SetActive(false);
        }
    }
    public void DrawMultipleCards(int amountToDraw)
    {
        StartCoroutine(DrawMultipleCo(amountToDraw));
    }
    IEnumerator DrawMultipleCo(int amountToDraw)
    {
        for (int i = 0; i < amountToDraw; i++)
        {
            DrawCardToHand();
            yield return new WaitForSeconds(waitBetweenDrawingCards);
        }
    }

}
