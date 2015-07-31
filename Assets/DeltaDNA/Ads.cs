﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace DeltaDNA
{
	public class Ads : MonoBehaviour {
	
		#if UNITY_ANDROID
		private DeltaDNA.Android.AdService adService;	
		
		private System.Object queueLock = new System.Object();
		private List<Action> eventQueue = new List<Action>();
		
		#endif
		
		#region Public interface
		
		public event Action OnDidRegisterForAds;
		public event Action<string> OnDidFailToRegisterForAds;
		public event Action OnAdOpened;
		public event Action OnAdFailedToOpen;
		public event Action OnAdClosed;
	
		public void RegisterForAds()
		{
			if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				adService = new DeltaDNA.Android.AdService(new DeltaDNA.Android.AdListener(this));	
				adService.RegisterForAds();	
				#endif
			}
		}
		
		public void ShowAd()
		{
			if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				if (adService != null) {
					adService.ShowAd();
				} else {
					Logger.LogError("You must first register for ads");
				}
				#endif
			}
		}
		
		public void ShowAd(string adPoint)
		{
			if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				if (adService != null) {
					adService.ShowAd(adPoint);
				} else {
					Logger.LogError("You must first register for ads");
				}
				#endif
			}
		}
		
		#endregion
		
		#region Native Bridge
		
		public void DidRegisterForAds()
		{
			if (OnDidRegisterForAds != null) {
				OnDidRegisterForAds();
			}
		}
		
		public void DidFailToRegisterForAds(string reason)
		{
			if (OnDidFailToRegisterForAds != null) {
				OnDidFailToRegisterForAds(reason);
			}
		}
		
		public void AdOpened()
		{
			if (OnAdOpened != null) {
				OnAdOpened();
			}
		}
		
		public void AdFailedToOpen()
		{
			if (OnAdFailedToOpen != null) {
				OnAdFailedToOpen();
			}
		}
		
		public void AdClosed()
		{
			if (OnAdClosed != null) {
				OnAdClosed();
			}
		}
		
		#endregion
		
		void Awake()
		{
			gameObject.name = this.GetType().ToString();
			DontDestroyOnLoad(this);
		}
		
		void Update() {
		
			#if UNITY_ANDROID
			// Post any events that may have come from background threads
			lock(queueLock) {
				while (eventQueue.Count > 0) {
					Action e = eventQueue[0];
					e();
					eventQueue.RemoveAt(0);
				}
			}
			#endif
		}
		
		void OnApplicationPause(bool pauseStatus)
		{
			if (Application.platform == RuntimePlatform.Android) {
				
				#if UNITY_ANDROID
				if (adService != null) {
					if (pauseStatus) {
						adService.OnPause();
					} else {
						adService.OnResume();
					}
				}
				#endif 	
			}
		}
		
		void OnDestroy()
		{
			if (Application.platform == RuntimePlatform.Android) {
				#if UNITY_ANDROID
				if (adService != null) {
					adService.OnDestroy();
				}			
				#endif
			}
		}
		
		internal void RecordEvent(string eventName, Dictionary<string,object> eventParams)
		{
			#if UNITY_ANDROID
			lock(queueLock) {
				eventQueue.Add(() => {
					DDNA.Instance.RecordEvent(eventName, eventParams);
				});
			}
			#endif
		}
	}
}