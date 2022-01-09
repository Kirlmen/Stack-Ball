using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{

    private Rigidbody rb;
    private float _currentTime;
    private bool _invincible;

    private bool _smash;

    public enum PlayerState
    {
        Prepared,
        Playing,
        Died,
        Finished
    }

    [HideInInspector] public PlayerState playerState = PlayerState.Prepared;
    public AudioClip bounceOffClip, deadClip, winClip, destroyClip, invDestroyClip;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        #region Playing
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _smash = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _smash = false;
            }
            if (_invincible)
            {
                _currentTime -= Time.deltaTime * .35f;
            }
            else
            {
                if (_smash)
                {
                    _currentTime += Time.deltaTime * .8f;
                }
                else
                {
                    _currentTime -= Time.deltaTime * .5f;
                }
            }
            //ui check

            if (_currentTime >= 1)
            {
                _currentTime = 1;
                _invincible = true;
                Debug.Log("Done");
            }
            else if (_currentTime <= 0)
            {
                _currentTime = 0;
                _invincible = false;
            }
        }
        #endregion

        if (playerState == PlayerState.Prepared)
        {
            //tap to start
            if (Input.GetMouseButtonDown(0))
                playerState = PlayerState.Playing;
        }
        if (playerState == PlayerState.Finished)
        {
            //tap to start
            if (Input.GetMouseButtonDown(0))
                FindObjectOfType<LevelSpawner>().NextLevel();
        }

        Debug.LogWarning(playerState);
    }


    private void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButton(0))
            {
                _smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }

        if (rb.velocity.y > 5)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }

    public void IncreaseBrokenStacks() //add score 1
    {
        if (!_invincible)
        {
            ScoreManager.Instance.AddScore(1);
            SoundManager.Instance.PlaySoundFX(destroyClip, 0.3f);
        }
        else
        {
            ScoreManager.Instance.AddScore(4);
            SoundManager.Instance.PlaySoundFX(invDestroyClip, 0.3f);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!_smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.Instance.PlaySoundFX(bounceOffClip, 0.1f);
        }
        else
        {
            if (_invincible)
            {
                if (other.gameObject.tag == "enemy" || other.gameObject.tag == "plane")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {
                if (other.gameObject.tag == "enemy")
                {
                    other.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
                if (other.gameObject.tag == "plane")
                {
                    Debug.Log("GameOver");
                    ScoreManager.Instance.ResetScore();
                    SoundManager.Instance.PlaySoundFX(deadClip, 0.5f);
                }
            }

        }

        if (other.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finished;
            SoundManager.Instance.PlaySoundFX(winClip, 0.3f);
        }




    }

    private void OnCollisionStay(Collision other)
    {
        if (!_smash || other.gameObject.tag == "Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }


}

