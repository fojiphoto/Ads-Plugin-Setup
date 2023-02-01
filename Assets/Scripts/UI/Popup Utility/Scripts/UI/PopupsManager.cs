using System;
using UnityEngine;

namespace PopupUtility
{
    public class PopupsManager : MonoSingleton<PopupsManager>
    {
        [SerializeField] private SimpleMessagePopup m_SimplePopup;
        [SerializeField] private SimpleToast m_SimpleToast;
        [SerializeField] private WaitingScreen m_WaitingScreen;

        public void ShowSimpleMessage(MessagePopupConfig messagePopupConfig)
        {
            m_SimplePopup.Show(messagePopupConfig);
        }

        public void ShowWaitingScreen(WaitingScreenConfig waitingScreenConfig)
        {
            m_WaitingScreen.Show(waitingScreenConfig);
        }

        public void HideWaitingScreen()
        {
            m_WaitingScreen.Hide();
        }
        
        public void ShowSimpleToastMessage(string message)
        {
            m_SimpleToast.ShowToast(message);
        }
    }
}
