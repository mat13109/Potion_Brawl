using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUIManager : MonoBehaviour
{


    public SpriteRenderer bg;

    public Sprite sprite1;
    public Sprite sprite2;

    private float min = 0f;
    private float max = 2f;
    private float random;


    [SerializeField] Text p1Text, p2Text;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomBackground();

    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerPrefs.GetInt("P1"))
        {
            case 3:
                p1Text.text = "¤ ¤ ¤";
                break;
            case 2:
                p1Text.text = "¤ ¤";
                break;
            case 1:
                p1Text.text = "¤";
                break;
            case 0:
                p1Text.text = "";
                break;
        }

        switch (PlayerPrefs.GetInt("P2"))
        {
            case 3:
                p2Text.text = "¤ ¤ ¤";
                break;
            case 2:
                p2Text.text = "¤ ¤";
                break;
            case 1:
                p2Text.text = "¤";
                break;
            case 0:
                p2Text.text = "";
                break;
        }

        if (PlayerPrefs.GetInt("P1") == 0 || PlayerPrefs.GetInt("P2") == 0)
        {
            Invoke("LoadVictory", 2.5f);
        }
    }

    void LoadVictory()
    {
        ChooseRandomBackground();
        PlayerPrefs.SetFloat("terrain", Random.Range(min, max));
        SceneManager.LoadScene("Victory");
        

    }

    public void ChooseRandomBackground()
    {
        PlayerPrefs.SetFloat("terrain", Random.Range(min, max));

    }

    public void SetRandomBackground()
    {
        if (PlayerPrefs.GetFloat("terrain") > 1)
        {
            bg.sprite = sprite1;
        }
        else
        {
            bg.sprite = sprite2;
        }
    }


}
