using System;

namespace IS.DocumenFormater.api.Factories
{
    public class AttributesTools
    {
        public static String GetTableName(Type type)
        {
            String tableName = "";
            foreach (var attrib in Attribute.GetCustomAttributes(type))
            {
                if (attrib.GetType().Name == "TableAttribute")
                {
                    tableName = $"[{attrib.GetType().GetProperty("Schema").GetValue(attrib)}].[{attrib.GetType().GetProperty("Name").GetValue(attrib)}]";
                }
            }
            return tableName;
        }
    }
}
