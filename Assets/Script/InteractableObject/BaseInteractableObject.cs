using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseInteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string audioName;
    [SerializeField]
    private ObjectAudioView audioView;

    private bool isYeet = false;
    private bool isAudioPlayed = false;

    public void Interact()
    {
        
    }

    public bool IsInteractable()
    {
        return true;
    }

    private void OnCollisionEnter()
    {
        if(!this.isYeet || this.isAudioPlayed || this.audioName == "" || this.audioName == null) return;

        this.audioView.Play(this.audioName);
        this.isAudioPlayed = true;
    }

    public void Yeet()
    {
        this.isYeet = true;
    }
}
