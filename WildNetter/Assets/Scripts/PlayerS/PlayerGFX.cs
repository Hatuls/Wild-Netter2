
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public enum VFXWorldType {PlayerGotHit, EnemyGotHit};
public class PlayerGFX : MonoBehaviour
{

    //Variables:
    [SerializeField] VisualEffect enemyHitVFX, playerGotHitVFX;
    //Components:
    public Animator _Animator;
    Dictionary<string, float> animationDurationDict = new Dictionary<string, float>();

    //Functions:

    public void Init()
    {
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
    public void SetAnimationTrigger(string paramName)
    {
       // _Animator.SetTrigger(paramName);

       // StartCoroutine(AnimCoru());
    }
    public void SetAnimationFloat(float value, string paramName)
    {
        //_Animator.SetFloat(paramName, value);

    }
    IEnumerator AnimCoru()
    {

        yield return new WaitForSeconds(_Animator.GetCurrentAnimatorClipInfo(0).Length);

        Debug.Log("End");
    }

    public void ApplyPlayerVFX(Vector2 position, VFXWorldType vFXWorldType)
    {

        var effect = GetVisualEffectFromEnum(vFXWorldType);
        if (effect == null)
            return;

        var effectGO = effect.gameObject;
        effectGO.gameObject.transform.position = position;
        StartCoroutine(VFXCooldown(0.5f, effectGO));
    }

    IEnumerator VFXCooldown(float duration, GameObject vfxGO)
    {

        vfxGO.SetActive(true);
        yield return new WaitForSeconds(duration);
        vfxGO.SetActive(false);

    }
    VisualEffect GetVisualEffectFromEnum(VFXWorldType type)
    {
        switch (type)
        {
            case VFXWorldType.PlayerGotHit:
                return playerGotHitVFX;
            case VFXWorldType.EnemyGotHit:
                return enemyHitVFX;

        }
        return null;
    }
}