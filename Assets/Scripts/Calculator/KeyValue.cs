using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    public enum KeyType 
    {
        Number,
        Compute,
        Operate,
        Result
    }
    public class KeyValue : MonoBehaviour
    {
        private string Value { get; set; } = string.Empty;

        public KeyType keyType = KeyType.Number;
        Button Btn;

        void Start()
        {
            Value = transform.GetChild(0).GetComponent<Text>().text;

            Btn = GetComponent<Button>();
            Btn.onClick.AddListener(delegate { UIManager.setNumber(keyType, Value); });
        }


    }
}