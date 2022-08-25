using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : State
{
   public GameObject gameObject;
   public EntityMovement(GameObject gameObject)
   {
        this.gameObject = gameObject;
   }
}
