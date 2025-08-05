# 🧟 Eclipse of the Undead 3D

**Eclipse of the Undead 3D** is a **3D zombie survival game** built in Unity.  
It features **AAA-level gameplay mechanics**, including dynamic day-night cycles, scalable zombie waves, AI survivors to rescue, and a final car escape sequence.

---

## 🎮 Features


- ✅ **Wave-based Zombie Spawning** with increasing difficulty  
- ✅ **Rescue AI Survivors** hidden in a mansion  
- ✅ **Car Escape Mechanic** – escort survivors to the car to win  
- ✅ **First-Person Shooter Combat** with shooting, reloading, and melee  
- ✅ **Player Health & Damage System**  
- ✅ **Pause Menu** with Resume, Main Menu, and Quit options  
- ✅ **Main Menu** with Play and Quit options  


---

## 🗂 Project Structure

📁 EclipseOfTheUndead3D
┣ 📁 Assets
┃ ┣ 📁 Scripts
┃ ┃ ┣ GameManager.cs
┃ ┃ ┣ PlayerCombat.cs
┃ ┃ ┣ PlayerHealth.cs
┃ ┃ ┣ UIManager.cs
┃ ┃ ┣ InventorySystem.cs
┃ ┃ ┣ WaveSpawner.cs
┃ ┃ ┣ DayNightCycle.cs
┃ ┃ ┣ CarSystem.cs
┃ ┃ ┣ AISurvivor.cs
┃ ┃ ┣ SurvivorManager.cs
┃ ┃ ┣ GameEnding.cs
┃ ┃ ┣ PauseMenu.cs
┃ ┃ ┣ MainMenu.cs
┃ ┃ ┗ ZombieHealth.cs
┃ ┣ 📁 Prefabs
┃ ┣ 📁 Models
┃ ┣ 📁 Animations
┃ ┣ 📁 Materials
┃ ┣ 📁 Audio
┃ ┗ 📁 UI
┣ 📁 Scenes
┃ ┣ MainMenu.unity
┃ ┗ Game.unity
┗ 📄 ProjectSettings


---

## ▶️ How to Play

1. **Start the game** from the Main Menu.
2. **Survive zombie waves**, rescue survivors hidden in the mansion.
3. **Escort survivors** to the car while fighting off zombies.
4. **Escape with all survivors** to trigger the ending cinematic.

---

## 🕹 Controls

| Action          | Key |
|------------------|-----|
| Move             | W/A/S/D |
| Jump             | Space |
| Shoot            | Left Click |
| Reload           | R |
| Melee Attack     | V |
| Pause Menu       | ESC |

---

## 🛠 How to Install & Run

