﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
namespace LiveSplit.OriWotW {
    public static class Utility {
        public static List<Tuple<T, string>> GetEnumList<T>() where T : struct {
            List<Tuple<T, string>> returnValue = new List<Tuple<T, string>>();
            foreach (T value in Enum.GetValues(typeof(T))) {
                string valueString = value.ToString();
                if (valueString == "None") { continue; }

                MemberInfo info = typeof(T).GetMember(valueString)[0];
                DescriptionAttribute[] descriptions = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string description = GetEnumDescription<T>(value);
                returnValue.Add(new Tuple<T, string>(value, description));
            }
            return returnValue;
        }
        public static List<Tuple<T, string>> GetSortedEnumList<T>() where T : struct {
            var list = GetEnumList<T>();
            list.Sort((a, b) => String.Compare(a.Item2, b.Item2, StringComparison.Ordinal));
            return list;
        }
        public static string GetEnumDescription<T>(T value) where T : struct {
            MemberInfo info = typeof(T).GetMember(value.ToString())[0];
            DescriptionAttribute[] descriptions = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions == null || descriptions.Length == 0) {
                return value.ToString();
            } else {
                return descriptions[0].Description;
            }
        }
        public static T GetEnumValue<T>(string valueToFind) where T : struct {
            T value;
            if (Enum.TryParse<T>(valueToFind, true, out value)) {
                return value;
            }
            return default(T);
        }
        public static string PrintList<T>(this List<T> list) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++) {
                sb.Append(list[i].ToString()).Append(',');
            }
            if (list.Count > 0) {
                sb.Length--;
            }
            return sb.ToString();
        }
    }
}