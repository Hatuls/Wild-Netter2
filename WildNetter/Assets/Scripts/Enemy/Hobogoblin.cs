using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hobogoblin : Enemy
{
    //fake animation for Presentetion//
    public GameObject[] FakeAnimObj;
    public Hobogoblin() : base()
    {
        
    }
    public void DeployMesh(int pos)
    {
        foreach(GameObject found in FakeAnimObj)
        {
            if (found == FakeAnimObj[pos])
            {
                found.gameObject.SetActive(true);
            }
            else
            {
                found.gameObject.SetActive(false);

            }
        }

    }
   
    public override IEnumerator Attack1()
    {
        
        float timeInThread=0;
        attack1_inCd = true;
        _enemySheet.enemyState = EnemyState.Attack;
        //playAnimation//
        //time to connect with animation//
        yield return new WaitForSeconds(_enemySheet.Attack1_AnimDelay);
        timeInThread+= _enemySheet.Attack1_AnimDelay;
        Collider[] Hit = Physics.OverlapSphere(transform.position + transform.forward*_enemySheet.Attack1_RangeFromSource, _enemySheet.Attack1_Range, TargetLayer);
        foreach(Collider found in Hit)
        {

            PlayerCombat playerCombat = found.GetComponentInParent<PlayerCombat>();
            playerCombat.GetHit(base._enemySheet.attackDMG, transform.position);
            Debug.Log("GotHitByGoblin1");
        }
        //animation time-timeInThread//
        DeployMesh(1);
        yield return new WaitForSeconds(_enemySheet.attack1_animLenght - timeInThread);
        DeployMesh(2);
        timeInThread += _enemySheet.attack1_animLenght - timeInThread;
        _enemySheet.enemyState = EnemyState.Chase;
        //coolDown-timeInThread
        yield return new WaitForSeconds(_enemySheet.Attack1_Cd-timeInThread);
        DeployMesh(0);
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
            playerCombat.GetHit(base._enemySheet.attackDMG, transform.position);
            Debug.Log("GotHitByGoblin2");
            
        }
        //animation time-timeInThread//
        DeployMesh(1);
        yield return new WaitForSeconds(_enemySheet.attack2_animLenght-timeInThread);
        DeployMesh(2);
        timeInThread += _enemySheet.attack2_animLenght - timeInThread;
        _enemySheet.enemyState = EnemyState.Chase;
        //coolDown-timeInThread
        yield return new WaitForSeconds(_enemySheet.Attack1_Cd - timeInThread);
        DeployMesh(0);
        attack2_inCd = false;
    }
 
}