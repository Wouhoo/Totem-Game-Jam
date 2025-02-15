using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text quantityText;

    public void Setup(Sprite iconSprite, int quantity)
    {
        icon.sprite = iconSprite;
        quantityText.text = ToRomanNumeral(quantity);
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
}
