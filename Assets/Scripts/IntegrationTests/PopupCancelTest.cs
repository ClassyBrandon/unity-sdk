﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityTest;

namespace DeltaDNA.Messaging
{
	// Mimicks the Engage Response handler, creates an Image Composition
	// that can be drawn for the Popup.
	public class PopupCancelTest : MonoBehaviour {

		// Use this for initialization
		void Awake () {
			
			// Build a Composition and Launch the popup
			// Test assets stored in StreamingAssetsPath allows WWW to be used as in real code.
			string spriteMapPath = "file://" + Path.Combine(Application.streamingAssetsPath, "Images/Popup1.png");

			ImageComposition c = ImageComposition.BuildFromDictionary(new Dictionary<string, object>(){
				{"url", spriteMapPath},
				{"width", 1024},
				{"height", 512},
				{"format", "PNG"},
				{"viewport", new Dictionary<string, object>() {
					{"width", 960},
					{"height", 480}
				}},
				{"background", new Dictionary<string, object>() {
					{"x", 2},
					{"y", 52},
					{"imgX", 160},
					{"imgY", 40},
					{"width", 640},
					{"height", 400},
					{"actionType", "NONE"}
				}},
				{"button1", new Dictionary<string, object>() {
					{"x", 2},
					{"y", 2},
					{"imgX", 244},
					{"imgY", 310},
					{"width", 96},
					{"height", 48},
					{"label", "Purchase"},
					{"name", "purchase"},
					{"actionType", "NONE"}
				}},
				{"button2", new Dictionary<string, object>() {
					{"x", 644},
					{"y", 404},
					{"imgX", 600},
					{"imgY", 310},
					{"width", 96},
					{"height", 48},
					{"label", "Ignore"},
					{"name", "cancel"},
					{"actionType", "DISMISS"}
				}}
			});
				
			Popup popup = new Popup();
			popup.Dismiss += (sender, e) => {
				IntegrationTest.Pass();
			};
			popup.AfterLoad += (sender, e) => {
				((Popup)sender).ShowPopup();
			};
			popup.LoadResource(c);
		}		

		void Update()
		{
			// After awhile simulate a button press...
			if (Time.time > 5)
			{
				GameObject btn2 = GameObject.Find("Button2");
				if (btn2 != null) 
				{
					PopupActionHandler handler = btn2.GetComponent<PopupActionHandler>();
					if (handler != null) 
					{
						handler.OnMouseDown();
					}
				}
			}
		}

	}
}		