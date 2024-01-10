using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static DamagePopup Create(Vector3 position, int ammount, bool isCritical){
         Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);   
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(ammount);
        return damagePopup;
    }
    
    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textColor;
    float moveSpeed = 10f;
    float dissapearSpeed = 3f;

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int amount){
        textMesh.SetText(amount.ToString());
        textColor = textMesh.color;
    }
    private void Update() {
        transform.position += new Vector3(0, moveSpeed) * Time.deltaTime;
        dissapearTimer -= Time.deltaTime;
        if(dissapearTimer < 0){
            textColor.a -=  dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }
}
