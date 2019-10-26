using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Assets.Resources.Classes.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Classes
{
    /// <summary>
    /// Static Class <c>ApplicationTheme</c> is a class that represents the
    /// theme for the entire application.
    /// </summary>
    public static class ApplicationTheme
    {
        public static IList<GameTheme> Themes;
        public static GameTheme CurrentTheme;
        private const int DEFAULT_THEME_INDEX = 1;

        static ApplicationTheme()
        {
            Themes = new List<GameTheme> {new DefaultGameTheme(), new KnightTheme()};

            // Add all game themes to the linked list

            CurrentTheme = Themes[DEFAULT_THEME_INDEX];
        }

        public static void SwitchTheme(int index)
        {
            if (index >= Themes.Count || index < 0)
            {
                throw new ArgumentException("Index is out of range", nameof(index));
            }


            CurrentTheme = Themes[index];
        }

        /// <summary>
        /// Returns the current application theme
        /// </summary>
        /// <returns>
        /// The currently active application theme
        /// </returns>
        public static GameTheme GetTheme()
        {
            return CurrentTheme;
        }
    }
}
