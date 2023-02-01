using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRotationUtility : MonoBehaviour
{
    [SerializeField, Range(-1f,1f)] private float m_XMagnitude = 0f;
    [SerializeField, Range(-1f,1f)] private float m_YMagnitude = 0f;
    [SerializeField, Range(-1f, 1f)] private float m_ZMagnitude = 0f;

    [SerializeField,Range(0f, 1000f)] private float m_RotationSpeed = 0f;
    
    [SerializeField] private RectTransform m_RectTransform;

    void Update()
    {
        m_RectTransform.Rotate(new Vector3(m_XMagnitude, m_YMagnitude, m_ZMagnitude) * (m_RotationSpeed * Time.deltaTime));
    }
}
