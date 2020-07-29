using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opening
{
    public static int option = 0;
    public static int max_option = 3;
}

public class ui_control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            opening.option = (opening.option + 1) % opening.max_option;
            transform.position = new Vector2(transform.position.x, 440f - opening.option * 125);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            opening.option = opening.option - 1;
            transform.position = new Vector2(transform.position.x, 440f - opening.option * 125);
            if (opening.option < 0)
            {
                opening.option = opening.max_option - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            //switch(opening.option):
            if (opening.option == 0)
            {
                SceneManager.LoadScene("sence01");
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
