using System;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[CreateAssetMenu(fileName = "New Email List", menuName = "Scriptable Object/Emails")]
public class EmailsObject : ScriptableObject
{
    public Email[] emails;

    public Email[] GetRandomEmailList(int size)
    {
        Email[] rndEmails = new Email[size];
        Random rnd = new Random((uint)DateTime.Now.Millisecond);

        List<int> aux = new List<int>();
        while (aux.Count < size)
        {
            int rndIdx = rnd.NextInt(0, emails.Length);
            if (!aux.Contains(rndIdx)) aux.Add(rndIdx);
        }

        for (int i = 0; i < size; i++)
            rndEmails[i] = emails[aux[i]];

        return rndEmails;
    }
}

[Serializable]
public class Email
{
    public string sender;
    public string recipient;
    [TextArea(3, 5)] public string body;

    [Tooltip("Leave empty if reply is not required")] [TextArea(3, 5)]
    public string reply;
}