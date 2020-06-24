using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterControllerGazebo : MonoBehaviour
{
    int state;
    public Image blackScreen;
    public float blackScreenFadeTime = 2f;

    public GameObject player;
    FirstPersonController playerFPS;

    // Start is called before the first frame update
    void Start()
    {
        playerFPS = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        // float distancePlayerPoet = Vector3.Distance(horton.transform.position, player.transform.position);

        switch (state) {
            // Fade in slowly
            case 0: {
                    var temp = blackScreen.color;
                    temp.a -= (1 / blackScreenFadeTime) * Time.deltaTime;
                    blackScreen.color = temp;
                    playerFPS.canGetInput = true;
                    playerFPS.canLook = true;

                    if (temp.a <= 0) {
                        state++;
                    }
                    break;
                }
        }
        Debug.Log(state);
    }
}