1. Clone or download the repository:
   ```bash
   git clone https://github.com/YourUsername/EclipseOfTheUndead3D.git

Open the project in Unity (2021.3 or later).

Open the MainMenu.unity scene and click Play.

👨‍💻 Tech Stack
Unity (C#)

NavMesh AI

Particle Effects

AAA Audio & Animation Support


📜 License
This project is for educational purposes.
Feel free to modify and use for learning or portfolio purposes.

markdown
Copy
Edit

---

## 📦 **Final Integration Notes (How to Set Up Everything in Unity)**

1. **Create Scenes:**
   - `MainMenu.unity` → add UI Canvas with Play & Quit buttons, attach `MainMenu.cs`.
   - `Game.unity` → main gameplay scene with player, zombies, car, mansion, etc.

2. **Setup Player:**
   - Add `PlayerCombat.cs`, `PlayerHealth.cs`, and camera to FPS character.
   - Assign references (Animator, Camera, MuzzleFlash, UIManager).

3. **Setup UI:**
   - Create HUD Canvas → Health, Ammo, Score, Wave Count → link in `UIManager.cs`.
   - Create PauseMenu Canvas (disabled by default) → link in `PauseMenu.cs`.

4. **Setup Zombies:**
   - Create zombie prefab → add `ZombieHealth.cs`, animator, audio.
   - Assign this prefab to `WaveSpawner`.

5. **Setup Mansion & Survivors:**
   - Place AI survivors in mansion → add `AISurvivor.cs`, NavMeshAgent, Animator.
   - Add `SurvivorManager` to an empty GameObject.

6. **Setup Car:**
   - Add `CarSystem.cs` to the car object.
   - Assign entry trigger (collider) and link `GameEnding`.

7. **Setup Day/Night Cycle:**
   - Add `DayNightCycle.cs` to the sun light object.
   - Assign skybox materials and light color gradient.

8. **Setup Game Ending:**
   - Create a GameObject with `GameEnding.cs`.
   - Assign Victory UI Canvas and optional cinematic camera.

9. **Link Scripts Together:**
   - Ensure all references in the Inspector are correctly assigned.
   - UIManager → linked in all scripts needing UI updates.

10. **Build & Test:**
    - Press Play → Verify wave spawning, survivor rescue, and car escape flow.

---

Open the project in Unity (2021.3 or later).

Open the MainMenu.unity scene and click Play.

👨‍💻 Tech Stack
Unity (C#)

NavMesh AI

Particle Effects

AAA Audio & Animation Support

🏆 Credits
Developed by [Your Name].
Game Concept, Design, and Programming by You.

📜 License
This project is for educational purposes.
Feel free to modify and use for learning or portfolio purposes.

markdown
Copy
Edit

---

## 📦 **Final Integration Notes (How to Set Up Everything in Unity)**

1. **Create Scenes:**
   - `MainMenu.unity` → add UI Canvas with Play & Quit buttons, attach `MainMenu.cs`.
   - `Game.unity` → main gameplay scene with player, zombies, car, mansion, etc.

2. **Setup Player:**
   - Add `PlayerCombat.cs`, `PlayerHealth.cs`, and camera to FPS character.
   - Assign references (Animator, Camera, MuzzleFlash, UIManager).

3. **Setup UI:**
   - Create HUD Canvas → Health, Ammo, Score, Wave Count → link in `UIManager.cs`.
   - Create PauseMenu Canvas (disabled by default) → link in `PauseMenu.cs`.

4. **Setup Zombies:**
   - Create zombie prefab → add `ZombieHealth.cs`, animator, audio.
   - Assign this prefab to `WaveSpawner`.

5. **Setup Mansion & Survivors:**
   - Place AI survivors in mansion → add `AISurvivor.cs`, NavMeshAgent, Animator.
   - Add `SurvivorManager` to an empty GameObject.

6. **Setup Car:**
   - Add `CarSystem.cs` to the car object.
   - Assign entry trigger (collider) and link `GameEnding`.

7. **Setup Day/Night Cycle:**
   - Add `DayNightCycle.cs` to the sun light object.
   - Assign skybox materials and light color gradient.

8. **Setup Game Ending:**
   - Create a GameObject with `GameEnding.cs`.
   - Assign Victory UI Canvas and optional cinematic camera.

9. **Link Scripts Together:**
   - Ensure all references in the Inspector are correctly assigned.
   - UIManager → linked in all scripts needing UI updates.

10. **Build & Test:**
    - Press Play → Verify wave spawning, survivor rescue, and car escape flow.

---
Open the project in Unity (2021.3 or later).

Open the MainMenu.unity scene and click Play.

👨‍💻 Tech Stack
Unity (C#)

NavMesh AI

Particle Effects

AAA Audio & Animation Support

🏆 Credits
Developed by [Your Name].
Game Concept, Design, and Programming by You.

📜 License
This project is for educational purposes.
Feel free to modify and use for learning or portfolio purposes.

markdown
Copy
Edit

---

## 📦 **Final Integration Notes (How to Set Up Everything in Unity)**

1. **Create Scenes:**
   - `MainMenu.unity` → add UI Canvas with Play & Quit buttons, attach `MainMenu.cs`.
   - `Game.unity` → main gameplay scene with player, zombies, car, mansion, etc.

2. **Setup Player:**
   - Add `PlayerCombat.cs`, `PlayerHealth.cs`, and camera to FPS character.
   - Assign references (Animator, Camera, MuzzleFlash, UIManager).

3. **Setup UI:**
   - Create HUD Canvas → Health, Ammo, Score, Wave Count → link in `UIManager.cs`.
   - Create PauseMenu Canvas (disabled by default) → link in `PauseMenu.cs`.

4. **Setup Zombies:**
   - Create zombie prefab → add `ZombieHealth.cs`, animator, audio.
   - Assign this prefab to `WaveSpawner`.

5. **Setup Mansion & Survivors:**
   - Place AI survivors in mansion → add `AISurvivor.cs`, NavMeshAgent, Animator.
   - Add `SurvivorManager` to an empty GameObject.

6. **Setup Car:**
   - Add `CarSystem.cs` to the car object.
   - Assign entry trigger (collider) and link `GameEnding`.

7. **Setup Day/Night Cycle:**
   - Add `DayNightCycle.cs` to the sun light object.
   - Assign skybox materials and light color gradient.

8. **Setup Game Ending:**
   - Create a GameObject with `GameEnding.cs`.
   - Assign Victory UI Canvas and optional cinematic camera.

9. **Link Scripts Together:**
   - Ensure all references in the Inspector are correctly assigned.
   - UIManager → linked in all scripts needing UI updates.

10. **Build & Test:**
    - Press Play → Verify wave spawning, survivor rescue, and car escape flow.

---

## 📫 Contact


**Author:** [Sahil Choutele](https://github.com/SahilC999)

---
