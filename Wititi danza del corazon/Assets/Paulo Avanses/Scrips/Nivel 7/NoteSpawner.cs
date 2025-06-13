using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public SMParser smParser;

    public GameObject leftNotePrefab, downNotePrefab, upNotePrefab, rightNotePrefab;
    public Transform leftSpawn, downSpawn, upSpawn, rightSpawn;

    public float scrollSpeed = 5f;

    private int noteIndex = 0;

    void Start()
    {
        if (smParser == null) { Debug.LogError("❌ SMParser no asignado."); return; }

        smParser.Parse();
    }

    void Update()
    {
        if (smParser == null || smParser.notes == null || smParser.notes.Count == 0)
            return;

        while (noteIndex < smParser.notes.Count && Time.time >= smParser.notes[noteIndex].time)
        {
            SpawnNote(smParser.notes[noteIndex]);
            noteIndex++;
        }
    }

    void SpawnNote(NoteData note)
    {
        GameObject prefab = GetPrefab(note.type);
        Transform spawnPoint = GetSpawnPoint(note.type);

        if (prefab == null || spawnPoint == null) return;

        GameObject spawned = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
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

    Transform GetSpawnPoint(ArrowType type) => type switch
    {
        ArrowType.Left => leftSpawn,
        ArrowType.Down => downSpawn,
        ArrowType.Up => upSpawn,
        ArrowType.Right => rightSpawn,
        _ => null
    };
}
