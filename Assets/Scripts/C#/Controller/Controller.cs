using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Animator animator; 

	void Start () {

        // アニメーション情報の取得
        animator = GetComponent<Animator>();
	}
	
	void Update () {

        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.01f;
            animator.SetBool("is_running", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -10, 0);
        }


    }
}
