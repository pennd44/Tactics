using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : State
{
   public GameObject gameObject;
   public Character character;
   public EntityMovement(GameObject gameObject)
   {
      this.gameObject = gameObject;
      this.character = gameObject.GetComponent<Character>();
   }
    public override void exit()
    {
      character.unitAnimator.SetFloat("Speed", 0);
    }
}
