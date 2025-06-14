using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public AudioSource musicSource;
    public SMParser smParser;
    public GameObject leftNotePrefab, downNotePrefab, upNotePrefab, rightNotePrefab;
    public Transform leftArrow, downArrow, upArrow, rightArrow;
    public Transform spawnHeightReference;
    public float scrollSpeed = 5f;
    public float leadTime = 2.5f; // flechas se generan 2.5 segundos antes
    public float delayBeforeStart = 2f; // espera 2 segundos antes de empezar

    private int noteIndex = 0;
    private bool spawningEnabled = false;
    private float startTime;

    public void BeginSpawning()
    {
        if (musicSource != null && musicSource.time >= musicSource.clip.length)
        {
            spawningEnabled = false;
            return;
        }

        if (smParser == null) { Debug.LogError("❌ SMParser no asignado."); return; }
        smParser.Parse(musicSource);
        noteIndex = 0;
        startTime = Time.time + delayBeforeStart;
        spawningEnabled = true;
    }

    void Update()
    {
        if (!spawningEnabled || smParser == null || smParser.notes == null || smParser.notes.Count == 0)
            return;

        float songTime = Time.time - startTime;

        while (noteIndex < smParser.notes.Count && smParser.notes[noteIndex].time <= songTime + leadTime)
        {
            SpawnNote(smParser.notes[noteIndex]);
            noteIndex++;
        }
    }

    void SpawnNote(NoteData note)
    {
        GameObject prefab = GetPrefab(note.type);
        Vector3 spawnPos = GetSpawnPosition(note.type);

        if (prefab == null) return;

        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawned.AddComponent<NoteMover>().scrollSpeed = scrollSpeed;
        spawned.tag = "Note";
        spawned.name = note.type.ToString();
    }

    GameObject GetPrefab(ArrowType type) => type switch
    {
        ArrowType.Left => leftNotePrefab,
        ArrowType.Down => downNotePrefab,
        ArrowType.Up => upNotePrefab,
        ArrowType.Right => rightNotePrefab,
        _ => null
    };

    Vector3 GetSpawnPosition(ArrowType type)
    {
        Vector3 basePos = type switch
        {
            ArrowType.Left => leftArrow.position,
            ArrowType.Down => downArrow.position,
            ArrowType.Up => upArrow.position,
            ArrowType.Right => rightArrow.position,
            _ => Vector3.zero
        };

        return new Vector3(basePos.x, spawnHeightReference.position.y, basePos.z);
    }
}

