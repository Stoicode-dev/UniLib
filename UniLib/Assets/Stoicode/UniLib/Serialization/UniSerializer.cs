using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace Stoicode.UniLib.Serialization
{
    public static class UniSerializer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        static UniSerializer()
        {
            CompositeResolver.RegisterAndSetAsDefault(
                ContractlessStandardResolver.Instance);
            MessagePackSerializer.SetDefaultResolver(
                ContractlessStandardResolver.Instance);
        }

        /// <summary>
        /// Serialize object
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="data">Object to serialize</param>
        /// <returns>Serialized object</returns>
        public static byte[] Serialize<T>(T data)
        {
            return MessagePackSerializer.Serialize(data);
        }

        /// <summary>
        /// Serialize object and write to disk
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="data">Object to serialize</param>
        /// <param name="path">Target file path</param>
        /// <returns>Success status</returns>
        public static bool WriteToDisk<T>(T data, string path)
        {
            try
            {
                var serialized = Serialize(data);
                File.WriteAllBytes(path, serialized);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[UniSerializer] Failed to write to disk : {e}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Serialize object and write to player prefs
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="data">Object to serialize</param>
        /// <param name="key">PlayerPrefs key</param>
        public static void WriteToPlayerPrefs<T>(T data, string key)
        {
            var serialized = Serialize(data);
            var byteString = Convert.ToBase64String(serialized);

            PlayerPrefs.SetString(key, byteString);
        }

        /// <summary>
        /// Deserialize bytes into object
        /// </summary>
        /// <param name="data">Serialized object</param>
        /// <returns>Deserialized object</returns>
        public static object Deserialize(byte[] data)
        {
            return MessagePackSerializer.Deserialize<object>(data);
        }

        /// <summary>
        /// Deserialize bytes from disk
        /// </summary>
        /// <param name="path">Target file path</param>
        /// <returns>Deserialized object</returns>
        public static object ReadFromDisk(string path)
        {
            try
            {
                var raw = File.ReadAllBytes(path);
                var serialized = Deserialize(raw);

                return serialized;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"[UniSerializer] Failed to read from disk : {e}");
                return null;
            }
        }

        /// <summary>
        /// Deserialize PlayerPrefs content
        /// </summary>
        /// <param name="key">PlayerPrefs key</param>
        /// <returns>Deserialized object</returns>
        public static object ReadFromPlayerPrefs(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.LogWarning("UniSerializer] Attempting to access non-existent player pref key!");
                return null;
            }

            var byteString = PlayerPrefs.GetString(key);
            var raw = Convert.FromBase64String(byteString);

            return Deserialize(raw);
        }
    }
}