using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int score = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private AudioSource collectSound;

    private void Start()
    {
        scoreText.text = "Score:" + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
            score++;
            scoreText.text = "Score:" + score;
            collectSound.Play();
        }
    }
}
