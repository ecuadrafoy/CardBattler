using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public List<Card> heldCards = new List<Card>();

    public Transform minPosition, maxPosition;
    public List<Vector3> cardPosition = new List<Vector3>();


    void Start()
    {
        SetCardPositionsInHand();
    }



    public void SetCardPositionsInHand()
    {
        cardPosition.Clear();
        Vector3 distanceBetweenPoints = Vector3.zero;
        if (heldCards.Count > 1)
        {
            distanceBetweenPoints = (maxPosition.position - minPosition.position) / (heldCards.Count - 1);
        }
        for (int i = 0; i < heldCards.Count; i++)
        {
            cardPosition.Add(minPosition.position + (distanceBetweenPoints * i));
            //heldCards[i].transform.position = cardPosition[i];
            //heldCards[i].transform.rotation = minPosition.rotation;
            heldCards[i].MoveToPoint(cardPosition[i], minPosition.rotation);
            heldCards[i].inHand = true;
            heldCards[i].handPosition = i;
        }
    }


}
