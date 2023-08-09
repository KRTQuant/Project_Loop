using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimatorView : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void SetBool(string name, bool value)
    {
        this.animator.SetBool(name, value);
    }

    public void SetFloat(string name, float value)
    {
        this.animator.SetFloat(name, value);
    }

    public void SetTrigger(string name)
    {
        this.animator.SetTrigger(name);
    }

}
