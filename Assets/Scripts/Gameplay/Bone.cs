using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PickupBone();
    }

    private void PickupBone()
    {
        anim?.SetTrigger("Pickup");
        GameStats.Instance.CollectBone();
        //increment the bone count
        //increment the score
        //play sfx
        //trigger an animation
    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }

}
