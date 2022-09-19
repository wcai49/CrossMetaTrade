using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class characterSelection : MonoBehaviour
{
  Animator char_animator;

  private void Start()
  {
    char_animator = gameObject.GetComponentInChildren<Animator>();
  }

  public void lobbyActivated()
  {
    char_animator.SetBool("isHover", true);
  }
  public void lobbyDeactivated()
  {
    char_animator.SetBool("isHover", false);
  }
}
