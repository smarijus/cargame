using UnityEngine;
using System.Collections;

public class FileSystem
{
    DataBase db = new DataBase("database.db");


    // Funkcija paima iš duomenų bazės vartotojų sąrašą.
    // Grąžina:
    //              string[] tipas;
    //              Grąžina vartojų sąrašo masyvą.
    public string[] getUsersList()
    {
        string[] userList;
        var users = db.getUsersList();
        //Debug.Log("Nuskaitoma db");
        try
        {
            
            ArrayList tempList = new ArrayList();
            foreach (var user in users)
            {
                tempList.Add(user.UserName);
                //Debug.Log(user.UserName);
            }
            userList = (string[])tempList.ToArray(typeof(string));
            //Debug.Log("DB nuskaityta");
            return userList;
        }
        catch
        {

        }
        //Debug.Log("Nėra lentelės");
        return new string[0];
    }

    // Funkcija sukuria naują vartotoją su pateiktu vartotojo vardu.
    // Parametrai:
    //              userName - naujau kuriamo vartotojo vardas;
    public void addNewUser(string userName)
    {
        db.CreateUser(userName);
    }

    public void updateUserInfo(string userName, bool accurateDeformation, int highScore)
    {
        //var user = db.getUser(userName);
        db.updateUser(userName, accurateDeformation, highScore);
        //db.CreateUser(userName, accurateDeformation);
    }

    public User loadUserInfo(string userName)
    {
        //var user = db.getUser(userName);
        var userData = db.getUser(userName);
        return userData;
        //db.CreateUser(userName, accurateDeformation);
    }


    // Funkcija paima iš duomenų bazės geriausių rezultatų sąrašą sąrašą.
    public string[] getHighscoresList()
    {
        string[] highscoresList;
        var highscores = db.getHighscoresList();
        //Debug.Log("Nuskaitoma db");
        try
        {

            ArrayList tempList = new ArrayList();
            foreach (var highscore in highscores)
            {
                tempList.Add(highscore.UserName+" "+highscore.Score+" "+highscore.Date);
                //Debug.Log(highscores);
            }
            highscoresList = (string[])tempList.ToArray(typeof(string));
            //Debug.Log("DB nuskaityta");
            return highscoresList;
        }
        catch
        {

        }
        //Debug.Log("Nėra lentelės");
        return new string[0];
    }

    // Funkcija sukuria naują vartotoją su pateiktu vartotojo vardu.
    // Parametrai:
    //              userName - naujau kuriamo vartotojo vardas;
    public void insertNewHighscore(string userName, int score)
    {
        db.InsertHigscore(userName, score);
    }

    public void clearHighscores()
    {
        db.clearHighscores();
    }

    public void saveMeshToFile(MeshData[] meshData)
    {

    }
}
