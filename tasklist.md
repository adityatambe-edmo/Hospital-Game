# 🏥 Hospital Game — Beginner Roadmap

> **What you've built so far:** You have a solid foundation!
> You have the data layer (ScriptableObjects for patients, conditions, wards), a state machine with all the right states, a GameManager with events, a UI_Manager skeleton, and an Economy stub. The architecture is actually well thought out — now it's time to bring it to life.

---

## How to read this list

Each phase builds on the last. Don't skip ahead — finish one phase before moving to the next. Tasks marked 🔑 are the most important ones in that phase.

---

## Phase 1 — Get Something on Screen
*Goal: Open the game and see a UI panel appear. No logic, just visuals.*

- [ ] **1.1** 🔑 Open `SampleScene` and add an empty GameObject. Name it `_Managers`. Under it, add three more empty GameObjects: `GameManager`, `PatientStateMachine`, `UI_Manager`.
  - Attach the matching C# scripts to each one.
  - This is your "command center" for the whole game.

- [ ] **1.2** In the Unity Hierarchy, create a `Canvas` (GameObject → UI → Canvas). Set its **Render Mode** to `Screen Space - Overlay`.

- [ ] **1.3** Inside the Canvas, create **3 Panel** GameObjects:
  - `MainMenuPanel`
  - `WardPanel`
  - `MiniGamePanel`
  - Give each one a different background color so you can tell them apart.

- [ ] **1.4** 🔑 In `UI_Manager.cs`, drag the 3 panels into the matching serialized fields (`mainMenuPanel`, `wardPanel`, `miniGamePanel`) in the Inspector.

- [ ] **1.5** Fix the `hideAll()` method in `UI_Manager.cs` — uncomment or replace the body so it actually hides panels:
  ```csharp
  public void hideAll()
  {
      mainMenuPanel.SetActive(false);
      wardPanel.SetActive(false);
      miniGamePanel.SetActive(false);
  }
  ```

- [ ] **1.6** Update `showUI_Panel()` to accept a `GameObject panel` parameter and show only that panel:
  ```csharp
  public void showUI_Panel(GameObject panel)
  {
      hideAll();
      panel.SetActive(true);
  }
  ```

- [ ] **1.7** In `Start()` of `UI_Manager.cs`, call `showUI_Panel(mainMenuPanel)` so the main menu shows on launch. Hit Play and confirm you see only the main menu panel.

---

## Phase 2 — Wire Up the State Machine
*Goal: Clicking a button changes which panel is visible.*

- [ ] **2.1** 🔑 In `PatientStateMachine.cs`, add a public method to start the game flow from outside:
  ```csharp
  public void StartGame()
  {
      TransistionTo(PatientState.Triage);
  }
  ```

- [ ] **2.2** Add a **Start Game** button to your `MainMenuPanel`. In the Button's `OnClick` event in the Inspector, wire it to call `PatientStateMachine.StartGame()`.

- [ ] **2.3** In `GameManager.cs`, connect the `onDashboardRequest` UnityEvent to `UI_Manager.showUI_Panel(wardPanel)`. Do this in the Inspector by adding a listener to the event.
  - This means: when the state machine enters Triage → GameManager fires the event → UI_Manager shows the ward panel.

- [ ] **2.4** Hit Play, click Start Game, and confirm the UI switches from MainMenu to the Ward panel.

- [ ] **2.5** Add a **"Proceed to Diagnosis"** button on the `WardPanel`. Wire it to call `PatientStateMachine.TransistionTo(PatientState.Diagnostic)` — you'll need to make `TransistionTo` public first (change `void` to `public void`).

---

## Phase 3 — Create Real Patient Data
*Goal: Have a real patient case with symptoms and a correct diagnosis.*

- [ ] **3.1** 🔑 Create your first `MedicalCondition` asset:
  - Right-click in the Project window → Create → Hospital → Medical Condition
  - Fill in: `conditionName`, `conditionDescription`, and `possibleSymptoms`
  - Example: "Appendicitis", "Inflammation of the appendix", "abdominal pain, nausea, fever"

- [ ] **3.2** Create 2–3 more `MedicalCondition` assets. Start simple. You need at least 3 before the diagnosis step feels meaningful.

- [ ] **3.3** 🔑 Create your first `PatientCase` asset:
  - Right-click → Create → Hospital → Patient Case
  - Fill in the patient name, age, symptoms description, and set `correctDiagnosis` to one of your MedicalConditions
  - Set `symptomsKeywords` to a few comma-separated symptom words
  - Set difficulty to 1

- [ ] **3.4** Create 2–3 more patient cases at difficulty 1. These are your starter cases.

- [ ] **3.5** Create a `WardConfig` asset for the ward that treats your first condition:
  - Right-click → Create → Hospital → Ward Config
  - Fill in `wardName`, set `totalBeds` to 4, set `cost` to something reasonable
  - Link this ward to your MedicalCondition's `treatedInWard` field

---

## Phase 4 — Show the Patient
*Goal: The patient's name and symptoms appear on screen when the game starts.*

- [ ] **4.1** On your `WardPanel` Canvas, add Text UI elements:
  - A `Text` (or `TextMeshPro`) for the patient's name
  - A larger `Text` box for the symptoms description

- [ ] **4.2** Create a script `TriageUI.cs` that holds references to these text fields and a `PatientCase`. Add a method `DisplayPatient(PatientCase patientCase)` that sets the text fields from the case data.

- [ ] **4.3** In `PatientController.cs`, add a reference to a `PatientCase` asset (you already have the serialized field). In `Start()`, pass it to `TriageUI.DisplayPatient()`.
  - In the Inspector, drag one of your Patient Case assets into `PatientController.patientCase`.

