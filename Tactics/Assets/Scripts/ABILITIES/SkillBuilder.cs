using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillBuilder : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] List<AnimatorOverrideController> animatorOverrides;
    [SerializeField] List<Projectile> projectiles;
    [SerializeField] public int healthCost = 0;
    [SerializeField] public int staminaCost;
    [SerializeField] public int kiCost;

    [SerializeField] List<ActionRange> actionRange;
    [SerializeField] public int rangeHorizontal;
    [SerializeField] public int rangeVertical;
    [SerializeField] List<ActionArea> areaOfEffect;
    [SerializeField] public int AOEHorizontal;
    [SerializeField] public int AOEVertical;
    [SerializeField] List<ActionTargets> targetFilter;
    [SerializeField] List<ActionEffect> effects = new List<ActionEffect>();
    [SerializeField] public int [] effectAmmounts;
    [SerializeField] bool isRightHanded = true;

    void Start()
    {
        
    }
}
