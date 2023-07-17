using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator trampolineAnimator;
    [SerializeField] CapsuleCollider2D trampolineCollider;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (trampolineCollider.IsTouchingLayers(LayerMask.GetMask("Default")))
        {
            trampolineAnimator.SetTrigger("isWorking");
            GetComponent<AudioSource>().Play();
        }
    }
}