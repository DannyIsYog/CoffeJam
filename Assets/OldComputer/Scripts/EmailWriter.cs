using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EmailWriter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI replyText;
    [SerializeField] private UnityEvent onWritingFinish;

    private string _reply;
    private int _idx;

    private void OnEnable()
    {
        _idx = 0;
        _reply = replyText.text;
        replyText.text = "";
    }

    private void Update()
    {
        if (_idx == _reply.Length)
        {
            onWritingFinish.Invoke();
            _idx++;
        }
        if (Input.anyKeyDown && _idx < _reply.Length)
        {
            replyText.text += _reply[_idx];
            _idx++;
        }
    }
}
