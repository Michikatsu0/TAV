using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidad de movimiento
    public float jumpForce = 7f;  // Fuerza de salto
    public Transform groundCheck; // Punto de verificación de si está tocando el suelo
    public LayerMask groundLayer;  // Capa que representa el suelo
    public float groundDistance = 0.2f; // Distancia para detectar el suelo
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtenemos el Rigidbody del jugador
    }

    void Update()
    {
        // Movimiento
        float horizontal = Input.GetAxis("Horizontal"); // Entrada para la dirección horizontal (A/D o flechas)
        float vertical = Input.GetAxis("Vertical"); // Entrada para la dirección vertical (W/S o flechas)

        Vector3 move = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);

        // Verificación de si estamos en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}

