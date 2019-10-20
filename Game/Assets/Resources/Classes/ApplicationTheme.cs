using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Resources.Classes.Theme;

namespace Assets.Resources.Classes
{
    /// <summary>
    /// Static Class <c>ApplicationTheme</c> is a class that represents the
    /// theme for the entire application.
    /// </summary>
    public static class ApplicationTheme
    {
        public static LinkedList<GameTheme> Themes;
        public static GameTheme Theme;

        static ApplicationTheme()
        {
            Themes = new LinkedList<GameTheme>();

            // Add all game themes to the linked list
            Themes.AddLast(new DefaultGameTheme());
            Themes.AddLast(new KnightTheme());

            Theme = Themes.Last.Value;
        }

        
    }
}
