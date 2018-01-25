using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animator : MonoBehaviour {
    Animator anim;
    Rigidbody rigid;

	// Initialization
	void Start () {
        anim = this.GetComponent<Animator>();
        rigid = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 groundv = rigid.velocity;
        anim.SetFloat("speed", groundv.magnitude);
	}
}
