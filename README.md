 🧟 Zombies Defence Game \ FPS Modular Systems Prototype

📌 Overview
This project is a technical prototype focused strictly on robust systems engineering and clean code architecture. Rather than building a polished vertical slice with extensive UI/menus, this repository serves as a sandbox for implementing advanced Object-Oriented Programming (OOP) principles, procedural animation pipelines, and highly optimized FPS gameplay mechanics inspired by classic wave-based survival shooters.

⚙️ Core Technical Focus

  Clean Architecture & Data-Driven Design:  Game logic is fully decoupled using ScriptableObjects (e.g., `WeaponData`) for system configurations. This ensures high modularity, allowing new weapons and mechanics to be introduced without altering core scripts.

  Procedural Weapon Viewmodel:  Built a complex, multi-layered transform hierarchy to handle concurrent procedural animations (Sway, Walk/Sprint Bobbing, ADS, Kickback) without matrix conflicts. Features include smart raycast-based collision detection that dynamically tilts and displaces the weapon near obstacles, preventing geometry clipping.

  Camera & Visual Fidelity:  Advanced camera manipulation using Cinemachine and DOTween for seamless Aim-Down-Sights (ADS) transitions. Implemented dynamic FOV scaling and procedural noise suppression during ADS to ensure pixel-perfect aiming stability.

  Performance & Optimization:  Designed with performance in mind. Viewmodel oscillations are driven by optimized mathematical functions (`Mathf.Sin`/`Cos`) rather than physical rigidbodies. Uses zero-GC (Garbage Collection) allocation in critical `Update` loops to maintain stable frame rates during heavy zombie hordes.

  Decoupled & Deterministic Input:  Integrated Unity's Input System to drive viewmodel states. Weapon bobbing and sway rely strictly on raw player input vectors rather than physics-based velocity, completely eliminating visual jitter, floating-point drift, and "ghost" movements.
