using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillfeedManager : Singleton<KillfeedManager>
{
    [Header("Kill Position")]
    [SerializeField] private GameObject redKillPosition;
    [SerializeField] private GameObject blueKillPosition;
    [SerializeField] private GameObject creepKillPosition;
    [SerializeField] private GameObject baronKillPosition;

    [Space(5)]
    [Header("Blue Position")]
    [SerializeField] private GameObject redDeadPosition;
    [SerializeField] private GameObject blueDeadPosition;
    [SerializeField] private GameObject creepDeadPosition;
    [SerializeField] private GameObject baronDeadPosition;
}
