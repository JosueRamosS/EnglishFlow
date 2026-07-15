# EnglishFlow

Aplicación interactiva educativa desarrollada en Unity para practicar inglés en niveles B1-B2, con enfoque en experiencia de usuario (UI/UX): transiciones animadas, feedback visual y sonoro, y un sistema visual coherente en toda la app.

## Tecnología

- **Motor**: Unity 6 (6000.4.5f1)
- **Lenguaje**: C#
- **UI**: Canvas (uGUI) + TextMeshPro
- **Animaciones**: DOTween
- **Input**: Input System (modo "Both")
- **Persistencia**: PlayerPrefs, a través de una clase estática (`GameProgress`)

## Estado de los requisitos funcionales

| # | Requisito | Estado |
|---|---|---|
| R1 | Menú principal con navegación por teclado y mouse | ✅ Completo |
| R2 | Pantalla de configuración (nivel B1/B2, idioma, volumen) | ✅ Completo |
| R3 | Carga asíncrona de escenas con barra de progreso | ✅ Completo |
| R4 | Módulo de Vocabulario (emparejar palabra con definición) | ✅ Completo |
| R5 | Módulo de Gramática (completar oraciones, opción múltiple) | ✅ Completo |
| R6 | Módulo de Comprensión Lectora | ⬜ No implementado |
| R7 | Módulo de Expresión Escrita | ⬜ No implementado |
| R8 | Progreso por módulo (puntaje, intentos, dominio) | 🟡 Motor funcionando y visible al terminar cada módulo; falta la vista consolidada (ver R10) |
| R9 | Transiciones animadas entre pantallas (fade) | ✅ Completo |
| R10 | Pantalla de Resumen (progreso acumulado, racha, recomendación) | 🟡 Lógica implementada en código (`ModuloResumen.cs`); interfaz aún no armada en el editor |

**6 de 10 requisitos completos y funcionando de punta a punta.** R6 y R7 quedaron fuera de alcance por decisión de priorización (profundidad de experiencia en menos módulos, en vez de cobertura superficial de todos).

## Identidad visual

Sistema neobrutalista: paleta plana de colores saturados, bordes negros gruesos, sombras duras sin difuminado, y botones con efecto de "presionado" (la cara del botón se desliza sobre su sombra). Fuente: [Space Grotesk](https://fonts.google.com/specimen/Space+Grotesk).

## Fuentes del contenido educativo

- **Vocabulario**: palabras seleccionadas de *The Oxford 3000 by CEFR level* (Oxford University Press), clasificación pública de vocabulario por nivel del Marco Común Europeo.
- **Gramática**: estructuras seleccionadas de *British Council – EAQUALS Core Inventory for General English* (2ª edición, 2015), inventario público de contenidos gramaticales por nivel CEFR.

Las definiciones y oraciones de ejemplo son redacción propia; los documentos fuente solo aportan las palabras/estructuras y su nivel, no ejercicios.

## Estructura relevante

```
Assets/
  Scenes/        Escenas del juego (Menú, Configuración, Carga, módulos, Resumen)
  Scripts/        Código C#
  Prefabs/        Botones, tarjetas y opciones reutilizables
  Fonts/          Space Grotesk (TMP Font Assets)
```

## Documentación

- `MTD - Videojuegos.pdf`
- `Manual de Usuario - Videojuegos.pdf`
