using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    //修改数值操作
    public delegate void SetNumberTxt(KeyType keyType, string value);

    public class UIManager : MonoBehaviour
    {
        private bool _isReset;
        private bool _isResetNumberTxtA = true;
        private const int NumberMax = 21;
        private double _numberA;
        private double _numberB;
        private Calculate _operate;

        //显示
        private Text _numberTxtA;
        private Text _numberTxtB;

        public static SetNumberTxt setNumber;

        private void Start()
        {
            _numberTxtA = transform.Find("number/numberA").GetComponent<Text>();
            _numberTxtB = transform.Find("number/numberB").GetComponent<Text>();
            _numberTxtA.text = "0";
            _numberTxtB.text = string.Empty;

            setNumber += SetNumber;
        }

        //修改显示数字
        private void SetNumber(KeyType keyType, string value)
        {
            switch (keyType)
            {
                case KeyType.Number:
                    if (_isResetNumberTxtA)
                    {
                        _numberTxtA.text = value;
                    }
                    else
                    {
                        //判断是否超出界限
                        if (_numberTxtA.text.Length > NumberMax)
                            return;
                        _numberTxtA.text += value;
                    }

                    _numberA = double.Parse(_numberTxtA.text);
                    _isResetNumberTxtA = false;
                    break;
                case KeyType.Compute:
                    _isResetNumberTxtA = true;
                    _operate = CalculateFactory.CreatOperation(value);
                    _numberB = _numberA;
                    _numberTxtB.text = SetValueRichText(_numberTxtA.text + value);
                    break;
                case KeyType.Operate:
                    switch (value)
                    {
                        case "C":
                            _isResetNumberTxtA = true;
                            _numberTxtB.text = string.Empty;
                            _numberTxtA.text = "0";
                            _numberA = _numberB = 0;
                            break;
                        case "CE":
                            _isResetNumberTxtA = true;
                            _numberTxtA.text = "0";
                            _numberA = 0;
                            break;
                        case "back":
                            if (!_isReset)
                            {
                                if (_numberTxtA.text.Length <= 1)
                                {
                                    _numberTxtA.text = "0";
                                    _numberA = 0;
                                }
                                else
                                {
                                    // _numberTxtA.text = _numberTxtA.text.Substring(0, _numberTxtA.text.Length - 1);
                                    _numberTxtA.text = _numberTxtA.text[..^1];
                                    _numberA = double.Parse(_numberTxtA.text);
                                }
                            }
                            else
                            {
                                _isResetNumberTxtA = true;
                                _numberTxtB.text = string.Empty;
                                _numberTxtA.text = "0";
                                _numberA = _numberB = 0;
                            }

                            break;
                    }

                    break;
                case KeyType.Result:
                    _isResetNumberTxtA = true;
                    switch (value)
                    {
                        case "X²":
                            _operate = CalculateFactory.CreatOperation(value);
                            _operate.NumberA = _operate.NumberB = double.Parse(_numberTxtA.text);
                            _numberTxtB.text = SetValueRichText("sqr(" + _numberTxtA.text + ")");
                            _numberTxtA.text = _operate.GetResultValue().ToString();
                            break;
                        case "=":
                            _operate.NumberA = _numberB;
                            _operate.NumberB = _numberA;
                            _numberTxtB.text = SetValueRichText(_numberTxtB.text + _numberTxtA.text);
                            _numberTxtA.text = _operate.GetResultValue().ToString();
                            if (_numberTxtA.text.Length > NumberMax)
                                _numberTxtA.text = _numberTxtA.text.Substring(0, NumberMax);
                            _numberA = double.Parse(_numberTxtA.text);
                            //Debug.Log(oper.NumberA);
                            //Debug.Log(oper.NumberB);
                            break;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }

            //判断计算结果
            // if (value=="="|| value == "X²")
            if (value is "=" or "X²")
            {
                _isReset = _numberTxtA.text.Contains("E+") || _numberTxtA.text.Equals("Infinity");
            }
        }

        //调整富文本值
        private static string SetValueRichText(string value)
        {
            string richText = "<color=#808080>" + value + "</color>";
            return richText;
        }
    }
}