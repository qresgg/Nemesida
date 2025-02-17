using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemPanelButton : MonoBehaviour
{
    [SerializeField] Button ItemButton;
    [SerializeField] Animator animator;
    [SerializeField] bool isEnabled = false;

    string animationTriggerName = "Move";
    Vector3 moveAmount = new Vector3(36, 0, 0);

    void Start()
    {
        ItemButton.onClick.AddListener(OnClickMove);
    }

    void OnClickMove()
    {
        isEnabled = !isEnabled;
        animator.SetBool("IsEnable", isEnabled);

        if (isEnabled)
        {
            transform.Translate(-moveAmount);
        }
        else
        {
            transform.Translate(moveAmount);

        }
    }
}
