using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PopupUtility
{
   public struct WaitingScreenConfig
   {
      public string Title;
      public string BodyText;

      public int WaitDuration;
      public Action ActionAfterWait;
   }

   public class WaitingScreen : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI m_UITitle;
      [SerializeField] private TextMeshProUGUI m_BodyText;

      private int m_WaitingDuration = 3;
      private Action m_ActionAfterWait = null;

      private Coroutine m_Routine = null;


      public void Show(WaitingScreenConfig waitingScreenConfig)
      {
         m_WaitingDuration = waitingScreenConfig.WaitDuration;
         m_ActionAfterWait = waitingScreenConfig.ActionAfterWait;

         m_UITitle.text = waitingScreenConfig.Title;
         m_BodyText.text = waitingScreenConfig.BodyText;

         gameObject.SetActive(true);
         m_Routine = StartCoroutine(WaitingScreenRoutine());
      }

      public void Hide()
      {
         StopCoroutine(m_Routine);
         m_Routine = null;
         m_ActionAfterWait = null;
         gameObject.SetActive(false);
      }

      private IEnumerator WaitingScreenRoutine()
      {
         yield return new WaitForSeconds(m_WaitingDuration);
         m_ActionAfterWait?.Invoke();
         Hide();
      }
   }
}