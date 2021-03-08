using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
   [SerializeField]  static Transform pickUpContianer;
   [SerializeField] GameObject ItemPrefab;
    ItemData item;
    static Transform playerPos;
    public static PickUpObject SpawnItemInWorld(ItemData item, Vector3 pos , Transform PlayerTransform) {
        playerPos = PlayerTransform;

     Transform t =   Instantiate(ItemFactory._Instance.itemPF, pos, Quaternion.identity, pickUpContianer);

        PickUpObject pickUpObject = t.GetComponent<PickUpObject>();

      pickUpObject.GetComponent<Rigidbody>().AddForce(ShootItemInRandomDirection(),ForceMode.Impulse);

        pickUpObject.SetItem(item);

       pickUpObject.SetSprite(item); 

        pickUpObject.StartCoroutine(pickUpObject.CheckDistanceCoroutine());

        return pickUpObject;
    }
    private void SetSprite(ItemData item)
    { 
        GetComponent<SpriteRenderer>().sprite =ItemFactory._Instance.GetItemSprite(item.GetData.ID); ;

    }


  static  Vector3 ShootItemInRandomDirection() {
        float Amount= 10;
        float x, y, z;
        x = Random.Range(-1f, 1f);
        y = Random.Range(0f, 1f);
        z = Random.Range(-1f, 1f);


        return (new Vector3(x, y, z).normalized * Amount) ; 
    }
    public void SetItem(ItemData item) { 
        this.item = item;
    }
    public ItemData GetItem() { return item; }
    IEnumerator CheckDistanceCoroutine() {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position , playerPos.position) < 2f)
        {
           // need to add logic if the player can add this item or maybe he has max capcity on it
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
