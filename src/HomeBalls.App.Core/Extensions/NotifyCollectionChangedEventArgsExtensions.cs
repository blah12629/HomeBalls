namespace System.Collections.Specialized;

public static class NotifyCollectionChangedEventArgsExtensions
{
    public static Boolean IsChanged<TElement>(
        this NotifyCollectionChangedEventArgs e,
        TElement element) =>
        e.Action == NotifyCollectionChangedAction.Reset ||
        (e.OldItems?.Contains(element) ?? false) ||
        (e.NewItems?.Contains(element) ?? false);
}