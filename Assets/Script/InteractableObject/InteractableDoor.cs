using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected ReactiveProperty<bool> isOpen;
    [SerializeField]
    protected ObjectAnimatorView animatorView;
    [SerializeField]
    protected ObjectAudioView audioView;

    public bool IsOpen => this.isOpen.Value;

    public virtual void Interact()
    {
        if(this.isOpen.Value) return ;
        
        this.animatorView.SetTrigger("Trigger");
        this.audioView.Play("Open");
        this.isOpen.Value = true;
    }

    public virtual bool IsInteractable()
    {
        return true;
    }
}
