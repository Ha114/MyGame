using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();

    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;

    public Transform mainCaanvas;


    public float moveX = 180f;
    public float moveY = 70f;

    public Transform hotbarTransform;
    public Transform inventoryTransform;

    public GameObject massageManager;
    public GameObject massage;

    private Item item;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }*/
        if (Input.GetKeyDown(KeyCode.E))
        {

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {
                if (hit.collider.GetComponent<IamItem>()) {
                    IamItem currentItem = hit.collider.GetComponent<IamItem>();
                    int _id = currentItem.ID;
                    // Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
                    Inventory.instance.AddItem(itemList[_id]);
                    // GameObject msgObj = Instantiate(massage);
                    // msgObj.transform.SetParent(massageManager.transform);
                    Massage(_id);
                    currentItem.gameObject.SetActive(false);
                }

            }
        }
    }

    void Massage(int id){
        GameObject msgObj = Instantiate(massage);
        msgObj.transform.SetParent(massageManager.transform);
        Text msg = msgObj.transform.GetChild(1).GetComponent<Text>();
        Image msgImage = msgObj.transform.GetChild(0).GetComponent<Image>();

        Item currentItem = Inventory.instance.forMassage(itemList[id]);
        msg.text = currentItem.name;
        msgImage.sprite = currentItem.icon;
        DestroyMassage(msgObj);
    }

    void DestroyMassage(GameObject g) {
        Destroy(g, 1);    
    }

    void SortFood(int id) {
        Item currentItem = Inventory.instance.forMassage(itemList[id]);
        


    }

    public void OnStatItemuUse(StatItemType itemType, int amount)
    {
        Debug.Log("Consuming " + itemType + " Add amount: " + amount);
    }

    public void DisplayItemInfo(string itemName, string itemDescription, Vector2 buttonPos)
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        buttonPos.x -= moveX;
        buttonPos.y += moveY;

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);

    }

    public void DestroyItemInfo()
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }

}