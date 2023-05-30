using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private int playerLife = 3;
    private Rigidbody2D rb;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        foreach (Image img in hearts) {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < playerLife; i++) {
            hearts[i].sprite = fullHeart;
        }
    }

   private void OnCollisionEnter2D (Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Trap")) {
           Die();
       } 

         if (collision.gameObject.CompareTag("FallDetector")) {
           RestartLevel();
       } 
   }

   private void Die()
   {
       playerLife--;

        if (playerLife <= 0) {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetTrigger("death");
        } else {
            StartCoroutine(GetHurt());
        }
     
   }

   IEnumerator GetHurt() {
       anim.SetLayerWeight(1, 1);
       yield return new WaitForSeconds(2);
       anim.SetLayerWeight(1, 0);
   }

   private void RestartLevel()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
