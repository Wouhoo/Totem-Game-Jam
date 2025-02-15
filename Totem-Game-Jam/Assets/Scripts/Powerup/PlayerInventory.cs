using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{

    public enum PowerUpType
    {
        SPEED_UP,
        SLOW_DOWN
    }

    [System.Serializable]
    public class PowerUp
    {
        public PowerUpType type;
        public Sprite icon;
        public GameObject prefab;
        public int quantity;
    }


    public List<PowerUp> powerups = new List<PowerUp>();
    public UnityEvent onInventoryChanged; // For UI updates

    public bool CanUsePowerup(PowerUpType type)
    {
        PowerUp powerup = powerups.Find(p => p.type == type);
        return powerup != null && powerup.quantity > 0;
    }

    public void UsePowerup(PowerUpType type)
    {
        PowerUp powerup = powerups.Find(p => p.type == type);
        if (powerup != null && powerup.quantity > 0)
        {
            powerup.quantity--;
            onInventoryChanged.Invoke();
        }
    }



}
