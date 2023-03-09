using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [Header ("Movement")]
    [SerializeField] private float forceRotation;
    [SerializeField] private float forceThrust;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetButton("Jump"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            rigidBody.AddRelativeForce(Vector3.up * forceThrust * Time.deltaTime);
        }
        else
        {
            audioSource.Pause();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * forceRotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * forceRotation * Time.deltaTime);
        }
    }

    void ResetPlayerPosition()
    {
        transform.position = new Vector3(-18.72f, 1.59f, 0.3273762f);
        transform.rotation = Quaternion.identity;
        rigidBody.freezeRotation = true;
        rigidBody.drag = 1000;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            ResetPlayerPosition();
            StartCoroutine(SetDefaultRBValues());
        }
    
    IEnumerator SetDefaultRBValues()
    {
        yield return new WaitForSeconds(1.0f);
        rigidBody.drag = 1;
        rigidBody.freezeRotation = false;
    }
    }

}
