using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class MasterController : MonoBehaviour
{
    public GameObject horton;
    Animator hortonAnimator;

    public GameObject player;
    Rigidbody playerRigidBody;
    FirstPersonController playerFPS;

    public float delayUntilNextScene = 5f;
    

    public float poemBeginDistance;
    public float secondIntroBeginDistance;

    public AudioSource IBecameVeryFond;
    public AudioSource OneSabbath;
    public AudioSource Poem;
    public AudioSource HymnAudio;
    public AudioSource AmbienceAudio;

    public Image blackScreen;
    public float blackScreenFadeTime = 2f;

    private int state = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = player.GetComponent<Rigidbody>();
        playerFPS = player.GetComponent<FirstPersonController>();

        hortonAnimator = horton.GetComponent<Animator>();

        playerFPS.canGetInput = false;
        playerFPS.canLook = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayerPoet = Vector3.Distance(horton.transform.position, playerRigidBody.transform.position);
        Debug.Log("Player/poet distance: " + distancePlayerPoet);
        Debug.Log("State: " + state);
        // When within range, play audio, taking away player movement

        switch(state)
        {
            // When intro audio is done, fade in slowly
            case 0:
                {
                    if (!IBecameVeryFond.isPlaying)
                    {
                        var temp = blackScreen.color;
                        temp.a -= (1 / blackScreenFadeTime) * Time.deltaTime;
                        blackScreen.color = temp;
                        playerFPS.canGetInput = true;
                        playerFPS.canLook = true;

                        if(temp.a <= 0)
                        {
                            state++;
                        }
                    }
                    break;
                }
            // Start second intro, freeze look
            case 1:
                {
                    Debug.Log("distancePlayerPoet < secondIntroBeginDistance: " + (distancePlayerPoet < secondIntroBeginDistance));
                    if (distancePlayerPoet < secondIntroBeginDistance)
                    {
                        OneSabbath.Play();
                        playerFPS.canGetInput = false;
                        state++;
                    }
                    break;
                }
            // Wait until end of second intro, then return movement power and advance
            case 2:
                {
                    if(!OneSabbath.isPlaying)
                    {
                        playerFPS.canGetInput = true;
                        state++;
                    }
                    break;
                }
            // When in range, Horton stands
            case 3:
                {
                    if(distancePlayerPoet < poemBeginDistance)
                    {
                        hortonAnimator.SetTrigger("StandUp");
                        state++;
                    }
                    break;
                }
            // Wait until Horton stands, then play poem
            case 4:
                {
                    if(!hortonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Standing Up"))
                    {
                        Poem.Play();
                        state++;
                    }
                    break;
                }
            // When poem is over, notify animator to go to idle state
            case 5:
                {
                    if(!Poem.isPlaying)
                    {
                        hortonAnimator.SetTrigger("PoemOver");
                        state++;
                    }
                    break;
                }
            // Wait delayUntilNextScene seconds before beginning to fade out
            case 6:
                {
                    if (delayUntilNextScene > 0)
                    {
                        delayUntilNextScene -= Time.deltaTime;
                    }
                    else
                    {
                        state++;
                    }
                    break;
                }
            // Fade in black screen, wait for fade to complete
            // Fade out hymn and ambience audio
            case 7:
                {
                    var temp = blackScreen.color;
                    temp.a += (1 / blackScreenFadeTime) * Time.deltaTime;
                    blackScreen.color = temp;

                    float volume = HymnAudio.volume;
                    volume -= (1 / blackScreenFadeTime) * Time.deltaTime;
                    HymnAudio.volume = volume;

                    float volume2 = AmbienceAudio.volume;
                    volume2 -= (1 / blackScreenFadeTime) * Time.deltaTime;
                    AmbienceAudio.volume = volume2;

                    if (temp.a >= 1)
                    {
                        state++;
                    }
                    break;
                }
            // Switch to new scene
            case 8:
                {
                    SceneManager.LoadScene("Cotton");
                }
                break;
        }
    }
}
