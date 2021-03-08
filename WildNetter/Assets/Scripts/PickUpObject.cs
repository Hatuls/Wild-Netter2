using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] static Transform pickUpContianer;
    [SerializeField] GameObject ItemPrefab;
    // ItemData item;
    static Transform playerPos;
    private int itemID;
    private int amount;
    public static PickUpObject SpawnItemInWorld(int itemID,int amount, Vector3 pos , Transform PlayerTransform) {
        playerPos = PlayerTransform;
      
        Transform t =   Instantiate(ItemFactory._Instance.itemPF, pos, Quaternion.identity, pickUpContianer);

        PickUpObject pickUpObject = t.GetComponent<PickUpObject>();
       
        pickUpObject.itemID = itemID; 
        pickUpObject.GetComponent<SpriteRenderer>().sprite =ItemFactory._Instance.GetItemSprite(itemID);
        pickUpObject.amount = amount;
        pickUpObject.GetComponent<Rigidbody>().AddForce(ShootItemInRandomDirection(),ForceMode.Impulse);
        pickUpObject.StartCoroutine(pickUpObject.CheckDistanceCoroutine());

        return pickUpObject;
    }

  static  Vector3 ShootItemInRandomDirection() {
        float Amount= 10;
        float x, y, z;
        x = Random.Range(-1f, 1f);
        y = Random.Range(0f, 1f);
        z = Random.Range(-1f, 1f);


        return (new Vector3(x, y, z).normalized * Amount) ; 
    }

    IEnumerator CheckDistanceCoroutine() {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position , playerPos.position) < 2f)
        {
            // need to add logic if the player can add this item or maybe he has max capcity on it
            var item = ItemFactory._Instance.GenerateItem(itemID);
            item.amount = amount;
            PlayerInventory.GetInstance.AddToInventory(item);
            Destroy(this.gameObject);

        }
        else
        {
        StartCoroutine(CheckDistanceCoroutine());
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(CheckDistanceCoroutine());
    }

}
