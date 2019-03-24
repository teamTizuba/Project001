using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;


public class AdMob : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

		// アプリID
		string appId = "ca-app-pub-4760513462703258~5956190373";

		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

		RequestBanner();
	}

	private void RequestBanner()
	{

		// 広告ユニットID これはテスト用
		string adUnitId = "ca-app-pub-4760513462703258/1130883886";

		// Create a 320x50 banner at the top of the screen.
		//BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();

		// Load the banner with the request.
		bannerView.LoadAd(request);

		// Create a 320x50 banner at the top of the screen.
		//bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
	}

	// Update is called once per frame
	void Update()
	{

	}
}