﻿using UnityEngine;
using System.Collections;

namespace DeltaDNA
{
	public class SimpleAdScript : MonoBehaviour {
	
		// Use this for initialization
		void Start () {
			// Ads	
			
			DDNA.Instance.Ads.OnDidRegisterForAds += () => { Logger.LogDebug("Registered for ads.");};
			DDNA.Instance.Ads.OnDidFailToRegisterForAds += (string reason) => { Logger.LogDebug("Failed to register for ads, "+reason);};
			DDNA.Instance.Ads.OnAdReady += () => { Logger.LogDebug("An ad is ready.");};
			DDNA.Instance.Ads.OnAdClosed += () => { Logger.LogDebug("Ad closed.");};

			DDNA.Instance.Ads.RegisterForAds();
		}
		
		void OnGUI() {
		
			if (GUI.Button(new Rect(250, 20, 200, 80), "Show Ad")) {
				
				DDNA.Instance.Ads.ShowAd();
			}
			
			if (GUI.Button(new Rect(250, 120, 200, 80), "Engage Ad")) {
				
				DDNA.Instance.Ads.ShowAd("testAdPoint");
			}
			
		}
	}
}