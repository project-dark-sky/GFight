using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NumberField : MonoBehaviour
{
    private int number;

    public int GetNumber()
    {
        return this.number;
    }

    public void SetNumber(int newNumber)
    {
        this.number = newNumber;
        GetComponent<TextMeshProUGUI>().text = newNumber.ToString();
        Debug.Log("score is " + number + "  " + GetComponent<TextMeshProUGUI>().text);
    }

    public void AddNumber(int toAdd)
    {
        SetNumber(this.number + toAdd);
    }
}
