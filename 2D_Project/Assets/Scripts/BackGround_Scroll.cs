using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround_Scroll : MonoBehaviour
{
    [SerializeField]
    private Transform   target;
    [SerializeField]
    private float       scrollAmount;
    [SerializeField]
    private float       moveSpeed;
    [SerializeField]
    private Vector3     moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if(transform.position.x <= -scrollAmount)
        {
            transform.position = target.position - moveDirection * scrollAmount;
        }
    }
}
