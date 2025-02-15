using System.Linq;
using System.Text;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotsParent;
    private PlayerInventory inventory;

    void Start()
    { 
        inventory = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInventory>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in slotsParent) Destroy(child.gameObject);

        foreach (var powerup in inventory.powerups)
        {
            if (powerup.quantity > 0)
            {
                GameObject slot = Instantiate(slotPrefab, slotsParent);
                slot.GetComponent<PowerupSlotUI>().Setup(powerup.icon, powerup.quantity, powerup.prefab, powerup.type);
            }
        }
    }

   
}
