using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum CombatStatus
{
    WAITING_FOR_FIGHTER,
    FIGHTER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN,
    CHECK_FIGHTER_STATUS_CONDITION
}

public class CombatManager : MonoBehaviour
{
    public PuzzleData battle;
    public GameObject transition;
    public Result result;
    public AudioClip correct;
    public AudioClip error;

    public Fighter[] playerTeam;
    public Fighter[] enemyTeam;
    public Fighter[] finalTeam;
    public bool initFinalBattle;

    private Fighter[] fighters;
    private int fighterIndex;

    private bool isCombatActive;

    private CombatStatus combatStatus;

    private Skill currentFighterSkill;

    private List<Fighter> returnBuffer;
    public AudioClip finishCombat;
    public GameObject puzzleMusic;
    public int idPuzzle;
    public int idScene;

    void Start()
    {
        Cursor.visible = true;
        this.returnBuffer = new List<Fighter>();

        this.fighters = GameObject.FindObjectsOfType<Fighter>();

        this.SortFightersBySpeed();
        this.MakeTeams();
        this.RemoveFighters();

        LogPanel.Write("Battle initiated.");

        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
    }

    private void RemoveFighters()
    {
        List<Fighter> fightersAux = new List<Fighter>();
        foreach (Fighter f in fighters)
        {
            if (f.GetComponent<EnemyBody>() == null)
            {
                fightersAux.Add(f);
            }
        }
        fighters = fightersAux.ToArray();
    }
    private void SortFightersBySpeed()
    {
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;

            for (int i = 0; i < this.fighters.Length - 1; i++)
            {
                Fighter a = this.fighters[i];
                Fighter b = this.fighters[i + 1];

                float aSpeed = a.GetCurrentStats().speed;
                float bSpeed = b.GetCurrentStats().speed;

                if (bSpeed > aSpeed)
                {
                    this.fighters[i] = b;
                    this.fighters[i + 1] = a;

                    sorted = false;
                }
            }
        }
    }

    private void MakeTeams()
    {
        List<Fighter> playersBuffer = new List<Fighter>();
        List<Fighter> enemiesBuffer = new List<Fighter>();
        List<Fighter> finalBuffer = new List<Fighter>();

        foreach (var fgtr in this.fighters)
        {
            if (fgtr.team == Team.PLAYERS)
            {
                playersBuffer.Add(fgtr);
            }
            else if (fgtr.team == Team.ENEMIES)
            {
                enemiesBuffer.Add(fgtr);
            }else if (fgtr.team == Team.FINAL_ENEMIE)
            {
                finalBuffer.Add(fgtr);
            }

            fgtr.combatManager = this;
        }

        this.playerTeam = playersBuffer.ToArray();
        this.enemyTeam = enemiesBuffer.ToArray();
        this.finalTeam = finalBuffer.ToArray();
    }

    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break;

                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Write($"{this.fighters[this.fighterIndex].idName} usó {currentFighterSkill.skillName}.");

                    yield return null;

                    // Executing fighter skill
                    currentFighterSkill.Run();
                    // Wait for fighter skill animation
                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;

                    break;
                case CombatStatus.CHECK_ACTION_MESSAGES:
                    string nextMessage = this.currentFighterSkill.GetNextMessage();

                    if (nextMessage != null)
                    {
                        LogPanel.Write(nextMessage);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        this.currentFighterSkill = null;
                        this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                        yield return null;
                    }
                    break;

                case CombatStatus.CHECK_FOR_VICTORY:
                    bool arePlayersAlive = false;
                    foreach (var figther in this.playerTeam)
                    {
                        arePlayersAlive |= figther.isAlive;
                    }

                    bool areEnemiesAlive = false;
                    foreach (var figther in this.enemyTeam)
                    {
                        areEnemiesAlive |= figther.isAlive;
                    }

                    this.initFinalBattle = !areEnemiesAlive;
                    bool defeat = arePlayersAlive == false;
                    bool victory = !finalTeam[0].isAlive;

                    if (victory)
                    {
                        LogPanel.Write("Victoria!");
                        this.isCombatActive = false;
                        battle.finished = true;
                        LoadResult();
                        puzzleMusic.gameObject.SetActive(false);
                        SoundManager.instance.playSound(finishCombat,0.8f);

                        transition.GetComponent<SceneController>().SolvePuzzle(correct, battle.finished);
                    }

                    if (defeat)
                    {
                        puzzleMusic.gameObject.SetActive(false);
                        LogPanel.Write("Derrota!");
                        this.isCombatActive = false;
                        battle.finished = false;
                        transition.GetComponent<SceneController>().SolvePuzzle(error, battle.finished);
                    }

                    if (this.isCombatActive)
                    {
                        this.combatStatus = CombatStatus.NEXT_TURN;
                    }

                    yield return null;
                    break;
                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);

                    Fighter current = null;

                    do
                    {
                        this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;

                        current = this.fighters[this.fighterIndex];
                    } while (current.isAlive == false);

                    this.combatStatus = CombatStatus.CHECK_FIGHTER_STATUS_CONDITION;

                    break;

                case CombatStatus.CHECK_FIGHTER_STATUS_CONDITION:

                    var currentFighter = this.fighters[fighterIndex];

                    var statusCondition = currentFighter.GetCurrentStatusCondition();

                    if (statusCondition != null)
                    {
                        statusCondition.Apply();

                        while (true)
                        {
                            string nextSCMessage = statusCondition.GetNextMessage();
                            if (nextSCMessage == null)
                            {
                                break;
                            }

                            LogPanel.Write(nextSCMessage);
                            if (currentFighter.GetComponent<EnemyBody>() == null)
                            {
                                yield return new WaitForSeconds(2f);
                            }
                           
                        }

                        if (statusCondition.BlocksTurn())
                        {
                            combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                            break;
                        }
                    }
                    if (currentFighter.GetComponent<EnemyBody>()==null)
                    {
                        LogPanel.Write($"Turno de {currentFighter.idName}");
                    }

                    currentFighter.InitTurn();

                    combatStatus = CombatStatus.WAITING_FOR_FIGHTER;

                    break;

                
            }
        }
    }

    public Fighter[] FilterJustAlive(Fighter[] team)
    {
        this.returnBuffer.Clear();

        foreach (var fgtr in team)
        {
            if (fgtr.isAlive)
            {
                this.returnBuffer.Add(fgtr);
            }
        }

        return this.returnBuffer.ToArray();
    }

    public Fighter[] GetOpposingTeam()
    {
        Fighter currentFighter = this.fighters[this.fighterIndex];

        Fighter[] team = null;
        if (currentFighter.team == Team.PLAYERS)
        {
            if (initFinalBattle)
            {
                team = this.finalTeam;
            }
            else
            {
                team = this.enemyTeam;
            }
            
        }
        else if (currentFighter.team == Team.ENEMIES || currentFighter.team == Team.FINAL_ENEMIE)
        {
            team = this.playerTeam;
        }

        return this.FilterJustAlive(team);
    }

    public Fighter[] GetAllyTeam()
    {
        Fighter currentFighter = this.fighters[this.fighterIndex];

        Fighter[] team = null;
        if (currentFighter.team == Team.PLAYERS)
        {
            team = this.playerTeam;
        }
        else
        {
            team = this.enemyTeam;
        }

        return this.FilterJustAlive(team);
    }

    public void OnFighterSkill(Skill skill)
    {
        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.FIGHTER_ACTION;
    }

    public void NextTurn()
    {
        this.combatStatus = CombatStatus.NEXT_TURN;
    }

    private void LoadResult()
    {
        result.result = battle.finished;
        result.idPuzle = idPuzzle;
        result.idScene = idScene;
        result.well = "¡Vaya! parece que hay una llave detrás de las tareas... será mejor que la guarde en mi inventario...";
        result.bad = "Que lástima... controla bien la salud de tus compañeros...";
    }
}