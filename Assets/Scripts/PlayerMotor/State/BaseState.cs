using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent Class for animations

public abstract class BaseState : MonoBehaviour
{
    protected PlayerMotor motor;


    public virtual void Construct(){}
    public virtual void Destruct(){}
    public virtual void Transition(){}

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }

    public virtual Vector3 ProcessMotion()
    {
        Debug.Log("Process motion is not implemented in " + this.ToString());
        return Vector3.zero;
    }


}
