using ObjectOrientedSQL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ObjectOrientedSQL.Utilities;

namespace ObjectOrientedSQL
{
    public class ObjectSQL:IObjectSQL
    {
        private readonly string _sqlConnectionString;

        public ObjectSQL(string sqlConnection)
        {
            this._sqlConnectionString = sqlConnection;
        }

        public T SelectFirstOrDefault<T>(Expression<Func<T, bool>> where) where T : class
        {
            using (SqlConnection con = new SqlConnection(_sqlConnectionString))
            {
                con.Open();

                //Creating Dynamically instance through the Type of the Class
                var mappedObject = (T)Activator.CreateInstance(typeof(T));

                var query = SQLConverter.FuncTest(where);

                using (SqlCommand command = new SqlCommand($"SELECT  * from {typeof(T).Name} where {query} ;", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    var listOfProperties = typeof(T).GetProperties();

                    while (reader.Read())
                    {
                        for (var i = 0; i < listOfProperties.Length; i++)
                        {   
                            //Using Reflection to assign values of the Reader to the Properties of the Object
                            //TODO: Find a way to make it faster
                            PropertyInfo propertyInfo = mappedObject.GetType().GetProperty(listOfProperties[i].Name);
                            //Handling DBnull values
                            propertyInfo.SetValue(mappedObject, reader[listOfProperties[i].Name] == DBNull.Value ? null : reader[listOfProperties[i].Name], null);
                        }
                    }
                }
                return mappedObject;
            }
        }

        public List<T> SelectMany<T>(Expression<Func<T, bool>> where) where T : class
        {
            using (SqlConnection con = new SqlConnection(_sqlConnectionString))
            {
                con.Open();

                var listmappedObjects = new List<T>();

                var query = SQLConverter.FuncTest(where);

                using (SqlCommand command = new SqlCommand($"SELECT  * from {typeof(T).Name} where {query} ;", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    var listOfProperties = typeof(T).GetProperties();

                    while (reader.Read())
                    {
                        var mappedObject = (T)Activator.CreateInstance(typeof(T));

                        for (var i = 0; i < listOfProperties.Length; i++)
                        {
                            PropertyInfo propertyInfo = mappedObject.GetType().GetProperty(listOfProperties[i].Name);
                            propertyInfo.SetValue(mappedObject, reader[listOfProperties[i].Name], null);
                        }
                        listmappedObjects.Add(mappedObject);
                    }
                }
                return listmappedObjects;
            }
        }
    }
}
