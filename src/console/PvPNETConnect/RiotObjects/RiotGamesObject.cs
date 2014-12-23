#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Reroll.Pojo;

#endregion

namespace LoLLauncher.RiotObjects
{
    /// <summary>
    ///     RiotGamesObject is the base class for all Riot objects.
    /// </summary>
    public abstract class RiotGamesObject
    {
        public virtual string TypeName { get; private set; }

        /// <summary>
        ///     Talent class with information about talent.
        /// </summary>
        [InternalName("futureData")]
        public int FutureData { get; set; }

        /// <summary>
        ///     Talent class with information about talent.
        /// </summary>
        [InternalName("dataVersion")]
        public int DataVersion { get; set; }

        public TypedObject GetBaseTypedObject()
        {
            var typedObject = new TypedObject(TypeName);
            var objectType = GetType();

            foreach (
                var prop in
                    objectType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                             BindingFlags.DeclaredOnly))
            {
                var intern =
                    prop.GetCustomAttributes(typeof (InternalNameAttribute), false).FirstOrDefault() as
                        InternalNameAttribute;
                if (intern == null)
                    continue;

                object value = null;

                var type = prop.PropertyType;

                var typeName = type.Name;
                if (type == typeof (int[]))
                {
                    var test = prop.GetValue(this) as int[];
                    if (test != null) value = test.Cast<object>().ToArray();
                }
                else if (type == typeof (double[]))
                {
                    var test = prop.GetValue(this) as double[];
                    if (test != null) value = test.Cast<object>().ToArray();
                }
                else if (type == typeof (string[]))
                {
                    var test = prop.GetValue(this) as string[];
                    if (test != null) value = test.Cast<object>().ToArray();
                }
                //List = Array Collection. Object array = object array
                else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>))
                {
                    var listValues = prop.GetValue(this) as IList;
                    if (listValues != null)
                    {
                        var finalArray = new object[listValues.Count];
                        listValues.CopyTo(finalArray, 0);
                        var finalObjList = new List<object>();
                        foreach (var ob in finalArray)
                        {
                            var obType = ob.GetType();

                            if (typeof (RiotGamesObject).IsAssignableFrom(obType))
                            {
                                var rgo = ob as RiotGamesObject;

                                value = rgo.GetBaseTypedObject();
                            }

                            else
                            {
                                value = ob;
                            }

                            finalObjList.Add(value);
                        }
                        value = TypedObject.MakeArrayCollection(finalObjList.ToArray());
                    }
                }
                else if (typeof (RiotGamesObject).IsAssignableFrom(type))
                {
                    var rgo = prop.GetValue(this) as RiotGamesObject;

                    if (rgo != null) value = rgo.GetBaseTypedObject();
                }
                else
                {
                    value = prop.GetValue(this);
                }

