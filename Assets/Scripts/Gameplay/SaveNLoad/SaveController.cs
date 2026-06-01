using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Gameplay.Rating;
using UnityEngine;
using Utility;

namespace Gameplay.SaveNLoad
{
    /**
     * Plain C# class that's capable of saving and loading game
     */
    [Serializable]
    public class SaveController
    {        
        public static void SaveGame(string filename, string sceneName)
        {
            SaveState state = new();
            BinaryFormatter binaryFormatter = new();
            FileStream file = new(filename, FileMode.OpenOrCreate);

            state.currentSceneName = sceneName;
            state.currentRatingValue = RatingController.PersistentRating;
            
            binaryFormatter.Serialize(file, state);
            file.Close();
        }

        public static bool LoadGame(string filename, out string sceneName)
        {
            BinaryFormatter binaryFormatter = new();
            sceneName = string.Empty;
            FileStream file;
            try
            {
                file = new FileStream(filename, FileMode.Open);
            }
            catch
            {
                Debug.LogWarning($"No save file: {filename}");
                return false;
            }

            var state = binaryFormatter.Deserialize(file) as SaveState;

            if (state is null)
            {
                Debug.LogWarning($"Missing or corrupted save file: {filename}");
                return false;
            }
            
            RatingController.PersistentRating = state.currentRatingValue;
            // Notify if we already have an instance -->
            ISingleton<RatingController>.Instance?.Set(state.currentRatingValue);
            sceneName = state.currentSceneName;
            return true;
        }
    }
}
