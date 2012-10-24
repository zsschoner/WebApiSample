using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public static class Values
    {
        private static readonly List<ValueClass> valueList = new List<ValueClass>(new ValueClass[] { new ValueClass() { Id = 1, Value = "Value1" }, 
                                                                        new ValueClass() { Id = 2, Value = "Value2" } });

        public static IEnumerable<ValueClass> GetValues()
        {
            return valueList;
        }

        public static ValueClass GetValue(int id)
        {
            return valueList.First(value => value.Id == id);
        }

        public static ValueClass AddValue(ValueClass value)
        {
            value.Id = valueList.Max(val => val.Id) + 1;
            valueList.Add(value);

            return value;
        }
    }
}
