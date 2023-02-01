using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void ShowRewardedVideo()
    {
        AdmobAdsManager.Instance.ShowRewardedVideo(() =>
        {
            Debug.Log("Reward Given");
        });
    }
}
