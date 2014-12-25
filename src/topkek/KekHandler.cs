/*
 * Topkek GUI is part of the opensource VoliBot AutoQueuer project.
 * Credits to: shalzuth, Maufeat, imsosharp
 * Find assemblies for this AutoQueuer on LeagueSharp's official forum at:
 * http://www.joduska.me/
 * You are allowed to copy, edit and distribute this project,
 * as long as you don't touch this notice and you release your project with source.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace RitoBot.topkek
{
    public static class KekHandler
    {
        public static UserPanel userpanel = new UserPanel();
        public static LoginPanel loginpanel = new LoginPanel();
        public static void changeStatus(string status)
        {
            userpanel.changeStatus(status);
        }
    }
}
