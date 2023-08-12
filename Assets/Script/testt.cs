using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testt : MonoBehaviour
{

    public AnimationClip anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim.SampleAnimation(gameObject,50);
        transform.rotation = Quaternion.Euler(0, 100, 0);
        print(transform.rotation.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
