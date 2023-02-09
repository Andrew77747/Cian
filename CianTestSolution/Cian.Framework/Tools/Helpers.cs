using System;
using System.Collections.Generic;

namespace Cian.Framework.Tools
{
    public class Helpers
    {
        //Получаем значение всех полей класса
        public static List<string> GetFieldsValue(Type type)
        {
            var subMenuNamesList = new List<string>();

            var fields = type.GetFields();

            foreach (var field in fields)
            {
                subMenuNamesList.Add(field.GetValue(type)?.ToString());
            }

            return subMenuNamesList;
        }
    }
}