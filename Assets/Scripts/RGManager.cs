using UnityEngine;

public class RGManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Animator animator;

    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    private bool isRagdoll = false;

    void Start()
    {
        // Obtenemos todos los componentes de los hijos (huesos)
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Desactivamos el ragdoll al inicio
        SetRagdollState(false);
    }

    void Update()
    {
        // Alternar estados para pruebas
        if (Input.GetKeyDown(KeyCode.R)) SetRagdollState(true);
        if (Input.GetKeyDown(KeyCode.T)) SetRagdollState(false);

    }


    public void SetRagdollState(bool state)
    {
        isRagdoll = state;

        // 1. Desactivar/Activar el Animator
        animator.enabled = !state;

        // 2. Configurar cada hueso
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state;
        }

        // 3. Importante: Los colliders de los huesos deben ignorar al personaje 
        // o desactivarse para no chocar con el Capsule Collider principal
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = state;
        }

        // 4. Desactivar el collider principal si existe para que no flote el Ragdoll
        if (GetComponent<Collider>()) GetComponent<Collider>().enabled = !state;
    }
}
