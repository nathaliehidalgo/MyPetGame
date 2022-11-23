using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    public float offsetSpeed = 1f;

    private void Update()
    {
        transform.position = Vector3.forward *  player.transform.position.z;
        material.SetVector("grassOffset", new Vector2(0, -transform.position.z * offsetSpeed));
    }

}
