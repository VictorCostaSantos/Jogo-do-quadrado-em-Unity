using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Inimigo : MonoBehaviour
{

    Vector3 rota;
    public ParticleSystem inimigoquebra;
    private CinemachineImpulseSource cinemachineImpulseSource;
    private Jogador player;
    private void Start()
    {
        var xRotation = Random.Range(90, 180);
        if(xRotation == 0)
        {
            xRotation = 1;
        }
        rota =  new Vector3(xRotation, 0);
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindObjectOfType<Jogador>();   
    }

    private void Update()
    {
        transform.Rotate(rota * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("fruta"))

            Destroy(gameObject);
        Instantiate(inimigoquebra, transform.position, Quaternion.identity);

        if (player != null)
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            var force = 1f / distance;
            cinemachineImpulseSource.GenerateImpulse(force);
        }

        

    }

}
