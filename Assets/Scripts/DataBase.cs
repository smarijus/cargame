using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using SQLite4Unity3d;
using System.Collections.Generic;
using System.IO;

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

    public IEnumerable<User> getUsersList()
    {
        return _connection.Table<User>(); 
	}

    public User CreatePerson(string userName)
    {
        var p = new User
        {
				UserName = userName,
		};
        //Debug.Log("Kuriamas naujas įrašas");
        try
        {
            //_connection.CreateTable<User>();
            _connection.Insert(p);
            //Debug.Log("Įrašas sukurtas");
            return p;
        }
        catch
        {

        }
        //Debug.Log("Sukurit nepavyko");
        _connection.CreateTable<User>();
        _connection.Insert(p);
        Debug.Log(p);
        //Debug.Log("Įrašas sukurtas");
		return p;
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
}
