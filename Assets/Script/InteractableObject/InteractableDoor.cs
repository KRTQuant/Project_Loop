using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    private ReactiveProperty<bool> IsOpen;
    private ObjectAnimatorView animatorView;

    public void Interact()
    {
        this.IsOpen.Value = !this.IsOpen.Value;

        this.animatorView.SetTrigger("Trigger");
    }

    public bool IsInteractable()
    {
        return true;
    }
}
