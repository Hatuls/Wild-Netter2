using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGFX : MonoBehaviour
{
    public static PlayerGFX _instance;
    //Variables:

    public PlayerAnimation playerAnimation;
    public enum PlayerAnimation { PlayerIdle, PlayerWalk, Stabbing}

    //Components:
    public Animator _Animator;


    //Functions:

    private void Awake()
    {
        _instance = this;
    }
    public void Init() { }
    public void SetAnimationTrigger(string paramName) {
        _Animator.SetTrigger(paramName);
  
    }
    public void SetAnimationFloat(float value, string paramName)
    {
        _Animator.SetFloat(paramName, value);

    }
    IEnumerator AnimCoru() { yield return null; }
}
