using UnityEngine;
using System.Collections;
using SQLite4Unity3d;
using System;

public class Highscore
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    public string UserName { get; set; }
    public int Score { get; set; }
    public DateTime Date { get; set; }
    
}
