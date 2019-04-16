using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{


    [System.Serializable]
    public struct Item
    {
        public int price;
        public string Explain;
        public Button ItemBtn;
    }

    public GameObject Shop;
    public AudioSource btnClickSound;
    public AudioSource coin;
    [Space]
    [Header("Base Script")]
    public UnityAdsHelper AdsHelper;
    public MoneyHelper Money;
    public PlayerInventoryManager inventory;
    [Space]
    [Header("Item")]
    public Item[] item;
    public Text[] InventoryText;
    [Space]
    [Header("Money")]
    public Text MoneyText;
    public Text WishText;
    [Space]
    [Header("BuyCheck")]
    public GameObject BuyCheck;
    public Button BuyBtn;
    public Button CancelBtn;
    public Text itemTitle;
    public Text ItemExplain;
    
    
    public InAppPurchaser IapCon;

    private void Start()
    {
        SetInventory();
        MoneyText.text = "" + Money.Money;
        WishText.text = "" + Money.Wish;
        IapCon.Intial();

        item[0].ItemBtn.onClick.AddListener(() => itemBtn(0));
        item[1].ItemBtn.onClick.AddListener(() => itemBtn(1));
        item[2].ItemBtn.onClick.AddListener(() => itemBtn(2));
        item[3].ItemBtn.onClick.AddListener(() => itemBtn(3));
        item[4].ItemBtn.onClick.AddListener(() => itemBtn(4));
        item[5].ItemBtn.onClick.AddListener(() => itemBtn(5));
        item[10].ItemBtn.onClick.AddListener(() => getGoldBtn());

        /* IAP TEST */
        item[6].ItemBtn.onClick.AddListener(() => IAPurchase("wish100"));
        item[7].ItemBtn.onClick.AddListener(() => IAPurchase("wish310"));
        item[8].ItemBtn.onClick.AddListener(() => IAPurchase("wish550"));
        item[9].ItemBtn.onClick.AddListener(() => IAPurchase("wish1200"));
        
        CancelBtn.onClick.AddListener(() => Cancel());
    }


     void IAPurchase(string pid){
        btnClickSound.Play();
        IapCon.BuyProductID(pid);
    }
    public void itemBtn(int index)
    {
        btnClickSound.Play();
        BuyCheck.SetActive(true);
        if (item[index].price <= Money.Money)
        {
            BuyBtn.image.enabled = true;
            BuyBtn.enabled = true;
            BuyBtn.gameObject.SetActive(true);
        }
        else
        {
            BuyBtn.gameObject.SetActive(false);
            BuyBtn.image.enabled = false;
            BuyBtn.enabled = false;
        }

        ItemExplain.text = item[index].Explain;

        BuyBtn.onClick.RemoveAllListeners();
        BuyBtn.onClick.AddListener(() => ItemBuyCheck(index));
    }

    public void getGoldBtn()
    {
        btnClickSound.Play();
        BuyCheck.SetActive(true);
        if (item[10].price <= Money.Wish)
        {
            BuyBtn.image.enabled = true;
            BuyBtn.enabled = true;
            BuyBtn.gameObject.SetActive(true);
        }
        else
        {
            BuyBtn.gameObject.SetActive(false);
            BuyBtn.image.enabled = false;
            BuyBtn.enabled = false;
        }

        ItemExplain.text = item[10].Explain;

        BuyBtn.onClick.RemoveAllListeners();
        BuyBtn.onClick.AddListener(() => ItemBuyCheck(10));
    }

    public void ItemBuyCheck(int index)
    {
        coin.Play();
        switch (index)
        {
            case 0:
                inventory.FairyMagic++;
                Buy(item[index].price);
                break;
            case 1:
                inventory.SubBattery++;
                Buy(item[index].price);
                break;
            case 2:
                inventory.BreakMusicBox++;
                Buy(item[index].price);
                break;
            case 3:
                inventory.Bangle++;
                Buy(item[index].price);
                break;
            case 4:
                inventory.Lens++;
                Buy(item[index].price);
                break;
            case 5:
                AdsHelper.ShowAd("Wish");
                break;
            case 10:
                Money.Wish -= 10;
                Money.Money += 1000;
                WishText.text = "" + Money.Wish;
                MoneyText.text = "" + Money.Money;
                BuyCheck.SetActive(false);
                break;
        }
        SetInventory();
    }

    public void Buy(int price)
    {
        Money.Money -= price;
        BuyCheck.SetActive(false);
        MoneyText.text = "" + Money.Money;
    }

    public void SetInventory()
    {
        InventoryText[0].text = "보유 수 : " + inventory.FairyMagic;
        InventoryText[1].text = "보유 수 : " + inventory.SubBattery;
        InventoryText[2].text = "보유 수 : " + inventory.BreakMusicBox;
        InventoryText[3].text = "보유 수 : " + inventory.Bangle;
        InventoryText[4].text = "보유 수 : " + inventory.Lens;
    }

    public void Cancel()
    {
        btnClickSound.Play();
        BuyCheck.SetActive(false);
    }

    public void GetAdsReward()
    {
        Money.Wish += 10;
        Cancel();
        WishText.text = "" + Money.Wish;
    }

    public void Exit()
    {
        btnClickSound.Play();
        Shop.SetActive(false);
    }

}

