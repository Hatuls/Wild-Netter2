using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemOfPrey : TotemSO
{
    private List<GameObject> enemyCatched = new List<GameObject>();
    public TotemOfPrey(string[] lootData, string[] totemData) : base(lootData, totemData)
    {

    }
    public override void DoEffect(Vector3 totemLocation, GameObject totem)
    {
        Collider[] objectCollider;
        objectCollider = Physics.OverlapSphere(totemLocation, range, TotemManager._instance.enemiesLayer);
        Debug.Log(objectCollider.Length);
        foreach (Collider col in objectCollider)
        {
            if (CheckRange(totemLocation, col.transform.position, range))
            {
                if (enemyCatched.Contains(col.gameObject))
                {
                    continue;
                }

                else
                {
                    enemyCatched.Add(col.gameObject);
                    col.transform.root.GetComponent<Enemy>().TotemEffect(TotemName.prey, totem);
                }
            }


            /*if (pull)
            {
                //remove from here when enemy is done
                col.GetComponent<Enemy>().TotemEffect(TotemType.prey, totem);
                if (currentRealTime > GetCurrentTime())
                {
                    pull = false;
                    col.GetComponent<Enemy>().CancelEffect();
                }
            }*/
        }
    }
    public override IEnumerator ActivateTotemEffect(GameObject totem)
    {
        float timeToDestroy = duration + GetCurrentTime();
        while (true)
        {
            this.DoEffect(totem.transform.position, totem);
            if (GetCurrentTime() > timeToDestroy)
            {
                foreach (GameObject go in enemyCatched)
                {
                    go.GetComponent<Enemy>().CancelEffect();
                }
                break;
            }
            currentRealTime = GetCurrentTime();
            yield return new WaitForSeconds(1);
        }
    }
}
