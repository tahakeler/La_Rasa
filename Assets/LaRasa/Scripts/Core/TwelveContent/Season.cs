namespace LaRasa.Core.TwelveContent
{
    /// <summary>
    /// The four seasons of the La Rasa calendar. Assigned per archetype-wheel
    /// position in <see cref="ArchetypeWheel"/>.
    ///
    /// Per <c>design/gdd/twelve-content-framework.md</c> the season assignment
    /// on each wheel position is an explicitly editable placeholder — do not
    /// treat the default mapping as final. The enum itself (four seasons) is
    /// structurally fixed by <c>design/gdd/daily-loop-and-calendar.md</c>
    /// (36 days/season, 144 days/year).
    /// </summary>
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autumn = 2,
        Winter = 3
    }
}
