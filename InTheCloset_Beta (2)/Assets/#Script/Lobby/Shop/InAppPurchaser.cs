using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
 
public class InAppPurchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;
    public const string productId1 = "wish100"; // 1000w
    public const string productId2 = "wish310"; // 3000w
    public const string productId3 = "wish550"; // 5000w
    public const string productId4 = "wish1200"; // 10000w


    public MoneyHelper Money;
    public Text WishText;
    void Start()
    {
        
        
    
    }

    public void Intial(){
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }
    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(productId1, ProductType.Consumable, new IDs {{ productId1, GooglePlay.Name }});
        builder.AddProduct(productId2, ProductType.Consumable, new IDs {{ productId2, GooglePlay.Name }});
        builder.AddProduct(productId3, ProductType.Consumable, new IDs {{ productId3, GooglePlay.Name }});
        builder.AddProduct(productId4, ProductType.Consumable, new IDs {{ productId4, GooglePlay.Name }});
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized() { return (storeController != null && extensionProvider != null); }
 
    public void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product p = storeController.products.WithID(productId);
                if (p != null && p.availableToPurchase)
                {
                   // Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    storeController.InitiatePurchase(p);
                }
              //  else Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
            else {
               // Debug.Log("IAP Not Initialized[!] .");

            } 
        }
        catch (Exception e) {
//			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
			}
    }

  
 
    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    { 
        //Debug.Log("OnInitialized : PASS");
 
        storeController = sc;
        extensionProvider = ep;
    }
 
    public void OnInitializeFailed(InitializationFailureReason reason) { 
	//Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason); 
	}
 
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        switch (args.purchasedProduct.definition.id)
        {
            case productId1:
                Money.Wish += 100;
                WishText.text = "" + Money.Wish;
                break;
            case productId2:
                Money.Wish += 310;
                WishText.text = "" + Money.Wish;
                break;
            case productId3:
                Money.Wish += 550;
                WishText.text = "" + Money.Wish;
                break; 
            case productId4:
                Money.Wish += 1200;
                WishText.text = "" + Money.Wish;
                break;
        }
        return PurchaseProcessingResult.Complete;
    }
 
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
		//Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason)); 
	}
}
