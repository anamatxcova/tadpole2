using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setBar(int food)
    {
        slider.value = food;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
