using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterControllerCotton : MonoBehaviour
{
    public float startPoemDistance = 10f;
    public AudioSource hummingPoemAudioSource;
    public AudioClip poemAudio;
    
    int state;
    public Image blackScreen;
    public float blackScreenFadeTime = 2f;

    public GameObject player;
    FirstPersonController playerFPS;

    public AudioSource workingSoundSource;
    public AudioClip workingSoundAudio;
    public float workingSoundDelay = 2f;
    private float workingSoundTimer;

    public float hummingEndDelay = 2f;

    public GameObject horton;

    // Start is called before the first frame update
    void Start()
    {
        playerFPS = player.GetComponent<FirstPersonController>();
        workingSoundTimer = workingSoundDelay;
    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayerPoet = Vector3.Distance(horton.transform.position, player.transform.position);

        if(workingSoundTimer <= 0) {
            workingSoundSource.PlayOneShot(workingSoundAudio);
            workingSoundTimer = workingSoundDelay;
        }
        workingSoundTimer -= Time.deltaTime;

        switch (state)
        {
            // When intro audio is done, fade in slowly
            case 0:
                {
                    var temp = blackScreen.color;
                    temp.a -= (1 / blackScreenFadeTime) * Time.deltaTime;
                    blackScreen.color = temp;
                    playerFPS.canGetInput = true;
                    playerFPS.canLook = true;

                    if (temp.a <= 0)
                    {
                        state++;
                    }
                    break;
                }
            case 1: {
                    if (distancePlayerPoet <= startPoemDistance) {
                        hummingPoemAudioSource.Stop();
                        hummingPoemAudioSource.clip = poemAudio;
                        hummingPoemAudioSource.Play();
                        state++;
                    }
                    break;
                }
            case 2: {
                    if(hummingEndDelay <= 0) {
                        state++;
                    }
                    hummingEndDelay -= Time.deltaTime;
                }
                break;
            case 3: { // Wait for poem to end
                    if(!hummingPoemAudioSource.isPlaying) {
                        state++;
                    }
                    break;
                }
            case 4:
                {
                    var temp = blackScreen.color;
                    temp.a += (1 / blackScreenFadeTime) * Time.deltaTime;
                    blackScreen.color = temp;

                    if (temp.a >= 1)
                    {
                        state++;
                    }
                    break;
                }
            case 5:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
                }
        }

        Debug.Log(state);
    }
}
