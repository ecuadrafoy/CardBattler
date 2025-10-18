using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] SOCard cardSO;
    public int currentHealth;
    public int attackPower, manaCost;

    public TMP_Text healthText, attackText, manaText, nameText, actionDescriptionText, loreText;

    public Image characterImage, backgroundArt;

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public float moveSpeed = 5f, rotateSpeed = 540f;
    public bool inHand;
    public int handPosition;

    private HandController handController;

    private bool isSelected;
    private Collider cardCollider;
    public LayerMask whatIsDesktop, whatisPlacement;
    private bool justPressed;
    public CardPlacePoint assignedPlace;
    void Start()
    {
        SetupCard();
        handController = FindObjectOfType<HandController>();
        cardCollider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        if (isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, whatIsDesktop))
            {
                MoveToPoint(hit.point + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonDown(0) && justPressed == false)
            {
                if (Physics.Raycast(ray, out hit, 100f, whatisPlacement))
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();
                    if (selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        if (BattleController.instance.playerMana >= manaCost)
                        {
                            //Place the card
                            selectedPoint.activeCard = this;
                            assignedPlace = selectedPoint;
                            MoveToPoint(selectedPoint.transform.position, Quaternion.identity);
                            inHand = false;
                            isSelected = false;
                            handController.RemoveCardFromHand(this);
                            BattleController.instance.SpendPlayerMana(manaCost);
                        }
                        else
                        {
                            UICOntroller.instance.ShowManaWarning();
                            ReturnToHand();
                        }

                    }
                    else
                    {
                        ReturnToHand();

                    }
                }
                else
                {
                    ReturnToHand();
                }
            }
        }
        justPressed = false;
    }

    public void SetupCard()
    {
        currentHealth = cardSO.currentHealth;
        attackPower = cardSO.attackPower;
        manaCost = cardSO.manaCost;

        healthText.text = currentHealth.ToString();
        attackText.text = attackPower.ToString();
        manaText.text = manaCost.ToString();

        nameText.text = cardSO.cardName;
        actionDescriptionText.text = cardSO.actionDescription;
        loreText.text = cardSO.cardLore;

        characterImage.sprite = cardSO.characterSprite;
        backgroundArt.sprite = cardSO.backgroundSprite;
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotationToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRotation = rotationToMatch;
    }
    private void OnMouseOver()
    {
        if (inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition] + new Vector3(0f, 1f, 0.5f), Quaternion.identity);
            Debug.Log("Mouse Detected");
        }
    }
    private void OnMouseExit()
    {
        if (inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition], handController.minPosition.rotation);
        }
    }
    void OnMouseDown()
    {
        if (inHand)
        {
            isSelected = true;
            cardCollider.enabled = false;
            justPressed = true;
        }
    }
    public void ReturnToHand()
    {
        isSelected = false;
        cardCollider.enabled = true;
        MoveToPoint(handController.cardPosition[handPosition], handController.minPosition.rotation);
    }
}
