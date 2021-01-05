using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    public static PlayerGFX _instance;
    //Variables:

    //Components:
    public Animator _Animator;
    Dictionary<string, float> animationDurationDict = new Dictionary<string, float>();

    //Functions:

    private void Awake()
    {
        _instance = this;
    }
    public void Init() {





        UpdateAnimClipTimes();
    
    }

public void UpdateAnimClipTimes()
{
    AnimationClip[] clips = _Animator.runtimeAnimatorController.animationClips;
    foreach (AnimationClip clip in clips)
    {
            animationDurationDict.Add(clip.name, clip.length);
    }
}
public void SetAnimationTrigger(string paramName) {
        _Animator.SetTrigger(paramName);
     
        StartCoroutine(AnimCoru(paramName));

        


    }
    public void SetAnimationFloat(float value, string paramName)
    {
        _Animator.SetFloat(paramName, value);

    }
    IEnumerator AnimCoru(string paramName) {


                PlayerMovement.SetPlayerRotateAble = false;
   
                yield return new WaitForSeconds(_Animator.GetCurrentAnimatorClipInfo(0).Length - 0.15f);
                PlayerMovement.SetPlayerRotateAble = true;
                Debug.Log("End");

        
        
       
    }
}
