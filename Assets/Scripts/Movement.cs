using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

  Rigidbody rb;
  AudioSource audioSource;

  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotationThrust = 100f;
  [SerializeField] AudioClip mainEngine;

  [SerializeField] ParticleSystem rocketJetParticles;
  [SerializeField] ParticleSystem leftThrusterParticles;
  [SerializeField] ParticleSystem rightThrusterParticles;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

      if (!audioSource.isPlaying & !rocketJetParticles.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
        rocketJetParticles.Play();
      }
    }
    else
    {
      audioSource.Stop();
      rocketJetParticles.Stop();
    }
  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotationThrust);
      leftThrusterParticles.Play();
    }
    else if (Input.GetKey(KeyCode.D))
    {
      ApplyRotation(-rotationThrust);
      rightThrusterParticles.Play();
    }
    else
    {
      leftThrusterParticles.Stop();
      rightThrusterParticles.Stop();
    }
  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true; // Freeze game rotation physics
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false; // Allow game physics
  }
}
