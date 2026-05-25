# \# Sistema de Combate y Destrucción en Unity (C#)

# 

# Este repositorio contiene un conjunto de scripts en \*\*C#\*\* listos para usar en \*\*Unity\*\*. Implementan mecánicas clave para un juego de acción o shooter en tercera/primera persona, incluyendo sistemas de disparo por Raycast, inteligencia artificial básica para enemigos, retroalimentación visual y sonora, destrucción de entornos y una interfaz de usuario (GUI) interactiva.

# 

# \---

# 

# \## 🚀 Características Principales

# 

# \* \*\*Disparo Basado en Raycast:\*\* Mecánicas de disparo optimizadas tanto para el jugador como para los enemigos utilizando rayos de físicas en línea recta hacia adelante.

# \* \*\*Retroalimentación Audiovisual (FX):\*\* Sistema dinámico de partículas en el punto de impacto y efectos de sonido en tiempo real al disparar o golpear superficies.

# \* \*\*Inteligencia Artificial de Enemigos:\*\* Enemigos que rastrean al jugador usando `NavMeshAgent`, se aproximan a una distancia segura y realizan ataques mágicos automáticos con enfriamiento (`cooldown`).

# \* \*\*Objetos Destructibles y Feedback de Daño:\*\* Estructuras que pueden acumular daño y cambiar visualmente de material (simulando grietas) antes de destruirse por completo.

# \* \*\*Interfaz de Usuario (GUI) Dinámica:\*\* Barra de progreso (`ProgressBar`) para la salud del jugador y un contador de puntaje que se updates en tiempo real en la pantalla.

# \* \*\*Protección contra Caídas:\*\* Sistema automático para reiniciar la escena si el jugador cae al vacío fuera de los límites del mapa.

# 

# \---

# 

# \## 🛠️ Estructura de los Scripts

# 

# El proyecto se divide en las siguientes áreas funcionales:

# 

# \### 1. Control del Jugador y la GUI

# \* \*\*`Jugador\_control.cs`\*\*: El núcleo de las mecánicas del jugador. Administra la animación de ataque, procesa el daño recibido, conecta los datos de vida con la barra de interfaz y maneja el contador de puntaje. Suma `+2` puntos al eliminar enemigos y `+1` punto al romper objetos.

# \* \*\*`saludJugador.cs`\*\*: Un componente alternativo ligero para la gestión aislada de la salud y eventos de muerte del personaje.

# 

# \### 2. Sistema de Armas y Disparo (Hacia Adelante)

# \* \*\*`DisparoJugador.cs`\*\*: Emite un disparo en línea recta (`transform.forward`). Utiliza `Physics.RaycastAll` para procesar colisiones, ignorando triggers de manera inteligente. Si impacta un objetivo válido, posiciona un sistema de partículas en el punto exacto, orienta la explosión según la normal de la superficie (`Vector3.up`), reproduce sonido y aplica daño.

# \* \*\*`DisparoEnemigo.cs`\*\*: Funciona de forma similar al del jugador pero optimizado para la IA. Utiliza un `LineRenderer` y una `Light` temporal para simular el destello y la trayectoria de un rayo láser de forma visual.

# 

# \### 3. Enemigos e Inteligencia Artificial

# \* \*\*`Enemigo.cs`\*\*: Controla el ciclo de vida y comportamiento del oponente. Persigue al jugador en un mapa de navegación. Al estar a rango de ataque, activa una animación (`isSendingMagic`) e invoca periódicamente el script de disparo adjunto a su proyectil o poder, reproduciendo efectos de audio y animaciones de daño/muerte.

# 

# \### 4. Destrucción de Entornos

# \* \*\*`ObjetoDestruible.cs`\*\*: Script genérico para barriles, cajas o coberturas con puntos de vida fijos que se destruyen al llegar a cero. Requiere que el objeto tenga el Tag \*\*"Destruir"\*\*.

# \* \*\*`Contador.cs`\*\*: Un script de destrucción especializado por golpes. Al recibir impactos cambia la propiedad `rend.material` por un material agrietado (`damagedMaterial`) para dar feedback visual antes de desaparecer de la escena.

# 

# \---

# 

# \## 🔧 Configuración en Unity (Setup)

# 

# Para que el sistema funcione correctamente en tu escena de Unity, asegúrate de realizar los siguientes pasos de asignación en el \*\*Inspector\*\*:

# 

# \### Configuración de Capas y Etiquetas (Tags \& Layers)

# 1\. Crea una capa llamada \*\*`Shootable`\*\* en tu proyecto.

# 2\. Los objetos del entorno que desees que interactúen o blokeen los rayos láser deben estar asignados a esta capa.

# 3\. Asegúrate de colocarle la etiqueta (Tag) \*\*`Player`\*\* al GameObject del jugador para que la IA enemiga pueda rastrear su posición automáticamente mediante código.

# 4\. Coloca la etiqueta (Tag) \*\*`Destruir`\*\* a los objetos con el script `ObjetoDestruible.cs`.

# 

# \### Configuración del Jugador (`Jugador\_control`)

# \* Arrastra un GameObject vacío con los componentes `LineRenderer`, `Light` y el script `DisparoJugador.cs` al espacio de \*\*Poder\*\*.

# \* Asigna tu componente de UI de texto al campo \*\*Texto Contador\*\*.

# \* Asigna tu Slider o script de interfaz al campo \*\*Pb\*\* (ProgressBar).

# 

# \### Configuración del Enemigo (`Enemigo`)

# \* Asegúrate de que tu escenario tiene un mapa horneado (`NavMesh`) y que el enemigo cuenta con el componente \*\*NavMeshAgent\*\*.

# \* Asigna un objeto hijo con el script `DisparoEnemigo.cs` al campo \*\*Poder\*\*.

# \* Asigna los componentes `AudioSource` correspondientes para los sonidos de \*\*Laser\*\* y \*\*Muerte\*\*.

# 

# \---

# 

# \## 📋 Requisitos del Sistema

# \* \*\*Unity\*\* 2020.3 LTS o superior.

# \* Componente \*\*Navigation\*\* instalado en el proyecto (para el `NavMeshAgent` del enemigo).

# \* Un paquete básico de componentes de interfaz (como \*UI Text\* o \*TextMeshPro\* adaptado) para el renderizado del puntaje.

