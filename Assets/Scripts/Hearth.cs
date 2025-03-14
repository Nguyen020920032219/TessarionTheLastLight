using UnityEngine;
using UnityEngine.UI;

public class Hearth : MonoBehaviour
{
    public PlayerController player;
    public Sprite[] spritesHearth;
    

    public Image imageHeart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        imageHeart.sprite = spritesHearth[player.hearth];

    }
}
