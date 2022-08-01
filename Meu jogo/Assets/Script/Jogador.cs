using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Jogador : MonoBehaviour
{

    public float forceMultiplier = 3f;
    public ParticleSystem Playerparticle;
    public CinemachineImpulseSource cinemachineImpulseSource;
  
    
  


    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();    
    }       

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        var horizontalInput = Input.GetAxis("Horizontal");
        if (GetComponent<Rigidbody>().velocity.magnitude<= 5f)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }

    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, 187.03f, 0);
        transform.rotation = Quaternion.identity;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("fruta"))
        {
            game();

            Instantiate(Playerparticle, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();

            
        }

        if (collision.gameObject.CompareTag("FallDown"))
        {
            game();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FallDown"))
        {
            game();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("FallDown"))
        {
            game();
        }
    }

    private void game()
    {
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
