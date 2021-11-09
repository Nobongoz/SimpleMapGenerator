using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValue : MonoBehaviour
{
    public float val;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        setVal();
    }
    public void setVal()
    {
        val = slider.value;
    }
}
