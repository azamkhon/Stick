using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Stick : MonoBehaviour
{
    [SerializeField]
    Rigidbody[] _ragdollRb;

    [SerializeField]
    float _force;
    Animator _animator;
    CharacterController _characContr;

    [SerializeField]
    Transform Hips;


    [SerializeField]
     Camera _camera;


    boneInfo[] _standBoneTrans;
    boneInfo[] _GetBoneTrans;
    
    boneInfo[] _ragDollBoneTrans;
      [SerializeField]
    Transform[] _bones;

      Vector3 stickPos;
      Quaternion stickRot;
    void Start()
    {
        isStand = false;
        stickPos = transform.position;
        stickRot = transform.rotation;
        _ragdollRb = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characContr = GetComponent<CharacterController>();

        _bones = Hips.GetComponentsInChildren<Transform>();
        _standBoneTrans = new boneInfo[_bones.Length];
        _GetBoneTrans = new boneInfo[_bones.Length];
        _ragDollBoneTrans = new boneInfo[_bones.Length];

        for (int i = 0; i < _bones.Length; i++) {
            _standBoneTrans[i] = new boneInfo();
            _GetBoneTrans[i] = new boneInfo();
            _ragDollBoneTrans[i] = new boneInfo();
        }
       
        firsteFrameStandUp();

    }

    class boneInfo
    {
        public Vector3 pos { get; set; }
        public Quaternion rot { get; set; }

    }

    // Update is called once per frame

    public void resetGame() {
        SceneManager.LoadScene(0);
    }

    public void resetStick() {
        checkMoveHits = false;
        isStand = false;
        CancelInvoke("chec");
        _animator.StopPlayback();
        _animator.Rebind();
        _animator.Play("idle");

        _animator.enabled = false;
        _characContr.enabled = false;
        foreach (var rb in _ragdollRb)
        {
            rb.isKinematic = false;
        }


        transform.position = stickPos;
         transform.rotation = stickRot;
        _animator.enabled = true;
        _characContr.enabled = true;

        foreach (var rb in _ragdollRb)
        {
            rb.isKinematic = true;
        }
    }

    bool isStand ;
    void Update()
    {


        if (checkMoveHits) {
            if (Hips.GetComponent<Rigidbody>().velocity.magnitude < 0.05f) {
                checkMoveHits = false;
                trueReset();
                
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
           
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5000))
            {
                if (hit.collider.tag == "RD")
                {
                    enblRagDoll();
                    hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(_camera.transform.TransformDirection(Vector3.forward) * _force);

                }
            }
        }

        if (isStand) {
          
            resetPosRotBone();
        }
      
    }

    [SerializeField]
    float _time;

    [SerializeField]
    float _timeReset;

    void resetPosRotBone() {

        _time += Time.deltaTime;
        float elapsedPercentage = _time / _timeReset;
        if (isFaceUp)
        {
            for (int i = 0; i < _bones.Length; i++)
            {
                _bones[i].localPosition = Vector3.Lerp(_ragDollBoneTrans[i].pos, _standBoneTrans[i].pos, elapsedPercentage);
                _bones[i].localRotation = Quaternion.Lerp(_ragDollBoneTrans[i].rot, _standBoneTrans[i].rot, elapsedPercentage);

            }
        }
        else {
            for (int i = 0; i < _bones.Length; i++)
            {
                _bones[i].localPosition = Vector3.Lerp(_ragDollBoneTrans[i].pos, _GetBoneTrans[i].pos, elapsedPercentage);
                _bones[i].localRotation = Quaternion.Lerp(_ragDollBoneTrans[i].rot, _GetBoneTrans[i].rot, elapsedPercentage);

            }
        }
        if (elapsedPercentage >= 1) {
            isStand = false;
            _time = 0;
            
            disRagDoll();
        }

    }

    void disRagDoll() {

        if (isFaceUp)
        {
            _animator.Rebind();
            _animator.Play("stand");
        }
        else {
            _animator.Rebind();

            _animator.Play("getUp");
        
        }
        foreach (var rb in _ragdollRb) {
            rb.isKinematic = true;
        }
        _animator.enabled = true;
        _characContr.enabled = true;
       
    }
    bool checkMoveHits;
    void enblRagDoll()
    {

        foreach (var rb in _ragdollRb)
        {
            rb.isKinematic = false;
        }

        _animator.enabled = false;
        _characContr.enabled = false;

        CancelInvoke("chec");
        Invoke("chec", 2);
    }


    void chec() {
        checkMoveHits = true;
    }

    void trueReset() {
        resetPos();
        addBoneInfo(_ragDollBoneTrans);
        isStand = true;
    }

    bool isFaceUp;

    void resetPos()
    {
        Vector3 oldPos = Hips.position;
        Quaternion oldRot = Hips.rotation;

        if (Hips.transform.forward.y > 0)
        {
            isFaceUp = true;
        }
        else {
            isFaceUp = false;
        }

        transform.position = Hips.position;
        transform.rotation = Hips.rotation;
        transform.rotation = Quaternion.Euler(0, Hips.rotation.eulerAngles.y+180, 0);

        Hips.position = oldPos;
        Hips.rotation = oldRot;

    }

    void addBoneInfo(boneInfo[] boneinf)
    {

        for (int i = 0; i < _bones.Length; i++)
        {
            boneinf[i].pos = _bones[i].localPosition;
            boneinf[i].rot = _bones[i].localRotation;
        }
       
    }

    void firsteFrameStandUp()
    {

        Vector3 posOld = transform.position;
        Quaternion rotOld = transform.rotation;
        bool s =false;
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            
            if (clip.name == "stand")
            {
                clip.SampleAnimation(gameObject, 0);
                
                addBoneInfo(_standBoneTrans);
                if (s)
                {
                    break;
                }
                s = true;
            }

            if (clip.name == "getUp")
            {
                clip.SampleAnimation(gameObject, 0);

                addBoneInfo(_GetBoneTrans);
                if (s) {
                    break;
                }
                s = true;
            }
        }

        transform.position = posOld;
        transform.rotation = rotOld;
    }


}
