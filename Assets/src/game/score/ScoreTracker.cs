﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

  public int leftScore;
  public Text leftText;
  public Goal leftGoal;

  public int rightScore;
  public Text rightText;
  public Goal rightGoal;

  public Text leftLargeText;
  public Text rightLargeText;

  public delegate void GoalCallback();
  public GoalCallback callback;

	// Use this for initialization
	void Start () {
    leftGoal.callback += ScoreLeft;
    rightGoal.callback += ScoreRight;
    leftText.text = "0";
    rightText.text = "0";
    leftLargeText.text = "0";
    rightLargeText.text = "0";
    ToggleSize(Size.Small);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void ScoreLeft() {
    leftScore += 1;
    leftText.text = leftScore.ToString();
    leftLargeText.text = leftScore.ToString();
    // if (leftScore >= 5) {
    //   leftText.text = "Blue Wins!";
    //   leftLargeText.text = "Blue Wins!";
    //   rightText.text = "";
    //   rightLargeText.text = "";
    // }
    PublishGoal();
  }

  void ScoreRight() {
    rightScore += 1;
    rightText.text = rightScore.ToString();
    rightLargeText.text = rightScore.ToString();
    // if (rightScore >= 5) {
    //   leftText.text = "";
    //   leftLargeText.text = "";
    //   rightText.text = "Red Wins!";
    //   rightLargeText.text = "Red Wins!";
    // }
    PublishGoal();
  }

  void PublishGoal() {
    if (callback != null) {
      callback();
    }
  }

  public enum Size {
    None,
    Small,
    Large,
  }

  public void ToggleSize(Size size) {
    leftLargeText.enabled = size == Size.Large;
    rightLargeText.enabled = size == Size.Large;
    leftText.enabled = size == Size.Small;
    rightText.enabled = size == Size.Small;
  }
}
