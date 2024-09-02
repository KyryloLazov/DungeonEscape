using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    public GameObject Shop;
    private Player player;
    [SerializeField]
    GameObject[] buttons;

    private int ItemSelected;
    private int ItemSelectedCost = 200;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
                Shop.SetActive(true);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Shop.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateSelection(96);
                ItemSelected = 0;
                ItemSelectedCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateSelection(-3);
                ItemSelected = 1;
                ItemSelectedCost = 300;
                break;
            case 2:
                UIManager.Instance.UpdateSelection(-96);
                ItemSelected = 2;
                ItemSelectedCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if (player != null)
        {
            if(player.diamonds >= ItemSelectedCost)
            {
                if(ItemSelected == 0)
                {
                    player.SwipeAnim(2);
                    GameManager.Instance.BoughtSword = 1;
                    Destroy(buttons[0]);
                }

                if(ItemSelected == 1)
                {
                    player.hasDoubleJump = true;
                    GameManager.Instance.BoughtJump = 1;
                    Destroy(buttons[1]);
                }
                if(ItemSelected == 2)
                {
                    GameManager.Instance.HasKey = true;
                    Destroy(buttons[2]);
                }
                Debug.Log("Item purchased");
                player.diamonds -= ItemSelectedCost;
                UIManager.Instance.UpdateGemCount(player.diamonds);
            }
            else
            {
                Shop.SetActive(false);
            }
        }
    }
}
