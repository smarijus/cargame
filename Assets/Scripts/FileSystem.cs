using UnityEngine;
using System.Collections;

public class FileSystem
{
    DataBase db = new DataBase("database.db");// = new DataBase("database.db");
    //public void createDB()
    //{
    //    //DataBase db = new DataBase("database.db");
    //    db.CreateDB();
    //}

    public string[] getUsersList()
    {
        string[] userList;
        var users = db.getUsersList();
        Debug.Log("Nuskaitoma db");
        try
        {
            
            ArrayList tempList = new ArrayList();
            foreach (var user in users)
            {
                tempList.Add(user.UserName);
                Debug.Log(user.UserName);
            }
            userList = (string[])tempList.ToArray(typeof(string));
            Debug.Log("DB nuskaityta");
            return userList;
        }
        catch
        {

        }
        Debug.Log("Nėra lentelės");
        return new string[0];
    }

    public void addNewUser(string userName)
    {
        db.CreatePerson(userName);
    }
}
