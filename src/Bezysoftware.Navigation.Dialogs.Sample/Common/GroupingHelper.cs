namespace Svatky.Common
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Windows.Globalization.Collation;

    public static class GroupingHelper
    {
        /// <summary>
        /// Groups and sorts into a list of alpha groups based on a string selector.
        /// </summary>
        /// <typeparam name="TSource">Type of the items in the list.</typeparam>
        /// <param name="source">List to be grouped and sorted.</param>
        /// <param name="selector">A selector that will provide a value that items to be sorted and grouped by.</param>
        /// <returns>A list of JumpListGroups.</returns>
        public static List<Grouping<string, TSource>> ToAlphaGroups<TSource>(this IEnumerable<TSource> source, Func<TSource, string> selector)
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var group = new Grouping<string, TSource>("\uD83C\uDF10", source.OrderBy(selector));

                return new List<Grouping<string, TSource>> { group };
            }

            // Get the letters representing each group for current language using CharacterGroupings class
            var characterGroupings = new CharacterGroupings();

            // Create dictionary for the letters and replace '...' with proper globe icon
            var keys = characterGroupings.Where(x => x.Label.Any()).Select(x => x.Label).ToDictionary(x => x);
            keys["..."] = "\uD83C\uDF10";

            // Create groups for each letters
            var groupDictionary = keys.Select(x => new Grouping<string, TSource>(x.Value)).ToDictionary(x => (string)x.Key);

            // Sort and group items into the groups based on the value returned by the selector
            var query = from item in source
                        orderby selector(item)
                        select item;

            foreach (var item in query)
            {
                var sortValue = selector(item);
                var label = characterGroupings.Lookup(sortValue);
                var key = keys[label];
                groupDictionary[key].Add(item);
            }

            return groupDictionary.Select(x => x.Value).ToList();
        }
    }
}