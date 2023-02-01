using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppOpenAdDisplayHandler : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI m_Text;
   
   [SerializeField] private bool m_CanShowAppOpenAd = true;
   
   private void OnApplicationFocus(bool pauseStatus)
   {
      if (pauseStatus)
      {
         AdmobAdsManager.Instance.ShowAppOpenAd();
      }

      m_Text.text = pauseStatus ? "Came From Focus" : "None";
   }
}
