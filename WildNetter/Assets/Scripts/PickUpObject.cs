using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
   [SerializeField]  static Transform pickUpContianer;
   [SerializeField] GameObject ItemPrefab;
    Item item;
    static Transform playerPos;
    public static PickUpObject SpawnItemInWorld(Item item, Vector3 pos , Transform PlayerTransform) {
        playerPos = PlayerTransform;

     Transform t =   Instantiate(ItemFactory.GetInstance().itemPF, pos, Quaternion.identity, pickUpContianer);

        PickUpObject pickUpObject = t.GetComponent<PickUpObject>();

        pickUpObject.GetComponent<Rigidbody>().AddForce(Vector3.right);

        pickUpObject.SetItem(item);

       pickUpObject.SetSprite(item); 

        pickUpObject.StartCoroutine(pickUpObject.CheckDistanceCoroutine());

        return pickUpObject;
    }
    private void SetSprite(Item item)
    { 
        GetComponent<SpriteRenderer>().sprite =ItemFactory.GetInstance().GetItemSprite(item.ID); ;

    }

    public void SetItem(Item item) { 
        this.item = item;
    }
    public Item GetItem() { return item; }
    IEnumerator CheckDistanceCoroutine() {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position , playerPos.position) < 2f)
        {
           // need to add logic if the player can add this item or maybe he has max capcity on it
            PlayerInventory.AddToInventory(item);
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
