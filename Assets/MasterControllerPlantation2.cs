using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterControllerPlantation2 : MonoBehaviour
{
    public Animator hortonAnimator;
    public AudioSource hortonAudioSource;
    public Animator alstonAnimator;
    public AudioSource alstonAudioSource;

    public AudioClip HortonWeAreAll;
    public AudioClip HortonCanISeeIt;
    public AudioClip HortonISaidThat;
    public AudioClip AlstonAsBest;
    public AudioClip HortonCanIHave;
    public AudioClip AlstonItsYours;
    public AudioClip AlstonInsteadOf;
    public AudioClip HortonWhyWouldI;
    public AudioClip AlstonThenICould;
    public AudioClip HortonWhyWouldYou;
    public AudioClip AlstonYesIfYou;
    public AudioClip HortonOkay;
    public AudioClip HortonRiseUpMyLove;

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
                        hortonAudioSource.PlayOneShot(HortonWeAreAll);
                        hortonAnimator.SetTrigger("talking");
                    }
                    break;
                }
            case 1: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("talking");
                        alstonAnimator.SetTrigger("idle");
                        hortonAudioSource.PlayOneShot(HortonCanISeeIt);
                        state++;
                    }
                    break;
                }
            case 2: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAudioSource.PlayOneShot(HortonCanIHave);
                        state++;
                    }
                    break;
                }
            case 3: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("idle");
                        alstonAudioSource.PlayOneShot(AlstonItsYours);
                        state++;
                    }
                    break;
                }
            case 4: {
                    if (!alstonAudioSource.isPlaying) {
                        alstonAudioSource.PlayOneShot(AlstonInsteadOf);
                        state++;
                    }
                    break;
                }
            case 5: {
                    if (!alstonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("talking");
                        hortonAudioSource.PlayOneShot(HortonWhyWouldI);
                        state++;
                    }
                    break;
                }
            case 6: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("idle");
                        alstonAudioSource.PlayOneShot(AlstonThenICould);
                        state++;
                    }
                    break;
                }
            case 7: {
                    if (!alstonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("talking");
                        hortonAudioSource.PlayOneShot(HortonWhyWouldYou);
                        state++;
                    }
                    break;
                }
            case 8: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("idle");
                        alstonAudioSource.PlayOneShot(AlstonYesIfYou);
                        state++;
                    }
                    break;
                }
            case 9: {
                    if (!alstonAudioSource.isPlaying) {
                        hortonAnimator.SetTrigger("talking");
                        hortonAudioSource.PlayOneShot(HortonOkay);
                        state++;
                    }
                    break;
                }
            case 10: {
                    if (!hortonAudioSource.isPlaying) {
                        hortonAudioSource.PlayOneShot(HortonRiseUpMyLove);
                        state++;
                    }
                    break;
                }
            case 11: {
                    if (fadeOutDelayTime <= 0) state++;
                    fadeOutDelayTime -= Time.deltaTime;
                    break;
                }
            case 12:
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
            case 13:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
                }
        }

        Debug.Log(state);
    }
}
