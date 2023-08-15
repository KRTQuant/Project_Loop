using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected ReactiveProperty<bool> IsOpen;
    [SerializeField]
    protected ObjectAnimatorView animatorView;

    public virtual void Interact()
    {
        this.IsOpen.Value = !this.IsOpen.Value;
        this.animatorView.SetTrigger("Trigger");
    }

    public virtual bool IsInteractable()
    {
        return true;
    }
}
