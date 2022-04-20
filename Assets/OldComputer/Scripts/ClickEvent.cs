using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public EmailUnityEvent onClickResponse;
}

[Serializable]
public class EmailUnityEvent : UnityEvent<EmailPlaceholder>
{
}