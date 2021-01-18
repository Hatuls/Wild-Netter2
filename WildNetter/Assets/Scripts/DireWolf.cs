using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireWolf : Enemy
{
    public DireWolf() : base()
    {
        
    }

    public override IEnumerator Attack1()
    {
        float timeInThread = 0;
        attack1_inCd = true;
        _enemySheet.enemyState = EnemyState.Attack;
        //playAnimation//
        //time to connect with animation//
        yield return new WaitForSeconds(_enemySheet.Attack1_AnimDelay);
        timeInThread+= _enemySheet.Attack1_AnimDelay;
        Collider[] Hit = Physics.OverlapSphere(transform.position + transform.forward* _enemySheet.Attack1_RangeFromSource, _enemySheet.Attack1_Range, TargetLayer);
        foreach (Collider found in Hit)
        {
            Debug.Log("GotHitByOrc1");
        }
        //animation time-timeInThread//
        yield return new WaitForSeconds(_enemySheet.attack1_animLenght - timeInThread);
        timeInThread += _enemySheet.attack1_animLenght - timeInThread;
        _enemySheet.enemyState = EnemyState.Chase;
        //coolDown-timeInThread
        yield return new WaitForSeconds(_enemySheet.Attack1_Cd - timeInThread);
        attack1_inCd = false;
    }

    public override IEnumerator Attack2()
    {
        float timeInThread = 0;
        attack2_inCd = true;
        _enemySheet.enemyState = EnemyState.Attack;
        //playAnimation//
        //time to connect with animation//
        yield return new WaitForSeconds(_enemySheet.Attack2_AnimDelay);
        timeInThread+= _enemySheet.Attack2_AnimDelay;
        Collider[] Hit = Physics.OverlapSphere(transform.position + transform.forward* _enemySheet.Attack2_RangeFromSource, _enemySheet.Attack2_Range, TargetLayer);
        foreach (Collider found in Hit)
        {

            PlayerCombat playerCombat = found.GetComponentInParent<PlayerCombat>();
            playerCombat.GetHit();
            Debug.Log("GotHitByOrc2");
        }
        //animation time-timeInThread//
        yield return new WaitForSeconds(_enemySheet.attack2_animLenght - timeInThread);
        timeInThread += _enemySheet.attack2_animLenght - timeInThread;
        _enemySheet.enemyState = EnemyState.Chase;
        //coolDown-timeInThread
        yield return new WaitForSeconds(_enemySheet.Attack1_Cd - timeInThread);
        attack2_inCd = false;
    }
}
   
   
    

