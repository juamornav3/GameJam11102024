using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveInventory : MonoBehaviour
{
    Inventory inventory;
    SaveData data;

    string savePath;
    public Sprite background;
    private List<GameObject> itemObjects = new List<GameObject>();
    public List<Conversation> conversationsObjects = new List<Conversation>();


    public void Awake()
    {
        Item[] itemsInScene = FindObjectsOfType<Item>();
        foreach (var item in itemsInScene)
        {
            itemObjects.Add(item.gameObject);
        }
        

        inventory = FindObjectOfType<Inventory>();
        savePath = Application.persistentDataPath + "/saves/";

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        savePath += "inventorySave.dat";

        if (!File.Exists(savePath))
        {
            SaveData newData = new SaveData();
            newData.items = new List<ItemSave>();
            Save(newData);
        }
        data = Load();
    }

    public string GetSavePath()
    {
        return savePath;
    }

    public void Save(SaveData dataToSave)
    {
        string jsonData = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(savePath, jsonData);
    }

    public SaveData Load()
    {
        string loadedData = File.ReadAllText(savePath);
        SaveData dataToReturn = JsonUtility.FromJson<SaveData>(loadedData);
        return dataToReturn;
    }

    public void SaveInventoryButton()
    {
        GameObject[] slots = inventory.GetComponent<Inventory>().slot;
        List<ItemSave> items = new List<ItemSave>();
        for (int i = 0; i < inventory.GetComponent<Inventory>().allSlots; i++)
        {
            if (!slots[i].GetComponent<Slot>().empty)
            {
                ItemSave item = new ItemSave();
                item.id = slots[i].GetComponent<Slot>().id;
                item.type = slots[i].GetComponent<Slot>().type;
                item.description = slots[i].GetComponent<Slot>().description;
                item.iconName = slots[i].GetComponent<Slot>().icon.name;
                items.Add(item);
            }

            
        }

        data.items = items;

        Save(data);
    }

    public void LoadInventoryButton()
    {
        GameObject[] slots = inventory.GetComponent<Inventory>().slot;

        CleanInventory(slots);
        ItemsState();
        for (int i = 0; i < data.items.Count; i++)
        {
            slots[i].GetComponent<Slot>().id = data.items[i].id;
            slots[i].GetComponent<Slot>().type = data.items[i].type;
            slots[i].GetComponent<Slot>().description = data.items[i].description;
            string spriteName = data.items[i].iconName;
            Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
            slots[i].GetComponent <Slot>().icon = loadedSprite;


            slots[i].GetComponent<Slot>().UpdateSlot();
            slots[i].GetComponent<Slot>().empty = data.items[i] == null;

        }
    }

    private void CleanInventory(GameObject[] slots)
    {
        for (int i = 0; i < inventory.GetComponent<Inventory>().allSlots; i++)
        {
            slots[i].GetComponent<Slot>().id = 0;
            slots[i].GetComponent<Slot>().type = null;
            slots[i].GetComponent<Slot>().description = null;
            slots[i].GetComponent<Slot>().icon = background;
            slots[i].GetComponent<Slot>().UpdateSlot();
            slots[i].GetComponent<Slot>().empty = true;
        }
    }

    public void ItemsState()
    {
        List<int> itemsId = new List<int>();
        for (int i=0; i<data.items.Count;i++)
        {
            itemsId.Add(data.items[i].id);
        }
        foreach (GameObject item in itemObjects)
        {
            if (item != null)
            {
                if (itemsId.Contains(item.GetComponent<Item>().id))
                {
                    item.SetActive(false);
                }
                else
                {
                    item.SetActive(true);
                }
            }
        }
    }

    public void DeleteSaves()
    {
        string deletePath = savePath.Replace("inventorySave.dat","");
        if (Directory.Exists(deletePath))
        {
            Directory.Delete(deletePath, true);
            Debug.Log("Datos de guardado borrados correctamente.");
        }
        else
        {
            Debug.Log("No se encontraron datos de guardado.");
        }
    }


}

public class SaveData
{
    public List<ItemSave> items;
    public List<List<Conversation>> conversations;
}

[System.Serializable]
public class ItemSave
{
    public int id;
    public string type;
    public string description;
    public string iconName;

}
