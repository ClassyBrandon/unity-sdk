﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeltaDNA;
using MiniJSON = DeltaDNA.MiniJSON;

public class TestSDK : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		// Test mode - clear any data from last run
		// and enable additional logging.
		//SDK.Instance.ClearPersistentData();
		SDK.Instance.Settings.DebugMode = true;
		
		// Set client external information		
		SDK.Instance.ClientVersion = "1.0";
		
		// Enable event hashing
		SDK.Instance.HashSecret = "1VLjWqChV2YC1sJ4EPKGzSF3TbhS26hq";
	
		// Initialise the SDK
		SDK.Instance.StartSDK(
			"76410301326725846610230818914037", 					// Environment Key	(UnitySDK)		
			"collect2470ntysd.deltadna.net/collect/api",			// Collect URI
			"engage2470ntysd.deltadna.net",							// Engage URI
			SDK.AUTO_GENERATED_USER_ID								// User ID
		);
		
		// Send some more complicated events
		EventBuilder achievementParams = new EventBuilder()
			.AddParam("achievementName", "Sunday Showdown Tournament Win")
			.AddParam("achievementID", "SS-2014-03-02-01")
			.AddParam("reward", new EventBuilder()
				.AddParam("rewardProducts", new ProductBuilder()
					.AddRealCurrency("USD", 5000)
					.AddVirtualCurrency("VIP Points", "GRIND", 20)
					.AddItem("Sunday Showdown Medal", "Victory Badge", 1))
				.AddParam("rewardName", "Medal"));
		
		SDK.Instance.RecordEvent("achievement", achievementParams);	
		
		EventBuilder transactionParams = new EventBuilder()
			.AddParam("transactionName", "Weapon type 11 manual repair")
			.AddParam("transactionID", "47891208312996456524019-178.149.115.237:51787")
			.AddParam("transactorID", "62.212.91.84:15116")
			.AddParam("productID", "4019")
			.AddParam("transactionType", "PURCHASE")
			.AddParam("paymentCountry", "GB")
			.AddParam("productsReceived", new ProductBuilder()
				.AddItem("WeaponMaxConditionRepair:11", "WeaponMaxConditionRepair", 5))
			.AddParam("productsSpent", new ProductBuilder()
				.AddVirtualCurrency("Credit", "GRIND", 710));
				
		SDK.Instance.RecordEvent("transaction", transactionParams);
		
		// Try out Transaction helpers
		SDK.Instance.Transaction.BuyVirtualCurrency("Buy Gold Coins", "USD", 10000, "Gold", "PREMIUM_GRIND", 5, "12567335-DFEWFG-sdfgr-343");
		
		// Play with Engage
		var engageParams = new Dictionary<string, object>()
		{
			{ "userLevel", 4 },
			{ "experience", 1000 },
			{ "missionName", "Disco Volante" }
		};
		
		SDK.Instance.RequestEngagement("gameLoaded", engageParams, (response) =>
		{
			string data = MiniJSON.Json.Serialize(response);
			Debug.Log("Engage returned '"+data+"'");
		});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}