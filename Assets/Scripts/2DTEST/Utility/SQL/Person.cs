using SQLite4Unity3d;

public class Person
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public int Age { get; set; }

    public UnityEngine.Vector2 wall { get; set; }  //左右墙限制
    //[Unique("Book", 0)]
    //public Book Book { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1},  Surname={2}, Age={3}]", Id, Name, Surname, Age);
    }
}

[System.Serializable]
public class Book
{
    public int b { get; set; }
}