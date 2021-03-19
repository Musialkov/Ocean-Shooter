using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] GameObject[] images;
    PlayerScript player;
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.state == PlayerScript.State.finish)
        {
            for(int i = 0; i<player.score; i++)
            {
                images[i].gameObject.SetActive(true);
            }
        }
    }
}
