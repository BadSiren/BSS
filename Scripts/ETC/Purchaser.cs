using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

namespace BSS {
public class Purchaser : MonoBehaviour, IStoreListener
{
	
	private static IStoreController storeController;
	private static IExtensionProvider extensionProvider;

	#region 상품ID
	// 상품ID는 구글 개발자 콘솔에 등록한 상품ID와 동일하게 해주세요.
	public const string productId1 = "gem1";
	public const string productId2 = "gem2";
	public const string productId3 = "gem3";
	public const string productId4 = "gem4";
	public const string productId5 = "gem5";
	#endregion

	void Start()
	{
		InitializePurchasing();
	}

	private bool IsInitialized()
	{
		return (storeController != null && extensionProvider != null);
	}

	public void InitializePurchasing()
	{
		if (IsInitialized())
			return;

		var module = StandardPurchasingModule.Instance();

		ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

		builder.AddProduct(productId1, ProductType.Consumable, new IDs
			{
				{ productId1, AppleAppStore.Name },
				{ productId1, GooglePlay.Name },
			});

		builder.AddProduct(productId2, ProductType.Consumable, new IDs
			{
				{ productId2, AppleAppStore.Name },
				{ productId2, GooglePlay.Name }, }
		);

		builder.AddProduct(productId3, ProductType.Consumable, new IDs
			{
				{ productId3, AppleAppStore.Name },
				{ productId3, GooglePlay.Name },
			});

		builder.AddProduct(productId4, ProductType.Consumable, new IDs
			{
				{ productId4, AppleAppStore.Name },
				{ productId4, GooglePlay.Name },
			});

		builder.AddProduct(productId5, ProductType.Consumable, new IDs
			{
				{ productId5, AppleAppStore.Name },
				{ productId5, GooglePlay.Name },
			});

		UnityPurchasing.Initialize(this, builder);
	}

	public void BuyProductID(string productId)
	{
		try
		{
			if (IsInitialized())
			{
				Product p = storeController.products.WithID(productId);

				if (p != null && p.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
					storeController.InitiatePurchase(p);
				}
				else
				{
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		catch (Exception e)
		{
			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}

	public void RestorePurchase()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");

			var apple = extensionProvider.GetExtension<IAppleExtensions>();

			apple.RestoreTransactions
			(
				(result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
			);
		}
		else
		{
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	public void OnInitialized(IStoreController sc, IExtensionProvider ep)
	{
		Debug.Log("OnInitialized : PASS");

		storeController = sc;
		extensionProvider = ep;
	}

	public void OnInitializeFailed(InitializationFailureReason reason)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		bool validPurchase = true; // Presume valid for platforms with no R.V.

		// Unity IAP's validation logic is only included on these platforms.
		#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
		// Prepare the validator with the secrets we prepared in the Editor
		// obfuscation window.
		var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
			AppleTangle.Data(), Application.identifier);
		
		try
		{
			
			// On Google Play, result has a single product ID.
			// On Apple stores, receipts contain multiple products.
			var result = validator.Validate(args.purchasedProduct.receipt);
			// For informational purposes, we list the receipt(s)
			Debug.Log("Receipt is valid. Contents:");

			foreach (IPurchaseReceipt productReceipt in result)
			{
				Debug.Log(productReceipt.productID);
				Debug.Log(productReceipt.purchaseDate);
				Debug.Log(productReceipt.transactionID);
			}
		}
		catch (IAPSecurityException)
		{
			Debug.Log("Invalid receipt, not unlocking content");
			validPurchase = false;
		}
		#endif

		if (validPurchase) {

			Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			switch (args.purchasedProduct.definition.id) {
			case productId1:
				UserJson.instance.addGem (75);
				break;

			case productId2:
				UserJson.instance.addGem (1000);
				break;

			case productId3:
				break;

			case productId4:
				break;

			case productId5:
				break;
			}
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}


}
}