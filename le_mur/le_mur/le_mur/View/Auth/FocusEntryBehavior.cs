using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace le_mur.View.Auth
{
    public class FocusEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is Entry currentEntry))
                return;

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                // Move focus to the next entry
                var nextEntry = currentEntry.GetType().GetProperty("NextView")?.GetValue(currentEntry) as Entry;
                nextEntry?.Focus();
            }
            else
            {
                // Move focus to the previous entry
                var previousEntry = currentEntry.GetType().GetProperty("PreviousView")?.GetValue(currentEntry) as Entry;
                previousEntry?.Focus();
            }
        }
    }
}
