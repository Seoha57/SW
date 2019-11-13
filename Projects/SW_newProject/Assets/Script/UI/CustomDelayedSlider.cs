using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDelayedSlider : MonoBehaviour
{
  [Header("Child Objects")]
  public GameObject Background;
  public GameObject Fill;
  public GameObject DelayedFill;

  [Header("Slider Numbers")]
  [Range(0.0f, 1.0f)]
  public float value;
  public float MaxValue;
  public float CurrentValue;

  [Header("Delay Settings")]
  public bool UseDelayed;
  public float DelaySpeed;

  //[Header("Size Settings")]
  //public float Width;
  //public float Height;

  //Flags for updating
  bool update;
  bool FillUpdated;
  bool DelayedFillUpdated;

  //Slider Original Positions & Sizes
  RectTransform FillTransform;
  Vector2 FillSize;
  float FillPosition;
  RectTransform DelayedFillTransform;
  Vector2 DelayedFillSize;
  float DelayedFillPosition;

  //Delayed Speed Values;
  float DelayedModifier;

  // Use this for initialization
  void Start()
  {
    //Set Sizes
    //Background.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
    //Fill.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);
    //DelayedFill.GetComponent<RectTransform>().sizeDelta = new Vector2(Width, Height);

    //Grab Transforms
    FillTransform = Fill.GetComponent<RectTransform>();
    DelayedFillTransform = DelayedFill.GetComponent<RectTransform>();

    //Grab Sizes
    FillSize = FillTransform.sizeDelta;
    FillPosition = FillTransform.anchoredPosition.x;

    if (UseDelayed)
    {
      DelayedFill.SetActive(true);
      DelayedFillSize = DelayedFillTransform.sizeDelta;
      DelayedFillPosition = DelayedFillTransform.anchoredPosition.x;

      Debug.Log("DelayedFillPosition: " + DelayedFillPosition);
    }
    else
    {
      DelayedFill.SetActive(false);
    }

    Debug.Log("Initializing Custom Slider");
    FillUpdated = false;
    DelayedFillUpdated = false;
    update = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (update)
    {
      //Calculate the value ratio
      value = CurrentValue / MaxValue;

      //Update fill objects
      UpdateFill(value);
      UpdateDelayedFill(value);

      //Check if fills are done updating
      if (FillUpdated && DelayedFillUpdated)
        update = false;
    }
  }

  void UpdateFill(float val)
  {
    //Regular Fill get's set immediately
    FillTransform.sizeDelta = new Vector2(FillSize.x * val, FillSize.y);
    FillTransform.anchoredPosition = new Vector3(FillPosition * val, 0, 0);

    //FillUpdated = true;
  }

  void UpdateDelayedFill(float val)
  {
    //Using Delayed Check
    if (!UseDelayed)
      return;

    //Only do a delay if going downward
    if (DelayedModifier < 0.0f)
    {
      //Delayed Fill tries to reach set position in some time.
      DelayedFillTransform.sizeDelta = new Vector2(
        DelayedFillTransform.sizeDelta.x + DelayedFillSize.x * DelayedModifier * Time.deltaTime,
        DelayedFillSize.y);
      DelayedFillTransform.anchoredPosition =
        new Vector3(DelayedFillTransform.anchoredPosition.x + DelayedFillPosition * DelayedModifier * Time.deltaTime, 0, 0);

      //Check if fill is complete
      if (DelayedFillTransform.anchoredPosition.x <= DelayedFillPosition * val)
      {
        Debug.Log("Delayed Fill in range");

        //Set to values
        DelayedFillTransform.sizeDelta = DelayedFillSize * val;
        DelayedFillTransform.anchoredPosition = new Vector3(DelayedFillPosition * val, 0, 0);

        //Update Complete
        //DelayedFillUpdated = true;
      }
    }
    else
    {
      //Regular Fill get's set immediately
      DelayedFillTransform.sizeDelta = new Vector2(DelayedFillSize.x * val, DelayedFillSize.y);
      DelayedFillTransform.anchoredPosition = new Vector3(DelayedFillPosition * val, 0, 0);

      //DelayedFillUpdated = true;
    }
  }

  void CalculateDelayFillMod()
  {
    //Using Delayed Check
    if (!UseDelayed)
      return;
    //Current % Value
    float CurrentValue = DelayedFillTransform.anchoredPosition.x / DelayedFillPosition;
    //Set Modifier
    DelayedModifier = value - CurrentValue;

    Debug.Log("Value: " + value);
    Debug.Log("DelayedFillPosition: " + DelayedFillPosition);
    Debug.Log("Current Value: " + CurrentValue);
    Debug.Log("Delay Modifier set to " + DelayedModifier);
  }

  public void SetCurrentValue(float val)
  {
    if (val >= 0.0f)
      CurrentValue = val;
    else
      CurrentValue = 0;

    //Update value
    value = CurrentValue / MaxValue;

    //Set the delay modifier
    CalculateDelayFillMod();

    FillUpdated = false;
    DelayedFillUpdated = false;
    update = true;
  }

  public void SetMaxValue(float val)
  {
    if (val > 0.0f)
      MaxValue = val;
    else
      MaxValue = 1.0f;

    //Update value
    value = CurrentValue / MaxValue;

    FillUpdated = false;
    DelayedFillUpdated = false;
    update = true;
  }
}
