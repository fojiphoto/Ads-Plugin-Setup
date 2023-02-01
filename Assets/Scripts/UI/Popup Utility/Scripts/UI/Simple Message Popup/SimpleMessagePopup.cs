using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PopupUtility
{

    public enum MessageType
    {
        Simple,
        Choice
    }
    
    public struct MessagePopupConfig
    {
        public string Title;
        public string Message;

        public Action OkayAction;
        public Action CancelAction;

        public MessageType MessageType;
    }
    
    public class SimpleMessagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_MessageText;
        [SerializeField] private TextMeshProUGUI m_TitleText;

        [SerializeField] private Button m_OkayButton;
        [SerializeField] private Button m_CancelButton;

        private void OnDisable()
        {
            m_CancelButton.gameObject.SetActive(false);
        }

        public void Show(MessagePopupConfig messagePopupConfig)
        {
            m_TitleText.text = messagePopupConfig.Title;
            m_MessageText.text = messagePopupConfig.Message;

            ButtonAction(m_OkayButton, messagePopupConfig.OkayAction);
            ButtonAction(m_CancelButton,messagePopupConfig.CancelAction);

            m_CancelButton.gameObject.SetActive(messagePopupConfig.MessageType is MessageType.Choice);
            gameObject.SetActive(true);
        }

        private void ButtonAction(Button button, Action action)
        {
            button.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                action?.Invoke();
            });
        }
    }
}
