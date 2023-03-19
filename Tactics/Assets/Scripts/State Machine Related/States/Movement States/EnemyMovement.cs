///Move all this to seperate detect script

using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : EntityMovement {
       public EnemyMovement(GameObject gameObject) : base(gameObject){}
       
       private NavMeshAgent agent;
       
       public override void enter()
       {
              agent = gameObject.GetComponent<NavMeshAgent>();
       }
       public override void Tick(){
 
       }
}