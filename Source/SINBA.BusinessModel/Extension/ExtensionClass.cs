using Sinba.BusinessModel.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Sinba.BusinessModel.Entity
{
    public static partial class ExtensionClass
    {
        private static string DecryptKey = "a+Hqmucuq7wIcwUR0JCcAHtU6zIPxRfFABK4k20RI9A=";
        private static string DecryptVector = "Iae9QpANmKSyJOhmZEKYJg==";

        public static bool GetValue(this bool? value)
        {
            return value.HasValue ? value.Value : false;
        }

        public static object CloneObject(this object objSource)
        {
            //Get the type of source object and create a new instance of that type
            Type typeSource = objSource.GetType();
            object objTarget = Activator.CreateInstance(typeSource);

            //Get all the properties of source object type
            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Assign all source property to taget object 's properties
            foreach (PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(string)))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(objSource, null);

                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }

                        else
                        {
                            property.SetValue(objTarget, objPropertyValue.CloneObject(), null);
                        }
                    }
                }
            }

            return objTarget;
        }

        public static T CloneSerialize<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static string Encrypt(this string text)
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                byte[] encrypted = EncryptStringToBytes(text, Convert.FromBase64String(DecryptKey), Convert.FromBase64String(DecryptVector));

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string Decrypt(this string text)
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                string decrypted = DecryptStringFromBytes(Convert.FromBase64String(text), Convert.FromBase64String(DecryptKey), Convert.FromBase64String(DecryptVector));

                return decrypted;
            }
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }

        public static bool Equals<T>(this T[] first, T[] second)
        {
            return first.OrderBy(x => x).SequenceEqual(second.OrderBy(x => x));
        }

        public static bool EqualsIC(this string element, string compare)
        {
            if (string.IsNullOrWhiteSpace(element) && string.IsNullOrWhiteSpace(compare))
            {
                return true;
            }
            else if (string.IsNullOrWhiteSpace(element) || string.IsNullOrWhiteSpace(compare))
            {
                return false;
            }
            return element.Equals(compare, StringComparison.OrdinalIgnoreCase);
        }

        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException();
            }
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }

        public static Guid? ToNullableGuid(this string property)
        {
            return string.IsNullOrWhiteSpace(property) ? null : new Guid?(new Guid(property));
        }

        /// <summary>
        /// Retourne une liste d'un seul type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// 02.08.2016 - rto: creation
        /// Change history:
        //public static List<Liste> Get(this List<Liste> value, TypeListeEnum type)
        //{
        //    return value.Where(x => x.IdSysListeType == (int)type).ToList();
        //}

        #region DateTime
        /// <summary>
        /// Gets the java script date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static long GetJavaScriptDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToJavaScriptDate() : 0;
        }

        /// <summary>
        /// Gets the java script date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static long ToJavaScriptDate(this DateTime date)
        {
            return Convert.ToInt64(date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
        }
        #endregion

    }
}
