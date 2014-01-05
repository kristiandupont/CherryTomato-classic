using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace CherryTomato.Core
{
    public static class Helpers
    {
        public static string ToAgeString(this TimeSpan age, bool roundUp)
        {
            string result;

            result = AgeStringFrom(age.TotalSeconds, 60, "second", roundUp); if(result != null) return result;
            result = AgeStringFrom(age.TotalMinutes, 60, "minute", roundUp); if (result != null) return result;
            result = AgeStringFrom(age.TotalHours, 24, "hour", roundUp); if (result != null) return result;
            result = AgeStringFrom(age.TotalDays, 7, "day", roundUp); if (result != null) return result;
            result = AgeStringFrom(age.TotalDays, 7 * 5, "week", roundUp); if (result != null) return result;

            return Pluralize("month", Round(age.TotalDays / 30.5, roundUp));
        }

        private static string AgeStringFrom(double count, int max, string unit, bool roundUp)
        {
            var c = Round(count, roundUp);
            return c < max ? Pluralize(unit, c) : null;
        }

        public static string Pluralize(string unit, int count)
        {
            return count == 1 ? count + " " + unit : count + " " + unit + "s";
        }

        private static int Round(double d, bool roundUp)
        {
            if (roundUp) return (int)Math.Ceiling(d);
            return (int)Math.Floor(d);
        }
        
        public static string DownloadString(string url)
        {
            try
            {
                var s = WebRequest.Create(url).GetResponse().GetResponseStream();
                var result = new StreamReader(s, Encoding.UTF8).ReadToEnd();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static void DownloadStringAsync(string url, StringDownloader.StringReceivedDelegate stringReceived)
        {
            var sd = new StringDownloader(url, stringReceived);
            sd.Run();
        }

        public static Icon LoadIcon(string path)
        {
            if (path.StartsWith("res://"))
            {
                var resourceName = path.Substring("res://".Length);
                var assemblyWithResources = Assembly.GetCallingAssembly();
                var resPath = assemblyWithResources.GetName().Name + ".Resources." + resourceName;
                var resourceStream = assemblyWithResources.GetManifestResourceStream(resPath);
                if (resourceStream == null)
                {
                    throw new NotSupportedException(
                        string.Format(
                            "The {0} embedded resource was not found in assembly {1}.\n" +
                            "Seems like you forgot to add the icon to your Resources/ folder, " +
                            "OR didn't mark it as Embedded Resource.",
                        path, 
                        assemblyWithResources.GetName().Name));
                }

                return new Icon(resourceStream);
            }

            throw new NotSupportedException("Currently, only resource icons are supported (path='" + path + "')");
        }

        /// <summary>
        /// Perform the action in the GUI thread (if necessary).
        /// </summary>
        /// <param name="control">The control with should be used for thread detection.</param>
        /// <param name="action">The action to perform.</param>
        public static void UpdateControl(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Gets the Value->String dictionary of each enumeration value.
        /// String is read from Description attribute if available.
        /// </summary>
        /// <typeparam name="EnumT">The enumerable type to search in.</typeparam>
        /// <returns>Enum values and its string representations.</returns>
        public static Dictionary<EnumT, string> GetEnumValuesDescriptionPairs<EnumT>()
        {
            var retValue = new Dictionary<EnumT, string>();
            foreach (EnumT enumValue in Enum.GetValues(typeof(EnumT)))
            {
                retValue[enumValue] = GetDescription<EnumT>(enumValue);
            }

            return retValue;
        }

        /// <summary>
        /// Gets the (human readable) description attribute from a enumeration
        /// value. If no description attribute is found, the default text 
        /// representation is used.
        /// </summary>
        /// <param name="enumValue">The enumeration value</param>
        /// <returns>The corresponding enumeration description.</returns>
        public static string GetDescription<EnumT>(EnumT enumValue)
        {
            FieldInfo info = typeof(EnumT).GetField(enumValue.ToString());
            if (info == null)
            {
                throw new ArgumentException(string.Format(
                    "Value of {0} is not defined in enum {1}.",
                    enumValue,
                    typeof(EnumT).Name));
            }

            object[] attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            DescriptionAttribute descrAttr = attrs.OfType<DescriptionAttribute>().FirstOrDefault();

            return descrAttr == null ? enumValue.ToString() : descrAttr.Description;
        }
    }
}
