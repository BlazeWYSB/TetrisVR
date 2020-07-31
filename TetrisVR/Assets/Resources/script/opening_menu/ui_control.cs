using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class opening
{
    public static int option = 0;
    public static int max_option = 3;
}

public class ui_control : MonoBehaviour
{
    public Text[] buttons;
    public RawImage black_screen;
    // Start is called before the first frame update
    IEnumerator fade_out(){
        float temp_alpha = black_screen.GetComponent<RawImage>().color.a;
        int times=2000;
        while(temp_alpha<=1f && (times--) > 0){
            temp_alpha = black_screen.GetComponent<RawImage>().color.a;
            black_screen.GetComponent<RawImage>().color = new Color(0,0,0,temp_alpha + 0.006f);
            yield return 0;
        }
        SceneManager.LoadScene("sence01");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            opening.option = (opening.option + 1) % opening.max_option;
            transform.position = new Vector2(transform.position.x, buttons[opening.option].transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            opening.option = opening.option - 1;
            if (opening.option < 0)
            {
                opening.option = opening.max_option - 1;
            }
            transform.position = new Vector2(transform.position.x, buttons[opening.option].transform.position.y);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            //switch(opening.option):
            if (opening.option == 0)
            {
                StartCoroutine("fade_out");
            }
            if (opening.option == 1)
            {
                ;
            }
            if (opening.option == 2)
            {
                Application.Quit();
            }
        }
    }
}
