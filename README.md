# CustomORM

A funny approach to how you can create a Custom ORM with just native SQL.

At current stage of implementation the Library(ObjectOrientedSQL project) supports the following options:

1)T SelectFirstOrDefault<T>(Expression<Func<T, bool>> where) where T : class

The parameter is a lambda expression that is converted to SQL query and returns maximum one Object of T type.

2)List<T> SelectMany<T>(Expression<Func<T, bool>> where) where T : class

The parameter is a lambda expression that is converted to SQL query and returns a list of Objects of T type.

#Example of Usages :

In the project TestSQLObjected of the solution(Console application).

create an instance of the class ObjectSQL,

var test = new ObjectSQL("Server=XXXXX;Database=XXXXX;User Id=XXXXX;Password=XXXXX;");

var results = test.SelectMany<PocoClass>(x => x.property >= 0);

In order to use the above functionality you have to create a Poco Class that is the mapping of the Table you want from the Database.

In Detail if you have a table called Tokens[int id,varchar authtoken].

Then you create the class

public class Tokens{
public int id {get;set;}
public string authtoken {get;set;}
}

Then you can you use any of the above functions like :

var test = instanceofObjectSQL.SelectMany<Tokens>(p=>p.id>100)

var test2 =instanceofObjectSQL.SelectFirstOrDefault<Tokens>(p=>p.id==100)

#Milestones

1)Add Support for Update , Delete , Insert Functions

2)Make optimization to the mapping of the Objects created (currently using reflection)




