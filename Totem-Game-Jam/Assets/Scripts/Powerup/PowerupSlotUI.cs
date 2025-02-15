using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static PlayerInventory;

public class PowerupSlotUI : MonoBehaviour
{

    [System.Serializable]
    public class BorderSettings
    {
        public Color defaultColor = Color.gray;
        public Color hoverColor = Color.yellow;
        public Color selectedColor = Color.blue;
    }

    public PowerUpType powerUpType;
    public GameObject icon;
    public GameObject border;
    public GameObject text;

    public BorderSettings borderSettings;
    public float dragOpacity = 0.6f;

    private PlayerInventory inventory;
    private GameObject dragObject;
    private GameObject powerupPrefab;



    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInventory>();
    }

    public void Setup(Sprite iconSprite, int quantity, GameObject prefab, PowerUpType powerUpType)
    {
        icon.GetComponent<SpriteRenderer>().sprite = iconSprite;
        text.GetComponent<TextMeshPro>().text = ToRomanNumeral(quantity);
        this.powerupPrefab = prefab;
        this.powerUpType = powerUpType;
    }

    public void SetBorderColor(Color color)
    {
        border.GetComponent<SpriteRenderer>().color = color;
    }


    private static string ToRomanNumeral(int number)
    {

        var retVal = new StringBuilder(5);
        var valueMap = new SortedDictionary<int, string>
                           {
                               { 1, "I" },
                               { 4, "IV" },
                               { 5, "V" },
                               { 9, "IX" },
                               { 10, "X" },
                               { 40, "XL" },
                               { 50, "L" },
                               { 90, "XC" },
                               { 100, "C" },
                               { 400, "CD" },
                               { 500, "D" },
                               { 900, "CM" },
                               { 1000, "M" },
                           };

        foreach (var kvp in valueMap.Reverse())
        {
            while (number >= kvp.Key)
            {
                number -= kvp.Key;
                retVal.Append(kvp.Value);
            }
        }

        return retVal.ToString();
    }


    public GameObject GetDragObject()
    {
        return dragObject;
    }

    public GameObject GetPrefab()
    {
        return powerupPrefab;
    }

    public PlayerInventory GetInventory()
    {
        return inventory;
    }

    public void SetDragObject(GameObject dragObject)
    {
        this.dragObject = dragObject;
    }


    public void OnMouseUp()
    {
        if (GetDragObject() == null)
        {
            return;
        }

        Destroy(GetDragObject());

        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!isValidPosition(position))
        {
            return;
        }
        GameObject powerupObject = Instantiate(GetPrefab(), position, Quaternion.identity);
        powerupObject.GetComponent<BoxCollider2D>().isTrigger = true;
        powerupObject.transform.position = new Vector3(powerupObject.transform.position.x, powerupObject.transform.position.y, -9);
        powerupObject.GetComponent<DragPowerup>().Start();
        GetInventory().UsePowerup(powerUpType);
    }

    private bool isValidPosition(Vector2 position)
    {
        return true;
    }

    public void Update()
    {
        if (GetDragObject() == null)
        {
            return;
        }
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    GetCanvas().GetComponent<RectTransform>(), eventData.position, GetCanvas().worldCamera, out Vector2 localPoint);
        GetDragObject().transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetDragObject().transform.position = new Vector3(GetDragObject().transform.position.x, GetDragObject().transform.position.y, 0);
    }

    private void OnMouseDown() 
    {
        if (!GetInventory().CanUsePowerup(powerUpType))
        {
            return;
        }

        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject dragObject = Instantiate(GetPrefab(), position, Quaternion.identity);
        SetDragObject(dragObject);

        //dragObject.transform.SetParent(GetCanvas().transform, false);
        //dragObject.transform.SetParent(transform);

        SpriteRenderer spriteRenderer = dragObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = icon.GetComponent<SpriteRenderer>().sprite;
        //spriteRenderer.color.a = dragOpacity;
        spriteRenderer.color = new Color(1, 1, 1, dragOpacity);
        dragObject.transform.position = icon.transform.position;
        dragObject.transform.position = new Vector3(dragObject.transform.position.x, dragObject.transform.position.y, 0);
    }

    public void OnSelect()
    {
        SetBorderColor(borderSettings.selectedColor);
    }
}
