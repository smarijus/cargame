using UnityEngine;
using System.Collections;
using SQLite4Unity3d;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    //[PrimaryKey]
    public string UserName { get; set; }
    public bool AccurateDeformation { get; set; }
    public int HighScore { get; set; }

    //public override string ToString()
    //{
    //    //return string.Format("[Person: Id={0}, UserName={1}]", ID, UserName);
    //}
}
