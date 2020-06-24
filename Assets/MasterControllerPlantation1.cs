using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterControllerPlantation1 : MonoBehaviour
{
    public float startConversationDistance = 5f;

    public Animator hortonAnimator;
    public AudioSource hortonAudioSource;
    public Animator alstonAnimator;
    public AudioSource alstonAudioSource;

    public AudioClip AlstonLetMe;
    public AudioClip HortonOkayIts;
    public AudioClip AlstonImReady;
    public AudioClip HortonRiseUp;

    int state;
    public float fadeOutDelayTime = 1f;
    public Image blackScreen;
    public float blackScreenFadeTime = 2f;

    public GameObject player;
    FirstPersonController playerFPS;
    public GameObject horton;

    // Start is called before the first frame update
    void Start()
    {
        playerFPS = player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distancePlayerPoet = Vector3.Distance(horton.transform.position, player.transform.position);

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
            case 1: {
                    if(distancePlayerPoet <= startConversationDistance) {
                        alstonAnimator.SetTrigger("talking");
                        alstonAudioSource.PlayOneShot(AlstonLetMe);
                        state++;
                    }
                    break;
                }
            case 2: {
                    if(!alstonAudioSource.isPlaying) {
                        alstonAnimator.SetTrigger("idle");
                        hortonAnimator.SetTrigger("talking");
                        hortonAudioSource.PlayOneShot(HortonOkayIts);
                        state++;
                    }
                    break;
                }
            case 3: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("idle");
                        alstonAnimator.SetTrigger("talking");
                        alstonAudioSource.PlayOneShot(AlstonImReady);
                        state++;
                    }
                    break;
                }
            case 4: {
                    if (!alstonAudioSource.isPlaying) {
                        alstonAnimator.SetTrigger("idle");
                        hortonAnimator.SetTrigger("talking");
                        hortonAudioSource.PlayOneShot(HortonRiseUp);
                        state++;
                    }
                    break;
                }
            case 5: {
                    if (fadeOutDelayTime <= 0) state++;
                    fadeOutDelayTime -= Time.deltaTime;
                    break;
                }
            case 6:
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
            case 7:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
                }
        }

        Debug.Log(state);
    }
}
