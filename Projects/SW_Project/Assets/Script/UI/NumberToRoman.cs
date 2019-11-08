using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberToRoman : MonoBehaviour
{
  string value;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    value = GetComponent<Text>().text;
    if (!isRomanNumeral(value))
      ConvertToRomanNumeral();
  }

  bool isRomanNumeral(string val)
  {
    if (val == "I" ||
        val == "II" ||
        val == "III" ||
        val == "IV" ||
        val == "V" ||
        val == "VI" ||
        val == "VII" ||
        val == "VIII" ||
        val == "IX" ||
        val == "X" ||
        val == "XI" ||
        val == "XII" ||
        val == "XII" ||
        val == "XIV" ||
        val == "XV" ||
        val == "XVI" ||
        val == "XVII" ||
        val == "XVII" ||
        val == "XVIII" ||
        val == "XIX" ||
        val == "XX")
      return true;
    return false;
  }

  void ConvertToRomanNumeral()
  {
    if (value == "1")
    {
      GetComponent<Text>().text = "I";
    }
    else if (value == "1")
    {
      GetComponent<Text>().text = "I";
    }

    switch(value)
    {
      case "1":
        GetComponent<Text>().text = "I";
        break;
      case "2":
        GetComponent<Text>().text = "II";
        break;
      case "3":
        GetComponent<Text>().text = "III";
        break;
      case "4":
        GetComponent<Text>().text = "IV";
        break;
      case "5":
        GetComponent<Text>().text = "V";
        break;
      case "6":
        GetComponent<Text>().text = "VI";
        break;
      case "7":
        GetComponent<Text>().text = "VII";
        break;
      case "8":
        GetComponent<Text>().text = "VIII";
        break;
      case "9":
        GetComponent<Text>().text = "IX";
        break;
      case "10":
        GetComponent<Text>().text = "X";
        break;
      case "11":
        GetComponent<Text>().text = "XI";
        break;
      case "12":
        GetComponent<Text>().text = "XII";
        break;
      case "13":
        GetComponent<Text>().text = "XIII";
        break;
      case "14":
        GetComponent<Text>().text = "XIV";
        break;
      case "15":
        GetComponent<Text>().text = "XV";
        break;
      case "16":
        GetComponent<Text>().text = "XVI";
        break;
      case "17":
        GetComponent<Text>().text = "XVII";
        break;
      case "18":
        GetComponent<Text>().text = "XVIII";
        break;
      case "19":
        GetComponent<Text>().text = "XIX";
        break;
      case "20":
        GetComponent<Text>().text = "XX";
        break;
      default:
        GetComponent<Text>().text = "I";
        break;
    }



  }
}
