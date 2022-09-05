using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StrategyManager : MonoBehaviour
{
    public InputField price;
    public InputField number;
    public Button enterBtn;
    public Button resetBtn;

    public Dropdown discountType;

    void Start()
    {
        InitDropdown();
    }

    void Update()
    {
        
    }

    void InitDropdown() 
    {
        discountType.options.Clear();
        List<Dropdown.OptionData> dropdownList = new List<Dropdown.OptionData>() 
        {
            new Dropdown.OptionData("正常收费"),
            new Dropdown.OptionData("满300返100"),
            new Dropdown.OptionData("打8折")
        };
        discountType.AddOptions(dropdownList);
    }
}
