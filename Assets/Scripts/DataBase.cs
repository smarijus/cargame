using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using SQLite4Unity3d;
using System.Collections.Generic;
using System.IO;
using System;
using System.Globalization;

public class DataBase
{

	private ISQLiteConnection _connection;

	public DataBase(string DatabaseName)
    {

        var factory = new Factory();

        #if UNITY_EDITOR
                    var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        #else
                // check if file exists in Application.persistentDataPath
                var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
                if (!File.Exists(filepath))
                {
                    Debug.Log("Database not in Persistent path");
                    // if it doesn't ->
                    // open StreamingAssets directory and load the db ->

        #if UNITY_ANDROID 
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
                    while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                    // then save to Application.persistentDataPath
                    File.WriteAllBytes(filepath, loadDb.bytes);
        #elif UNITY_IOS
                         var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                        // then save to Application.persistentDataPath
                        File.Copy(loadDb, filepath);
        #elif UNITY_WP8
                        var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                        // then save to Application.persistentDataPath
                        File.Copy(loadDb, filepath);

        #elif UNITY_WINRT
			        var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
			        // then save to Application.persistentDataPath
			        File.Copy(loadDb, filepath);
        #endif

                    Debug.Log("Database written");
                }

                var dbPath = filepath;
        #endif
                if (!Directory.Exists(Path.GetDirectoryName(dbPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dbPath));
                _connection = factory.Create(dbPath);
                //Debug.Log("Final PATH: " + dbPath);     

	}

    // Funkcija grąžina vartotojų lentelės turinį iš duomenų bazės.
    public IEnumerable<User> getUsersList()
    {
        return _connection.Table<User>(); 
	}

    // Funkcija įterpia naują vartotojo į duomenų bazę.
    // Jei vartotojų lentelė neegizstuoja, funkcija sukuria naują lentelę.
    // Parametrai:
    //              carPosition - dabartinė automobilio pozicija;
    public User CreateUser(string userName)
    {
        var u = new User
        {
				UserName = userName,
                AccurateDeformation = false,
                HighScore = 0,
		};
        //Debug.Log("Kuriamas naujas įrašas");
        try
        {
            //_connection.CreateTable<User>();
            _connection.Insert(u);
            //Debug.Log("Įrašas sukurtas");
            return u;
        }
        catch
        {

        }
        //Debug.Log("Sukurit nepavyko");
        _connection.CreateTable<User>();
        _connection.Insert(u);
        //Debug.Log(u);
        //Debug.Log("Įrašas sukurtas");
		return u;
	}

    public User getUser(string userName)
    {
        var user = _connection.Table<User>().Where(x => x.UserName == userName).FirstOrDefault();
        //Debug.Log(user.AccurateDeformation);
        return user;
    }

    public void updateUser(string userName, bool accurateDeformation, int highScore)
    {
        var u = getUser(userName);
        u.AccurateDeformation = accurateDeformation;
        u.HighScore = highScore;
        //Debug.Log(u.AccurateDeformation);
        //var data = _connection.Delete(u);
        var data = _connection.Update(u);
        //Debug.Log(data);
        //u = getUser(userName);
        //Debug.Log(u.AccurateDeformation);
        //return user;
    }
    //private bool checkIfTableExist()
    //{
    //    try
    //    {
    //        _connection.Table<User>();
    //        Debug.Log("Lentelė egzistuoja");
    //        return true;
    //    }
    //    catch
    //    {
    //    }
    //    Debug.Log("Lentelė neegistuoja");
    //    return false;
    //}

    //internal User getUser(string mUserName)
    //{
    //    return _connection.Table<User>().Where(x => x.UserName == mUserName).FirstOrDefault();
    //}

    // Funkcija grąžina vartotojų geriausius rezultatus.
    public IEnumerable<Highscore> getHighscoresList()
    {
        return _connection.Table<Highscore>();
    }

    public Highscore InsertHigscore(string userName, int score)
    {
        Debug.Log(DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss"));
        var h = new Highscore
        {
            UserName = userName,
            Score = score,
            Date = DateTime.Parse(DateTime.Now.ToString(new CultureInfo("lt-LT")))
        };
        //Debug.Log("Kuriamas naujas įrašas");
        try
        {
            //_connection.CreateTable<User>();
            _connection.Insert(h);
            //Debug.Log("Įrašas sukurtas");
            return h;
        }
        catch
        {

        }
        //Debug.Log("Sukurit nepavyko");
        _connection.CreateTable<Highscore>();
        _connection.Insert(h);
        Debug.Log(h);
        //Debug.Log("Įrašas sukurtas");
        return h;
    }

    public void clearHighscores()
    {
        try
        {
            _connection.DropTable<Highscore>();
            _connection.CreateTable<Highscore>();
        }
        catch
        {
        }
    }
}