                typedObject.Add(intern.Name, value);
            }

            var objectBaseType = objectType.BaseType;
            if (objectBaseType != null)
                foreach (
                    var prop in
                        objectBaseType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                                     BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    var intern =
                        prop.GetCustomAttributes(typeof (InternalNameAttribute), false).FirstOrDefault() as
                            InternalNameAttribute;
                    if (intern == null || typedObject.ContainsKey(intern.Name))
                        continue;

                    typedObject.Add(intern.Name, prop.GetValue(this));
                }

            return typedObject;
        }

        /// <summary>
        ///     The base virtual DoCallback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public virtual void DoCallback(TypedObject result)
        {
        }

        /// <summary>
        ///     Sets the fields of the object and decode/parse into correct fields.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="result">The result.</param>
        /// <exception cref="System.NotSupportedException"></exception>
        public void SetFields<T>(T obj, TypedObject result)
        {
            if (result == null)
                return;

            TypeName = result.Type;

            foreach (
                var prop in
                    typeof (T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                             BindingFlags.DeclaredOnly))
            {
                var intern =
                    prop.GetCustomAttributes(typeof (InternalNameAttribute), false).FirstOrDefault() as
                        InternalNameAttribute;
                if (intern == null)
                    continue;

                object value;
                var type = prop.PropertyType;

                if (!result.TryGetValue(intern.Name, out value))
                {
                    continue;
                }


                try
                {
                    if (result[intern.Name] == null)
                    {
                        value = null;
                    }
                    else if (type == typeof (string))
                    {
                        value = Convert.ToString(result[intern.Name]);
                    }
                    else if (type == typeof (Int32))
                    {
                        value = Convert.ToInt32(result[intern.Name]);
                    }
                    else if (type == typeof (Int64))
                    {
                        value = Convert.ToInt64(result[intern.Name]);
                    }
                    else if (type == typeof (double))
                    {
                        value = Convert.ToInt64(result[intern.Name]);
                    }
                    else if (type == typeof (bool))
                    {
                        value = Convert.ToBoolean(result[intern.Name]);
                    }
                    else if (type == typeof (DateTime))
                    {
                        value = result[intern.Name];
                    }
                    else if (type == typeof (TypedObject))
                    {
                        value = (TypedObject) result[intern.Name];
                    }

                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (List<>))
                    {
                        var temp = result.GetArray(intern.Name);

                        // Create List<T> with correct T type by reflection
                        var elementType = type.GetGenericArguments()[0];
                        var genericListType = typeof (List<>).MakeGenericType(elementType);
                        var objectList = (IList) Activator.CreateInstance(genericListType);

                        foreach (var data in temp)
                        {
                            if (data == null)
                            {
                                objectList.Add(null);
                            }
                            if (elementType == typeof (string))
                            {
                                objectList.Add((string) data);
                            }
                            else if (elementType == typeof (Participant))
                            {
                                var dataAsTo = (TypedObject) data;
                                if (dataAsTo.Type == "com.riotgames.platform.game.BotParticipant")
                                    objectList.Add(new BotParticipant(dataAsTo));
                                else if (dataAsTo.Type == "com.riotgames.platform.game.ObfruscatedParticipant")
                                    objectList.Add(new ObfruscatedParticipant(dataAsTo));
                                else if (dataAsTo.Type == "com.riotgames.platform.game.PlayerParticipant")
                                    objectList.Add(new PlayerParticipant(dataAsTo));
                                else if (dataAsTo.Type == "com.riotgames.platform.reroll.pojo.AramPlayerParticipant")
                                    objectList.Add(new AramPlayerParticipant(dataAsTo));
                            }
                            else if (elementType == typeof (Int32))
                            {
                                objectList.Add((Int32) data);
                            }
                            else
                            {
                                objectList.Add(Activator.CreateInstance(elementType, data));
                            }
                        }

                        value = objectList;
                    }
                    else if (type == typeof (Dictionary<string, object>))
                    {
                        var dict = (Dictionary<string, object>) result[intern.Name];

                        value = dict;
                    }
                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Dictionary<,>))
                    {
                        var dict = (Dictionary<string, object>) result[intern.Name];

                        value = dict;
                        //TypedObject to = result.GetTO(intern.Name);

                        //Type[] elementTypes = type.GetGenericArguments();
                        //var genericDictionaryType = typeof(Dictionary<,>).MakeGenericType(elementTypes);
                        //IDictionary objectDictionary = (IDictionary)Activator.CreateInstance(genericDictionaryType);

                        /*
                        foreach (string key in to.Keys)
                        {
                           if (to[key] == null)
                              objectDictionary.Add(key, null);
                           else
                              objectDictionary.Add(key, Activator.CreateInstance(elementTypes[1], to[key]));
                        }
                        */
                        //value = objectDictionary;
                    }
                    else if (type == typeof (Int32[]))
                    {
                        value = result.GetArray(intern.Name).Cast<Int32>().ToArray();
                    }
                    else if (type == typeof (String[]))
                    {
                        value = result.GetArray(intern.Name).Cast<String>().ToArray();
                    }
                    else if (type == typeof (object[]))
                    {
                        value = result.GetArray(intern.Name);
                    }
                    else if (type == typeof (object))
                    {
                        value = result[intern.Name];
                    }
                    else
                    {
                        try
                        {
                            value = Activator.CreateInstance(type, result[intern.Name]);
                        }
                        catch (Exception e)
                        {
                            throw new NotSupportedException(
                                string.Format("Type {0} not supported by flash serializer", type.FullName), e);
                        }
                    }
                    prop.SetValue(obj, value, null);
                }
                catch
                {
                    //Console.WriteLine(type, result[intern.Name]);
                }
            }
        }
    }

    /// <summary>
    ///     The InternalName Atribute class to specify the name that Riot's server is expecting.
    /// </summary>
    public class InternalNameAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InternalNameAttribute" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public InternalNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Gets or sets the name of the InternalName
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
    }
}