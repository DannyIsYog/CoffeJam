using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class EmailGenerator : MonoBehaviour
{
    [SerializeField] private EmailsObject emailsObject;
    [SerializeField] private GameObject emailPreviewPrefab;
    [SerializeField] private Transform emailPreviewParent;
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI recipientText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private TextMeshProUGUI replyText;
    [SerializeField] private Transform replyButton;
    [SerializeField] private Transform replyBox;
    [SerializeField] private Transform replySend;
    [SerializeField] private Transform trashButton;

    public bool allTaskComplete;

    private readonly List<GameObject> _emailObjs = new List<GameObject>();
    private Email[] emails;
    private EmailPlaceholder _currentEmail;

    private void Start()
    {
        Random rnd = new Random();
        int rndIdx = rnd.Next(2, 5);
        
        emails = emailsObject.GetRandomEmailList(rndIdx);

        foreach (Email email in emails)
        {
            GameObject emailObj = Instantiate(emailPreviewPrefab, emailPreviewParent);
            emailObj.GetComponent<EmailPlaceholder>().email = email;
            emailObj.GetComponent<ClickEvent>().onClickResponse.AddListener(UpdateContentWindow);
            TextMeshProUGUI[] texts = emailObj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = email.sender;
            texts[1].text = email.body;
            _emailObjs.Add(emailObj);
        }
    }

    private void Update()
    {
        if (_currentEmail != null && _currentEmail.taskComplete) _currentEmail.gameObject.SetActive(false);

        if (!_emailObjs.Any(emailObj => emailObj.activeSelf))
            allTaskComplete = true;
    }

    public void UpdateContentWindow(EmailPlaceholder emailPlaceholder)
    {
        _currentEmail = emailPlaceholder;
        senderText.text = emailPlaceholder.email.sender;
        recipientText.text = emailPlaceholder.email.recipient;
        bodyText.text = emailPlaceholder.email.body;
        replyText.text = emailPlaceholder.email.reply;

        replyBox.gameObject.SetActive(false);
        replySend.gameObject.SetActive(false);
        
        if (emailPlaceholder.email.reply.Length > 0)
        {
            trashButton.gameObject.SetActive(false);
            replyButton.gameObject.SetActive(true);
        }
        else
        {
            replyButton.gameObject.SetActive(false);
            trashButton.gameObject.SetActive(true);
        }
    }

    public void SetTaskComplete()
    {
        _currentEmail.taskComplete = true;
    }
}