- [ ] **4.4** 🔑 In `PatientStateMachine.cs`, store a reference to the current `PatientCase` and pass it along when transitioning. The `patientCase` field is already there — just make sure it gets set before the state machine starts.

- [ ] **4.5** Hit Play and confirm you see the patient name and symptoms on the Ward panel.

---

## Phase 5 — The Diagnosis Step
*Goal: The player can pick a diagnosis from a list and get feedback.*

- [ ] **5.1** Add a new panel to the Canvas: `DiagnosisPanel`. Wire up `onDiagnosticRequest` in the GameManager to show this panel.

- [ ] **5.2** On `DiagnosisPanel`, create a scrollable list area. For now, just put 3–4 Buttons, one per MedicalCondition.

- [ ] **5.3** Create a script `DiagnosisUI.cs`. When a diagnosis button is clicked, compare the selected `MedicalCondition` to `patientCase.correctDiagnosis`. Show a "Correct!" or "Wrong, try again" message.

- [ ] **5.4** 🔑 If correct: call `PatientStateMachine.TransistionTo(PatientState.Ward)` to move the patient to the ward.

- [ ] **5.5** Add a score or feedback text so the player knows whether they were right.

---

## Phase 6 — Economy (Money System)
*Goal: The player earns money for treating patients correctly.*

- [ ] **6.1** 🔑 In `Economy.cs`, add basic fields and methods:
  ```csharp
  public static Economy Instance;
  public int currentMoney = 1000;

  void Awake() { Instance = this; }

  public void AddMoney(int amount) { currentMoney += amount; }
  public void SpendMoney(int amount) { currentMoney -= amount; }
  public bool CanAfford(int amount) { return currentMoney >= amount; }
  ```

- [ ] **6.2** Add an Economy script to your `_Managers` GameObject.

- [ ] **6.3** When a patient is successfully treated (`PatientState.Treated`), call `Economy.Instance.AddMoney(100)`.

- [ ] **6.4** Display the current money balance in a Text element in the top corner of the Canvas (visible on all panels).

- [ ] **6.5** When upgrading a ward, check `Economy.CanAfford(wardConfig.cost)` before allowing it.

---

## Phase 7 — A Simple Mini-Game
*Goal: After diagnosis, the player does a simple interactive task to "treat" the patient.*

- [ ] **7.1** Keep it very simple for your first mini-game. Ideas:
  - A "click the correct button 3 times" challenge
  - A "match the symptom to the body part" drag-and-drop
  - A timer-based challenge ("give the right medicine before time runs out")

- [ ] **7.2** Create a new Scene called `MiniGame_Scene` or a dedicated `MiniGamePanel` on your canvas.

- [ ] **7.3** Create a `MiniGameManager.cs` script. It should have:
  - `StartMiniGame(WardConfig ward)` — sets up the game
  - `OnMiniGameSuccess()` — fires when player wins
  - `OnMiniGameFail()` — fires when player loses

- [ ] **7.4** 🔑 On success, call `PatientStateMachine.TransistionTo(PatientState.Treated)`.

- [ ] **7.5** Assign the mini-game prefab in the `WardConfig` asset.

---

## Phase 8 — Polish & Loop
*Goal: The game can be played from start to finish, and then a new patient appears.*

- [ ] **8.1** In `GameManager.cs`, add a `NextPatient()` method that picks a random `PatientCase` from a list and resets the state machine to `Triage`.

- [ ] **8.2** After `PatientState.Treated`, show a results screen (patient name, correct/wrong, money earned) and a "Next Patient" button.

- [ ] **8.3** Add a simple Main Menu with a Play button and the game title.

- [ ] **8.4** Add a Game Over condition — for example, if the player runs out of money or treats 3 patients wrong in a row.

- [ ] **8.5** 🔑 Playtest the full loop 5 times. Fix anything that feels broken or confusing.

---

## Bugs / Known Issues to Fix Now

- [ ] **BUG** `PatientStateMachine.Awake()` calls `GetComponent<GameManager>()` — this only works if `GameManager` is on the **same** GameObject. Either put them on the same object, or use `FindObjectOfType<GameManager>()` instead.

- [ ] **BUG** `UI/..cs` is a file with an invalid name (it has `..` in it). Unity may not compile it properly. Rename the file to `UI_Manager.cs` and make sure it matches the class name inside.

- [ ] **INCOMPLETE** `GameManager.spawnPlayer()` is empty — don't worry about this yet, but remember to come back and implement it in Phase 4.

- [ ] **INCOMPLETE** `PatientStateMachine.TransistionTo()` is never called from anywhere — Phase 2 task 2.2 fixes this.

---

## Quick Reference: What Each Script Does

| Script | Purpose | Status |
|---|---|---|
| `GameManager.cs` | Central hub — fires events when game state changes | 🟡 Skeleton |
| `PatientStateMachine.cs` | Tracks which phase the patient is in | 🟡 Skeleton |
| `Economy.cs` | Tracks money | 🔴 Empty |
| `PatientController.cs` | Spawns and controls the patient object | 🔴 Empty |
| `UI_Manager.cs` | Shows and hides UI panels | 🟡 Skeleton |
| `PatientCase.cs` | Data: who the patient is, what their symptoms are | ✅ Done |
| `MedicalCondition.cs` | Data: what conditions exist, which ward treats them | ✅ Done |
| `WardConfig.cs` | Data: ward settings, cost, beds, mini-game | ✅ Done |

---

*You're building something genuinely interesting — a medical education game with a proper data-driven architecture. Keep going!*
