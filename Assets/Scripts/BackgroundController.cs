using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Image background1;
    [SerializeField] private Image background2;
    [SerializeField] private Animator animator;

    private bool isSwitched;
    private static readonly int SwitchFirst = Animator.StringToHash("SwitchFirst");
    private static readonly int SwitchSecond = Animator.StringToHash("SwitchSecond");

    public void SwitchBackground(Sprite sprite)
    {
        if (!isSwitched)
        {
            background2.sprite = sprite;
            animator.SetTrigger(SwitchFirst);
        }
        else
        {
            background1.sprite = sprite;
            animator.SetTrigger(SwitchSecond);
        }

        isSwitched = !isSwitched;
    }

    public void SetBackground(Sprite sprite)
    {
        if (!isSwitched) background1.sprite = sprite;
        else background2.sprite = sprite;
    }
}