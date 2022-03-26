using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    [Header("Player Character detection")]
    [SerializeField] private GameObject player;
    [Tooltip("Object will turn on normal material when player is within this distance")]
    [SerializeField] private float playerDetectionDistance = 3;

    [Header("Hint Bool")]
    public bool playerNeedsHint;

    [Header("Materials")]
    public Material normalMaterial;
    public Material hintMaterial;
    private MeshRenderer thisRenderer;

    private float distance;

    //NESSIE-AUDIO
    public AudioSource audioSource;

    private void Start()
    {
        thisRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (player == null) { try { player = GameObject.Find("Player").gameObject; } catch { Debug.LogError(name + "object named Player not found in hierarchy"); } }
    }

    private void Update()
    {
        if(playerNeedsHint)
        {
            thisRenderer.material = hintMaterial;
            distance = Vector3.Distance(transform.position, player.transform.position);
            audioSource.Play();
            

            if(distance < playerDetectionDistance)
            {
                playerNeedsHint = false;
                audioSource.Stop();
            }
        }

        else
        {
            thisRenderer.material = normalMaterial;
            audioSource.Stop();
        }
    }
}
