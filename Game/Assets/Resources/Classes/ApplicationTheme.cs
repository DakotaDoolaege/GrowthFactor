using System.Collections.Generic;
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
        public static LinkedList<GameTheme> Themes;
        private static LinkedListNode<GameTheme> themeNode;

        static ApplicationTheme()
        {
            Themes = new LinkedList<GameTheme>();

            // Add all game themes to the linked list
            Themes.AddFirst(new DefaultGameTheme());
            Themes.AddFirst(new KnightTheme());

            themeNode = Themes.First;
        }

        /// <summary>
        /// Progresses the application theme to the next theme in the list
        /// </summary>
        public static void Next()
        {
            if (themeNode.Next == null)
            {
                themeNode = Themes.First;
            }
            else
            {
                themeNode = themeNode.Next;
            }
        }

        /// <summary>
        /// Returns the current application theme
        /// </summary>
        /// <returns>
        /// The currently active application theme
        /// </returns>
        public static GameTheme GetTheme()
        {
            return themeNode.Value;
        }
    }
}
