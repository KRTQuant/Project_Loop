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
    [SerializeField]
    protected ObjectAudioView audioView;

    public virtual void Interact()
    {
        this.IsOpen.Value = !this.IsOpen.Value;
        this.animatorView.SetTrigger("Trigger");
        this.audioView.Play("Open");
    }

    public virtual bool IsInteractable()
    {
        return true;
    }
}
