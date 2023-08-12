using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HitRD : MonoBehaviour
{
   // // Start is called before the first frame update
   // [SerializeField]
   // Camera cam;
   //// RaycastHit hit;
   // [SerializeField]
   // Rigidbody[] rb;

   // [SerializeField]
   // Animator anim;

   // [SerializeField]
   // PuppetMaster PM;

   // [SerializeField]
   // Transform Hips;

   // [SerializeField]
   // Transform StickmanRoot;
   // void Start()
   // {

   //     anim = GetComponent<Animator>();

   //     rb = GetComponentsInChildren<Rigidbody>();

   //   //  anim.Play("stand");
       
   // }


   


   // // Update is called once per frame
   // void Update()
   // {
   //     if (Input.GetMouseButtonDown(0))
   //     {

   //         Ray ray = cam.ScreenPointToRay(Input.mousePosition);
   //         RaycastHit hit;
   //         if (Physics.Raycast(ray, out hit, 100)) {
   //                 if (hit.collider.tag == "RD")
   //                 {
   //                    // RdContr(false);
   //                   //  anim.enabled = false;
   //                        PM.state = PuppetMaster.State.Frozen;
   //                     //  PM.mode = PuppetMaster.Mode.Kinematic;
                      
   //                     hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(cam.transform.TransformDirection(Vector3.forward) * 4000);

   //                     anim.Rebind();
   //                     anim.Play("stand");
   //                     Invoke("resetPos", 8);
   //                 }
   //         }

   //     }
   // }

   // void resetPos() {
   //     Vector3 oldPos = Hips.position;
   //  //   Vector3 oldPosLoc = Hips.localPosition;
   //     float oldRotY = Hips.rotation.y;
   //     StickmanRoot.position = new Vector3(Hips.position.x, 1.1f, Hips.position.z);
       
       
   //     //StickmanRoot.rotation = Quaternion.EulerAngles(StickmanRoot.rotation.x, oldRotY, StickmanRoot.rotation.z);
   //     Hips.position = oldPos;
   //     print("asdasd");

   //   //  anim.enabled = true;
       
   //     PM.state = PuppetMaster.State.Alive;
  

       

    
   // }



   // void RdContr(bool s) {
   //     for (int i = 0; i < rb.Length; i++) {
   //         rb[i].isKinematic = s;
   //     }
    
   // }
}
