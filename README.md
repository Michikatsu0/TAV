# TAV Taller de Animación para Videojuegos
En este repositorio se trabaja la implementacion de mecanicas animadas de juegos AAA en tercera persona, se trabaja con el equipo para consolidar las animaciones principales de los personajes que se podrán encontrar aquí como las mecanicas basicas de un juego AAA de forma simultanea y en conjunto, (como programador se trabaja con el paquete Animation Rigging Package (ARP) para implementar mecanicas IK's interesantes de apuntado de cualquier tipo de arma sin usar mixamo)

- Todas las animaciones de movimientos basicos son de la autoria de mis compañeros Tomas Alvares y Andres Arango Velez.

- La animacion del arma llevandose a la espalda y de apuntado es solamente de mi autoria Sebastian Segovia Medina y es un ejemplo del potencial que tiene el Paquete ARP para hacer mecanicas con IK's


Actualización bugs referentes con las animaciones y maquinas de estados.

![image](https://github.com/Michikatsu0/TAV/assets/68073260/e27b6ba0-fb3d-4da6-afdd-bfad952cdeb0)

Controles: 
- Movimiento: W. A. S. D.
- Correr: Left Shift + Movimiento.
- Saltar: Spacebar.
- idleBreaker: Espere 10 segudos en idle.
  
Camaras: Acerquese a los focos de luz de la otra sala del mapa, estas irán cambiando dependiendo de su zona de control.

IKs:
- Apuntado: Clic Izquierdo
  
![image](https://github.com/Michikatsu0/TAV/assets/68073260/f17fd8da-06ed-4b6d-a153-5631cbdf78c7)

- Disparo: Clic Derecho

![image](https://github.com/Michikatsu0/TAV/assets/68073260/c05d2b6f-4230-4219-9f88-4c9dd8f21b5a)

Extras:
- Instances Materials: instancias de materiales que se pueden cambiar en tiempo real, por algunos segundos o de manera instantanea al clic. (normalmente eso cuesta dinero en el assets store y soluciona el problema de tener que crear muchos materiales para diferentes personajes u objetos en un juego grande y asi optimizar el peso total de la producción).

- Sistema de Multi-Position y Multi-Aim de Armas, cada una con su personalidad.

- Sistema de cambio de arma Multi-Parent basado en PickUps

- Armas

# !!! FINALL !!!! 🧑‍🔧

Actualizacion IKs: 
- Apuntado configurado con zoom y un mejor cinemachine.

![image](https://github.com/Michikatsu0/TAV/assets/68073260/5ecc1e2c-4605-4ad1-aa10-7227fdd6bc99)

- Enemigo añadido, vida, hitboxes + animhit y UI.

![image](https://github.com/Michikatsu0/TAV/assets/68073260/b285d52d-6e54-4737-b908-1740d36c0b09)

## Autoevaluacion y Extras del proyecto

- Se cumple con dos personajes rigegeados por el equipo
- Se cumple con un set completo de animaciones necesarias para representar las mecanicas acciones e interacciones de los personajes
- Se cumple con que estas animaciones se integran utilizando maquinas de estados, o en algunas cosas para algunas animaciones se utilizan cajas negras para leer estos datos y reaacion sin necesidad de tomar en cuenta los otros o algunos de ellos.
- Se cumple con el uso de cinemachine para implementar una camara para personajes en tercera persona equipado con un zoom de apuntado estilo GTA V, tambien cuenta con la opcion de las transiciones de caaras virtuales en la escena cuando se entra en el rango del collider que encierra el respectivo foco de luz.
- Se cumple con la interactividad de los personajes al 100% mostrando cada uno de sus estados de animaciones posibles (en la presentacion olvide mostrar el idle break que se activa con el tiempo).

En cuanto a Animaciones

- Se logra captar el concepto de movimiento siendo catalogado como bueno, por lo que deberia y esta abierto a mejoras en cuanto a fluides y tematica de personaje por el lado de algunas animaciones de apuntado y algunas hacia los lados del personaje main. por otro lado creo que las demas animaciones cumplen de manera optima y no es necesario sujetarlas a mejoras aunque si podrian hacerce.

Extras

- Implementacion de sonido de balas y al disparar la bala del muzzle y al rebotar en una superficie de manera eficiente y auditivamente acorde a la distacia del choque del impacto de la bala asi que si disparas cerca se escucha fuerte al maximo pero si dispara lejos se escuchará pero en menor grado segun un valor clampeado basado en la distancia del impacto de la bala y el player.
- Implementacion de sistema de armas basado de IKs para apuntado y cambio de arma.
